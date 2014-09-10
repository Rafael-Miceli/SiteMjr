using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        private static List<Client> Clients; 

        static void Main(string[] args)
        {
            RegisterClients();
            InstallServerInOneClient();

            foreach (var client in Clients)
            {
                Console.WriteLine(client.Name);
                foreach (var cameraServer in client.CameraServers)
                {
                    Console.WriteLine(cameraServer.Name);
                    foreach (var channel in cameraServer.Channels)
                    {
                        Console.WriteLine(channel.ChannelName + ": " + channel.Status);
                    }
                }
            }

            Console.ReadKey();
        }

        private static void InstallServerInOneClient()
        {
            var portoverano = Clients.Find(c => c.Id == 1);
            var portoveranoCameraServers = portoverano.CameraServers.ToList();

            var servidor1 = new CameraServer
            {
                Id = 1,
                Client = portoverano,
                Name = "Servidor 1"
            };

            portoveranoCameraServers.Add(servidor1);

            InstallCameraOnServer(servidor1);

            portoveranoCameraServers.Add(new CameraServer
            {
                Id = 2,
                Client = portoverano,
                Name = "Servidor 2"
            });

            portoverano.CameraServers = portoveranoCameraServers;
        }

        private static void InstallCameraOnServer(CameraServer servidor1)
        {
            for (int i = 0; i < 16; i++)
            {
                Channel.ChannelStatus status;

                if (i % 2 == 0)
                {
                    status = Channel.ChannelStatus.Ok;
                }
                else if(i == 7 || i == 11 || i == 13)
                {
                    status = Channel.ChannelStatus.Out;
                }
                else
                {
                    status = Channel.ChannelStatus.Interference;
                }

                var channel = new Channel
                {
                    Id = i + 1,
                    ChannelName = "Camera" + (i + 1),
                    Status = status,
                    CameraServer = servidor1
                };

                servidor1.Channels.Add(channel);
            }
        }

        static void RegisterClients()
        {
            Clients = new List<Client>
            {
                new Client
                {
                    Id = 1,
                    Name = "Portoverano"
                },
                new Client
                {
                    Id = 2,
                    Name = "Portomare"
                },
            };
        }
    }

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

        private ICollection<Channel> _channels;
        public ICollection<Channel> Channels
        {
            get { return _channels ?? (_channels = new Collection<Channel>()); }
            set { _channels = value; }
        }

        public Client Client { get; set; }
    }

    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }

        private ICollection<CameraServer> _cameraServers;
        public ICollection<CameraServer> CameraServers
        {
            get { return _cameraServers ?? (_cameraServers = new Collection<CameraServer>()); }
            set { _cameraServers = value; } 
        }
    }

    public class Call
    {
        public Guid Id { get; set; }
        public Client Client { get; set; }
        public string Tile { get; set; }
        public ServiceType ServiceType { get; set; }
    }

    public class ServiceType
    {
        public string Description { get; set; }
    }

    public class CameraServiceType
    {
        public IEnumerable<CameraServer> CameraServers { get; set; }
    }
}
