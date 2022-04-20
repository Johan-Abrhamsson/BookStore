using System;
using RestSharp;
public class Book
{
    RestClient client = new RestClient("https://pokeapi.co/api/v2/");
    Random generator = new Random();
    string name;



    public Book()
    {
        //list to pokeapi and randomise a name
        RestRequest request = new RestRequest($"pokemon/{generator.Next(0, 600)}");
        IRestResponse response = client.Get(request);
        string iventory = response.Content;
        //section the name of the listed name
        int namePlace = iventory.IndexOf("\"species\":{\"name\":\"");
        var i = 0;
        //add letters until a stop has been found
        while (true)
        {
            i++;
            if (iventory[namePlace + 19 + i] == '\"')
            {
                break;
            }
        }
        //the name created
        this.name = $"{iventory.Substring(namePlace + 19, i)}";
    }

    public string Name()
    {
        //to get the name
        return name;
    }

}
