namespace TextBasedAdventureGame.Classes;

internal class YesNoCharacter : NonPlayerCharacter
{
    public YesNoCharacter(string name, string description, Item item) : base(name, description, item)
    {
        this.name = name;
        this.description = description;
        this.item = item;
    }
}
