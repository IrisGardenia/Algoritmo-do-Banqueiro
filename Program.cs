using System;

using System.Threading;

class Program
{
    const int NUMBER_OF_CUSTOMERS = 5;
    const int NUMBER_OF_RESOURCES = 3;

    static int[] available = new int[NUMBER_OF_RESOURCES];

    static int[,] maximum = new int[NUMBER_OF_CUSTOMERS, NUMBER_OF_RESOURCES]
    {
        {7,5,3},
        {3,2,2},
        {9,0,2},
        {2,2,2},
        {4,3,3}
    };

    static int[,] allocation = new int[NUMBER_OF_CUSTOMERS, NUMBER_OF_RESOURCES];
    static int[,] need = new int[NUMBER_OF_CUSTOMERS, NUMBER_OF_RESOURCES];

    static object locker = new object();

    static void Main(string[] args)
    {
        if (args.Length != NUMBER_OF_RESOURCES)
        {
            Console.WriteLine("Uso: dotnet run 10 5 7");
            return;
        }

        for (int i = 0; i < NUMBER_OF_RESOURCES; i++)
            available[i] = int.Parse(args[i]);

        // need = maximum - allocation
        for (int i = 0; i < NUMBER_OF_CUSTOMERS; i++)
        {
            for (int j = 0; j < NUMBER_OF_RESOURCES; j++)
            {
                need[i, j] = maximum[i, j];
                allocation[i, j] = 0;
            }
        }

        PrintState();

        Thread[] clients = new Thread[NUMBER_OF_CUSTOMERS];

        for (int i = 0; i < NUMBER_OF_CUSTOMERS; i++)
        {
            int id = i;
            clients[i] = new Thread(() => Customer(id));
            clients[i].Start();
        }

        foreach (Thread t in clients)
            t.Join();
    }

    static void Customer(int id)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());

        for (int loop = 0; loop < 10; loop++)
        {
            int[] request = new int[NUMBER_OF_RESOURCES];

            lock (locker)
            {
                for (int j = 0; j < NUMBER_OF_RESOURCES; j++)
                    request[j] = rnd.Next(need[id, j] + 1);
            }

            RequestResources(id, request);

            Thread.Sleep(rnd.Next(500, 1500));

            int[] release = new int[NUMBER_OF_RESOURCES];

            lock (locker)
            {
                for (int j = 0; j < NUMBER_OF_RESOURCES; j++)
                    release[j] = rnd.Next(allocation[id, j] + 1);
            }

            ReleaseResources(id, release);

            Thread.Sleep(rnd.Next(500, 1500));
        }
    }

    static int RequestResources(int customer, int[] request)
    {
        lock (locker)
        {
            // request <= need
            for (int j = 0; j < NUMBER_OF_RESOURCES; j++)
            {
                if (request[j] > need[customer, j])
                {
                    Console.WriteLine($"Cliente {customer}: pedido inválido.");
                    return -1;
                }
            }

            // request <= available
            for (int j = 0; j < NUMBER_OF_RESOURCES; j++)
            {
                if (request[j] > available[j])
                {
                    Console.WriteLine($"Cliente {customer}: recursos insuficientes.");
                    return -1;
                }
            }

            // simula alocação
            for (int j = 0; j < NUMBER_OF_RESOURCES; j++)
            {
                available[j] -= request[j];
                allocation[customer, j] += request[j];
                need[customer, j] -= request[j];
            }

            // testa segurança
            if (IsSafe())
            {
                Console.WriteLine($"Cliente {customer} pediu [{request[0]}, {request[1]}, {request[2]}] -> APROVADO");
                PrintState();
                return 0;
            }
            else
            {
                // desfaz
                for (int j = 0; j < NUMBER_OF_RESOURCES; j++)
                {
                    available[j] += request[j];
                    allocation[customer, j] -= request[j];
                    need[customer, j] += request[j];
                }

                Console.WriteLine($"Cliente {customer} pediu [{request[0]}, {request[1]}, {request[2]}] -> NEGADO (inseguro)");
                return -1;
            }
        }
    }

    static int ReleaseResources(int customer, int[] release)
    {
        lock (locker)
        {
            for (int j = 0; j < NUMBER_OF_RESOURCES; j++)
            {
                if (release[j] > allocation[customer, j])
                    release[j] = allocation[customer, j];

                allocation[customer, j] -= release[j];
                available[j] += release[j];
                need[customer, j] += release[j];
            }

            Console.WriteLine($"Cliente {customer} liberou [{release[0]}, {release[1]}, {release[2]}]");
            PrintState();

            return 0;
        }
    }

    static bool IsSafe()
    {
        int[] work = new int[NUMBER_OF_RESOURCES];
        bool[] finish = new bool[NUMBER_OF_CUSTOMERS];

        for (int j = 0; j < NUMBER_OF_RESOURCES; j++)
            work[j] = available[j];

        bool found;

        do
        {
            found = false;

            for (int i = 0; i < NUMBER_OF_CUSTOMERS; i++)
            {
                if (!finish[i])
                {
                    bool possible = true;

                    for (int j = 0; j < NUMBER_OF_RESOURCES; j++)
                    {
                        if (need[i, j] > work[j])
                        {
                            possible = false;
                            break;
                        }
                    }

                    if (possible)
                    {
                        for (int j = 0; j < NUMBER_OF_RESOURCES; j++)
                            work[j] += allocation[i, j];

                        finish[i] = true;
                        found = true;
                    }
                }
            }

        } while (found);

        for (int i = 0; i < NUMBER_OF_CUSTOMERS; i++)
        {
            if (!finish[i])
                return false;
        }

        return true;
    }

    static void PrintState()
    {
        Console.WriteLine("\n--- ESTADO ATUAL ---");

        Console.Write("Available: ");
        for (int j = 0; j < NUMBER_OF_RESOURCES; j++)
            Console.Write(available[j] + " ");
        Console.WriteLine();

        Console.WriteLine("Allocation:");
        for (int i = 0; i < NUMBER_OF_CUSTOMERS; i++)
        {
            for (int j = 0; j < NUMBER_OF_RESOURCES; j++)
                Console.Write(allocation[i, j] + " ");
            Console.WriteLine();
        }

        Console.WriteLine("Need:");
        for (int i = 0; i < NUMBER_OF_CUSTOMERS; i++)
        {
            for (int j = 0; j < NUMBER_OF_RESOURCES; j++)
                Console.Write(need[i, j] + " ");
            Console.WriteLine();
        }

        Console.WriteLine("--------------------\n");
    }
}

