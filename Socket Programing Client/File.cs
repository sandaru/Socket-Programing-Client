using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Socket_Programing_Client
{
    class FileHandler
    {
        private static string _filename =null;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="filename"></param>
        public FileHandler(string filename)
        {
            _filename = filename;
        }
        //create a file
        public void createFile()
        {
            File.Create(_filename).Dispose();
        }
        //Reade file in array with respect to the lines
        public string[] readLines()
        {
            string[] lines = File.ReadAllLines(_filename);
            return lines;
        }
        //Reade whole file in to single line
        public string readAllintoLine()
        {
            String line = string.Empty;
            try
            {
                using (StreamReader sr = new StreamReader(_filename))
                {
                    line = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return line;
        } 
        //Write in to a file
        public void writeLine(string textToWrite)
        {
            using (StreamWriter file = new StreamWriter(_filename))
            {
                file.WriteLine(textToWrite);
            }
        }
        //Append to a line
        public void appendLine(string textToAppend)
        {
            using (StreamWriter file= File.AppendText(_filename))
            {
                file.Write(textToAppend);
            }
        }
        //encrypt the given text 
        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            //compute hash from the bytes of text
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            //get hash result after compute it
            byte[] result = md5.Hash;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }
            return strBuilder.ToString();
        }
    }
}
