using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Socket_Programing_Client
{
    class SocketHandler
    {
        private static int _SocketNumber = 0;//Socket that assigned by the user
        private static string _ip = null;

        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="socket"></param>
        public SocketHandler(int socket,string ip)
        {
            _SocketNumber = socket; //initiate the socket 
            _ip = ip;
        }
        private Socket _clientSocket = new Socket
            (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //Method to connect
        public bool connect()
        {
            try
            {
                _clientSocket.Connect(_ip, _SocketNumber);
                return true;
            }
            catch(SocketException)
            {
                Console.Clear();
                Console.WriteLine("Connection Attempt Failed");
                return false;
            }
        }
        //Send data to the socket
        public void sendData(string toSend)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(toSend);
            _clientSocket.Send(buffer);
        }
        //Listening to the port
        public string listen()
        {
            byte[] recivedBuffer = new byte[1024];
            int recived = _clientSocket.Receive(recivedBuffer);

            byte[] data = new byte[recived];
            Array.Copy(recivedBuffer, data, recived);
            return Encoding.ASCII.GetString(data);
        }
    }
}
