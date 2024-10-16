// Fabio Fantini 4H 15/10/24
// Classe ArrayList e LinkedList

using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace LinkedList
{

    class LinkedList
    {
        private class Node
        {
            // Essendo "private" l'intera class, posso usare "public" per i campi
            public int value;
            public Node next;
        }

        private Node head;
        public LinkedList()
        {
            head = null;
        }

        public int Count 
        { 
            get 
            {
                if (head == null)
                    return 0;
                else
                { // Se esiste un head, conto i nodi
                    int count = 1;
                    Node curr = head;

                    while (curr.next != null)
                    {
                        curr = curr.next;
                        count++;
                    }

                    return count;
                }
            } 
        }
        public int this[int idx] 
        { 
            get 
            {
                if (idx < 0)
                    throw new IndexOutOfRangeException();

                // Nessun nodo
                if (head == null)
                    throw new NullReferenceException();

                else if(idx == 0)
                    return head.value;

                // Scorri i nodi e prendo il valore
                else
                {
                    int pos = 0;
                    Node curr = head;

                    while (curr.next != null)
                    {
                        curr = curr.next;
                        pos++;

                        if (pos == idx)
                            return curr.value;
                    }
                    
                    throw new IndexOutOfRangeException();
                }
            }
            set
            {
                if (idx < 0)
                    throw new IndexOutOfRangeException();

                // Nessun nodo
                if (head == null)
                    throw new NullReferenceException();

                else if (idx == 0)
                {
                    head.value = value;
                    return;
                }

                // Scorri i nodi e prendo il valore
                else
                {
                    int pos = 0;
                    Node curr = head;

                    while (curr.next != null)
                    {
                        curr = curr.next;
                        pos++;

                        if (pos == idx)
                        {
                            curr.value = value;
                            return;
                        }
                    }

                    throw new IndexOutOfRangeException();
                }
            }
        }

        public void Add(int value)
        {
            // Crea il nuovo nodo
            Node n = new Node();
            n.value = value;
            n.next = null;

            if (head == null)  // lista vuota?
            {
                head = n;
            }
            else
            {
                Node prev = head;
                while (prev.next != null)
                    prev = prev.next;

                prev.next = n;
            }
        }
        public void RemoveAt(int idx)
        {
            if (idx < 0)
                throw new IndexOutOfRangeException();

            // Nessun nodo
            if (head == null)
                throw new NullReferenceException();

            // Se [0], head diventa [1]
            else if (idx == 0)
                head = head.next;

            // Scorri i nodi, il next del nodo idx-1 = idx+1
            else
            {
                int posCurr = 0;
                Node curr = head;
                Node prev = null;

                while (curr.next != null)
                {
                    posCurr++;
                    prev = curr;
                    curr = curr.next;

                    if (posCurr == idx)
                        prev.next = curr.next;
                }

                throw new IndexOutOfRangeException();
            }
        }
        public void RemoveValue(int value)
        {
            // Nessun nodo
            if (head == null)
                throw new NullReferenceException();

            // Se [0], head diventa [1]
            else if (value == head.value)
                head = head.next;

            // Scorri i nodi, trovato il primo valore: next del nodo idx-1 = idx+1
            else
            {
                int posCurr = 0;
                Node curr = head;
                Node prev = null;

                while (curr.next != null)
                {
                    posCurr++;
                    prev = curr;
                    curr = curr.next;

                    if (curr.value == value)
                        prev.next = curr.next;
                }
            }
        }
        public int Search(int value)
        {
            int i = 0;
            for (Node curr = head; curr != null; curr = curr.next, ++i)
            {
                if (curr.value == value)
                    return i;
            }

            return -1;
        }
    }
    class ArrayList
    {
        private int[] data;
        private int count;
        public ArrayList(int capacity)
        {
            data = new int[capacity];
            count = 0;
        }

        public int Count { get { return count; } }
        public int this[int idx]
        {
            get
            {
                if (idx < 0 || count < idx)
                    throw new IndexOutOfRangeException();
                return data[idx];
            }
            set
            {
                if (idx < 0 || count < idx)
                    throw new IndexOutOfRangeException();
                data[idx] = value;
            }
        }
        public void Add(int value)
        {
            if (count == data.Length)
                Realloc(2 * data.Length);

            data[count++] = value;
        }
        public void RemoveAt(int idx)
        {
            if (idx < 0 || count < idx)
                throw new IndexOutOfRangeException();
            ShiftLeft(idx);
        }
        public void RemoveValue(int value)
        {
            int idx = Search(value);
            if (idx >= 0)
                RemoveAt(idx);
        }
        public int Search(int value)
        {
            for (int i = 0; i < count; ++i)
            {
                if (data[i] == value)
                    return i;
            }

            return -1;
        }

        // i metodi che seguono sono stati presi da https://classroom.google.com/c/NjI0MDAwODEyNDMx/m/NjcyOTQ3NjgwMjM5/details
        private void Realloc(int new_capacity)
        {
            int[] new_data = new int[new_capacity];
            int idx_max = Math.Min(data.Length, new_data.Length);
            for (int i = 0; i < idx_max; ++i)
                new_data[i] = data[i];
            data = new_data;
        }
        private void ShiftRight(int idx)
        {
            if (idx < 0 || count < idx)
                throw new IndexOutOfRangeException();
            if (count == data.Length)
                Realloc(2 * data.Length);
            int move_count = count - idx; // numero di elementi da spostare
            for (int k = move_count; k > 0; --k)
                data[idx + k] = data[idx + k - 1];
            ++count;
        }
        private void ShiftLeft(int idx)
        {
            if (idx < 0 || count <= idx)
                throw new IndexOutOfRangeException();
            int move_count = count - idx - 1; // numero di elementi da spostare
            for (int k = 0; k < move_count; ++k)
                data[idx + k] = data[idx + k + 1];
            --count;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            LinkedList list = new LinkedList();

            list.Add(0);
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);

            Console.WriteLine("Get");
            Console.WriteLine("Lista: {0} {1} {2} {3} {4} {5}\n", list[0], list[1], list[2], list[3], list[4], list[5]);

            Console.WriteLine("Set ([1] = '1234')");
            list[1] = 1234;
            Console.WriteLine("Lista: {0} {1} {2} {3} {4} {5}\n", list[0], list[1], list[2], list[3], list[4], list[5]);

            Console.WriteLine("Count: 6?");
            Console.WriteLine("Count = {0}\n", list.Count);

            Console.WriteLine("RemoveAt/Value\n");
            list = null;
            list = new LinkedList();
            Console.WriteLine("Partenza: Count --> 1");
            list.Add(5);
            list.RemoveAt(0);
            Console.WriteLine("RemoveAt Count: {0}", list.Count);
            list.Add(5);
            list.RemoveValue(5);
            Console.WriteLine("RemoveAt Count: {0}\n", list.Count);

            // TODO : finire controlli su RemoveAt/Value
        }
    }
}