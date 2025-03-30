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
		Room outside = new Room("outside the main entrance of the university");
		Room theatre = new Room("in a lecture theatre");
		Room pub = new Room("in the campus pub");
		Room lab = new Room("in a computing lab");
		Room office = new Room("in the computing admin office");
		Room backstage = new Room("in the backstage of the theatre");
		Room uperpub = new Room("in the upper floor of the pub");


		// Initialise room exits
		outside.AddExit("east", theatre);
		outside.AddExit("south", lab);
		outside.AddExit("west", pub);

		theatre.AddExit("west", outside);
		theatre.AddExit("east", backstage);

		backstage.AddExit("west", theatre);

		pub.AddExit("east", outside);
		pub.AddExit("up", uperpub);

		uperpub.AddExit("down", pub);

		lab.AddExit("north", outside);
		lab.AddExit("east", office);

		office.AddExit("west", lab);

		// Create your Items here
		// Item sword = new (5, " knife");
		// Item bandage = new (5, " bandage");
		// Item toygun = new (5, "toygun");

		// And add them to the Rooms
		pub.AddItem(new Item(5, "knife"));
		lab.AddItem(new Item(5, "bandage"));
		theatre.AddItem(new Item(5, "toygun"));


		// Start game outside
		player.CurrentRoom = outside;
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
		Console.WriteLine("You are lost. You are alone.");
		Console.WriteLine("You wander around at the university.");
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
		player.Damage(5);
		if (player.Use("bandage") == "You used a bandage.")
		{
			player.Damage(0);
		}
		player.Checkhealth();
	}


}
