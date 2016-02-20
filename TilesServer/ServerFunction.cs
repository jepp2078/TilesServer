using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TilesServer
{
    public class ServerFunction
    {
        private ServerInstance instance;

        public ServerFunction(ServerInstance instance)
        {
            this.instance = instance;
        }

        public void receiveUnpackedNetworkCommand(UnpackedNetworkCommand command)
        {
            switch (command.networkCommands[0])
            {
                case "ADDUSER":
                    addUser(command.data);
                    instance.controller.consoleTextAdd("Processed {0}...", command.networkCommands[0]);
                    break;
                case "QUIT":
                    closeConnection(command.uID);
                    instance.controller.consoleTextAdd("Processed {0}...", command.networkCommands[0]);
                    break;
                default:
                    instance.controller.consoleTextAdd("Command {0} not defined in server functions!", command.networkCommands[0]);
                    break;
            }
        }

        //SERVER FUNCTIONS
        private void addUser(string[] data)
        {
            if(data.Length == 2)
            {
                instance.controller.consoleTextAdd("Added user: {0}, with password {1}", data[0], data[1]);
            }
        }

        private void closeConnection(string uID)
        {
            int count = 0;
            foreach (var stream in instance.currNetworkCommands)
            {
                if (stream.uID == uID)
                {
                    break;
                }
                count++;
            }
            instance.currClients[count].Close();
            instance.currClientStreams[count].Close();
            instance.currClients.RemoveAt(count);
            instance.currClientStreams.RemoveAt(count);
            instance.controller.consoleTextAdd("Closed connection! {0} still connections active...", instance.currClients.Count);
        }

    }
}
