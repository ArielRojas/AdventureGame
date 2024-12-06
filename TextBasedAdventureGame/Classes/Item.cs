using TextBasedAdventureGame.Enums;

namespace TextBasedAdventureGame.Classes;

public class Item
{
    private readonly string _name;
    private readonly ItemType _type;
    private readonly string _description;

    public Item(string name, ItemType type, string description)
    {
        _name = name;
        _type = type;
        _description = description;
    }

    public string Name
    {
        get => _name;
    }

    public ItemType Type
    {
        get => _type;
    }

    public string Description
    {
        get => _description;
    }

    public string ShowItem()
    {
        return $"\nNombre del item: {Name} \nDescripcion: {Description} \nTipo: {Type.ToString()}\n";
    }
}
