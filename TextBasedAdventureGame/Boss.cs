namespace TextBasedAdventureGame;

using Spectre.Console;

internal class Boss: NonPlayerCharacter
{
    private int _lifePoints;
    private int _attackPoints;

    public Boss(string name, string description, Item item, int lifePoints, int attackPoints) : base(name, description, item)
    {
        this.name = name;
        this.description = description;
        this.item = item;
        _lifePoints = lifePoints;
        _attackPoints = attackPoints;
    }

    public int LifePoints
    {
        get => _lifePoints;
    }

    public int AttackPoints
    {
        get => _attackPoints;
    }

    public override string ShowNonPlayerCharacter()
    {
        return base.ShowNonPlayerCharacter() + $"\nLife Points: {LifePoints}";
    }

    public int ReceiveAttack(int attack)
    {
        return _lifePoints -= attack;
    }

    public void ShowBossPoints()
    {
        var pointsTable = new Table();
        pointsTable.AddColumn($"[red]Puntos de vida de {name}: {_lifePoints}[/]");
        pointsTable.AddRow($"[red]Puntos de ataque de {name}: {_attackPoints}[/]");
        AnsiConsole.Write(pointsTable);
    }
}
