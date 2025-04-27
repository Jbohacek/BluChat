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
using BluChat.Core.Messages;

namespace BluChat.Core.ServerFolder
{
    public class Server
    {
        public IpPort Adress { get; set; }

        private ILogger Logger { get; set; }

        private SimpleTcpServer server { get; set; }

        public List<User> ConnectedUsers { get; set; } = new List<User>();
        public List<UserServerStatus> AnonymousUsers { get; set; } = new List<UserServerStatus>();

        public UnitOfWork Database { get; set; }
        public MessageServerManager MessageServerManager { get; set; }
        
        public DateTime? ServerStartDate { get; set; }

        private Server()
        {
        }

        private void SetEvents()
        {
            if (server == null) throw new Exception("server is null");
            server.Events.ClientConnected += OnUserConnection!;
            server.Events.ClientDisconnected += OnUserDisconect!;
            server.Events.DataReceived += OnDataReceived;



            Logger.LogAdded += OnLogAdded!;
        }

        private void OnDataReceived(object? sender, DataReceivedEventArgs e)
        {
            string content = Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count);

            MessageServerManager.RecieveMessage(content,e.IpPort);
        }

        public void Start()
        {
            Logger.Add(LogFactory.ServerStarted(Adress));
            server.Start();
            ServerStartDate = DateTime.Now;
        }
        //todo: přidat stop možnost :D

        //todo: Tohle fuj, dat pryc
        public void Send(User user, string message)
        {
            server.Send(user.Adress.ToString(), message);
        }


        private void OnUserConnection(object sender, ConnectionEventArgs e)
        {
            UserServerStatus status = new UserServerStatus(e.IpPort,DateTime.Now); 
            AnonymousUsers.Add(status);
            Logger.Add(LogFactory.AnonymousUserConnected(new IpPort(e.IpPort)));


            //User user = new User(e.IpPort, DateTime.Now);
            //Logger.Add(LogFactory.UserConnected(user));
            //ConnectedUsers.Add(user);

            //server.Send(user.Adress.ToString(), "Hello to server :)");
        }

        private void OnUserDisconect(object sender, ConnectionEventArgs e)
        {
            User? user = ConnectedUsers.SingleOrDefault(x => x.Adress.ToString() == e.IpPort);
            if (user != null)
            {
                ConnectedUsers.Remove(user);
                Logger.Add(LogFactory.UserDisconnected(user));
                return;
            }

            UserServerStatus? anonymous = AnonymousUsers.SingleOrDefault(x => x.Adress.ToString() == e.IpPort);
            if(anonymous != null)
            {
                AnonymousUsers.Remove(anonymous);
                Logger.Add(LogFactory.AnonymousUserDisconnected(anonymous.Adress));
                return;
            }

            Logger.Add(LogFactory.UserNotFound(e.IpPort));
        }

        public void OnLogAdded(object sender, LogEventHandler e)
        {
            Console.ForegroundColor = Enums.LevelExtensions.ToConsoleColor(e.Log.Level);
            Console.WriteLine(e.Log);
            Console.ResetColor();
        }

        private void OnServerClose(object sender, EventArgs e)
        {
            Logger.Add(LogFactory.ServerStopping());
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

            public ServerBuilder SetOnClosingEvent()
            {
                AppDomain.CurrentDomain.ProcessExit += _server.OnServerClose!;
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

                //Server ChatManager
                _server.MessageServerManager = new MessageServerManager(_server.Database, _server.Logger,_server.server,_server);
                
                

                return _server;
            }

        }

    }
}

