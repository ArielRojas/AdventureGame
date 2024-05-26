namespace TextBasedAdventureGame.Classes;

internal class QuestionCharacter : NonPlayerCharacter
{
    public QuestionCharacter(string name, string description, Item item) : base(name, description, item)
    {
        this.name = name;
        this.description = description;
        this.item = item;
    }
}
