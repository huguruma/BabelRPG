using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelRPG
{
    internal class Item
    {
        public string Name { get; private set; } = "";
        public string Description { get; private set; }
        public int ItemType { get;private set; }

        public int[] BonusParams=new int[4] {0,0,0,0};

        public Item() { }

        public Item(string name, string description, int itemType, int[] bonusParams)
        {
            this.Name = name;
            this.Description = description;
            this.ItemType = itemType;
            this.BonusParams = bonusParams;
        }

        public Item Clone()
        {
            return (Item)MemberwiseClone();
        }
    }
}



