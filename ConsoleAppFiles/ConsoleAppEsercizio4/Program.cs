using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEsercizio4
{
    internal class Program
    {
        static string fi = @"..\..\..\dati.txt";

        static void Main(string[] args)
        {
            Dictionary<int, int> freq = new Dictionary<int, int>();

            using(StreamReader sr = new StreamReader(fi))
            {
                while(!sr.EndOfStream)
                {
                    int num = int.Parse(sr.ReadLine());
                    if (freq.ContainsKey(num))
                    {
                        freq[num]++;
                    }
                    else
                    {
                        freq[num] = 1;
                    }
                }
                sr.Close();
            }
            using(StreamWriter sw = new StreamWriter("dati_out.txt"))
            {
                for(int i = 0; i < freq.Count; i++)
                {
                    if (freq.ContainsKey(i))
                    {
                        sw.WriteLine(i + ", " + freq[i]);
                    }
                }
                sw.Close();
            }
        }
    }
}
