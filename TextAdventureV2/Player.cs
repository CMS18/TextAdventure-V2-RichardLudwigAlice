using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventureV2
{
    class Player
    {
        private string Name { get; set; }
        private string Age { get; set; }

        List<Item> inventory = new List<Item>();

        public Player()
        {
            Name = "";
            Age = "";
        }

        public void ShowInventory()
        {
            if (inventory.Count > 0)
            {
                Console.WriteLine("Your inventory consists of: ");
                foreach (Item item in inventory)
                {
                    Console.WriteLine(item.Name);
                }
            }
            else Console.WriteLine("Your inventory is empty.");
        }

        public string GetName()
        {
            return Name;
        }

        public void AddItem(Item item)
        {
            inventory.Add(item);
        }

        public void RemoveItem(Item item)
        {
            inventory.Remove(item);
        }

        public List<Item> GetInventory()
        {
            return inventory;
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetAge(string age)
        {
            Age = age;
        }

        public string GetAge()
        {
            return Age;
        }
    }
}
