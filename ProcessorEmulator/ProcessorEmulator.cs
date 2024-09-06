namespace ProcessorEmulator
{
    internal class Program
    {
        static ushort[] regs = new ushort[8];
        static ushort PC = 0;
        static ushort[] RAM = new ushort[32768];
        static Stack<ushort> stack = new Stack<ushort>();

        static void ResetMachine()
        {
            PC = 0;
            stack.Clear();
            for (int i = 0; i < regs.Length; ++i)
                regs[i] = 0;

            for (int i = 0; i < RAM.Length; ++i)
                RAM[i] = 0;
        }

        static ushort FetchByte()
        {
            return RAM[PC++];
        }

        static int GetRegister(ref ushort op, bool controllo)
        {
            switch (op)
            {
                case 32768:
                    op = regs[0];
                    return 0;
                case 32769:
                    op = regs[1];
                    return 1;
                case 32770:
                    op = regs[2];
                    return 2;
                case 32771:
                    op = regs[3];
                    return 3;
                case 32772:
                    op = regs[4];
                    return 4;
                case 32773:
                    op = regs[5];
                    return 5;
                case 32774:
                    op = regs[6];
                    return 6;
                case 32775:
                    op = regs[7];
                    return 7;
            }
            //Se c'è il controllo e non ho preso nessun valore da nessun registro interrpo.
            if (controllo)
                throw new Exception($"Il primo operando non è un registro, errore all'indirizzo {PC}");

            return 8;
        }

        static void OverflowCut(ref ushort value)
        {
            if (value > 32767)
                value %= 32767;
        }

        static void RunMachine()
        {
            while (true)
            {
                // Instruction Fetch
                ushort instr_addr = PC;
                ushort instr_code = FetchByte();
                ushort op1, op2, op3;

                int regIndex = 0;

                // Instruction Decode + Execute
                switch (instr_code)
                {
                    case 0: // halt
                        throw new Exception($"Istruzione di halt all'indirizzo {instr_addr}");
                        break;

                    case 1: // set op1, op2
                        op1 = FetchByte();
                        op2 = FetchByte();

                        regIndex = GetRegister(ref op1, true);
                        GetRegister(ref op2, false);

                        regs[regIndex] = op2;

                        break;

                    case 2: // push op1
                        op1 = FetchByte();

                        GetRegister(ref op1, false);
                        stack.Push(op1);

                        break;

                    case 3: // pop
                        op1 = FetchByte();

                        regIndex = GetRegister(ref op1, true);
                        if (stack.Count == 0)
                            throw new Exception($"Impossibile effettuare pop sullo stack vuoto, errore all'indirizzo {instr_addr}");

                        regs[regIndex] = stack.Pop();

                        break;

                    case 4: // eq op1, op2, op3
                        op1 = FetchByte();
                        op2 = FetchByte();
                        op3 = FetchByte();

                        regIndex = GetRegister(ref op1, true);
                        GetRegister(ref op2, false);
                        GetRegister(ref op3, false);

                        regs[regIndex] = op2 == op3 ? (ushort)1 : (ushort)0;

                        break;

                    case 5: // gt op1, op2, op3
                        op1 = FetchByte();
                        op2 = FetchByte();
                        op3 = FetchByte();

                        regIndex = GetRegister(ref op1, true);
                        GetRegister(ref op2, false);
                        GetRegister(ref op3, false);

                        regs[regIndex] = op2 > op3 ? (ushort)1 : (ushort)0;

                        break;

                    case 6: // jump op1
                        op1 = FetchByte();

                        GetRegister(ref op1, false);
                        PC = op1;

                        break;

                    case 7: // jt op1, op2
                        op1 = FetchByte();
                        op2 = FetchByte();

                        GetRegister(ref op1, false);
                        GetRegister(ref op2, false);

                        PC = op1 != 0 ? op2 : PC;

                        break;

                    case 8: // jf op1, op2
                        op1 = FetchByte();
                        op2 = FetchByte();

                        GetRegister(ref op1, false);
                        GetRegister(ref op2, false);

                        PC = op1 == 0 ? op2 : PC;

                        break;

                    case 9: //add op1, op2, op3
                        op1 = FetchByte();
                        op2 = FetchByte();
                        op3 = FetchByte();

                        regIndex = GetRegister(ref op1, true);
                        GetRegister(ref op2, false);
                        GetRegister(ref op3, false);

                        regs[regIndex] = (ushort)(op2 + op3);

                        OverflowCut(ref regs[regIndex]);

                        break;

                    case 10: //mult op1, op2, op3
                        op1 = FetchByte();
                        op2 = FetchByte();
                        op3 = FetchByte();

                        regIndex = GetRegister(ref op1, true);
                        GetRegister(ref op2, false);
                        GetRegister(ref op3, false);

                        regs[regIndex] = (ushort)(op2 * op3);

                        OverflowCut(ref regs[regIndex]);

                        break;

                    case 11: //mod op1, op2, op3
                        op1 = FetchByte();
                        op2 = FetchByte();
                        op3 = FetchByte();

                        regIndex = GetRegister(ref op1, true);
                        GetRegister(ref op2, false);
                        GetRegister(ref op3, false);

                        if (op3 == 0)
                            throw new DivideByZeroException($"Errore all'indirizzo {instr_addr}");

                        regs[regIndex] = (ushort)(op2 % op3);

                        break;
                    
                    case 12: //and op1, op2, op3
                        op1 = FetchByte();
                        op2 = FetchByte();
                        op3 = FetchByte();

                        regIndex = GetRegister(ref op1, true);
                        GetRegister(ref op2, false);
                        GetRegister(ref op3, false);

                        regs[regIndex] = (ushort)(op2 & op3);

                        OverflowCut(ref regs[regIndex]);

                        break;
                    
                    case 13: //or op1, op2, op3
                        op1 = FetchByte();
                        op2 = FetchByte();
                        op3 = FetchByte();

                        regIndex = GetRegister(ref op1, true);
                        GetRegister(ref op2, false);
                        GetRegister(ref op3, false);

                        regs[regIndex] = (ushort)(op2 | op3);

                        OverflowCut(ref regs[regIndex]);

                        break;

                    case 14: //not op1, op2
                        op1 = FetchByte();
                        op2 = FetchByte();

                        regIndex = GetRegister(ref op1, true);
                        GetRegister(ref op2, false);

                        regs[regIndex] = (ushort)~op2;

                        OverflowCut(ref regs[regIndex]);

                        break;

                    case 15: //rmen op1, op2
                        op1 = FetchByte();
                        op2 = FetchByte();

                        regIndex = GetRegister(ref op1, true);
                        GetRegister(ref op2, false);

                        regs[regIndex] = RAM[op2];

                        OverflowCut(ref regs[regIndex]);

                        break;

                    case 16: //wmen op1, op2
                        op1 = FetchByte();
                        op2 = FetchByte();

                        GetRegister(ref op1, false);
                        GetRegister(ref op2, false);

                        RAM[op1] = op2;

                        OverflowCut(ref regs[regIndex]);

                        break;

                    case 17: //call op1
                        op1 = FetchByte();

                        GetRegister(ref op1, false);

                        stack.Push(PC);
                        PC = op1;

                        return;

                    case 18: //ret
                        if (stack.Count == 0)
                            throw new Exception($"Impossibile effettuare pop sullo stack vuoto, errore all'indirizzo {instr_addr}");

                        PC = stack.Pop();

                        break;

                    case 19: // out op1
                        op1 = FetchByte();

                        GetRegister(ref op1, false);
                        Console.Write((char)op1);

                        break;
                    case 20: // in op1
                        op1 = FetchByte();

                        regIndex = GetRegister(ref op1, true);

                        ConsoleKeyInfo key = Console.ReadKey();
                        regs[regIndex] = key.KeyChar;

                        break;

                    case 21: // nop
                        break;

                    default:
                        throw new Exception($"Istruzione {instr_code} non valida all'indirizzo {instr_addr}");
                }
            }
        }

        static void LoadRAM(string file_name)
        {
            using (StreamReader sr = new StreamReader(file_name))
            {
                for (int i = 0; !sr.EndOfStream && i < RAM.Length; ++i )
                {
                    if (!ushort.TryParse(sr.ReadLine(), out RAM[i]))
                    {
                        throw new Exception($"Error parsing file '{file_name}' at line {i + 1}");
                    }
                }
                sr.Close();
            }
        }

        static void Main(string[] args)
        {
            ResetMachine();

            LoadRAM(@"..\..\..\challenge.txt");

            try
            {
                RunMachine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
