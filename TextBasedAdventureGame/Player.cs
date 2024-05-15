namespace TextBasedAdventureGame;

using Spectre.Console;

internal class Player : Character
{
    private Item[] _itemsList;
    private int _lifePoints;
    private int _attackPoints;
    private int _numberOfItems;

    public Player(string name, string description, int lifePoints, int attackPoints) : base(name, description)
    {
        this.name = name;
        this.description = description;
        _lifePoints = lifePoints;
        _attackPoints = attackPoints;
        _itemsList = new Item[11];
        _numberOfItems = 0;
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

    public int IncreaseAttackPoints(int points)
    {
        return _attackPoints += points;
    }

    public int IncreaseLifePoints(int points)
    {
        return _lifePoints += points;
    }

    public int ReceiveAttack(int attack)
    {
        return _lifePoints -= attack;
    }

    public void AddItem(Item item)
    {
        _itemsList[_numberOfItems] = item;
        _numberOfItems++;
    }

    public Item[] GetItems()
    {
        return _itemsList;
    }

    public string SelectOption()
    {

        var prompt = new TextPrompt<string>("[green]Selecciona un item para pelear[/]?");
        for (int count = 0; count < _numberOfItems; count++)
        {
            prompt.AddChoice(_itemsList[count].Name);
        }

        var selectItem = AnsiConsole.Prompt(prompt);
        return selectItem;
    }

    public Item searchItem(string itemName)
    {
        Item? item = null;
        for (int count = 0; count < _numberOfItems; count++)
        {
            if (itemName.Equals(_itemsList[count].Name))
            {
                item = _itemsList[count];
            }
        }

        return item;
    }

    public string SelectItem()
    {
        var answer = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title($"[green]Selecciona un item para pelear:[/]")
                .AddChoices(GetItemNamesList().ToArray()));

        return answer;
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

    private string[] GetItemNamesList()
    {
        string[] itemNamesList = new string[_numberOfItems];
        for (int count = 0; count < _numberOfItems; count++)
        {
            itemNamesList[count] = _itemsList[count].Name;
        }

        return itemNamesList;
    }

    private void ShowItemsOnTable()
    {
        var table = new Table();

        AnsiConsole.WriteLine("\nItems del jugador:");
        table.AddColumn("Item");
        table.AddColumn("Descripcion");
        table.AddColumn("Tipo");

        for (int count = 0; count < _numberOfItems; count++)
        {
            table.AddRow(_itemsList[count].Name, _itemsList[count].Description, _itemsList[count].Type.ToString());
        }
        AnsiConsole.Write(table);
    }
}
