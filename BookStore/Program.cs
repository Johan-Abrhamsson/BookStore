using System.Collections;
using System.Globalization;
using System;
using System.Collections.Generic;
class Program
{
    static Dictionary<int, Action> Scene = new Dictionary<int, Action>();
    static Queue<Book> Libaray = new Queue<Book>();
    static void Main(string[] args)
    {

        Console.WriteLine("Hello and welcome to your library!");
        MainGame();
    }

    static void MainGame()
    {
        bool game = true;
        string choise;
        int intChoise = 0;
        while (game)
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Gain a book");
            Console.WriteLine("2. See your books");
            Console.WriteLine("3. Read book");
            Console.WriteLine("4. Write in a book");
            choise = Console.ReadLine();
            try
            {
                bool resalt = int.TryParse(choise, out intChoise);
            }
            catch
            {
                Console.WriteLine("That did not work");
            }
            SceneChange(intChoise);
        }
    }
    static void SceneChange(int scene)
    {
        bool sceneActive = true;
        while (sceneActive)
        {
            switch (scene)
            {
                case 1:
                    sceneActive = GainBook(sceneActive);
                    break;
                default:
                    break;
            }

        }

    }

    static bool GainBook(bool sceneActive)
    {
        Console.WriteLine("These are the books avilabe");
        Book thisone = new Book();
        Console.WriteLine(thisone.Name());
        Console.ReadLine();
        return true;
    }
}

