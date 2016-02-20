using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TilesServer
{
    public struct NetworkCommand
    {
        public string commandText;
        public byte[] msg;
        public NetworkStream stream;
        public string uID;
        public NetworkCommand(string commandText, byte[] msg, NetworkStream stream, string uID)
        {
            this.commandText = commandText;
            this.msg = msg;
            this.stream = stream;
            this.uID = uID;
        }
    }

    public struct UnpackedNetworkCommand
    {
        public string[] networkCommands;
        public string[] data;
        public string uID;
        public UnpackedNetworkCommand(string[] networkCommands, string[] data, string uID)
        {
            this.networkCommands = networkCommands;
            this.data = data;
            this.uID = uID;
        }
    }

    class Globals
    {
    }
}
