using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppRipassoVerificaMatriciFile
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string file_input = @"..\..\..\dati.txt";
            string file_output = @"..\..\..\output1.txt";

            int sum = 0, count = 0;
            using(StreamReader sr = new StreamReader(file_input))
            {
                while (!sr.EndOfStream)
                {
                    int num = int.Parse(sr.ReadLine());
                    sum += num;
                    count++;
                }

                sr.Close();
            }

            double media = sum / count;
            
            using(StreamReader sr = new StreamReader(file_input))
            using(StreamWriter sw = new StreamWriter(file_output))
            {
                while (!sr.EndOfStream)
                {
                    int num = int.Parse(sr.ReadLine());
                    if (num <= media) sw.WriteLine(num);
                }

                sw.Close();
            }
        }
    }
}
