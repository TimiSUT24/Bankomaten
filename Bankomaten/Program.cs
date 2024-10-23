
//Tim Petersen SUT24
namespace Bankomaten
{
    internal class Program
    {
        static string[] users = ["Tim", "Adam", "Mos", "Sam", "Kim"];
        static int[] passwords = [145, 223, 345, 4120, 5242];
        static int userid;
        
        static decimal[][] accounts = new decimal[][]
        {
            new decimal[] {1,2},
            new decimal[] {5,6,7},
            new decimal[] {9,10,11},
            new decimal[] {13,14,15,16},
            new decimal[] {17,18,19,20},
        };

        static string[][] accountNames = new string[][]
        {
            new string [] {"Sparkonto", "Konto" },
            new string [] {"Investera","Test","Test1" },
            new string [] {"La","Pa","Wa" },
            new string [] {"Na", "No","Op","lo" },
            new string [] {"Hej", "då", "dag", "mo" }
        };


        static void Main(string[] args)
        {
            StartMenu();
        }

        //StartMenu method for the user. 
        static void StartMenu()
        {
            Console.WriteLine("Välkommen till bankomaten");
            Console.WriteLine("1: Logga in" +
                              "\n2: Exit");

                int menu;
                string startMenu = Console.ReadLine();         
                if (int.TryParse(startMenu, out menu))
                {
                    switch (menu)
                    {
                        case 1:
                            Login(users, passwords, userid);
                            break;
                        case 2:
                            Console.WriteLine("Stänger bankomaten...");
                            break;                          
                        default:
                            StartMenu();
                            break;
                    }
                }
                else
                {
                    StartMenu();
                }
                                             
        }
        //Login method thats checks if user exists and uses parameters in the method so we can transfer userid to other methods. 
        static void Login(string[] users, int[] passwords, int userid)
        {
            int guesses = 0;
            bool loggedIn = false;
            try
            {   //While loop will run while guesses is lower than 3 and is not loggedin. 
                while (guesses < 3 && !loggedIn)
                {
                    Console.WriteLine("Enter Your username");
                    string username = Console.ReadLine();
                    Console.WriteLine("Enter Your password");
                    int password = int.Parse(Console.ReadLine());

                    //Loops through users and if user and password have same userid Example 0-0 it will login user (0). 
                    for (userid = 0; userid < users.Length; userid++)
                    {
                        if (username == users[userid] && password == passwords[userid])
                        {

                            Console.WriteLine("Du lyckades logga in " + users[userid]);
                            loggedIn = true;
                            Console.Clear();
                            LoggedIn(users, userid);
                        }
                    }
                    //If statement that checks if the user is not logged in guesses will increase.
                    if (!loggedIn)
                    {
                        guesses++;
                        Console.WriteLine("Fel lösenord eller användarnamn");
                    }
                    //If guesses is 3 the program shutsdown. 
                    if (guesses == 3)
                    {
                        Console.WriteLine("Stänger bankomaten...");
                        break;
                    }
                }
            }
            catch
            {
                Console.WriteLine("Fel input endast siffror!\n");
                Console.Clear();
                StartMenu();
            }           
        }

        //(LoggedIn method) When user has logged in prints a menu and can choose from 1-4 in a switch case. 
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
            //Using TryParse if user chooses a number outside the switch case and goes back to the menu. 
            if (int.TryParse(userChoose, out loggedInMenu))
            {
                switch (loggedInMenu)
                {
                    case 1:
                        Console.Clear();
                        Accounts(userid);
                        break;
                    case 2:
                        Console.Clear();
                        Transfer(userid);
                        break;
                    case 3:
                        Console.Clear();
                        PrintOut(userid);
                        break;
                    case 4:
                        Console.Clear();
                        StartMenu();
                        break;
                    default:
                        Console.Clear();
                        LoggedIn(users, userid);
                        break;
                }
            }
            //Goes back to menu if user pressed any other keys.
            else 
            {
                Console.WriteLine("Ogiltigt val");              
                LoggedIn(users, userid);
            }

        }

        //User can see the amount of money in the accounts 
        static void Accounts(int userid)
        {
          
            //User presses enter to return to menu 
            Console.WriteLine("\nKlicka Enter för att komma till Menyn");
            ConsoleKeyInfo enter = Console.ReadKey();
            if (enter.Key == ConsoleKey.Enter)
            {
                Console.Clear();
                LoggedIn(users, userid);
            }
            else
            {                
                Accounts(userid);
            }
        }
        //Transfer method where the user can transfer money through other accounts. 
        static void Transfer(int userid)
        {   
            //User chooses which account to take money from. 
            Console.WriteLine("\nVälj konto att ta pengar från" +
                              "\n1: Lönekonto" +
                              "\n2: Sparkonto");
           

                   
              

        }
        //PrintOut method so user can take out money from accounts. 
        static void PrintOut(int userid)
        {
            Console.WriteLine("\nVilket konto vill du ta ut ifrån? " +
                              "\n1: Sparkonto" +
                              "\n2: Lönekonto");
           

        }

    }
}