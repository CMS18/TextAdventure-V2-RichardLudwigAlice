using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventureV2
{
    class Room
    {
        private string Name { get; set; }
        private string Description { get; set; }
        private bool visitedBefore = false;

        private StringBuilder roomAndItemDescriptions = new StringBuilder();

        List<Item> inventory;
        List<Exit> exits;

        public Room()
        {
            Name = "";
            Description = "";
            inventory = new List<Item>();
            exits = new List<Exit>();
        }

        public void AddDescription(string description)
        {
            Description = description;
            roomAndItemDescriptions.Append(Description);
        }

        public void AddName(string name)
        {
            Name = name;
        }

        public void AddItem(Item item)
        {
            inventory.Add(item);
        }

        public void AddExit(Exit exit)
        {
            exits.Add(exit);
        }

        public string GetDescription()
        {
            roomAndItemDescriptions.Clear();
            roomAndItemDescriptions.Append(Description);

            foreach (Item item in inventory)
            {
                roomAndItemDescriptions.Append(" " + item.RoomDescription);
            }

            return roomAndItemDescriptions.ToString();            
        }

        public string GetName()
        {
            return Name;
        }

        public List<Item> GetInventory()
        {
            return inventory;
        }

        public void RemoveItem(Item item)
        {
            inventory.Remove(item);
        }

        public bool VisitedStatus()
        {
            return visitedBefore;
        }

        public void SetAsVisited()
        {
            visitedBefore = true;
        }

        public void SetAsUnvisited()
        {
            visitedBefore = false;
        }

        public List<Exit> GetExits()
        {
            return exits;
        }
    }
}
