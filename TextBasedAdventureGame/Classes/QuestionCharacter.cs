using TextBasedAdventureGame.Interfaces;

namespace TextBasedAdventureGame.Classes;

internal class QuestionCharacter : IQuestion
{
    private string _name;
    private string _description;
    private Item _item;
    public string _answer;

    public QuestionCharacter(string name, string description, Item item, string answer)
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
        
    }
}
