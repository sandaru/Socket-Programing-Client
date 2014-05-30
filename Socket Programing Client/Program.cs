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
        public static void  Main(string[] args)
        {
            SocketHandler handler = new SocketHandler(20000,"127.0.0.1");
            if (handler.connect())
            {
                handler.sendData("Hello World");
            }
            Console.Write(handler.listen()+"\n");

            SocketHandler handler2 = new SocketHandler(20001,"127.0.0.1");
            if (handler2.connect())
            {
                Console.WriteLine(handler2.listen());
            }
            Console.ReadLine();
            //FileHandler file = new FileHandler("application.txt");
            //file.createFile();

            //file.writeLine("Hello World");
            //file.appendLine("Sandaru Weerasooriya");
            //String[] text = file.readLines();
            //foreach (var item in text)
            //{
            //    Console.WriteLine(item.ToString());
            //}
            //Console.WriteLine(file.readAllintoLine());
            //Console.ReadLine();
        }      
    }
}
