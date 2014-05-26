﻿/*application to connect with PHP TCP Server
 * intend to send frame buffer via this application
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
namespace Socket_Programing_Client
{
    class Program
    {
        //Socket to use
        private static Socket _clientSocket = new Socket
            (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        //Main method
        static void Main(string[] args)
        {
            Console.Title = "Client";
            LoopConnect();
            sendLoop();
            Console.ReadLine();
        }

        private static void sendLoop()
        {
            //while (true)
            //{
                Console.Write("Enter a Request: ");
                string req=Console.ReadLine();
                byte[] buffer = Encoding.ASCII.GetBytes(req);
                _clientSocket.Send(buffer);


                byte[] recivedBuffer = new byte[1024];
                int rec = _clientSocket.Receive(recivedBuffer);

                byte[] data = new byte[rec];
                Array.Copy(recivedBuffer,data,rec);
                Console.WriteLine("Recived : "+Encoding.ASCII.GetString(data));
                
            //}
        }

        private static void LoopConnect()
        {
            int attempts = 0;
            while (!_clientSocket.Connected)
            {
                try
                {
                    attempts++;
                    _clientSocket.Connect("127.0.0.1", 25003);
                }
                catch (SocketException ex)
                {
                    Console.Clear();
                    Console.WriteLine("Connection Attempt : " + attempts.ToString());
                }
            }

            Console.Clear();
            Console.WriteLine("Connected");

        }
    }
}
