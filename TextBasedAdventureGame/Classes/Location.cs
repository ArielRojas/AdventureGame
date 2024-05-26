namespace TextBasedAdventureGame.Classes;

using Spectre.Console;
using TextBasedAdventureGame.Interfaces;

internal class Location
{
    private readonly string _name;
    private readonly string _description;
    private IQuestion _question;
    private ICharacter _character;

    public Location(string name, string description, IQuestion question)
    {
        _name = name;
        _description = description;
        _question = question;
    }

    public Location(string name, string description, ICharacter character)
    {
        _name = name;
        _description = description;
        _character = character;
    }

    public string Name
    {
        get => _name;
    }

    public string Description
    {
        get => _description;
    }

    public IQuestion Question
    {
        get => _question;
    }

    public ICharacter Character { get => _character; }

    public void ShowLocationInformation()
    {
        Console.WriteLine("==============================================================================================\n");
        AnsiConsole.WriteLine($"Nombre de localidad: {Name} \nDescripcion: {Description}");
        Console.WriteLine("\n==============================================================================================\n");
    }
}
