namespace TextBasedAdventureGame;

internal class Character
{
    protected string name;
    protected string description;

    public Character(string name, string description)
    {
        this.name = name;
        this.description = description;
    }

    public string Name
    {
        get => name;
    }
}
