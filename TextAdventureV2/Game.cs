﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace TextAdventureV2
{
    class Game
    {
        Player player;

        Room currentRoom;
        Room hospitalBedroom;
        Room hallway;
        Room bathroom;

        List<Room> allRooms = new List<Room>();
        List<Item> playerInventory = new List<Item>();
        List<Item> roomInventory = new List<Item>();
        List<Exit> exits = new List<Exit>();

        Item key;
        Item journal;

        private bool gameIsRunning = true;

        public Game()
        {
            this.StartPage();
            this.InitializePlayer();
            this.InitializeItems();
            this.InitializeRooms();            
            this.Introduction();
            this.Update();
        }

        private void StartPage()
        {
            // Skriv ut namn på spelet osv.
            Int32 width = Console.LargestWindowWidth;
            Int32 height = Console.LargestWindowHeight;
            Console.Title = "The Escape from Zombie Hospital";            
            Console.WindowWidth = width/2;
            Console.WindowHeight = height/2;
            Console.SetWindowPosition(Console.WindowLeft, Console.WindowTop);
            //Console.SetWindowSize(Width, Height);
            Console.WriteLine("The escape from zombie hospital V0.1");
            this.Pause();
            Console.Clear();
        }

        private void InitializeItems()
        {
            key = new Item();
            key.AddName("KEY");
            key.AddDescription("An old brass key. Looks just like any other.");
            key.AddRoomDescription("A brass key lays on the floor.");
            key.AddId("id1");
            key.SetUsable(true);

            journal = new Item();
            journal.AddName("JOURNAL");
            journal.AddDescription("The journal reads:\n"
                + $"NAME: {player.GetName()}\n"
                + $"AGE: {player.GetAge()}\n"
                + "SYMPTONS: Coma.");
            journal.AddRoomDescription("There is a thrown journal on the ground.");

        }

        private void InitializeRooms()
        {
            hospitalBedroom = new Room();
            hospitalBedroom.AddName("HOSPITAL BEDROOM");
            hospitalBedroom.AddDescription("There is an empty bed with blood on it right next to yours.\n"
                + "You can also see a door to the west wall that seems to be leading into the bathroom.\n"
                + "There is also another door on the east wall.");
            hospitalBedroom.AddItem(journal);

            bathroom = new Room();
            bathroom.AddName("BATHROOM");
            bathroom.AddDescription("The bathroom looks clean but like someone left in a rush.\n"
                + "The water is still running and the toilet has not been flushed (Ugh!).\n"
                + "There is also a broken window here and alot of broken glass lying around.");
            bathroom.AddItem(key);

            hallway = new Room();
            hallway.AddName("HALLWAY");
            hallway.AddDescription("There's no people around and everything else seems fine.\n"
                + "There's an empty reception with a computer and a phone at the far end of the hallway.\n"
                + "To the east the corridor turns and to the north is an emergency exit");

            hospitalBedroom.AddExit(new Exit(hallway, "East", true, "The door is locked", "id1", "door", "A white door. Looks like it leads out to the hall"));
            hospitalBedroom.AddExit(new Exit(bathroom, "West"));
            hallway.AddExit(new Exit(hospitalBedroom, "West"));
            bathroom.AddExit(new Exit(hospitalBedroom, "East"));

            allRooms.Add(hospitalBedroom);
            allRooms.Add(bathroom);
            allRooms.Add(hallway);

            currentRoom = hospitalBedroom;
        }

        private void InitializePlayer()
        {
            player = new Player();
            Console.WriteLine("What is your name? ");            
            player.SetName(Console.ReadLine());
            Console.WriteLine("How old are you? ");
            player.SetAge(Console.ReadLine());

            Console.Clear();
        }

        private void Introduction()
        {
            Console.WriteLine($"You wake up in a hospital bed dressed in a patient gown.\n"
                + "The dark room is only being lit up by a small dingy light on the oppside side of the room.\n"
                + "The clock above the door on the east wall ticks with a loud \"TICK, TOCK\" in an otherwise silent room.\n"
                + "You unplug the IV from your arm and start to get up...");
            Pause();
        }

        private void Update()
        {
            do
            {
                Console.WriteLine("");
                CurrentRoomDescription();
                Console.Write("> ");
                string input = Console.ReadLine().ToUpper();
                input = Regex.Replace(input, "THE", "");                
                string[] inputArray = input.Split(' ');

                if (inputArray[0] == "LOOK")
                {
                    currentRoom.SetAsUnvisited();
                    continue;
                }

                if (inputArray[0] == "GET" || inputArray[0] == "TAKE" || inputArray[0] == "PICK")
                {
                    if (inputArray[1] == "UP")
                    {
                        Get(inputArray.Skip(2).ToArray());
                        continue;
                    }
                    Get(inputArray.Skip(1).ToArray());
                }
                else if (inputArray[0] == "INVENTORY" || inputArray[0] == "I")
                {
                    player.ShowInventory();
                }
                else if (inputArray[0] == "DROP")
                {
                    Drop(inputArray.Skip(1).ToArray());
                }
                else if (inputArray[0] == "GO")
                {
                    Go(inputArray.Skip(1).ToArray());
                }
                else if (inputArray[0] == "NORTH" || inputArray[0] == "EAST" || inputArray[0] == "SOUTH" || inputArray[0] == "WEST")
                {
                    Go(inputArray);
                }
                else if (inputArray[0] == "INSPECT")
                {
                    Inspect(inputArray.Skip(1).ToArray());
                }
                else if (inputArray[0] == "USE")
                {
                    Use(inputArray.Skip(1).ToArray());
                }
                else if (inputArray[0] == "H")
                {
                    ShowHelp();
                }
                else
                {
                    Console.WriteLine("What?");
                    continue;
                }

            } while (gameIsRunning);

        }

        public void CurrentRoomDescription()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(currentRoom.GetName());
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" (Type h for Help)");
            Console.ResetColor();
            if (!currentRoom.VisitedStatus())
            {
            Console.WriteLine(currentRoom.GetDescription());
            }
            currentRoom.SetAsVisited();
        }

        private void Pause()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Press enter to continue...");
            Console.ResetColor();
            Console.ReadLine();
        }

        private void Get(string[] input)
        {
            roomInventory = currentRoom.GetInventory();
            if (input.Length < 1)
            {
                Console.WriteLine("What do you want to pick?");
                input = Console.ReadLine().ToUpper().Split(' ');
            }

            foreach(Item item in roomInventory)
            {
                if (input.Contains(item.Name))
                {
                    Console.WriteLine("Taken.");
                    currentRoom.RemoveItem(item);
                    player.AddItem(item);                   

                    return;
                }                
            }
            Console.WriteLine("What?");
        }

        private void Drop(string[] input)
        {
            playerInventory = player.GetInventory();

            foreach(Item item in playerInventory)
            {
                if (item.Name == input[0])
                {
                    player.RemoveItem(item);
                    currentRoom.AddItem(item);
                    Console.WriteLine("You dropped the " + input[0].ToLower() + ".");
                    return;
                }
            }
            Console.WriteLine("You dont have a " + input[0].ToLower() + " in your inventory.");
        }

        private void Go(string[] input)
        {
            exits = currentRoom.GetExits();
            foreach (Exit exit in exits)
            {
                if (exit.GetDirection().ToUpper() == input[0])
                {
                    if (!exit.IsLocked())
                    {                   
                        currentRoom = exit.LeadsTo();
                        return;
                    }
                    else if (exit.IsLocked())
                    {
                        Console.WriteLine(exit.GetLockedDescription());
                        return;
                    }
                }
            }
            Console.WriteLine("Can't go that way.");
        }

        private void Inspect(string[] input)
        {
            playerInventory = player.GetInventory();
            exits = currentRoom.GetExits();
            foreach (Item item in playerInventory)
            {
                if (item.Name == input[0])
                {
                    Console.WriteLine(item.Description);
                    return;
                }
            }
            foreach (Exit exit in exits)
            {
                if (exit.GetLockedDescription() == input[0])
                {
                    Console.WriteLine(exit.lockDescription);
                    return;
                }
            }
        }

        private void Use(string[] input)
        {
            playerInventory = player.GetInventory();
            exits = currentRoom.GetExits();
            foreach (Item item in playerInventory)
            {
                if (input.Contains(item.Name.ToUpper()))
                {
                    if (item.IsUsable())
                    {
                        if (input.Length < 3)
                        {
                            Console.WriteLine("On what?");
                            input = Console.ReadLine().ToUpper().Split(' ');
                        }
                        foreach (Exit exit in exits)
                        {
                            if (input.Contains(exit.GetLockType().ToUpper()))
                            {
                                if (exit.GetId() == item.GetId())
                                {
                                    exit.Unlock();
                                    Console.WriteLine("Unlocked.");
                                    player.RemoveItem(item);
                                    return;
                                }
                            }
                        }
                        Console.WriteLine("Can't use (the) " + item.Name.ToLower() + " on that.");
                        return;
                        
                    } else
                    {
                        Console.WriteLine("Can't use that");
                        return;
                    }
                }
            }
            Console.WriteLine("You dont have that in your inventory.");
        }

        private void ShowHelp()
        {
            Console.WriteLine("\nCommand                Description.");
            Console.WriteLine("\nH                        Help.");
            Console.WriteLine("LOOK                     Look around the room.");
            Console.WriteLine("GET/TAKE/PICK/PICK UP    Pick up something.");
            Console.WriteLine("INVENTORY/I              Check your inventory.");
            Console.WriteLine("DROP ...                 Drop an item from your inventory.");
            Console.WriteLine("GO ...                   Tries to go a certain direction");
            Console.WriteLine("INSPECT ...              Inspects your inventory.");
            Console.WriteLine("USE ...                  Use an item from your inventory.");            
        }
    }
}