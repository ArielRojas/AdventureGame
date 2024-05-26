namespace TextBasedAdventureGame.Interfaces;

internal interface ICharacter
{
    string Name { get; }

    int ReceiveAttack();

    void ShowPoints();

    void InteractInGame();
}
