using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using DotNetOpenAuth.AspNet;
using WebSiteMjr.Domain.Exceptions;
using WebSiteMjr.Domain.Interfaces.Membership;
using WebSiteMjr.Domain.Model.Membership;
using WebSiteMjr.Domain.Model.Roles;
using Microsoft.Web.WebPages.OAuth;

namespace FlexProviders.Membership
{
    public class FlexMembershipProvider : IFlexMembershipProvider
                                         
    {
        private const int TokenSizeInBytes = 16;

        private static readonly Dictionary<string, AuthenticationClientData> AuthenticationClients =
            new Dictionary<string, AuthenticationClientData>(StringComparer.OrdinalIgnoreCase);

        private readonly IApplicationEnvironment _applicationEnvironment;
        private readonly ISecurityEncoder _encoder;
        private readonly IFlexMembershipRepository _membershipRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="FlexMembershipProvider" /> class.
        /// </summary>
        /// <param name="membershipRepository">The user store.</param>
        /// <param name="applicationEnvironment">The application environment.</param>
        public FlexMembershipProvider(IFlexMembershipRepository membershipRepository, IApplicationEnvironment applicationEnvironment)
        {
            _membershipRepository = membershipRepository;
            _applicationEnvironment = applicationEnvironment;
            _encoder = new DefaultSecurityEncoder();
        }

        #region IFlexMembershipProvider Members

        /// <summary>
        /// Determines whether the provided <paramref name="username"/> and
        /// <paramref name="password"/> combination is valid
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="rememberMe">
        /// if set to <c>true</c> [remember me].
        /// </param>
        /// <returns>
        /// 
        /// </returns>
        public bool Login(string username, string password, bool rememberMe = false)
        {
            IFlexMembershipUser user = _membershipRepository.GetUserByUsername(username);
            if (user == null || user.StatusUser != StatusUser.Active)
            {
                return false;
            }

            string encodedPassword = _encoder.Encode(password, user.Salt);
            bool passed = encodedPassword.Equals(user.Password);
            if (passed)
            {
                _applicationEnvironment.IssueAuthTicket(username, rememberMe);
                return true;
            }
            return false;
        }

        /// <summary>
        ///   Logout the current user
        /// </summary>
        public void Logout()
        {
            _applicationEnvironment.RevokeAuthTicket();
        }

        /// <summary>
        ///   Creates an account.
        /// </summary>
        /// <param name="user"> The user. </param>
        public void CreateAccount(User user)
        {
            IFlexMembershipUser existingUser = _membershipRepository.GetUserByUsername(user.Username);
            if (existingUser != null && existingUser.StatusUser == StatusUser.Active)
            {
                throw new FlexMembershipException(FlexMembershipStatus.DuplicateUserName);
            }

            user.Salt = user.Salt ?? _encoder.GenerateSalt();
            user.Password = _encoder.Encode(user.Password, user.Salt);
            _membershipRepository.Add(user);
        }

        /// <summary>
        ///   Updates the account.
        /// </summary>
        /// <param name="user"> The user. </param>
        public void UpdateAccount(User user)
        {
            _membershipRepository.Save(user);
        }

        /// <summary>
        ///   Determines whether the specific <paramref name="username" /> has a
        ///   local account
        /// </summary>
        /// <param name="username"> The username. </param>
        /// <returns> <c>true</c> if the specified username has a local account; otherwise, <c>false</c> . </returns>
        public bool HasLocalAccount(string userName)
        {
            IFlexMembershipUser user = _membershipRepository.GetUserByUsername(userName);
            return user != null && !String.IsNullOrEmpty(user.Password);
        }

        public bool Exists(string userName)
        {
            IFlexMembershipUser user = _membershipRepository.GetUserByUsername(userName);
            return user != null;
        }

        /// <summary>
        ///   Changes the password for a user
        /// </summary>
        /// <param name="username"> The username. </param>
        /// <param name="oldPassword"> The old password. </param>
        /// <param name="newPassword"> The new password. </param>
        /// <returns> </returns>
        public bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            var user = _membershipRepository.GetUserByUsername(username);
            if (user == null) throw new FlexMembershipException(FlexMembershipStatus.InvalidUserName);
            string encodedPassword = _encoder.Encode(oldPassword, user.Salt);
            if (!encodedPassword.Equals(user.Password))
            {
                return false;
            }

