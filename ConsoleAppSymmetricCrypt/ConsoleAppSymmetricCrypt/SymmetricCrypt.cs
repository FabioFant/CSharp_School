using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleAppSymmetricCrypt
{
    internal class SymmetricCrypt
    {
        public static byte[] Crypt(byte[] data, byte[] key) 
        {
            byte[] DATA = new byte[data.Length];
            for( int i = 0; i < data.Length; i++ )
            {
                DATA[i] = (byte)((int)data[i] ^ (int)key[i % key.Length]);
            }
            return DATA;
        }
        public static string Crypt(string data, string key)
        {
            byte[] data_byte = Encoding.Unicode.GetBytes(data);
            byte[] key_byte = Encoding.Unicode.GetBytes(key);
            byte[] DATA = new byte[data_byte.Length];
            for (int i = 0; i < data_byte.Length; i++)
            {
                DATA[i] = (byte)((int)data_byte[i] ^ (int)key_byte[i % key_byte.Length]);
            }

            string DATA_string = Encoding.Unicode.GetString(DATA);
            return DATA_string;
        }

        private byte[] _key;

        public SymmetricCrypt(byte[] key) 
        { 
            _key = key;
        }
        public SymmetricCrypt(string key)
        {
            _key = Encoding.Unicode.GetBytes(key);
        }

        public byte[] Crypt(byte[] data)
        {
            return SymmetricCrypt.Crypt(data, _key);
        }
        //public string Crypt(string msg) 
        //{
            //return SymmetricCrypt.Crypt(msg, _key);
        //}
    }
}
