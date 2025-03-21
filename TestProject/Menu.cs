namespace TestProject;

public class Menu
{
    /// <summary>
    /// Menu interface
    /// </summary>
    public static int Choice(string[] msg)
    {
        int pos = 1;
        int len = msg.Length;
        // if (msg.Length < 3)
        // {
        //     throw new Exception("It must be at least 2 options");
        // }
        while (true)
        {
            Console.Clear();
            Console.WriteLine(msg[0]);
            for (int i = 1; i < len; i++)
            {
                if (i == pos)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine(msg[i]);
                    Console.ResetColor();
                    continue;
                }
                Console.WriteLine(msg[i]);
            }

            var key = Console.ReadKey().Key;
            if (key == ConsoleKey.UpArrow)
            {
    
                pos = pos == 1 ? msg.Length - 1 : pos - 1;
            }
            if (key == ConsoleKey.DownArrow)
            {
                pos = pos == msg.Length - 1 ? 1 : pos + 1;
            }
            if (key == ConsoleKey.Enter)
            {
                return pos;
            }
        }
    }

    /// <summary>
    /// Menu for maps
    /// </summary>
    public static string ChoiceStr(string title, string[] msg)
    {
        int pos = 0;
        int len = msg.Length;
        while (true)
        {
            Console.Clear();
            Console.WriteLine(title);
            for (int i = 0; i < len; i++)
            {
                if (i == pos)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine(msg[i]);
                    Console.ResetColor();
                    continue;
                }
                Console.WriteLine(msg[i]);
            }

            var key = Console.ReadKey().Key;
            if (key == ConsoleKey.UpArrow)
            {
                pos = pos == 0 ? msg.Length - 1 : pos - 1;
            }
            if (key == ConsoleKey.DownArrow)
            {
                pos = pos == msg.Length - 1 ? 1 : pos + 1;
            }
            if (key == ConsoleKey.Enter)
            {
                return msg[pos];
            }
        }
    }
    
    /// <summary>
    /// Func for reading integer
    /// </summary>
    public static int EnterBalance()
    {
        Console.Clear();
        while (true)
        {
            Console.WriteLine("Enter balance: ");
            var ans = Console.ReadLine();
            if (int.TryParse(ans, out int balance) && balance > 0)
            {
                return balance;
            }
            Console.WriteLine("\nError: Balance must be greater than zero\nPress any key to try again");
            Console.ReadKey();
            Console.Clear();
        }
    }

    /// <summary>
    /// Menu for reading string
    /// </summary>
    public static string EnterName(string msg)
    {
        Console.Clear();
        while (true)
        {
            Console.WriteLine(msg);
            var ans = Console.ReadLine();
            if (string.IsNullOrEmpty(ans))
            {
                Console.WriteLine("\nError: Name must contain something\nPress any key to try again");
                Console.ReadKey();
                Console.Clear();
            }
            else
            {
                return ans;
            }
        }
    }
}