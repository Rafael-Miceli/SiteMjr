using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteMjr.Domain.Interfaces.Repository.Sena;

namespace WebSiteMjr.SenaData
{
    public class SenaUserRepository : ISenaUserRepository
    {
        public static MobileServiceClient MobileService; 

        public SenaUserRepository()
        {           
            MobileService = new MobileServiceClient("https://arduinoapp.azure-mobile.net/", "QkTMsFHSEaNGuiKVsywYYHpHnIHMUB64");
        }

        public void Add(string email, string companyName)
        {
            accounts user = new accounts { Username = email, Cliente = companyName, Password = "vazio", Salt = "vazio" };          
            MobileService.GetTable<accounts>().InsertAsync(user).Wait();
        }

        public string GetGuidByEmail(string email)
        {
            var users = MobileService.GetTable<accounts>().Where(x => x.Username == email).ToListAsync().Result;

            if (users.Count == 0)
                return null;
            else
                return users.First().Id;
        }
    }

    public class accounts
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Cliente { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
    }
}
