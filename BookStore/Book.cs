using System;
using RestSharp;
public class Book
{
    RestClient client = new RestClient("https://pokeapi.co/api/v2/");
    Random generator = new Random();
    string name;



    public Book()
    {
        RestRequest request = new RestRequest($"pokemon/{generator.Next(1, 899)}");
        IRestResponse response = client.Get(request);
        string iventory = response.Content;
        Console.WriteLine(iventory);
        //this.name = $"{iventory} the {}";
    }

    public string Name()
    {
        return name;
    }

}
