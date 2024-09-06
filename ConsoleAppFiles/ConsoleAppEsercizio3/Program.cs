using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEsercizio3
{
    internal class Program
    {
        static string fi = @"..\..\..\dati.txt";

        static void Main(string[] args)
        {
            List<int> values = new List<int>();
            using(StreamReader sr = new StreamReader(fi))
            {
                while (!sr.EndOfStream)
                {
                    values.Add(int.Parse(sr.ReadLine()));
                }
                sr.Close();
            }
            values.Sort();
            using(StreamWriter sw = new StreamWriter("dati_out.txt"))
            {
                for(int i = 0; i < values.Count; i++)
                {
                    sw.WriteLine(values[i]);
                }
                sw.Close();
            }
        }
    }
}
