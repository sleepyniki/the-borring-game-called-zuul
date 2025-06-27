using System.Collections.Generic;

class Room
{
	// Private fields
	public string description;
	private Dictionary<string, Room> exits; // stores exits of this room.
	private List<Item> items; // stores items in this room.
							  // Create a room described "description". Initially, it has no exits.
							  // "description" is something like "in a kitchen" or "in a court yard".
	private Inventory chest;

	public Inventory Chest
	{
		get { return chest; }
	}

	public Room(string desc)
	{
		description = desc;
		exits = new Dictionary<string, Room>();
		items = new List<Item>();
		chest = new Inventory(999999); // Initialize the inventory with a large capacity
	}


	// Define an exit for this room.
	public void AddExit(string direction, Room neighbor)
	{
		exits.Add(direction, neighbor);
	}
	// adds an item to the room
	public void AddItem(Item item)
	// property 
	{
		 chest.Putinbackpack(item);
	}
	// constructor 
	// public Room()
	// {
	// 	// a Room can handle a big Inventory. 
	// 	chest = new Inventory(999999);
	// }
	// Return the description of the room.
	public string GetShortDescription()
	{
		return description;
	}

	// Return a long description of this room, in the form:
	//     You are in the kitchen.
	//     Exits: north, west
	public string GetLongDescription()
	{
		string str = "You are ";
		str += description;
		str += ".\n";
		str += GetExitString();
		return str;
	}

	// Return the room that is reached if we go from this room in direction
	// "direction". If there is no room in that direction, return null.
	public Room GetExit(string direction)
	{
		if (exits.ContainsKey(direction))
		{
			return exits[direction];
		}
		return null;
	}

	// Return a string describing the room's exits, for example
	// "Exits: north, west".
	private string GetExitString()
	{
		string str = "Exits: ";
		str += String.Join(", ", exits.Keys);

		return str;
	}

	public Item GetItem(string itemName)
	{
		Item foundItem = items.Find(item => item.Name == itemName);
		if (foundItem != null)
		{
			return foundItem;
		}
		return null; // Item not found
	}

	public void Dropitem(string itemName)
	{
		Item itemToRemove = items.Find(item => item.Name == itemName);
		if (itemToRemove != null)
		{
			items.Remove(itemToRemove);
		}



	}
	public bool Showroom()
	{
		Console.WriteLine("room:");
		var chestItems = chest.GetItems(); // Assuming chest has a GetItems method
		if (chestItems.Count == 0)
		{
			Console.WriteLine("looks like there nothing in here");
			return false; // Indicate inventory is empty
		}
		else
		{
			foreach (var item in chestItems)
			{
				Console.WriteLine($"the item is a {item.Value.Description} and it Weight {item.Value.Weight}KG");
			}
			return true; // Indicate room has items
		}
	}
}