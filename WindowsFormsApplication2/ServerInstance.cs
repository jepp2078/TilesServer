using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace TilesServer
{
    public class ServerInstance
    {
        //Is server listening?
        public bool listening = false;
        // Create Server Tick timer
        private System.Windows.Forms.Timer timer;
        // Set the TcpListener on port 1337.
        private Int32 port = 1337;
        //Server instance
        TcpListener server = null;
        //Set local address to 127.0.0.1
        private IPAddress localAddr = IPAddress.Parse("127.0.0.1");
        //ServerController instance
        public ServerController controller;
        //Create list of currently connected clients
        public List<TcpClient> currClients = new List<TcpClient>();
        //Create list of client network streams
        public List<NetworkStream> currClientStreams = new List<NetworkStream>();
        //Create list of unprocessed commands
        public List<NetworkCommand> currNetworkCommands = new List<NetworkCommand>();
        //Create list containing command history
        private List<NetworkCommand> networkCommandHistory = new List<NetworkCommand>();
        //Create command controller
        private CommandController commandController;

        public void Start(ServerController controller)
        {
            this.controller = controller;
            commandController = new CommandController(this);
            controller.consoleTextAdd("Starting server...");
            try
            {
                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, port);
                controller.consoleTextAdd("Server running...");
                //Start listining for connections
                acceptConnections();
                //Start server tick
                InitTimer();
                controller.consoleTextAdd("Actively listening for connections...");
            }
            catch (SocketException e)
            {
                controller.consoleTextAdd("SocketException: {0}", e);
            }
        }

        public void Stop()
        {
            listening = false;
            timer.Stop();
            closeAllConnections();
            server.Stop();
            controller.consoleTextAdd("Server stopped...");
        }

        private void InitTimer()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 5000; // in miliseconds
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            serverTick();
        }

        private void acceptConnections()
        {
            listening = true;
            server.Start();
            Thread tmp_thread = new Thread(new ThreadStart(() =>
            {
                while (listening)
                {
                    if (!server.Pending())
                    {
                        //controller.consoleTextAdd("No connection requests currently pending...");
                        Thread.Sleep(100); //<--- timeout
                    } else {

                        //Accept the pending client connection and return a TcpClient object initialized for communication.
                        currClients.Add(server.AcceptTcpClient());
                        //Add the TcpClient network stream to the network stream list
                        currClientStreams.Add(currClients[currClients.Count-1].GetStream());
                        //controller.consoleTextAdd("Added a client! Theres {0} clients currently connected...", currClients.Count);
                    }
                }
            }));
            tmp_thread.Start();
        }

        private void closeAllConnections()
        {
            var count = currClients.Count;
            foreach (var clients in currClients)
            {
                //Shutdown and end all current connections
                clients.Close();
            }
            //Clear client and network stream lists
            currClients.Clear();
            currClientStreams.Clear();
            controller.consoleTextAdd("Closed all clients! {0} clients dropped...", count);
        }

        private void serverTick()
        {
            recieveCommands();
            ackCommands();
            processCommands();
        }

        private void recieveCommands()
        {
            foreach (var stream in currClientStreams)
            {
                //Buffer for reading data
                Byte[] bytes = new Byte[256];
                String data = null;

                data = null;

                int i;

                // Loop to receive all the data sent by the client.
                if ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    // Translate data bytes to a ASCII string.
                    data = Encoding.ASCII.GetString(bytes, 0, i);
                    controller.consoleTextAdd("Received: {0}", data);

                    // Process the data sent by the client.
                    data = data.ToUpper();
                    byte[] msg = Encoding.ASCII.GetBytes(data);
                    NetworkCommand command = new NetworkCommand(data, msg, stream, (string)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds.ToString());
                    currNetworkCommands.Add(command);
                }
            }
        }

        private void ackCommands()
        {
            // Send an acknowledgement back to all commands
            foreach (var command in currNetworkCommands)
            {
                command.stream.Write(command.msg, 0, command.msg.Length);
                controller.consoleTextAdd("Acknowledged {0}", command.commandText);
                networkCommandHistory.Add(command);
            }
        }

        private void processCommands()
        {
            foreach (var command in currNetworkCommands)
            {
                controller.consoleTextAdd("Processing {0}...", command.commandText);
                commandController.recieveCommand(command);
            }
            //Clear networkCommands since they have been proccessed and acknowledged
            currNetworkCommands.Clear();
        }
    }
}
