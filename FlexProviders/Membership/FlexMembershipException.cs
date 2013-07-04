using System;
using System.Runtime.Serialization;
using WebSiteMjr.Domain.Model.Membership;

namespace FlexProviders.Membership
{
    [Serializable]
    public class FlexMembershipException : Exception
    {
        public FlexMembershipStatus StatusCode { get; set; }

        public FlexMembershipException()
        {
        }

        public FlexMembershipException(string message) : base(message)
        {
        }

        public FlexMembershipException(string message, Exception inner) : base(message, inner)
        {
        }

        public FlexMembershipException(FlexMembershipStatus statusCode)
        {
            StatusCode = statusCode;
        }

        protected FlexMembershipException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}