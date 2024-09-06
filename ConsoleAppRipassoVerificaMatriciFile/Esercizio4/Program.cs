using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esercizio4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string file_input = @"..\..\..\dati.txt";
            string file_output = @"..\..\..\output4.txt";

            Dictionary<int, int> values = new Dictionary<int, int>();

            using (StreamReader sr = new StreamReader(file_input)) 
            { 
                while(!sr.EndOfStream)
                {
                    int num = int.Parse(sr.ReadLine());

                    if (values.ContainsKey(num)) values[num]++;
                    else values[num] = 1;
                }

                sr.Close();
            }

            using (StreamWriter sw = new StreamWriter(file_output))
            {
                int count = 0;
                int key = 0;
                while(count < values.Count)
                {
                    if(values.ContainsKey(key))
                    {
                        sw.WriteLine(key + ", " + values[key]);
                        count++;
                    }

                    key++;
                }

                sw.Close();
            }
        }
    }
}
