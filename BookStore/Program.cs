using System.ComponentModel;
using System.Collections;
using System.Globalization;
using System;
using System.Collections.Generic;
using RestSharp;
using System.Text.Json;

class Program
{
    //https://localhost:5001/api/book/
    static Dictionary<int, Action> scene = new Dictionary<int, Action>();
    static Queue<Book> library = new Queue<Book>();
    static RestClient client = new RestClient("https://localhost:5001/api/book/");
    static bool game = true;
    static void Main(string[] args)
    {
        //For the logic of the system to change the scenes
        Action gainBook = GainBook;
        Action readBooks = ReadBooks;
        Action loadBooks = LoadBooks;
        Action saveBooks = SaveBooks;
        Action closeprogram = CloseProgram;
        scene.Add(1, gainBook);
        scene.Add(2, readBooks);
        scene.Add(3, loadBooks);
        scene.Add(4, saveBooks);
        scene.Add(5, closeprogram);

        //Startup text and logic
        Console.WriteLine("Hello and welcome to your library!");
        MainGame();
    }

    static void MainGame()
    {
        string choise;
        int intChoise = 0;

        //Set part of how the game is ment to be played
        while (game)
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Obtain a book");
            Console.WriteLine("2. See your books");
            Console.WriteLine("3. Load books");
            Console.WriteLine("4. Save books");
            Console.WriteLine("5. Close program");

            //to see if the response was valid
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
    static void SceneChange(int scenevalue)
    {
        //Atempt to start a specified scene as stated in dictionary of "scene"
        try
        {
            scene[scenevalue]();
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
            Console.Clear();
        }
        catch
        {
            Console.WriteLine("That did not work, try that again");
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
            Console.Clear();
        }
    }

    static void GainBook()
    {
        //Creates a book and enqueue that book
        Console.WriteLine("This is your new book");
        Book thisone = new Book();
        library.Enqueue(thisone);
        Console.WriteLine(thisone.Name());
    }

    static void ReadBooks()
    {
        //To see the name of all books that have been listed
        Console.WriteLine("These are your books");
        foreach (Book C in library)
        {
            Console.WriteLine(C.Name());
        }
    }

    static void LoadBooks()
    {
        //To load books in the database and overwrite the base
        RestRequest request = new RestRequest("list");
        IRestResponse response = client.Get(request);
        string iventory = response.Content;

        //Make the loaded list from the database to be the updated library
        Queue<Book> load = JsonSerializer.Deserialize<Queue<Book>>(iventory);
        library = load;
    }


    static void SaveBooks()
    {
        //Save the created and listed books in the database
        //To load the current amount of books in the database
        RestRequest request = new RestRequest($"length");
        IRestResponse response = client.Get(request);
        string iventory = response.Content;
        int length;
        bool worked = int.TryParse(iventory, out length);

        //For every book it sends it over to the database and it saves the books
        foreach (Book c in library)
        {
            request = new RestRequest($"newbook/number/{length}/{c.Name()}");
            response = client.Get(request);
            iventory = response.Content;
            Console.WriteLine(c.Name() + " is saved at " + length);
            length++;
        }
        Console.WriteLine("Save is complete");
    }

    static void CloseProgram()
    {
        //Close the program
        game = false;
    }
}

