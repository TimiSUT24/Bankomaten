namespace Bankomaten
{
    internal class Program
    {
        static string[] users = ["Tim", "Adam", "Mos", "Sam", "Kim"];
        static int[] passwords = [1, 2, 3, 4, 5];
        static int userid;
        static double[] savingsaccount = [5, 10, 15, 20, 25];
        static double[] paymentaccount = [10, 15, 20, 25, 30];

        static void Login(string[] users, int[] passwords, int userid)
        {
            int guesses = 0;
            bool loggedin = false;

            while (guesses < 3 && !loggedin)
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
                        loggedin = true;
                        LoggedIn(users, userid);
                        

                    }
                }

                if (!loggedin)
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

            int startmenu = int.Parse(Console.ReadLine());

            switch (startmenu)
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

            int loggedinmenu;
            string userchoose = Console.ReadLine();
            if(int.TryParse(userchoose, out loggedinmenu))
            {
                switch (loggedinmenu)
                {
                    case 1:
                        
                        break;
                    case 2:
                        
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

        static void Accounts(double[] savingsaccount, int userid, double[] paymentaccount)
        {
            Console.WriteLine("Lönekonto: " + paymentaccount[userid] + "kr");
            Console.WriteLine("Sparkonto: " + savingsaccount[userid] + "kr");

            Console.WriteLine("\nKlicka Enter för att komma till Menyn");
            ConsoleKeyInfo enter = Console.ReadKey();
            if (enter.Key == ConsoleKey.Enter)
            {
                LoggedIn(users, userid);
            }

        }

    }
}