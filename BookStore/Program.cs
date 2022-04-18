
using System.ComponentModel;
using System.Collections;
using System.Globalization;
using System;
using System.Collections.Generic;
using RestSharp;
using System.Text.Json;

class Program
{
    static Dictionary<int, Action> Scene = new Dictionary<int, Action>();
    static Queue<Book> Library = new Queue<Book>();
    static RestClient client = new RestClient("https://localhost:5001/api/book/");
    static void Main(string[] args)
    {
        Action gainBook = GainBook;
        Action readBooks = ReadBooks;
        Action loadBooks = LoadBooks;
        Action saveBooks = SaveBooks;

        Scene.Add(1, gainBook);
        Scene.Add(2, readBooks);
        Scene.Add(3, loadBooks);
        Scene.Add(4, saveBooks);
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
            Console.WriteLine("3. Load books");
            Console.WriteLine("4. Save books");
            choise = Console.ReadLine();
            try
            {
                bool resalt = int.TryParse(choise, out intChoise);
            }
            catch
            {
                Console.WriteLine("That did not work, try that again");
            }
            SceneChange(intChoise);
        }
    }
    static void SceneChange(int scene)
    {
        try
        {
            Scene[scene]();
        }
        catch
        {
            Console.WriteLine("That did not work, try that again");
            Console.ReadLine();
            Console.Clear();
        }
    }

    static void GainBook()
    {
        Console.WriteLine("This is your new book");
        Book thisone = new Book();
        Library.Enqueue(thisone);
        Console.WriteLine(thisone.Name());
        Console.ReadLine();
        Console.Clear();
    }

    static void ReadBooks()
    {
        Console.WriteLine("These are your books");
        foreach (Book C in Library)
        {
            Console.WriteLine(C.Name());
        }
        Console.ReadLine();
        Console.Clear();
    }

    static void LoadBooks()
    {
        RestClient client = new RestClient("https://localhost:5001/api/book/");


        //https://localhost:5001/api/book/newbook/number/5/bob

        RestRequest request = new RestRequest("list");

        IRestResponse response = client.Get(request);

        string iventory = response.Content;

        Console.WriteLine(response);
        Console.WriteLine(iventory);
        Console.ReadLine();
    }


    static void SaveBooks()
    {
        RestRequest request = new RestRequest($"length");
        IRestResponse response = client.Get(request);
        string iventory = response.Content;
        int length;
        bool worked = int.TryParse(iventory, out length);
        foreach (Book c in Library)
        {
            request = new RestRequest($"newbook/number/{length}/{c.Name()}");
            response = client.Get(request);
            iventory = response.Content;
            Console.WriteLine(iventory);
            Console.WriteLine($"newbook/number/{length}/{c.Name()}");
            length++;
        }
        Console.WriteLine("Save is complete");
        Console.ReadLine();
        Console.Clear();
    }
}

