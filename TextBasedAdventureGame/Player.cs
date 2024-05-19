namespace TextBasedAdventureGame;

using Spectre.Console;
using TextBasedAdventureGame.Constants;

internal class Player : Character
{
    private List<Item> _itemsList;
    private int _lifePoints;
    private int _attackPoints;

    public Player(string name, string description, int lifePoints, int attackPoints) : base(name, description)
    {
        this.name = name;
        this.description = description;
        _lifePoints = lifePoints;
        _attackPoints = attackPoints;
        _itemsList = new List<Item>();
    }

    public int AttackPoints
    {
        get => _attackPoints;
        set
        {
            _attackPoints = value;
        }
    }

    public int LifePoints
    {
        get => _lifePoints;
        set => _lifePoints = value;
    }

    public int ReceiveAttack(int attack)
    {
        return _lifePoints -= attack;
    }

    public void AddItem(Item item)
    {
        _itemsList.Add(item);
    }

    public string SelectOption()
    {
        var prompt = new TextPrompt<string>("[green]Selecciona un item para pelear[/]?");
        _itemsList.ForEach(item => prompt.AddChoice(item.Name));
        var selectItem = AnsiConsole.Prompt(prompt);

        return selectItem;
    }

    public void ShowInformation()
    {
        var showInformation = true;

        while(showInformation)
        {
            var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("[green]Informacion del Jugador[/]")
            .AddChoices(new[]
            {
                "Mostrar items", "Mostrar puntos de vida y ataque actuales", "Continuar con el juego"
            }));

            switch (option)
            {
                case "Mostrar items":
                    ShowItemsOnTable();
                    break;
                case "Mostrar puntos de vida y ataque actuales":
                    ShowPlayerPoints();
                    break;
                case "Continuar con el juego":
                    showInformation = false;
                    break;
            }
        }
    }

    public void ShowPlayerPoints()
    {
        var pointsTable = new Table();
        pointsTable.AddColumn($"[green]Tus puntos de vida: {_lifePoints}[/]");
        pointsTable.AddRow($"[green]Tus puntos de ataque: {_attackPoints}[/]");
        AnsiConsole.Write(pointsTable);
    }

    public void IncreasePower(Item item)
    {
        if (item.Type.Equals(ItemType.SANITY))
        {
            IncreaseLifePoints(PlayerConstants.LifePointsBySanity);
        }
        else if (item.Type.Equals(ItemType.VELOCITY))
        {
            IncreaseLifePoints(PlayerConstants.LifePointsByVelocity);
            IncreaseAttackPoints(PlayerConstants.AttackPointsByVelocity);
        }
        else if (item.Type.Equals(ItemType.POWER))
        {
            IncreaseLifePoints(PlayerConstants.LifePointsByPower);
            IncreaseAttackPoints(PlayerConstants.AttackPointsByPower);
        }
    }

    public int IncreaseLifePoints(int points)
    {
        return _lifePoints += points;
    }

    public Item SelectItemToFight()
    {
        string itemName = SelectItem();
        var item = SearchItem(itemName);
        return item;
    }

    private int IncreaseAttackPoints(int points)
    {
        return _attackPoints += points;
    }

    private List<string> GetItemNamesList()
    {
        List<string> itemNamesList = new List<string>();
        _itemsList.ForEach(item => itemNamesList.Add(item.Name));

        return itemNamesList;
    }

    private void ShowItemsOnTable()
    {
        var table = new Table();

        AnsiConsole.WriteLine("\nItems del jugador:");
        table.AddColumn(PlayerConstants.Item);
        table.AddColumn(PlayerConstants.Descripcion);
        table.AddColumn(PlayerConstants.Tipo);

        _itemsList.ForEach(item => table.AddRow(item.Name, item.Description, item.Type.ToString()));

        AnsiConsole.Write(table);
    }

    private Item SearchItem(string itemName)
    {
        return _itemsList.Where(item => itemName.Equals(item.Name)).First();
    }

    private string SelectItem()
    {
        var answer = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title($"[green]Selecciona un item para pelear:[/]")
                .AddChoices(GetItemNamesList()));

        return answer;
    }
}
