
//Tim Petersen SUT24
namespace Bankomaten
{
    internal class Program
    {
        static string[] users = ["Tim", "Adam", "Mos", "Sam", "Kim"];
        static int[] passwords = [145, 223, 345, 4120, 5242];
        static int userid;
        
        //Bankaccounts with Jaggedarrays 
        static decimal[][] accounts = new decimal[][]
        {
            new decimal[] {1000,2030},
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

            for(int display = 0; display < accountNames[userid].Length; display++)
            {
                Console.WriteLine($"{display + 1} {accountNames[userid][display]}: {accounts[userid][display]}kr");
            }
           
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
            try
            {
                //User chooses which account to take money from. 
                Console.WriteLine("Välj konto att ta pengar från.");
                for (int display = 0; display < accountNames[userid].Length; display++)
                {
                    Console.WriteLine($"{display + 1} {accountNames[userid][display]}");
                   
                }

                int chooseAccount = int.Parse(Console.ReadLine()) - 1;
                
                Console.WriteLine("Välj konto att överföra till");
                for (int transferAccount = 0; transferAccount < accountNames[userid].Length; transferAccount++)
                {
                    Console.WriteLine($"{transferAccount + 1} {accountNames[userid][transferAccount]}");
                }

                int chooseTransfer = int.Parse(Console.ReadLine()) - 1;

                Console.WriteLine("Hur mycket vill du överföra");
                decimal amount = decimal.Parse(Console.ReadLine());

                if (amount <= 0)
                {
                    Console.WriteLine("Kan inte välja 0 eller mindre");
                    LoggedIn(users, userid);
                }

                if (amount <= accounts[userid][chooseAccount])
                {
                    accounts[userid][chooseAccount] -= amount;
                    accounts[userid][chooseTransfer] += amount;
                    Console.WriteLine($"Överförde {amount}kr från {accountNames[userid][chooseAccount]} till {accountNames[userid][chooseTransfer]} ");
                }
                else
                {
                    Console.WriteLine("Kan inte ta ut så mycket pengar");
                }

                Console.WriteLine("\nKlicka Enter för att komma till Menyn");
                ConsoleKeyInfo enter = Console.ReadKey();
                if (enter.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    LoggedIn(users, userid);
                }
                else
                {
                    LoggedIn(users, userid);
                }
            }
            catch
            {
                LoggedIn(users,userid);
            }




        }
        //PrintOut method so user can take out money from accounts. 
        static void PrintOut(int userid)
        {
            
           

        }

    }
}