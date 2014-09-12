using System.Collections.Generic;

namespace WebSiteMjr.Domain.CustomerService.Model
{
    //public class CameraServiceType : ServiceType
    //{
    //    public ICollection<CameraServer> CameraServers { get; set; }
    //}

    public class Channel
    {
        public enum ChannelStatus
        {
            Ok,
            Out,
            Interference,
            NoCamera
        }

        public int Id { get; set; }
        public string ChannelName { get; set; }
        public CameraServer CameraServer { get; set; }
        public ChannelStatus Status { get; set; }
    }

    public class CameraServer
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}