using TextBasedAdventureGame.Interfaces;

namespace TextBasedAdventureGame.Classes;

internal class YesNoCharacter : IQuestion
{
    private string _name;
    private string _description;
    private Item _item;
    private string _answer;

    public YesNoCharacter(string name, string description, Item item, string answer)
    {
        _name = name;
        _description = description;
        _item = item;
        _answer = answer;
    }

    public string Name => _name;

    public Item Item => _item;

    public string Answer => _answer;

    public void InteractInGame()
    {
        throw new NotImplementedException();
    }
}
