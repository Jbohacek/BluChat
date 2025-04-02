using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BluChat.Core.Data;
using BluChat.Core.Logger;
using BluChat.Core.UserFolder;
using BluChat.Core.Logger.Interfaces;
using Microsoft.EntityFrameworkCore;
using SuperSimpleTcp;
using BluChat.Core.Data.Interfaces;

namespace BluChat.Core.Server
{
    public class Server
    {
        public IpPort Adress { get; set; }

        private ILogger Logger { get; set; }

        private SimpleTcpServer server { get; set; }

        public List<User> ConnectedUsers { get; set; } = new List<User>();

        public UnitOfWork Database { get; set; }

        private Server()
        {
        }

        private void SetEvents()
        {
            if (server == null) throw new Exception("server is null");
            server.Events.ClientConnected += OnUserConnection!;
            server.Events.ClientDisconnected += OnUserDisconect!;
            Logger.LogAdded += OnLogAdded!;
        }

        public void Start()
        {
            Logger.Add(LogFactory.ServerStarted(this.Adress));
            server.Start();
        }

        //todo: Tohle fuj, dat pryc
        public void Send(User user, string message)
        {
            server.Send(user.Adress.ToString(), message);
        }


        private void OnUserConnection(object sender, ConnectionEventArgs e)
        {
            User user = new User(e.IpPort, DateTime.Now);
            Logger.Add(LogFactory.UserConnected(user));
            ConnectedUsers.Add(user);

            server.Send(user.Adress.ToString(), "Hello to server :)");
        }

        private void OnUserDisconect(object sender, ConnectionEventArgs e)
        {
            User? user = ConnectedUsers.SingleOrDefault(x => x.Adress.ToString() == e.IpPort);
            if (user == null)
            {
                Logger.Add(LogFactory.UserNotFound(e.IpPort));
                return;
            }

            ConnectedUsers.Remove(user);
            Logger.Add(LogFactory.UserDisconnected(user));
        }

        public void OnLogAdded(object sender, LogEventHandler e)
        {
            Console.ForegroundColor = Enums.LevelExtensions.ToConsoleColor(e.Log.Level);
            Console.WriteLine(e.Log);
            Console.ResetColor();
        }


        public class ServerBuilder()
        {
            private readonly Server _server = new Server();
            private SimpleTcpServerSettings _settings = new SimpleTcpServerSettings();
            private readonly User _adminUser = new User();

            public ServerBuilder SetAdress(IpPort adress)
            {
                _server.Adress = adress;
                return this;
            }


            public ServerBuilder SetLogger(ILogger logger)
            {
                _server.Logger = logger;
                return this;
            }

            public ServerBuilder SetSettings(SimpleTcpServerSettings settings)
            {
                _settings = settings;
                return this;
            }

            public ServerBuilder SetDatabase(SqlLiteContext context)
            {
                _server.Database = new UnitOfWork(context);
                return this;
            }

            public ServerBuilder SetAdminUserPassword(string password)
            {
                _adminUser.UserName = "Admin";
                _adminUser.HashPassword = password;
                return this;
            }



            public Server Build()
            {
                if (_server.Adress == null) throw new Exception("Address not added");
                if (_server.Logger == null) throw new Exception("Logger not set");
                if (_server.Database == null) throw new Exception("Database not set");

                //Server inicilaization
                _server.server = new SimpleTcpServer(_server.Adress.ToString());
                _server.SetEvents();

                //Database initialization
                _server.Database.Logger = _server.Logger;

                if (!_server.Database.Users.Exists(x => x.UserName == _adminUser.UserName))
                {
                    _server.Database.Users.Add(_adminUser);
                    _server.Database.Save();
                }

                

                
                

                return _server;
            }

        }

    }
}

