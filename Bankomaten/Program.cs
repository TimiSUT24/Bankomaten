
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
            new decimal[] {5000,650,720},
            new decimal[] {9050,1043,344},
            new decimal[] {15004,1440,1340,2304},
            new decimal[] {43040,184,1911,556},
        };

        static string[][] accountNames = new string[][]
        {
            new string [] {"Sparkonto", "Lönekonto" },
            new string [] {"Sparkonto","Lönekonto","Investeringskonto" },
            new string [] {"Sparkonto","Lönekonto","Semesterkonto" },
            new string [] {"Sparkonto", "Lönekonto","Roadtripkonto","Familjekonto" },
            new string [] {"Sparkonto", "Lönekonto", "Semesterkonto", "Festkonto" }
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
            //Loops accountnames and which user is logged in and shows what accounts that user has and money. 
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
                //If user types number outside of accountNames array that the user has it will restart the method. 
                if (chooseAccount < 0 || chooseAccount >= accountNames[userid].Length)
                {
                    Console.WriteLine("Ogiltigt val");
                    Transfer(userid);
                }

                //User chooses which account to tranfer to. 
                Console.WriteLine("Välj konto att överföra till");
                for (int transferAccount = 0; transferAccount < accountNames[userid].Length; transferAccount++)
                {
                    Console.WriteLine($"{transferAccount + 1} {accountNames[userid][transferAccount]}");
                }

                int chooseTransfer = int.Parse(Console.ReadLine()) - 1;
                if (chooseTransfer < 0 || chooseTransfer >= accountNames[userid].Length)
                {
                    Console.WriteLine("Ogiltigt val");
                    Transfer(userid);
                }

                if(chooseTransfer == chooseAccount || chooseAccount == chooseTransfer )
                {
                    Console.WriteLine("Kan inte välja samma konto!!!"); 
                    Transfer(userid);
                }

                //User chosses how much to take out.
                Console.WriteLine("Hur mycket vill du överföra");
                decimal amount = decimal.Parse(Console.ReadLine());
                //If 0 or less goes to menu.
                if (amount <= 0)
                {
                    Console.WriteLine("Kan inte välja 0 eller mindre");
                    LoggedIn(users, userid);
                }
                //If Less or equal it will transfer money. 
                if (amount <= accounts[userid][chooseAccount])
                {
                    accounts[userid][chooseAccount] -= amount;
                    accounts[userid][chooseTransfer] += amount;
                    Console.WriteLine($"Överförde {amount}kr från {accountNames[userid][chooseAccount]} till {accountNames[userid][chooseTransfer]} ");
                    Console.WriteLine($"Pengar på {accountNames[userid][chooseAccount]}: {accounts[userid][chooseAccount]}kr");
                    Console.WriteLine($"Pengar på {accountNames[userid][chooseTransfer]}: {accounts[userid][chooseTransfer]}kr");

                }
                //If user takes out more money than the account. 
                else
                {
                    Console.WriteLine("Kan inte ta ut så mycket pengar");
                }
                //User goes to menu. 
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
            Console.WriteLine("Vilket konto vill du ta ut ifrån");
            try
            {   //Loop that shows all the accounts that the users has. 
                for (int display = 0; display < accountNames[userid].Length; display++)
                {
                    Console.WriteLine($"{display + 1} {accountNames[userid][display]}: {accounts[userid][display]}kr");
                }

                int chooseAccount = int.Parse(Console.ReadLine()) - 1;
                if(chooseAccount < 0 || chooseAccount >= accountNames[userid].Length)
                {
                    Console.WriteLine("Ogiltigt val");
                    PrintOut(userid); 
                }

                //User types how much money and the pincode. 
                Console.WriteLine("Hur mycket pengar vill du ta ut");
                decimal takeMoney = decimal.Parse(Console.ReadLine());
                Console.WriteLine("Bekräfta din transaktion med din pinkod");
                int pinCode = int.Parse(Console.ReadLine());
                //If wrong pin user starts over in the method. 
                if (pinCode != passwords[userid])
                {
                    Console.WriteLine("Fel lösenord");
                    PrintOut(userid);
                }
                else
                {   //Goes to menu if less or 0.
                    if(takeMoney <= 0)
                    {
                        Console.WriteLine("Ditt tal kan inte va 0 eller mindre");
                        LoggedIn(users, userid);
                    }
                    //If right amount of money it will print out the money and show how much is left. 
                    else if(takeMoney <= accounts[userid][chooseAccount])
                    {
                        accounts[userid][chooseAccount] -= takeMoney;
                        Console.WriteLine("Du tog ut " + takeMoney + "kr");
                        takeMoney = 0;
                        Console.WriteLine($"Pengar på: {accountNames[userid][chooseAccount]} {accounts[userid][chooseAccount]}kr ");
                        

                    }
                }
                //User goes to menu. 
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
                Console.WriteLine("Fel input");
                PrintOut(userid); 
            }


        }

    }
}