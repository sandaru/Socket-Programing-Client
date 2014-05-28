/*application to connect with PHP TCP Server
 * intend to send frame buffer via this application
 */
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Socket_Programing_Client
{
    class Program
    {
        //Socket to use
        private static Socket _clientSocket = new Socket
            (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        //Main method
        public static void Main(string[] args)
        {
            Console.Title = "Client";
            _LoopConnect();
            _sendLoop();
            Console.ReadLine();
        }

        //Get UUID
        private static String _getUuid()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            var attribute = (GuidAttribute)assembly.GetCustomAttributes(typeof(GuidAttribute), true)[0];
            var uuid = attribute.Value.ToString();
            return uuid;
        }

        //Send data
        private static void _sendLoop()
        {

            if (_registered())
            {
                Console.Write("Enter a Request: ");
                string req = "register:" + _getUuid();
                byte[] buffer = Encoding.ASCII.GetBytes(req);
                _clientSocket.Send(buffer);


                byte[] recivedBuffer = new byte[1024];
                int rec = _clientSocket.Receive(recivedBuffer);

                byte[] data = new byte[rec];
                Array.Copy(recivedBuffer, data, rec);
                Console.WriteLine("Received : " + Encoding.ASCII.GetString(data));
            }
            else
            {
                //TODO 
                //Send TCP request to add application to database
                String recived = null;
                try
                {
                    string req = "register:" + _getUuid();
                    byte[] buffer = Encoding.ASCII.GetBytes(req);
                    _clientSocket.Send(buffer);
                    byte[] recivedBuffer = new byte[1024];
                    int rec = _clientSocket.Receive(recivedBuffer);

                    byte[] data = new byte[rec];
                    Array.Copy(recivedBuffer, data, rec);
                    recived = Encoding.ASCII.GetString(data);
                }
                catch
                {
                    Console.WriteLine("Cannot be registered !!");
                }
                //Create verification file 
                if (recived == "success")
                {
                    File.Create("application.config.log").Dispose();
                    // Write the string to a file.
                    System.IO.StreamWriter file = new System.IO.StreamWriter("application.config.log");
                    file.WriteLine(_getUuid());
                    _AddEncryption("application.config.log");
                }
                else
                {
                    Console.WriteLine("Application cannot be registered ! ");
                }
            }

        }

        //Check application is registered or not
        private static  bool _registered()
        {
            if (File.Exists("application.config.log"))
            {
                string savedId = _readfile("application.config.log");
                if (savedId != null)
                {
                    return _matchUUID(savedId);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        //Match UUID
        private static bool _matchUUID(string uuid)
        {
            if (_getUuid() == uuid)
            {
                return true;
            }
            else
            {
                return false;
            }
        } 

        //Read file
        private  static string _readfile(String filename)
        {
            String line = null;
            try
            {
                using (StreamReader file = new StreamReader("TestFile.txt"))
                {
                    _RemoveEncryption(filename);//Decrypt file
                    line= file.ReadToEnd();//Read the file
                    _AddEncryption(filename);//Encrypt file
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error while reading the file");
                Console.ReadLine();
            }

            return line;
        }

        //Try to connect  
        private static void _LoopConnect()
        {
            int attempts = 0;
            while (!_clientSocket.Connected)
            {
                try
                {
                    attempts++;
                    _clientSocket.Connect("127.0.0.1", 25003);
                }
                catch (SocketException)
                {
                    Console.Clear();
                    Console.WriteLine("Connection Attempt : " + attempts.ToString());
                }
            }
            Console.Clear();
            Console.WriteLine("Connected");
        }

        // Encrypt a file. 
        private static void _AddEncryption(string FileName)
        {
            File.Encrypt(FileName);
        }

        // Decrypt a file. 
        private static void _RemoveEncryption(string FileName)
        {
            File.Decrypt(FileName);
        }
        
    }
}
