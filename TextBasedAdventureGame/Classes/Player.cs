﻿namespace TextBasedAdventureGame.Classes;

using Spectre.Console;
using TextBasedAdventureGame.Constants;
using TextBasedAdventureGame.Enums;
using TextBasedAdventureGame.Interfaces;

internal class Player : ICharacter
{
    private string _name;
    private string _description;
    private List<Item> _itemsList;
    private int _lifePoints;
    public int AttackPoints { get; set; }

    public Player(string name, string description, int lifePoints, int attackPoints)
    {
        _name = name;
        _description = description;
        _lifePoints = lifePoints;
        AttackPoints = attackPoints;
        _itemsList = [];
    }

    public int LifePoints
    {
        get => _lifePoints;
        set => _lifePoints = value;
    }

    public string Name => _name;

    public Item Item => throw new NotImplementedException();

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
        var prompt = new TextPrompt<string>($"[green]{PlayerConstants.SelectItemToFight}[/]?");
        _itemsList.ForEach(item => prompt.AddChoice(item.Name));
        var selectItem = AnsiConsole.Prompt(prompt);

        return selectItem;
    }

    public void ShowInformation(params string[] options)
    {
        var showInformation = true;

        while (showInformation)
        {
            var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title($"[green]{PlayerConstants.PlayerInformation}[/]")
            .AddChoices(options));

            Dictionary<string, Action> actions = new Dictionary<string, Action>
            {
                {options[0], ShowItemsOnTable},
                {options[1], ShowPoints},
                {options[2], () => showInformation = false}
            };

            actions[option]();
        }
    }

    public void ShowPoints()
    {
        var pointsTable = new Table();
        pointsTable.AddColumn($"[green]{PlayerConstants.LifePoints} {_lifePoints}[/]");
        pointsTable.AddRow($"[green]{PlayerConstants.AttackPoints} {AttackPoints}[/]");
        AnsiConsole.Write(pointsTable);
    }

    public void InitialInteraction()
    {
        ShowInformation(PlayerConstants.DisplayItems, PlayerConstants.DisplayLifeAndAttackPoints, PlayerConstants.ContinueWithGame);
        IncreasePower(SelectItemToFight());
        ShowPoints();
    }

    public void InteractInGame(ICharacter character)
    {
        Console.WriteLine("Ataca con todo tu poder!!!");
        Console.Write($"{AnimationStrings.animation1}");
        Thread.Sleep(AnimationStrings.timeToFight);
        Console.WriteLine($"{AnimationStrings.animation2}");
        Thread.Sleep(AnimationStrings.timeToFight);
        Console.WriteLine($"{AnimationStrings.animation3}");
        Thread.Sleep(AnimationStrings.timeToFight);
        character.ReceiveAttack(AttackPoints);
    }

    public void IncreasePower(Item item)
    {
        Dictionary<Enum, Action> increasePoints = new Dictionary<Enum, Action>
        {
            {ItemType.SANITY, () => IncreaseLifePoints(PlayerConstants.LifePointsBySanity)},
            {ItemType.VELOCITY, IncreasePointsByVelocityItem},
            {ItemType.POWER, IncreasePointsByPowerItem}
        };

        increasePoints[item.Type]();
    }

    public int IncreaseLifePoints(int points)
    {
        return _lifePoints += points;
    }

    public void IncreasePointsByVelocityItem()
    {
        IncreaseLifePoints(PlayerConstants.LifePointsByVelocity);
        IncreaseAttackPoints(PlayerConstants.AttackPointsByVelocity);
    }

    public void IncreasePointsByPowerItem()
    {
        IncreaseLifePoints(PlayerConstants.LifePointsByPower);
        IncreaseAttackPoints(PlayerConstants.AttackPointsByPower);
    }

    public Item SelectItemToFight()
    {
        string itemName = SelectItem();
        var item = SearchItem(itemName);

        return item;
    }

    private int IncreaseAttackPoints(int points)
    {
        return AttackPoints += points;
    }

    private List<string> GetItemNamesList()
    {
        return _itemsList.Select(item => item.Name).ToList();
    }

    private void ShowItemsOnTable()
    {
        var table = new Table();

        AnsiConsole.WriteLine($"\n{PlayerConstants.PlayerItems}");
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
                .Title($"[green]{PlayerConstants.SelectItemToFight}[/]")
                .AddChoices(GetItemNamesList()));

        return answer;
    }
}
