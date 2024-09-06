//Fabio Fantini 3H 2023/11/21
//Pianoforte semitoni
//Esercizio 2++

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
        // Indice -->               0    1    2    3    4    5    6    7    8    9    10   11
        // Nota -->                 DO   RE   MI   FA   SOL  LA   SI   DO#  MIb  FA#  SOL# SIb  
        static char[] keyboard = { 'a', 's', 'd', 'f', 'g', 'h', 'j', 'w', 'e', 't', 'y', 'u' };
        static int[] sound_freq = { 262, 294, 330, 349, 392, 440, 494, 279, 313, 367, 418, 470 };

        static void Main(string[] args)
        {
            Console.Title = "Pianoforte semplice by Fabio Fantini.";
            //Lista Comandi
            Console.WriteLine("                         COMANDI                          ");
            Console.WriteLine("DO   RE   MI   FA   SOL  LA   SI   DO#  MIb  FA#  SOL# SIb");
            Console.WriteLine("A    S    D    F     G   H    J     W    E    T    Y    U ");
            Console.WriteLine("\n                  Clicca X per terminare.                  ");

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
                    case 'w':
                        Console.Beep(sound_freq[7], 200);
                        break;
                    case 'e':
                        Console.Beep(sound_freq[8], 200);
                        break;
                    case 't':
                        Console.Beep(sound_freq[9], 200);
                        break;
                    case 'y':
                        Console.Beep(sound_freq[10], 200);
                        break;
                    case 'u':
                        Console.Beep(sound_freq[11], 200);
                        break;
                }
            } while (key.KeyChar != 'x'); //Termina se si preme X
        }
    }
}


