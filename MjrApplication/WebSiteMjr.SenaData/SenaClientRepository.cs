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
    public class SenaClientRepository : ISenaClientRepository
    {
        public static MobileServiceClient MobileService; 

        public SenaClientRepository()
        {           
            MobileService = new MobileServiceClient("https://arduinoapp.azure-mobile.net/", "QkTMsFHSEaNGuiKVsywYYHpHnIHMUB64");
        }

        public void Add(string name)
        {
            Clients clients = new Clients { Name = name };          
            MobileService.GetTable<Clients>().InsertAsync(clients).Wait();
        }

        public string GetClientGuidByName(string name)
        {
            var client = MobileService.GetTable<Clients>().Where(x => x.Name == name).ToListAsync().Result;

            if (client.Count == 0)
                return null;
            else
                return client.First().Id;
        }
    }

    public class Clients
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
