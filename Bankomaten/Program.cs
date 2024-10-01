namespace Bankomaten
{
    internal class Program
    {
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
