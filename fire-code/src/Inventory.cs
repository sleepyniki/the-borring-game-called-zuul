

class Inventory
{
    // fields
    private int maxWeight;
    private Dictionary<string, Item> items;

    // constructor
    public Inventory(int maxWeight)
    {
        this.maxWeight = maxWeight;
        this.items = new Dictionary<string, Item>();
    }

    public int TotalWeight()
    {
        int Weaight = 0;
        foreach (KeyValuePair<string, Item> item in items)
        {
            Weaight += item.Value.Weight;
        }
        return Weaight;
    }
    public int FreeWeight()
    {
        return maxWeight - TotalWeight();
    }
    public int MaxWeight()
    {
        return maxWeight;
    }

    public bool Putinbackpack(Item item)
    {
        // Check the Weight of the Item and check for enough space in the Inventory
        // Does the Item fit?
        if (item.Weight <= FreeWeight())
        {
            // Put Item in the items Dictionary
            items[item.Description] = item;
            return true;
        }

        return false;
    }

    public Item Get(string itemName)
    {
        if (itemName == null)
        {
            return null;
        }
        if (items.ContainsKey(itemName))
        {
            Item foundItem = items[itemName];
            return foundItem;
        }

        return null;
    }

    public Dictionary<string, Item> GetItems()
    {
        return items;
    }

    public bool IsEmpty()
    {
        return items.Count == 0;
    }

    public void Remove(string itemName)
    {
        if (items.ContainsKey(itemName))
        {
            items.Remove(itemName);
        }
    }

}