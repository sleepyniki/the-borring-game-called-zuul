class Item
{
    // fields 
    public int Weight { get; }
    public string Description { get; }
    // name of the item
    public string Name { get; set; }
    // constructor 
    public Item(int weight, string name)
    {
        Weight = weight;
        Description = name;
    }

}