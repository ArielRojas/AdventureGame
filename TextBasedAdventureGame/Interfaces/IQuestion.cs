using TextBasedAdventureGame.Classes;

namespace TextBasedAdventureGame.Interfaces;

internal interface IQuestion
{
    string Name { get; }
    Item Item { get; }
    string Answer { get; }

    void InteractInGame();
}
