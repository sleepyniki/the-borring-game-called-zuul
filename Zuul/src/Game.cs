using System;

class Game
{
	// Private fields
	private Parser parser;
	private Player player;

	// Constructor
	public Game()
	{
		parser = new Parser();
		player = new Player();
		CreateRooms();
	}

	// Initialise the Rooms (and the Items)
	private void CreateRooms()
	{
		// Create the rooms
		Room bedroom = new Room("your bedroom this is were you sleep before you we're rudly interupted by the attack ");
		Room hallway = new Room("this is the main hallway there 4 doors you can go to");
		Room armory = new Room("this is the armery you can find wapens here");
		Room dinnerhall = new Room("this is the dinnerhall this is where everyone will get there food");
		Room kitchen = new Room("this is the kitchen we're food is made");
		Room innergarden = new Room("this is the indoors garden ");
		Room outsidegarden = new Room("this is the outdoor garden there a lot of plants");


		// Initialise room exits
		bedroom.AddExit("down", hallway);

		hallway.AddExit("up", bedroom);
		hallway.AddExit("east", innergarden);
		hallway.AddExit("north", dinnerhall);
		hallway.AddExit("west", armory);

		innergarden.AddExit("west", hallway);
		innergarden.AddExit("north", outsidegarden);

		outsidegarden.AddExit("east", hallway);

		armory.AddExit("east", hallway);


		dinnerhall.AddExit("north", kitchen);
		dinnerhall.AddExit("south", hallway);

		kitchen.AddExit("south", dinnerhall);

		// Create your Items here
		// Item sword = new (5, " knife");
		// Item enchanted-apple = new (5, " enchanted-apple");
		// Item toygun = new (5, "toygun");

		// And add them to the Rooms
		armory.AddItem(new Item(5, "knife"));
		dinnerhall.AddItem(new Item(5, "enchanted-apple"));
		bedroom.AddItem(new Item(5, "toygun"));


		// Start game bedroom
		player.CurrentRoom = bedroom;
	}

	//  Main play routine. Loops until end of play.
	public void Play()
	{
		PrintWelcome();

		// Enter the main command loop. Here we repeatedly read commands and
		// execute them until the player wants to quit.
		bool finished = false;
		while (!finished)
		{
			Command command = parser.GetCommand();
			finished = ProcessCommand(command);
		}
		Console.WriteLine("Thank you for playing.");
		Console.WriteLine("Press [Enter] to continue.");
		Console.ReadLine();
	}

	// Print out the opening message for the player.
	private void PrintWelcome()
	{
		Console.WriteLine();
		Console.WriteLine("Welcome to Zuul!");
		Console.WriteLine("Zuul is a new, incredibly boring adventure game.");
		Console.WriteLine("Type 'help' if you need help.");
		Console.WriteLine();
		Console.WriteLine(player.CurrentRoom.GetLongDescription());
	}

	// Given a command, process (that is: execute) the command.
	// If this command ends the game, it returns true.
	// Otherwise false is returned.
	private bool ProcessCommand(Command command)
	{
		bool wantToQuit = false;

		if (command.IsUnknown())
		{
			Console.WriteLine("I don't know what you mean...");
			return wantToQuit; // false
		}

		switch (command.CommandWord)
		{
			case "help":
				PrintHelp();              // print help information
				break;
			case "go":
				GoRoom(command);          // go to a room
				break;
			case "use":
				Console.WriteLine(player.Use(command.SecondWord));
				break;
			case "look":
				Console.WriteLine(player.CurrentRoom.GetLongDescription());        // look around the room
				Console.WriteLine(player.CurrentRoom.Showroom());                  // show items in the room
				break;
			case "status":
				player.Checkhealth();                                        // check player health
				break;
			case "check-backpack":
				Console.WriteLine(player.Inventory());                                        // check backpack
				break;
			case "take":
				player.TakeFromroom(command.SecondWord);           // take item from chest
				break;
			case "drop":
				player.DropToroom(command);             // drop item to chest
				break;
			case "quit":
				wantToQuit = true;                                  // quit the game
				break;
		}

		return wantToQuit;
	}

	// ######################################
	// implementations of user commands:
	// ######################################

	// Print out some help information.
	// Here we print the mission and a list of the command words.
	private void PrintHelp()
	{
		Console.WriteLine("You are a demon princess .");
		Console.WriteLine("heaven is attacking you need to escape the castle.");
		Console.WriteLine();
		// let the parser print the commands
		parser.PrintValidCommands();
	}

	// Try to go to one direction. If there is an exit, enter the new
	// room, otherwise print an error message.
	private void GoRoom(Command command)
	{
		if (!command.HasSecondWord())
		{
			// if there is no second word, we don't know where to go...
			Console.WriteLine("Go where?");
			return;
		}

		string direction = command.SecondWord;

		// Try to go to the next room.
		Room nextRoom = player.CurrentRoom.GetExit(direction);
		if (nextRoom == null)
		{
			Console.WriteLine("There is no door to " + direction + "!");
			return;
		}

		player.CurrentRoom = nextRoom;
		Console.WriteLine(player.CurrentRoom.GetLongDescription());
		if (player.Bleeding)
		{
			player.Damage(5);
		}
		player.Checkhealth();
	}


}
