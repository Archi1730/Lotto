using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraLotto
{
    class Program
    {
        static int kumulacja;
        static int START = 30;
        static Random rng = new Random();

        static void Main(string[] args)
        {
            int pieniądze = START;
            int dzień = 0;
            do
            {
                pieniądze = START;
                dzień = 0;
                ConsoleKey wybór;
                do
                {
                    kumulacja = rng.Next(2, 37) * 1000000;
                    dzień++;
                    int losów = 0;
                    List<int[]> kupon = new List<int[]>();
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("DZIEŃ: {0}", dzień);
                        Console.WriteLine("Witaj w grze LOTTO, dziś do wygrania jest aż {0} zł", kumulacja);
                        Console.WriteLine("\nStan konta: {0}zł", pieniądze);
                        WyświetlKupon(kupon);

                        //MENU
                        if (pieniądze >= 3 && losów < 8)
                        {
                            Console.WriteLine("\n1 - Postaw nowy los - 3zł [{0}/8]", losów + 1);

                        }

                        Console.WriteLine("2 - Sprawdź kupon - losowanie");
                        Console.WriteLine("3 - Zakończ grę");

                        //MENU
                        wybór = Console.ReadKey().Key;
                        if (wybór == ConsoleKey.D1 && pieniądze >= 3 && losów < 8)
                        {
                            kupon.Add(PostawLos());
                            pieniądze -= 3;
                            losów++;


                        }

                    } while (wybór == ConsoleKey.D1);
                    Console.Clear();
                    if (kupon.Count > 0)
                    {
                        int wygrana = Sprawdz(kupon);
                        if (wygrana > 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\nBrawo wygrałeś {0]zł w tym losowaniu",wygrana);
                            Console.ResetColor();
                            pieniądze += wygrana;

                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nNiestety nie wygrałeś w tym losowaniu");
                            Console.ResetColor();
                        }

                    }
                    else
                    {
                        Console.WriteLine("Nie miałeś losów w tym losowaniu.");
                    }
                    Console.WriteLine("Enter - kontynuuj");
                    Console.ReadKey();
                } while (pieniądze >= 3 && wybór != ConsoleKey.D3);

                Console.Clear();
                Console.WriteLine("Dzień {0}. \nKoniec gry, twój wynik to: {1}zł", dzień, pieniądze - START);
                Console.WriteLine("Enter - graj od nowa.");
            } while (Console.ReadKey().Key == ConsoleKey.Enter);



            Console.ReadKey();

        }

        private static int Sprawdz(List<int[]> kupon)
        {
            int wygrana = 0;
            int[] wylosowane = new int[6];
            for (int i = 0; i < wylosowane.Length; i++)
            {
                int los = rng.Next(1, 50);
                if (!wylosowane.Contains(los))
                {
                    wylosowane[i] = los;

                }
                else
                {
                    i--;

                }
            }
            Array.Sort(wylosowane);
            Console.WriteLine("Wylosowane liczby to: ");
            foreach (int liczba in wylosowane)

            {
                Console.Write(liczba + ", ");
            }
            int[] trafione = SprawdzKupon(kupon, wylosowane);

            int wartość = 0;
            Console.WriteLine();
            if (trafione[0] > 0)
            {
                wartość = trafione[0] * 24;
                Console.WriteLine("3 Trafienia: {0} + {1}zł", trafione[0], wartość);
                wygrana += wartość;
            }
            if (trafione[1] > 0)
            {
                wartość = trafione[1] * rng.Next(100, 301);
                Console.WriteLine("4 Trafienia: {0} + {1}zł", trafione[1], wartość);
                wygrana += wartość;
            }
            if (trafione[2] > 0)
            {
                wartość = trafione[2] * rng.Next(4000, 8001);
                Console.WriteLine("4 Trafienia: {0} + {1}zł", trafione[2], wartość);
                wygrana += wartość;
            }
            if (trafione[3] > 0)
            {
                wartość = (trafione[3] * kumulacja) / (trafione[3] + rng.Next(0,5));
                Console.WriteLine("4 Trafienia: {0} + {1}zł", trafione[3], wartość);
                wygrana += wartość;
            }

            return wygrana;
        }

        private static int[] SprawdzKupon(List<int[]> kupon, int[] wylosowane)
        {
            int[] wygrane = new int[4];
            int i = 0;
            Console.WriteLine("\n\nTwój Kupon: ");
            foreach (int[] los in kupon)
            {
                i++;
                Console.Write(i + ": ");
                int trafień = 0;
                foreach (int liczba in los)
                {
                    if (wylosowane.Contains(liczba))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(liczba + ", ");
                        Console.ResetColor();
                        trafień++;
                    }
                    else
                    {
                        Console.WriteLine(liczba + ", ");
                    }
                }
                switch (trafień)
                {
                    case 3:
                        wygrane[0]++;
                        break;
                    case 4:
                        wygrane[1]++;
                        break;
                    case 5:
                        wygrane[2]++;
                        break;
                    case 6:
                        wygrane[3]++;
                        break;

                }
                Console.WriteLine(" - Trafiono{0}/6", trafień);

            }


            return wygrane;
        }

        private static int[] PostawLos()
        {
            int[] liczby = new int[6];
            int liczba = -1;
            for (int i = 0; i < liczby.Length; i++)
            {
                liczba = -1;
                Console.Clear();
                Console.Write("Postawione liczby: ");
                foreach (int l in liczby)
                {
                    if (l > 0)
                    {
                        Console.WriteLine(l + ", ");

                    }
                }
                Console.WriteLine("\n\nWybierz liczbe od 1 do 49: ");
                Console.Write("{0}/6: ", i + 1);
                bool prawidłowa = int.TryParse(Console.ReadLine(), out liczba);
                if (prawidłowa && liczba >= 1 && liczba <= 49 && !liczby.Contains(liczba))

                {
                    liczby[i] = liczba;

                }
                else
                {
                    Console.WriteLine("Niestety błędna liczba");
                    i--;
                    Console.ReadKey();
                }
            }
            Array.Sort(liczby);
            return liczby;
        }

        private static void WyświetlKupon(List<int[]> kupon)
        {
            if (kupon.Count == 0)
            {
                Console.WriteLine("Nie postawiłeś jeszcze żadnych losów");
            }
            else
            {
                int i = 0;
                Console.WriteLine("Twój kupon: ");

                foreach (int[] los in kupon)
                {
                    i++;
                    Console.Write(i + ": ");
                    foreach (int liczba in los)

                    {
                        Console.Write(liczba + ", ");
                    }
                    Console.WriteLine();

                }
            }
        }
    }
}
