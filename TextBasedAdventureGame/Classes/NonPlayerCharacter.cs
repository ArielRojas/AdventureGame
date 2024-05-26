namespace TextBasedAdventureGame.Classes;

internal class NonPlayerCharacter : Character
{
    protected Item item;
    public NonPlayerCharacter(string name, string description, Item item) : base(name, description)
    {
        this.name = name;
        this.description = description;
        this.item = item;
    }

    public Item Item
    {
        get => item;
    }

    public virtual string ShowNonPlayerCharacter()
    {
        return $"NPC Name: {name} \nNPC Description: {description} \nItem: {item.ShowItem()}";
    }
}
