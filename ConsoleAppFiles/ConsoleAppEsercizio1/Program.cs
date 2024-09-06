using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppFiles
{
    internal class Program
    {
        static string fi = @"..\..\..\dati.txt";

        static void Main(string[] args)
        {
            int sum = 0, count = 0;
            using (StreamReader sr = new StreamReader(fi))
            {
                while (!sr.EndOfStream)
                {
                    int num;
                    if (int.TryParse(sr.ReadLine(), out num))
                    {
                        sum += num;
                        count++;
                    }
                }

                sr.Close();
            }

            int media = (int)(0.5 + (double)(sum / count));

            using(StreamReader sr = new StreamReader(fi))
            using(StreamWriter sw = new StreamWriter("dati_out.txt"))
            {
                while(!sr.EndOfStream)
                {
                    int num;
                    if (int.TryParse(sr.ReadLine(), out num))
                        if(num  <= media)
                            sw.WriteLine(num);
                }
                sr.Close();
                sw.Close();
            }
        }
    }
}
