namespace TextBasedAdventureGame.Classes;

using Spectre.Console;
using TextBasedAdventureGame.Interfaces;

internal class Boss : ICharacter
{
    private string _name;
    private string _description;
    private Item _item;
    private int _lifePoints;
    private int _attackPoints;

    public Boss(string name, string description, Item item, int lifePoints, int attackPoints)
    {
        _name = name;
        _description = description;
        _item = item;
        _lifePoints = lifePoints;
        _attackPoints = attackPoints;
    }

    public string Name => _name;

    public int LifePoints
    {
        get => _lifePoints;
    }

    public int AttackPoints
    {
        get => _attackPoints;
    }

    public int ReceiveAttack(int attack)
    {
        return _lifePoints -= attack;
    }

    public void ShowPoints()
    {
        var pointsTable = new Table();
        pointsTable.AddColumn($"[red]Puntos de vida de {_name}: {_lifePoints}[/]");
        pointsTable.AddRow($"[red]Puntos de ataque de {_name}: {_attackPoints}[/]");
        AnsiConsole.Write(pointsTable);
    }

    public void InteractInGame()
    {
        throw new NotImplementedException();
    }
}