            user.Password = _encoder.Encode(newPassword, user.Salt);
            _membershipRepository.Save(user);
            return true;
        }

        /// <summary>
        ///   Sets the local password for a user
        /// </summary>
        /// <param name="username"> The username. </param>
        /// <param name="newPassword"> The new password. </param>
        public void SetLocalPassword(string username, string newPassword)
        {
            var user = _membershipRepository.GetUserByUsername(username);
            if (!String.IsNullOrEmpty(user.Password))
            {
                throw new FlexMembershipException("SetLocalPassword can only be used on accounts that currently don't have a local password.");
            }

            user.Salt = _encoder.GenerateSalt();
            user.Password = _encoder.Encode(newPassword, user.Salt);
            _membershipRepository.Save(user);
        }

        /// <summary>
        ///   Generates the password reset token for a user
        /// </summary>
        /// <param name="username"> The username. </param>
        /// <param name="tokenExpirationInMinutesFromNow"> The token expiration in minutes from now. </param>
        /// <returns> </returns>
        public string GeneratePasswordResetToken(string username, int tokenExpirationInMinutesFromNow = 1440)
        {
            var user = _membershipRepository.GetUserByUsername(username);
            if (user == null)
            {
                throw new FlexMembershipException(FlexMembershipStatus.InvalidUserName);
            }

            var tz = TimeZoneInfo.FindSystemTimeZoneById("Central Brazilian Standard Time");
            user.PasswordResetToken = GenerateToken();
            user.PasswordResetTokenExpiration = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow.AddMinutes(tokenExpirationInMinutesFromNow), tz);
            _membershipRepository.Save(user);

