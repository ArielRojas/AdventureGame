using TextBasedAdventureGame.Interfaces;

namespace TextBasedAdventureGame.Classes;

internal class YesNoCharacter : IQuestion
{
    private string _name;
    private string _description;
    private Item _item;

    public YesNoCharacter(string name, string description, Item item)
    {
        _name = name;
        _description = description;
        _item = item;
    }

    public string Name => _name;

    public Item Item => _item;

    public void InteractInGame()
    {
        throw new NotImplementedException();
    }
}
