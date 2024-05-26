namespace TextBasedAdventureGame.Classes;

using Spectre.Console;

internal class Location
{
    private readonly string _name;
    private readonly string _description;
    private NonPlayerCharacter _nonPlayerCharacter;

    public Location(string name, string description, NonPlayerCharacter nonPlayerCharacter)
    {
        _name = name;
        _description = description;
        _nonPlayerCharacter = nonPlayerCharacter;
    }

    public string Name
    {
        get => _name;
    }

    public string Description
    {
        get => _description;
    }

    public NonPlayerCharacter NonPlayerCharacter
    {
        get => _nonPlayerCharacter;
    }

    public void ShowLocationInformation()
    {
        Console.WriteLine("==============================================================================================\n");
        AnsiConsole.WriteLine($"Nombre de localidad: {Name} \nDescripcion: {Description}");
        Console.WriteLine("\n==============================================================================================\n");
    }
}