            return user.PasswordResetToken;
        }

        /// <summary>
        ///   Resets the password for the supplied
        ///   <paramref name="passwordResetToken" />
        /// </summary>
        /// <param name="passwordResetToken"> The password reset token to perform the lookup on. </param>
        /// <param name="newPassword"> The new password for the user. </param>
        /// <returns> </returns>
        public bool ResetPassword(string passwordResetToken, string newPassword)
        {
            var user = _membershipRepository.GetUserByPasswordResetToken(passwordResetToken);
            if (user == null)
            {
                return false;
            }

            if (String.IsNullOrEmpty(user.Salt))
            {
                user.Salt = _encoder.GenerateSalt();
            }
            user.Password = _encoder.Encode(newPassword, user.Salt);
            _membershipRepository.Save(user);

            return true;
        }

        public MjrAppRole GetUserRole(string userName)
        {
            var user = _membershipRepository.GetUserByUsername(userName);
            return user != null ? user.Roles.FirstOrDefault() : null;
        }

        public User GetUser(string userName)
        {
            return (User) _membershipRepository.GetUserByUsername(userName);
        }

        public string GenerateNewPassword()
        {
            return System.Web.Security.Membership.GeneratePassword(10, 2);
        }

        public User FindActiveUserByEmployeeId(int id)
        {
            return _membershipRepository.GetActiveUserByEmployeeId(id);
        }

        public void DeleteAccount(object userId)
        {
            _membershipRepository.Remove(userId);
        }

        #endregion

        #region IFlexOAuthProvider Members

        /// <summary>
        ///   Creates the OAuth account.
        /// </summary>
        /// <param name="provider"> The provider. </param>
        /// <param name="providerUserId"> The provider user id. </param>
        /// <param name="user"> The user. </param>
        public void CreateOAuthAccount(string provider, string providerUserId, User user)
        {
            var existingUser = _membershipRepository.GetUserByUsername(user.Username);
            if (existingUser == null)
            {
                _membershipRepository.Add(user);
            }
            _membershipRepository.CreateOAuthAccount(provider, providerUserId, existingUser ?? user);
        }

        /// <summary>
        ///   Dissassociates the OAuth account for a userid.
        /// </summary>
        /// <param name="provider"> The provider. </param>
        /// <param name="providerUserId"> The provider user id. </param>
        /// <returns> </returns>
        /*public bool DisassociateOAuthAccount(string provider, string providerUserId)
        {
            IFlexMembershipUser user = _userStore.GetUserByOAuthProvider(provider, providerUserId);
            if (user == null)
            {
                return false;
            }
            IEnumerable<OAuthAccount> accounts = _userStore.GetOAuthAccountsForUser(user.Username);

            if (HasLocalAccount(user.Username))
                return _userStore.DeleteOAuthAccount(provider, providerUserId);

            if (accounts.Count() > 1)
                return _userStore.DeleteOAuthAccount(provider, providerUserId);

            return false;
        }
        */
        /// <summary>
        ///   Gets the OAuth client data for a provider
        /// </summary>
        /// <param name="provider"> The provider. </param>
        /// <returns> </returns>
        public AuthenticationClientData GetOAuthClientData(string providerName)
        {
            return AuthenticationClients[providerName];
        }

        /// <summary>
        /// Gets the registered client data.
        /// </summary>
        /// <value>
        /// The registered client data.
        /// </value>
        public ICollection<AuthenticationClientData> RegisteredClientData
        {
            get { return AuthenticationClients.Values; }
        }

        /// <summary>
        ///   Gets the name of the OAuth accounts for a user.
        /// </summary>
        /// <param name="username"> The username. </param>
        /// <returns> </returns>
        //public IEnumerable<OAuthAccount> GetOAuthAccountsFromUserName(string username)
        //{
        //    return _userStore.GetOAuthAccountsForUser(username);
        //}

        /// <summary>
        /// Requests the OAuth authentication.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="returnUrl">The return URL.</param>
        //public void RequestOAuthAuthentication(string provider, string returnUrl)
        //{
        //    AuthenticationClientData client = AuthenticationClients[provider];
        //    _applicationEnvironment.RequestAuthentication(client.AuthenticationClient, this, returnUrl);
        //}

        /// <summary>
        ///   Verifies the OAuth authentication.
        /// </summary>
        /// <param name="action"> The action. </param>
        /// <returns> </returns>
        //public AuthenticationResult VerifyOAuthAuthentication(string returnUrl)
        //{
        //    string providerName = _applicationEnvironment.GetOAuthPoviderName();
        //    if (String.IsNullOrEmpty(providerName))
        //    {
        //        return AuthenticationResult.Failed;
        //    }

        //    AuthenticationClientData client = AuthenticationClients[providerName];
        //    return _applicationEnvironment.VerifyAuthentication(client.AuthenticationClient, this, returnUrl);
        //}

        /// <summary>
        /// Attempts to perform an OAuth Login.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="providerUserId">The provider user id.</param>
        /// <param name="persistCookie">if set to <c>true</c> [persist cookie].</param>
        /// <returns></returns>
        //public bool OAuthLogin(string provider, string providerUserId, bool persistCookie)
        //{
        //    AuthenticationClientData oauthProvider = AuthenticationClients[provider];
        //    HttpContextBase context = _applicationEnvironment.AcquireContext();
        //    var securityManager = new OpenAuthSecurityManager(context, oauthProvider.AuthenticationClient, this);
        //    return securityManager.Login(providerUserId, persistCookie);
        //}

        #endregion

        #region IOpenAuthDataProvider Members

        /// <summary>
        /// Gets the user name from open auth.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="providerUserId">The provider user id.</param>
        /// <returns></returns>
        public string GetUserNameFromOpenAuth(string provider, string providerUserId)
        {
            IFlexMembershipUser user = _membershipRepository.GetUserByOAuthProvider(provider, providerUserId);
            if (user != null)
            {
                return user.Username;
            }
            return String.Empty;
        }

        #endregion

        /// <summary>
        /// Generates the token.
        /// </summary>
        /// <returns></returns>
        private static string GenerateToken()
        {
            using (var prng = new RNGCryptoServiceProvider())
            {
                return GenerateToken(prng);
            }
        }

        /// <summary>
        /// Generates the token.
        /// </summary>
        /// <param name="generator">The generator.</param>
        /// <returns></returns>
        internal static string GenerateToken(RandomNumberGenerator generator)
        {
            var tokenBytes = new byte[TokenSizeInBytes];
            generator.GetBytes(tokenBytes);
            return HttpServerUtility.UrlTokenEncode(tokenBytes);
        }

        /// <summary>
        /// Registers the client.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="extraData">The extra data.</param>
        //public static void RegisterClient(IAuthenticationClient client,
        //                                  string displayName, IDictionary<string, object> extraData)
        //{
        //    var clientData = new AuthenticationClientData(client, displayName, extraData);
        //    AuthenticationClients.Add(client.ProviderName, clientData);
        //}
    }
}