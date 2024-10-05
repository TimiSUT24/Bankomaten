namespace Bankomaten
{
    internal class Program
    {
        static string[] users = ["Tim", "Adam", "Mos", "Sam", "Kim"];
        static int[] passwords = [1, 2, 3, 4, 5];
        static int userid;
        static double[] savingsAccount = [5, 10, 15, 20, 25];
        static double[] paymentAccount = [10, 15, 20, 25, 30];

        static void Login(string[] users, int[] passwords, int userid)
        {
            int guesses = 0;
            bool loggedIn = false;

            while (guesses < 3 && !loggedIn)
            {
                Console.WriteLine("Enter Your username");
                string username = Console.ReadLine();
                Console.WriteLine("Enter Your password");
                int password = int.Parse(Console.ReadLine());

                for (userid = 0; userid < users.Length; userid++)
                {
                    if (username == users[userid] && password == passwords[userid])
                    {
                        
                        Console.WriteLine("Du lyckades logga in " + users[userid]);
                        loggedIn = true;
                        LoggedIn(users, userid);
                        

                    }
                }

                if (!loggedIn)
                {
                    guesses++;
                    Console.WriteLine("fel");
                }

                if (guesses == 3)
                {
                    Console.WriteLine("Stänger bankomaten");
                    break;
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Välkommen till bankomaten");

            Console.WriteLine("1: Logga in" +
                              "\n2: Exit");

            int startMenu = int.Parse(Console.ReadLine());

            switch (startMenu)
            {
                case 1:
                    Login(users, passwords, userid);
                    break;
                case 2:
                    Console.WriteLine("Stänger bankomaten...");
                    break;

            }
        }

        static void LoggedIn(string[] users, int userid)
        {
            Console.WriteLine("\nDu är inloggad som " + users[userid] +
                                "\nVad vill du göra?\n");

            Console.WriteLine("1: Se dina konton och saldo\n" +
                              "2: Överföring mellan konton\n" +
                              "3: Ta ut pengar\n" +
                              "4: Logga ut");

            int loggedInMenu;
            string userChoose = Console.ReadLine();
            if(int.TryParse(userChoose, out loggedInMenu))
            {
                switch (loggedInMenu)
                {
                    case 1:
                        Accounts(savingsAccount, userid, paymentAccount);
                        break;
                    case 2:
                        Transfer(savingsAccount, userid, paymentAccount);
                        break;
                    case 3:
                        
                        break;
                    case 4:
                        Login(users, passwords, userid);
                        break;
                    default:
                        Console.WriteLine("Fel siffra");
                        LoggedIn(users, userid);
                        break;
                }
            }
            else
            {
                Console.WriteLine("Ogiltigt val");
                LoggedIn(users, userid);
            }

        }

        static void Accounts(double[] savingsAccount, int userid, double[] paymentAccount)
        {
            Console.WriteLine("Lönekonto: " + paymentAccount[userid] + "kr");
            Console.WriteLine("Sparkonto: " + savingsAccount[userid] + "kr");

            Console.WriteLine("\nKlicka Enter för att komma till Menyn");
            ConsoleKeyInfo enter = Console.ReadKey();
            if (enter.Key == ConsoleKey.Enter)
            {
                LoggedIn(users, userid);
            }

        }

        static void Transfer(double[] savingsAccount, int userid, double[] paymentAccount)
        {
            Console.WriteLine("Välj konto att ta pengar från" +
                              "\n1: Lönekonto" +
                              "\n2: Sparkonto");

            double transfer;
            int chooseAccount = int.Parse(Console.ReadLine());

            switch (chooseAccount)
            {
                case 1:
                    Console.WriteLine("Välj konto att överföra till" +
                                      "\n1: Sparkonto");
                    int chooseTransfer = int.Parse(Console.ReadLine());

                    switch (chooseTransfer)
                    {
                        case 1:
                            Console.WriteLine("Hur mycket vill du överföra");
                            transfer = double.Parse(Console.ReadLine());
                            if (transfer < 0)
                            {
                                Console.WriteLine("Ditt tal kan inte vara mindre än noll");
                                Transfer(savingsAccount, userid, paymentAccount);
                            }
                            if (transfer > paymentAccount[userid])
                            {
                                Console.WriteLine("Du kan inte ta ut så mycket pengar");
                                Transfer(savingsAccount, userid, paymentAccount);
                            }
                            else
                            {
                                paymentAccount[userid] -= transfer;
                                savingsAccount[userid] += transfer;
                                transfer = 0;
                                Console.WriteLine("Lönekonto: " + paymentAccount[userid] + "kr");
                                Console.WriteLine("Sparkonto: " + savingsAccount[userid] + "kr");
                            }
                            break;
                    }
                    break;

                case 2:
                    Console.WriteLine("Välj konto att överföra till" +
                                      "\n1: Lönekonto");
                    chooseTransfer = int.Parse(Console.ReadLine());
                    switch (chooseTransfer)
                    {
                        case 1:
                            Console.WriteLine("Hur mycket vill du överföra");
                            transfer = double.Parse(Console.ReadLine());
                            if (transfer < 0)
                            {
                                Console.WriteLine("Ditt tal kan inte vara mindre än noll");
                                Transfer(savingsAccount, userid, paymentAccount);
                            }
                            if (transfer > savingsAccount[userid])
                            {
                                Console.WriteLine("Du kan inte ta ut så mycket pengar");
                                Transfer(savingsAccount, userid, paymentAccount);
                            }
                            else
                            {
                                savingsAccount[userid] -= transfer;
                                paymentAccount[userid] += transfer;
                                transfer = 0;
                                Console.WriteLine("Sparkonto: " + savingsAccount[userid] + "kr");
                                Console.WriteLine("Lönekonto: " + paymentAccount[userid] + "kr");

                            }
                            break;
                    }
                    break;
            }
            Console.WriteLine("\nKlicka Enter för att komma till Menyn");
            ConsoleKeyInfo enter = Console.ReadKey();
            if (enter.Key == ConsoleKey.Enter)
            {
                LoggedIn(users, userid);
            }
        }

        static void PrintOut(double[] savingsAccount, int userid, double[] paymentAccount)
        {

        }
    }
}