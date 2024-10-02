// Fabio Fantini 4H 2024-10-02
// Classe SymmetricCrypt

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
        // Metodi statici per il crypt diretto con chiave e key di tipo byte e string
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

            byte[] DATA_byte = SymmetricCrypt.Crypt(data_byte, key_byte);
            string DATA_string = Encoding.Unicode.GetString(DATA_byte);
            return DATA_string;
        }

        // Chiave memorizzata in byte
        private byte[] _key;

        // Costruttore con chiave di tipo byte e string
        public SymmetricCrypt(byte[] key) 
        { 
            _key = key;
        }
        public SymmetricCrypt(string key)
        {
            _key = Encoding.Unicode.GetBytes(key);
        }

        // Metodi di crypt di tipo byte e string con la chiave memorizzata dal costruttore
        public byte[] Crypt(byte[] data)
        {
            return SymmetricCrypt.Crypt(data, _key);
        }
        public string Crypt(string msg) 
        {
            string key_string = Encoding.Unicode.GetString(_key);
            return SymmetricCrypt.Crypt(msg, key_string);
        }
    }
}
