using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventureV2
{
    class Exit
    {
        Room leadsTo;
        string direction;
        private bool Locked { get; set; }
        private string lockType;
        private string lockName;
        private string lockId;
        public string lockDescription;

        public Exit(Room leadsTo, string direction)
        {
            this.leadsTo = leadsTo;
            this.direction = direction;
            Locked = false;
            lockType = "/NULL/";
            lockName = "/NULL/";
            lockId = "/NULL/";
            lockDescription = "/NULL/";
        }

        public Exit(Room leadsTo, string direction, bool locked, string lockName, string lockId, string lockType, string lockDescription)
        {
            this.leadsTo = leadsTo;
            this.direction = direction;
            Locked = locked;
            this.lockName = lockName;
            this.lockId = lockId;
            this.lockType = lockType;
            this.lockDescription = lockDescription;
        }

        public string GetDirection()
        {
            return direction;
        }

        public Room LeadsTo()
        {
            return leadsTo;
        }

        public bool IsLocked()
        {
            return Locked;
        }

        public string GetLockedDescription()
        {
            return lockName;
        }

        public void Unlock()
        {
            Locked = false;
        }

        public string GetId()
        {
            return lockId;
        }

        public string GetLockType()
        {
            return lockType;
        }
    }
}
