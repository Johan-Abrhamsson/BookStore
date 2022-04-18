using System;
using RestSharp;
public class Book
{
    RestClient client = new RestClient("https://pokeapi.co/api/v2/");
    Random generator = new Random();
    string name;



    public Book()
    {
        RestRequest request = new RestRequest($"pokemon/{generator.Next(0, 255)}");
        IRestResponse response = client.Get(request);
        string iventory = response.Content;
        int namePlace = iventory.IndexOf("\"species\":{\"name\":\"");
        var i = 0;
        while (true)
        {
            i++;
            if (iventory[namePlace + 19 + i] == '\"')
            {
                break;
            }
        }
        this.name = $"A book about {iventory.Substring(namePlace + 19, i)}";
    }

    public string Name()
    {
        return name;
    }

}
