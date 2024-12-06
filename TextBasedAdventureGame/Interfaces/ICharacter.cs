using TextBasedAdventureGame.Classes;

namespace TextBasedAdventureGame.Interfaces;

internal interface ICharacter
{
    string Name { get; }
    Item Item { get; }

    int ReceiveAttack(int attack);

    void ShowPoints();

    void InitialInteraction();

    void InteractInGame(ICharacter character);
}
