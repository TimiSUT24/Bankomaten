namespace Bankomaten
{
    internal class Program
    {
        static string[] users = ["Tim", "Adam", "Mos", "Sam", "Kim"];
        static int[] passwords = [1, 2, 3, 4, 5];
        static int userid; 

        static void Login()
        {
            int fel = 0;

            Console.WriteLine("Enter Your username");
            string username = Console.ReadLine();
            Console.WriteLine("Enter Your password");
            int password = int.Parse(Console.ReadLine());

            for ()
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
