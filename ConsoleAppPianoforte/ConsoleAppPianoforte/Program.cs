//Fabio Fantini 3H 2023/11/21
//Pianoforte semplice
//Esercizio 2

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppPianoforteSemplice
{
    internal class Program
    {
        // Indice -->               0    1    2    3    4    5    6
        // Nota -->                 DO   RE   MI   FA   SOL  LA   SI
        static char[] keyboard = { 'a', 's', 'd', 'f', 'g', 'h', 'j' };
        static int[] sound_freq = { 262, 294, 330, 349, 392, 440, 494 };

        static void Main(string[] args)
        {
            Console.Title = "Pianoforte semplice by Fabio Fantini.";
            //Lista Comandi
            Console.WriteLine("             COMANDI            ");
            Console.WriteLine("DO   RE   MI   FA   SOL  LA   SI");
            Console.WriteLine("A    S    D    F     G   H    J ");
            Console.WriteLine("\n     Clicca X per terminare.    ");

            ConsoleKeyInfo key;
            do
            {//Lettura Tasto
                key = Console.ReadKey(true);
                switch (key.KeyChar)
                {
                    case 'a':
                        Console.Beep(sound_freq[0], 200);
                        break;
                    case 's':
                        Console.Beep(sound_freq[1], 200);
                        break;
                    case 'd':
                        Console.Beep(sound_freq[2], 200);
                        break;
                    case 'f':
                        Console.Beep(sound_freq[3], 200);
                        break;
                    case 'g':
                        Console.Beep(sound_freq[4], 200);
                        break;
                    case 'h':
                        Console.Beep(sound_freq[5], 200);
                        break;
                    case 'j':
                        Console.Beep(sound_freq[6], 200);
                        break;
                }
            } while (key.KeyChar != 'x'); //Termina se si preme X
        }
    }
}


