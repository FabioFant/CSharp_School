using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppMatriciVoti
{
    internal class Program
    {
        //Init
        static int IntInput(string messaggio, int min, int max)
        {
            int num;
            while (true)
            {
                Console.Write(messaggio);
                if (!int.TryParse(Console.ReadLine(), out num))
                    continue;
                if (num < min || num > max)
                    continue;
                break;
            }
            return num;
        }

        static void InitStringVector(ref string[] vector, string messaggio)
        {
            for (int i = 0; i < vector.Length; i++)
            {
                Console.Write($"{messaggio} {i + 1}: ");
                vector[i] = Console.ReadLine();
            }
        }

        static void InitIntMatrix(ref int[,] matrix, string[] vectX, string[] vectY)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Console.WriteLine(vectX[i] + ": ");
                for (int j = 0; j < matrix.GetLength(1); j++)
                    matrix[i, j] = IntInput(vectY[j] + ": ", 0, 10);
            }
        }
        //1°
        static double MatrixAverage(int[,] matrix)
        {
            int sum = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0;j < matrix.GetLength(1); j++)
                    sum += matrix[i, j];

            double media = sum / matrix.Length;

            return media;
        }

        static void ClassAveragesMarks(int[,] T1, int[,] T2)
        {
            double media1 = MatrixAverage(T1);
            double media2 = MatrixAverage(T2);

            Console.WriteLine($"Media 1° Quadrimaestre: {media1}.");
            Console.WriteLine($"Media 2° Quadrimaestre: {media2}.");
        }
        //2°
        static void BestStudent(int[,] marks, string[] names)
        {
            int sum = 0, max_index = 0;
            double max_media = 0;

            for(int i = 0; i < marks.GetLength(0); i++)
            {
                for(int j = 0; j < marks.GetLength(1); j++)
                    sum += marks[i, j];

                double media = sum / marks.GetLength(1);

                if (max_media < media)
                {
                    max_media = media;
                    max_index = i;
                }
                    
                sum = 0;
            }

            Console.WriteLine($"Studente con media più alta: {names[max_index]}");
        }
        //3° e 4°
        static string InputStringFromVector(string[] vector, string messaggio)
        {
            string value;
            while (true)
            {
                Console.Write(messaggio);
                value = Console.ReadLine();
                if (!vector.Contains(value))
                {
                    Console.WriteLine("Input Errato! Valore non esistente!");
                    continue;
                }
                break;
            }

            return value;
        }
        //3°
        static void InsufficientMarks(string student, int[,] marks, string[] names, string[] subjects)
        {
            int index = 0;
            for (int i = 0; i < names.Length; i++)
                if (names[i] == student)
                {
                    index = i;
                    break;
                }

            Console.WriteLine("Voti insufficienti:");
            for (int i = 0; i < marks.GetLength(1); i++)
                if (marks[index, i] < 6)
                    Console.WriteLine($"{subjects[i]}, {marks[index, i]}");
        }
        //4°
        static void AverageSubject(string subject, int[,]T1, int[,] T2, string[] subjects, int n_students)
        {
            //Trovo l'indice in cui si trova la materia
            int index = 0;
            for (int i = 0; i < subjects.Length; i++)
                if (subjects[i] == subject)
                {
                    index = i;
                    break;
                }
            //Calcolo la somma dei valori
            double media1 = 0.0, media2 = 0.0;
            for (int i = 0; i < n_students; i++)
            {
                media1 += T1[i, index];
                media2 += T2[i, index];
            }
            //Divido e ottengo la media
            media1 /= n_students;
            media2 /= n_students;
            //Stampo
            Console.WriteLine($"Media 1° Quadrimaestre per {subject}: {media1}");
            Console.WriteLine($"Media 2° Quadrimaestre per {subject}: {media2}");
        }

        static void Main(string[] args)
        {
            //Inizializzazione di N e M
            int N, M;
            N = IntInput("Inserisci il numero di Alunni: ", 1, 9999);
            M = IntInput("Inserisci il numero di Materie: ", 1, 9999);
            Console.WriteLine();

            //Creazione dei vettori degli alunni e materie
            string[] Nomi = new string[N];
            string[] Materie = new string[M];

            //Inizializzazione del vettore dei Nomi
            Console.WriteLine("Inserisci i nomi degli Alunni");
            InitStringVector(ref Nomi, "Alunno");
            Console.WriteLine();

            //Inizializzazione del vettore delle Materie
            Console.WriteLine("Inserisci le Materie");
            InitStringVector(ref Materie, "Materia");
            Console.WriteLine();

            //Creazione delle matrici
            int[,] T1 = new int[N, M];
            int[,] T2 = new int[N, M];

            //Inizializzazione della matrice T1
            Console.WriteLine("Inserimento dei voti del Primo quadrimaestre");
            InitIntMatrix(ref T1, Nomi, Materie);
            Console.WriteLine();

            //Inizializzazione della matrice T2
            Console.WriteLine("Inserimento dei voti del Secondo quadrimaestre");
            InitIntMatrix(ref T2, Nomi, Materie);
            Console.WriteLine();

            //Stampa media della classe nel 1° e 2° Quadrimaestre
            ClassAveragesMarks(T1, T2);
            Console.WriteLine();

            //Stampa dello studente con media più alta nel 1° e 2° Quadrimaestre
            Console.WriteLine("1° Quadrimaestre");
            BestStudent(T1, Nomi);
            Console.WriteLine("2° Quadrimaestre");
            BestStudent(T2, Nomi);
            Console.WriteLine();

            //Stampa voti insufficenti del 1° e 2° Quadrimaestre per uno studente scelto in input
            string student = InputStringFromVector(Nomi, "Inserisci lo studente per cui vedere le insufficenze del 1° Quadrimaestre: ");
            InsufficientMarks(student, T1, Nomi, Materie);
            Console.WriteLine();
                   student = InputStringFromVector(Nomi, "Inserisci lo studente per cui vedere le insufficenze del 2° Quadrimaestre: ");
            InsufficientMarks(student, T2, Nomi, Materie);
            Console.WriteLine();

            //Stampa media di una materia per 1° e 2° Quadrimaestre
            string subject = InputStringFromVector(Materie, "Inserisci la materia per cui trovare la media per 1° e 2° Quadrimaestre: ");
            AverageSubject(subject, T1, T2, Materie, N);
            Console.WriteLine();

            Console.ReadKey();
        }
    }
}


