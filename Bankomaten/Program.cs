namespace Bankomaten
{
    internal class Program
    {
        static string[] users = ["Tim", "Adam", "Mos", "Sam", "Kim"];
        static int[] passwords = [1, 2, 3, 4, 5];
        static int userid; 

        static void Login(string[] users, int[] passwords, int userid)
        {
            int fel = 0;
            bool loggedIn = false;

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

                }
            }

            if (!loggedIn)
            {
                fel++;
                Console.WriteLine("fel");
            }

            if (fel == 3)
            {
                Console.WriteLine("Stänger bankomaten");
                break;
            }

            static void Main(string[] args)
        {
            Console.WriteLine("Välkommen till bankomaten");

            Console.WriteLine("1: Logga in" +  
                              "2: Exit");

            int startMenu = int.Parse(Console.ReadLine());

            switch (startMenu)
            {
                case 1:
                    Login(); 
                    break;
                case 2:
                    Console.WriteLine("Stänger bankomaten...");
                    break;

            }
        }
    }
}
