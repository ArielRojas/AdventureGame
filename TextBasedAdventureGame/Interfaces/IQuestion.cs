using TextBasedAdventureGame.Classes;

namespace TextBasedAdventureGame.Interfaces;

internal interface IQuestion
{
    string Name { get; }
    Item Item { get; }
    void InteractInGame();
}
