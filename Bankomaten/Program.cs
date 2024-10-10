

namespace Bankomaten
{
    internal class Program
    {
        static string[] users = ["Tim", "Adam", "Mos", "Sam", "Kim"];
        static int[] passwords = [145, 223, 345, 4120, 5242];
        static int userid;
        static decimal[] savingsAccount = [500, 1000, 2500, 4204, 6000];
        static decimal[] paymentAccount = [1530, 250, 3536, 2500, 3302];

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
                        Accounts(savingsAccount, userid, paymentAccount);
                        break;
                    case 2:
                        Console.Clear();
                        Transfer(savingsAccount, userid, paymentAccount);
                        break;
                    case 3:
                        Console.Clear();
                        PrintOut(savingsAccount, userid, paymentAccount);
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
        static void Accounts(decimal[] savingsAccount, int userid, decimal[] paymentAccount)
        {
            Console.WriteLine("\nLönekonto: " + paymentAccount[userid] + "kr");
            Console.WriteLine("Sparkonto: " + savingsAccount[userid] + "kr");

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
                Accounts(savingsAccount, userid, paymentAccount);
            }
        }
        //Transfer method where the user can transfer money through other accounts. 
        static void Transfer(decimal[] savingsAccount, int userid, decimal[] paymentAccount)
        {   
            //User chooses which account to take money from. 
            Console.WriteLine("\nVälj konto att ta pengar från" +
                              "\n1: Lönekonto" +
                              "\n2: Sparkonto");
            try
            {
                decimal transfer;
                int chooseAccount = int.Parse(Console.ReadLine());

                switch (chooseAccount)
                {   //User chooses what account to transfer to.
                    case 1:
                        Console.WriteLine("Välj konto att överföra till" +
                                          "\n1: Sparkonto");
                        int chooseTransfer = int.Parse(Console.ReadLine());

                        switch (chooseTransfer)
                        {
                            case 1://User cant transfer less than 0 or 0 and if bigger than the account.
                                Console.WriteLine("Hur mycket vill du överföra");
                                transfer = decimal.Parse(Console.ReadLine());
                                if (transfer < 0 || transfer == 0)
                                {
                                    Console.WriteLine("Ditt tal kan inte va 0 eller mindre");
                                    LoggedIn(users, userid);
                                }
                                if (transfer > paymentAccount[userid])
                                {
                                    Console.WriteLine("Du kan inte ta ut så mycket pengar");
                                    Transfer(savingsAccount, userid, paymentAccount);
                                }
                                else
                                {   //PaymentAccount goes to transfer and transfer goes to savingsAccount and resets transfer.                                   
                                    paymentAccount[userid] -= transfer;
                                    savingsAccount[userid] += transfer;
                                    transfer = 0;
                                    Console.WriteLine("Lönekonto: " + paymentAccount[userid] + "kr");
                                    Console.WriteLine("Sparkonto: " + savingsAccount[userid] + "kr");
                                }
                                break;
                        }
                        break;

                    case 2: //Same goes for this code aswell but u take and transfer from another account. 
                        Console.WriteLine("Välj konto att överföra till" +
                                          "\n1: Lönekonto");
                        chooseTransfer = int.Parse(Console.ReadLine());
                        switch (chooseTransfer)
                        {
                            case 1:
                                Console.WriteLine("Hur mycket vill du överföra");
                                transfer = decimal.Parse(Console.ReadLine());
                                if (transfer < 0 || transfer == 0)
                                {
                                    Console.WriteLine("Ditt tal kan inte va 0 eller mindre");
                                    LoggedIn(users, userid);
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
                }//Press enter to go to menu. 
                Console.WriteLine("\nKlicka Enter för att komma till Menyn");
                ConsoleKeyInfo enter = Console.ReadKey();
                if (enter.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    LoggedIn(users, userid);
                }
                else
                {                  
                    Transfer(savingsAccount, userid, paymentAccount);
                }
            }
            catch//Also a try catch if user inputs wrong.
            {
                Console.WriteLine("Fel input");
                Transfer(savingsAccount, userid, paymentAccount);
            }

        }
        //PrintOut method so user can take out money from accounts. 
        static void PrintOut(decimal[] savingsAccount, int userid, decimal[] paymentAccount)
        {
            Console.WriteLine("\nVilket konto vill du ta ut ifrån? " +
                              "\n1: Sparkonto" +
                              "\n2: Lönekonto");
            try
            {
                int chooseAccount = int.Parse(Console.ReadLine());

                switch (chooseAccount)
                {
                    case 1://User chooses how much money and types pincode if wrong no money will be taken out. 
                        Console.WriteLine("Hur mycket pengar vill du ta ut? ");
                        decimal takeMoney = decimal.Parse(Console.ReadLine());
                        Console.WriteLine("Bekräfta din transaktion med din pinkod");
                        int pinCode = int.Parse(Console.ReadLine());

                        if (pinCode != passwords[userid])
                        {
                            Console.WriteLine("Fel lösenord");
                            PrintOut(savingsAccount, userid, paymentAccount);
                        }
                        else
                        {   //If bigger than account or less than 0 and equals to 0. User cant take out money. 
                            if (takeMoney > savingsAccount[userid])
                            {
                                Console.WriteLine("Du kan inte ta ut så mycket pengar");
                                PrintOut(savingsAccount, userid, paymentAccount);
                            }
                            if (takeMoney < 0 || takeMoney == 0)
                            {
                                Console.WriteLine("Ditt tal kan inte va 0 eller mindre");
                                LoggedIn(users, userid);
                            }
                            else
                            {   //Money gets printed out from the account if the other conditions are valid. 
                                savingsAccount[userid] -= takeMoney;
                                Console.WriteLine("Du tog ut " + takeMoney + "kr");
                                takeMoney = 0;
                                Console.WriteLine("Pengar på Sparkontot: " + savingsAccount[userid] + "kr");
                            }
                        }
                        break;

                    case 2://Here will be the same but just another account. 
                        Console.WriteLine("Hur mycket pengar vill du ta ut? ");
                        takeMoney = decimal.Parse(Console.ReadLine());
                        Console.WriteLine("Bekräfta din transaktion med din pinkod");
                        pinCode = int.Parse(Console.ReadLine());
                        if (pinCode != passwords[userid])
                        {
                            Console.WriteLine("Fel lösenord");
                            PrintOut(savingsAccount, userid, paymentAccount);
                        }
                        else
                        {
                            if (takeMoney > paymentAccount[userid])
                            {
                                Console.WriteLine("Du kan inte ta ut så mycket pengar");
                                PrintOut(savingsAccount, userid, paymentAccount);
                            }
                            if (takeMoney < 0 || takeMoney == 0)
                            {
                                Console.WriteLine("Ditt tal kan inte va 0 eller mindre");
                                LoggedIn(users, userid);                              
                            }
                            else
                            {
                                paymentAccount[userid] -= takeMoney;
                                Console.WriteLine("Du tog ut " + takeMoney + "kr");
                                takeMoney = 0;
                                Console.WriteLine("Pengar på Lönekonto: " + paymentAccount[userid] + "kr");
                            }
                        }
                        break;
                }//Press enter to go to menu 
                Console.WriteLine("\nKlicka Enter för att komma till Menyn");
                ConsoleKeyInfo enter = Console.ReadKey();
                if (enter.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    LoggedIn(users, userid);
                }
                else
                {                   
                    PrintOut(savingsAccount, userid, paymentAccount);
                }
            }
            catch
            {   //Here is also a try catch if wrong input. 
                Console.WriteLine("Fel input");
                PrintOut(savingsAccount, userid, paymentAccount);
            }

        }

    }
}