using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BabelRPG
{
    internal class ItemManager
    {
        public List<Item> AllItems=new List<Item>();

        public void SetAllItems()
        {
            List<string> itemsStr = File.ReadAllLines("../../data/AllItems.csv").ToList();
            string[] sList;
            foreach(string str in itemsStr)
            {
                sList = str.Split(',');
                this.AllItems.Add(new Item(sList[0], sList[1], int.Parse(sList[2]), new int[] { int.Parse(sList[3]), int.Parse(sList[4]), int.Parse(sList[5]), int.Parse(sList[6]) }));
            }
        }
    }
}
