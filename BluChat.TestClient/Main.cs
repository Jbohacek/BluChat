using System.ComponentModel;
using SuperSimpleTcp;

using System.Text;
using System.Diagnostics;
using BluChat.Core.Messages.MessageTypes.GetChats;
using BluChat.Core.ClientFolder;
using BluChat.Core.ClientFolder.EvenHandlers;
using System;
using BluChat.Core.Messages.Data;
using System.Windows.Forms;


namespace BluChat.TestClient
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private Client client;

        public BindingList<Log> _logs = new BindingList<Log>();

        private void Main_Load(object sender, EventArgs e)
        {
            dgw_log.DataSource = _logs;
            _logs.Add(new Log("ClientLoaded"));
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            client = new Client(txt_IpAdress.Text, Convert.ToInt32(txt_port.Text));
            SetEvents();
            client.Connect();

            if (client.IsConnected)
            {
                gr_Authentication.Visible = true;
            }
        }

        bool eventSet = false;
        private void SetEvents()
        {
            if (eventSet)
                return;

            eventSet = true;

            client.TcpEvents.DataReceived += DataReceived;
            client.Events.UserVerified += UserVerified;
            client.Events.UserFailedVerification += UserFailedVerification;
            client.Events.GetChatMessages += messagesChanged;
            client.Events.MessageRecieved += MessageRecieved!;
        }


        private void btn_dissconnect_Click(object sender, EventArgs e)
        {
            client.Disconnect();
            gr_Authentication.Visible = false;
            gr_chats_messages.Visible = false;
        }

        private void DataReceived(object sender, SuperSimpleTcp.DataReceivedEventArgs e)
        {
            string message = $"[{e.IpPort}] {Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count)}";
            var action = new Action((() => AddLog(message)));
            if (InvokeRequired)
            {
                Invoke(action);
            }
            else
            {
                action.Invoke();
            }
        }

        private void UserVerified(object sender, UserVerifiedEventHandler e)
        {
            var action = new Action(() =>
            {
                txt_output.Text =
                    $"{e.user.Id}\n{e.user.UserName}\n{e.user.ProfilePicPath}\nChat Count: {e.user.Chats.Count}";
                gr_chats_messages.Visible = true;

                box_chats.Items.Clear();
                box_messages.Items.Clear();
                foreach (var chat in e.user.Chats)
                {
                    box_chats.Items.Add(chat);
                }
            });
            InvokeRequiredTask(action);
        }

        private void UserFailedVerification(object sender, UserFailedVerificationHandler e)
        {
            var action = new Action(() =>
            {
                txt_output.Text = e.Reason;
            });
            InvokeRequiredTask(action);
        }



        private void InvokeRequiredTask(Action action)
        {
            if (InvokeRequired)
            {
                Invoke(action);
            }
            else
            {
                action.Invoke();
            }
        }


        private void AddLog(string message)
        {
            _logs.Add(new Log(message));
        }

        public class Log
        {
            public string context { get; set; }
            public DateTime time { get; set; }

            public Log(string message)
            {
                context = message;
                time = DateTime.Now;
            }
        }

        private void btn_RequestChats_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btn_authenticate_Click(object sender, EventArgs e)
        {
            if (!client.IsConnected)
            {
                MessageBox.Show("Nepøipojeno k server!");
                return;
            }
            client.Manager.SendAuthentication(txt_Username.Text, txt_Password.Text);
        }

        private Chat? _selectedChat = null;
        private void box_chats_SelectedValueChanged(object sender, EventArgs e)
        {

            if (box_chats.SelectedItem is not Chat)
                return;

            Chat? selectedChat = box_chats.SelectedItem as Chat;
            if (selectedChat == null)
                return;

            _selectedChat = selectedChat;

            client.Manager.GetChatMessages(selectedChat);

            Debug.WriteLine(sender);
        }

        private void messagesChanged(object? sender, GetChatMessagesEventHandler e)
        {
            Action action = new Action(() =>
            {
                if (_selectedChat == null)
                    return;

                box_messages.Items.Clear();
                foreach (var message in _selectedChat.Messages)
                {
                    box_messages.Items.Add(message);
                }
                btn_send.Enabled = true;
            });
            InvokeRequiredTask(action);
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            if (_selectedChat == null)
            {
                btn_send.Enabled = false;
                return;
            }

            client.Manager.SendMessage(txt_userInputMessage.Text,_selectedChat);
        }

        private void MessageRecieved(object sender, MessageRecievedArgs e)
        {
            Action action = new Action(() =>
            {
                if (_selectedChat == null)
                {
                    return;
                }
                _selectedChat.Messages.Add(e.Message);
                box_messages.Items.Add(e.Message.ToString());
            });
            InvokeRequiredTask(action);
        }
    }
}
