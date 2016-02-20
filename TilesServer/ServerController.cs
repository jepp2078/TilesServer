using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TilesServer
{
    public partial class ServerController : Form
    {
        private ServerInstance instance;

        public ServerController(ServerInstance instance)
        {
            this.instance = instance;
            InitializeComponent();
        }

        private void serverStart_Click(object sender, EventArgs e)
        {
            instance.Start(this);
        }

        private void serverStop_Click(object sender, EventArgs e)
        {
            instance.Stop();
        }

        private void consoleText_TextChanged(object sender, EventArgs e)
        {
            consoleText.SelectionStart = consoleText.Text.Length;
            consoleText.ScrollToCaret();
        }

        public void consoleTextAdd(string msg, params object[] list)
        {
            if (!string.IsNullOrEmpty(msg) || list != null)
            {
                string stringOut = "";
                string formattedString = string.Format(msg, list);

                if (consoleText.Text != "")
                {
                    stringOut += Environment.NewLine;
                }

                stringOut += "[" + DateTime.Now.ToString() + "] ";

                stringOut += formattedString;

                consoleText.AppendText(stringOut);
            }
        }
    }
}
