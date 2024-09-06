//Fabio Fantini 3H 06-02-2024
//Calcolatrice Bitwise con operandi a 8 bit

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace WpfAppCalcBitwise
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Metodi
        static uint BinaryToDecimal(string testo)
        {
            uint num = 0;
            uint count = 0;
            for (int i = testo.Length - 1; i >= 0; i--)
            {//Passo per il numero binario
                if (testo[i] == '1')
                {//Per ogni aggiungo al numero la potenza di 2 corrispondente
                    num += (uint)Math.Pow(2, count);
                }
                count++;
            }
            return num;
        }

        static string DecimalToBinary(uint num)
        {
            string bin = "";
            while (num > 0)
            {//Divido per 2 finchè non arrivo a 0
                if (num % 2 != 0)
                {//Con resto quindi metto 1
                    bin = "1" + bin;
                }
                else
                {//Senza resto quindi metto 0
                    bin = "0" + bin;
                }
                num /= 2;
            }
            return bin;
        }

        private string Not(string bin)
        {
            //Converto e eseguo il NOT
            uint num = BinaryToDecimal(bin);
            uint reversed = ~num;
            uint result = reversed & 255;
            //Converto e aggiungo dei bit se mancano
            bin = DecimalToBinary(result);
            if (bin.Length < 8)
            {
                int starter_lenght = bin.Length;
                for (int i = 0; i < 8 - starter_lenght; i++)
                    bin = "0" + bin;
            }
            return bin;
        }

        static string ShiftLeft(string bin, int inc)
        {
            //Converto e shifto il numero
            uint num = BinaryToDecimal(bin);
            //Se 0 non faccio nulla così evito che il numero si cancelli
            if (num == 0)
                return bin;
            uint shifted = num << inc;
            //Ri-converto e prendo solo le prime otto cifre solo e solo se sforo di 8 cifre
            string init_bin = DecimalToBinary(shifted);
            bin = init_bin.Substring(Math.Max(0, init_bin.Length - 8));
            return bin;
        }

        static string ShiftRight(string bin, int inc)
        {
            //Converto e shifto il numero
            uint num = BinaryToDecimal(bin);
            //Se 0 non faccio nulla così evito che il numero si cancelli
            if (num == 0)
                return bin;
            uint shifted = num >> inc;
            //Ri-converto
            bin = DecimalToBinary(shifted);
            return bin;
        }
        #endregion

        #region Operando 1
        private void btn_Zero1_Click(object sender, RoutedEventArgs e)
        {
            //Mette uno 0 se non è pieno
            if (txt_Operando1.Text.Length < 8)
                txt_Operando1.Text += "0";
        }

        private void btn_Uno1_Click(object sender, RoutedEventArgs e)
        {
            //Mette un 1 se non è pieno
            if (txt_Operando1.Text.Length < 8)
                txt_Operando1.Text += "1";
        }

        private void btn_Not1_Click(object sender, RoutedEventArgs e)
        {
            //Faccio il NOT se c'è un numero
            if (txt_Operando1.Text.Length > 0)
            {
                txt_Operando1.Text = Not(txt_Operando1.Text);
            }
        }

        private void btn_ShiftLeft1_Click(object sender, RoutedEventArgs e)
        {
            //Converto la stringa in int del numero dello shift left
            int inc;
            bool valido = int.TryParse(txt_ValShift1.Text, out inc);
            //Controllo che sia positivo
            if (inc <= 0 || inc > 8)
                valido = false;
            //Se il numero è valido procedo
            if (valido)
            {
                txt_Operando1.Text = ShiftLeft(txt_Operando1.Text, inc);
            }
            else
            {//Se il testo non è valido lo resetto
                txt_ValShift1.Text = "";
            }
        }

        private void btn_ShiftRight1_Click(object sender, RoutedEventArgs e)
        {
            //Converto la stringa in int del numero dello shift left
            int inc;
            bool valido = int.TryParse(txt_ValShift1.Text, out inc);
            //Controllo che sia positivo
            if (inc <= 0 || inc > 8)
                valido = false;
            //Se il numero è valido procedo
            if (valido)
            {
                txt_Operando1.Text = ShiftRight(txt_Operando1.Text, inc);
            }
            else
            {//Se il testo non è valido lo resetto
                txt_ValShift1.Text = "";
            }
        }

        private void btn_Clear1_Click(object sender, RoutedEventArgs e)
        {
            txt_Operando1.Text = "";
        }
        #endregion

        #region Operando 2
        private void btn_Zero2_Click(object sender, RoutedEventArgs e)
        {
            //Mette uno 0 se non è pieno
            if (txt_Operando2.Text.Length < 8)
                txt_Operando2.Text += "0";
        }

        private void btn_Uno2_Click(object sender, RoutedEventArgs e)
        {
            //Mette un 1 se non è pieno
            if (txt_Operando2.Text.Length < 8)
                txt_Operando2.Text += "1";
        }

        private void btn_Not2_Click(object sender, RoutedEventArgs e)
        {
            //Faccio il NOT se c'è un numero
            if (txt_Operando2.Text.Length > 0)
            {
                txt_Operando2.Text = Not(txt_Operando2.Text);
            }
        }

        private void btn_ShiftLeft2_Click(object sender, RoutedEventArgs e)
        {
            //Converto la stringa in int del numero dello shift left
            int inc;
            bool valido = int.TryParse(txt_ValShift2.Text, out inc);
            //Controllo che sia positivo
            if (inc <= 0 || inc > 8)
                valido = false;
            //Se il numero è valido procedo
            if (valido)
            {
                txt_Operando2.Text = ShiftLeft(txt_Operando2.Text, inc);
            }
            else
            {//Se il testo non è valido lo resetto
                txt_ValShift2.Text = "";
            }
        }

        private void btn_ShiftRight2_Click(object sender, RoutedEventArgs e)
        {
            //Converto la stringa in int del numero dello shift left
            int inc;
            bool valido = int.TryParse(txt_ValShift2.Text, out inc);
            //Controllo che sia positivo
            if (inc <= 0 || inc > 8)
                valido = false;
            //Se il numero è valido procedo
            if (valido)
            {
                txt_Operando2.Text = ShiftRight(txt_Operando2.Text, inc);
            }
            else
            {//Se il testo non è valido lo resetto
                txt_ValShift2.Text = "";
            }
        }

        private void btn_Clear2_Click(object sender, RoutedEventArgs e)
        {
            txt_Operando2.Text = "";
        }
        #endregion

        #region Risultato
        private void btn_And_Click(object sender, RoutedEventArgs e)
        {
            if (txt_Operando1.Text.Length > 0 && txt_Operando2.Text.Length > 0)
            {
                //Converto i numeri e effettuo l'AND
                uint num1 = BinaryToDecimal(txt_Operando1.Text);
                uint num2 = BinaryToDecimal(txt_Operando2.Text);
                uint ris = num1 & num2;
                //Creo il numero binario e lo riempo di cifre quante il numero con più cifre per creare una migliore interfaccia
                string bin = DecimalToBinary(ris);
                int max_lenght = Math.Max(txt_Operando1.Text.Length, txt_Operando2.Text.Length);
                int start_lenght = bin.Length;
                for (int i = 0; i < max_lenght - start_lenght; i++)
                {
                    bin = "0" + bin;
                }
                //Mostro il risultato
                txt_Risultato.Text = bin;
            }
        }

        private void btn_Or_Click(object sender, RoutedEventArgs e)
        {
            if (txt_Operando1.Text.Length > 0 && txt_Operando2.Text.Length > 0)
            {
                //Converto i numeri e effettuo l'OR
                uint num1 = BinaryToDecimal(txt_Operando1.Text);
                uint num2 = BinaryToDecimal(txt_Operando2.Text);
                uint ris = num1 | num2;
                //Creo il numero binario e lo riempo di cifre quante il numero con più cifre per creare una migliore interfaccia
                string bin = DecimalToBinary(ris);
                int max_lenght = Math.Max(txt_Operando1.Text.Length, txt_Operando2.Text.Length);
                int start_lenght = bin.Length;
                for (int i = 0; i < max_lenght - start_lenght; i++)
                {
                    bin = "0" + bin;
                }
                //Mostro il risultato
                txt_Risultato.Text = bin;
            }
        }

        private void btn_Xor_Click(object sender, RoutedEventArgs e)
        {
            if (txt_Operando1.Text.Length > 0 && txt_Operando2.Text.Length > 0)
            {
                //Converto i numeri e effettuo l'OR
                uint num1 = BinaryToDecimal(txt_Operando1.Text);
                uint num2 = BinaryToDecimal(txt_Operando2.Text);
                uint ris = num1 ^ num2;
                //Creo il numero binario e lo riempo di cifre quante il numero con più cifre per creare una migliore interfaccia
                string bin = DecimalToBinary(ris);
                int max_lenght = Math.Max(txt_Operando1.Text.Length, txt_Operando2.Text.Length);
                int start_lenght = bin.Length;
                for (int i = 0; i < max_lenght - start_lenght; i++)
                {
                    bin = "0" + bin;
                }
                //Mostro il risultato
                txt_Risultato.Text = bin;
            }
        }
        #endregion
    }
}