using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventureV2
{
    class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string RoomDescription { get; set; }
        public bool Usable { get; set; }
        public string Id { get; set; }

        public Item()
        {
            Name = "";
            Description = "";
            Usable = false;
            Id = "";
        }

        public void AddName (string name)
        {
            Name = name;
        }

        public void AddDescription (string description)
        {
            Description = description;
        }
        
        public void AddRoomDescription (string roomDescription)
        {
            RoomDescription = roomDescription;
        }

        public void AddId (string id)
        {
            Id = id;
        }

        public void SetUsable(bool value)
        {
            Usable = value;
        }

        public bool IsUsable()
        {
            return Usable;
        }        

        public string GetId()
        {
            return Id;
        }
    }
}
