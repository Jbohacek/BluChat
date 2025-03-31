using System.ComponentModel;
using BluChat.Core;
using SuperSimpleTcp;

using System.Text;
using System.Diagnostics;


namespace BluChat.TestClient
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private Core.Client client;

        public BindingList<Log> _logs = new BindingList<Log>();

        private void Main_Load(object sender, EventArgs e)
        {
            dgw_log.DataSource = _logs;
            _logs.Add(new Log("ClientLoaded"));
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            client = new Client(txt_IpAdress.Text, txt_port.Text);
            client.Events.DataReceived += DataReceived;
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
    }
}
