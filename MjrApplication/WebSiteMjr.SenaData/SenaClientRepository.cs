using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
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

        public async Task Add(string name)
        {
            var clients = new Clients { Name = name };          
            await MobileService.GetTable<Clients>().InsertAsync(clients);
        }

        public async Task<string> GetClientGuidByName(string name)
        {
            var client = await MobileService.GetTable<Clients>().Where(c => c.Name == name).ToListAsync();

            return client.Count == 0 ? null : client.First().Id;
        }
    }

    public class Clients
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
