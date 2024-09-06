using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esercizio3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string file_input = @"..\..\..\dati.txt";
            string file_output = @"..\..\..\output3.txt";

            List<int> values = new List<int>();
            using (StreamReader sr = new StreamReader(file_input)) 
            {
                while(!sr.EndOfStream)
                {
                    values.Add(int.Parse(sr.ReadLine()));
                }

                sr.Close();
            }

            values.Sort();

            using(StreamWriter sw = new StreamWriter(file_output))
            {
                for(int i = 0; i < values.Count; i++)
                {
                    sw.WriteLine(values[i]);
                }
            }
        }
    }
}
