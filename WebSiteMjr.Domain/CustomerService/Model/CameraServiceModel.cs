using System.Collections.Generic;

namespace WebSiteMjr.Domain.CustomerService.Model
{
    public class CameraServiceModel : ServiceType
    {
        public class CameraServer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Channels { get; set; }
        }

        public IEnumerable<CameraServer> CameraServers { get; set; }


    }
}