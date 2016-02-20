using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TilesServer
{
    public class CommandController
    {
        //Create server function layer
        private ServerFunction serverFunctions;

        public CommandController(ServerInstance instance)
        {
            serverFunctions = new ServerFunction(instance);
        }

        public void recieveCommand(NetworkCommand command)
        {
            UnpackedNetworkCommand commandToProccess = unpackNetworkCommand(command);
            serverFunctions.receiveUnpackedNetworkCommand(commandToProccess);
        }

        private UnpackedNetworkCommand unpackNetworkCommand(NetworkCommand command)
        {
            //Unpack command to string
            string packedCommand = command.commandText;
            //Split commands from data
            string[] unpackedCommand = packedCommand.Split(';');
            //Split last command from data
            List<string> unpackedDataList = unpackedCommand.Last().Split(':').ToList();
            //Insert last command without data to unpacked command array
            unpackedCommand[unpackedCommand.Length - 1] = unpackedDataList.First();
            //Remove the last command from data list
            unpackedDataList.RemoveAt(0);
            //Convert data list to array
            string[] unpackedData = unpackedDataList.ToArray(); 
            //Create unpacked network command object
            UnpackedNetworkCommand unpackedNetworkCommand = new UnpackedNetworkCommand(unpackedCommand, unpackedData, command.uID);
            return unpackedNetworkCommand;
        }
        
        private void processCommand(UnpackedNetworkCommand command)
        {

        }
    }
}
