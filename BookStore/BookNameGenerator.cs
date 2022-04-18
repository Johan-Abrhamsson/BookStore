using System;

public class BookNameGenerator<p>
{
    p value;
    string name;

    public string NameCreation()
    {
        name = $"{value}";
        return name;
    }

}