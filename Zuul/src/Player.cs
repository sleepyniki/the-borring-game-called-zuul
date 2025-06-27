class Player
{
    // auto property 

    // fields 
    public int health;

    private bool bleeding;

    private bool won;
    private Inventory backpack;
    public Room CurrentRoom { get; set; }

    public bool Bleeding { get { return bleeding; } }

    public bool wins {get { return wins; } }

    // constructor 
    public Player()
    {
        CurrentRoom = null;                 // player starts with no room
        health = 100;                       // player starts with 100 health
        bleeding = true;                  // player starts with bleeding
        // won = false;                        // player does not start having won
        backpack = new Inventory(25);       // player starts with an empty backpack that can fit 25KG
    }

    // method to check health
    public void Checkhealth() { Console.WriteLine("the player has " + health + " health"); } // displays the player's current health
    // methods 
    public void Damage(int amount) { health -= amount; if (health < 0) { health = 0; } if (health == 0) { Console.WriteLine("you have died"); } } // player loses some health 
    public void Heal(int amount) { health += amount; Console.WriteLine("player health increased by " + amount); } // player's health restores 
    public bool IsAlive() { return health > 0; } // checks whether the player is alive or not   
    public string Inventory() { string str = "You are carrying:"; if (backpack.IsEmpty()) { return "its all empty"; } foreach (KeyValuePair<string, Item> item in backpack.GetItems()) { str += " " + item.Key; } return str; }

    public string Use(string itemName)
    {
        if (itemName == null)
        {
            return "what do you want to use again?";
        }

        Item item = backpack.Get(itemName);
        if (item == null)
        {
            return "there not a " + itemName + " in your backpack";
        }

        if (itemName == "toygun")
        {
            Console.WriteLine("you use a toygun ");
            Console.WriteLine("a flag pops out of the toygun it does nothing");
            return "You used a toygun.";
        }

        if (itemName == "enchanted-apple")
        {
            Console.WriteLine("you ate the enchanted apple it healed your injurys");
            // health += 5;
            bleeding = false;
            backpack.Remove("enchanted-apple"); // Remove the item from the backpack

            return "You used a enchanted apple.";
        }

        if (itemName == "knife")
        {
            Console.WriteLine("you take the knife out of your backpack");
            Random random = new Random();
            string[] respones = {
            "you look at the knife it looks sharp",
            "you look at the knife it looks like a knife",
            "you try to do a cool trick with the knife but you drop it dealing 20 damage to yourself",
            "when you look at the knife you see a reflection of yourself",
            "you look at the knife it reminds you of the time you failed to make a sandwich",
            "you look at the knife perfect Weapon for a royal"
            };
            string randomResponse = respones[random.Next(respones.Length)];
            Console.WriteLine(randomResponse);                  // finds the random response
            if (randomResponse.Contains("dealing 20 damage")) // find respond 2
            {
                Damage(20);                                    // Deal 20 damage to the player
                Console.WriteLine(health);                     // Display the player's health after taking damage
            }
        }

        return "";
    }

    public bool TakeFromroom(string itemName)
    {
        Item item = CurrentRoom.Chest.Get(itemName);

        if (item != null)
        {
            if (backpack.Putinbackpack(item))
            {
                CurrentRoom.Chest.Remove(itemName);                     // Remove the item from the room's chest
                Console.WriteLine($"You have picked up {itemName}");    // Notify the user that the item has been picked up
                return true;
            }
            else if (item == null)
            {
                CurrentRoom.Chest.Putinbackpack(item);
                Console.WriteLine($"you have realized there nothing to pick up");
                return false;
            }
        }
        return false;

        
    }
    public void DropToroom(Command command)
    {

        Item item = backpack.Get(command.SecondWord);
        if (item != null)
        {
            CurrentRoom.Chest.Putinbackpack(item);
            Console.WriteLine("You dropped the " + command.SecondWord);
        }
        else
        {
            Console.WriteLine("You forgot what you wanted to drop");
        }
    }

}
