using System.Collections.Generic;

namespace WebSiteMjr.Domain.CustomerService.Model
{
    public class CameraServiceModel : ServiceType
    {
        public IEnumerable<string> CameraServerName { get; set; }
    }
}