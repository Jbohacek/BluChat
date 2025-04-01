using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BluChat.Core.Logger;
using BluChat.Core.Logger.Interfaces;
using SuperSimpleTcp;

namespace BluChat.Core.Server
{
    public class Server
    {
        private string Ip { get; set; }
        private string Port { get; set; }
        public string IpPort => Ip + ":" + Port;

        private ILogger _logger { get; set; }

        private SimpleTcpServer server { get; set; }


        private Server()
        {
            
        }

        public void Start()
        {
            _logger.Add(LogFactory.ServerStarted());
            server.Start();
        }



        public class ServerBuilder()
        {
            private readonly Server _server = new Server();
            private SimpleTcpServerSettings _settings = new SimpleTcpServerSettings();

            public ServerBuilder SetIp(string ip)
            {
                _server.Ip = ip;
                return this;
            }

            public ServerBuilder SetPort(string port)
            {
                _server.Port = port;
                return this;
            }

            public ServerBuilder SetLogger(ILogger logger)
            {
                _server._logger = logger;
                return this;
            }

            public ServerBuilder SetSettings(SimpleTcpServerSettings settings)
            {
                _settings = settings;
                return this;
            }

            public Server Build()
            {
                if (string.IsNullOrEmpty(_server.Ip)) throw new Exception("Ip not added");
                if (string.IsNullOrEmpty(_server.Port)) throw new Exception("Port not set");
                if (_server._logger == null) throw new Exception("Logger not set");

                _server.server = new SimpleTcpServer(_server.IpPort);

                return _server;
            }

        }

    }
}

