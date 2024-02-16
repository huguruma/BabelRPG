using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace BabelRPG
{
    internal class CharaManager
    {
        public List<Creature> AllCreatures=new List<Creature>();

        public void SetAllCreatures()
        {
            List<string> creaturesStr = File.ReadAllLines("../../data/AllCreatures.csv").ToList();
            string[] sList;
            foreach(string str in creaturesStr)
            {
                sList = str.Split(',');
                this.AllCreatures.Add(new Creature(sList[0], int.Parse(sList[1]), int.Parse(sList[2]), int.Parse(sList[3]), int.Parse(sList[4]), int.Parse(sList[5]),new Item(),new double[] { double.Parse(sList[6]), double.Parse(sList[7]), double.Parse(sList[8]), double.Parse(sList[9]) },new Dictionary<string, double>(), int.Parse(sList[10]), int.Parse(sList[11]), int.Parse(sList[12])));
            }
        }
        public void SetDropItems()
        {
            List<string> creaturesStr = File.ReadAllLines("../../data/DropItems.csv").ToList();
            string[] sList;
            foreach (string str in creaturesStr)
            {
                sList = str.Split(',');
                this.AllCreatures.Find(x => sList[0] == x.Name).DropItems.Add(sList[1], double.Parse(sList[2]));
            }
        }

        public List<Creature> AllFloorCreatures(int floor)
        {
            if (floor % 10 == 0) { return this.AllCreatures.Where(x => x.PopFloorMin == floor && x.PopFloorMax == floor).ToList(); }
            return this.AllCreatures.Where(x => x.PopFloorMin ==  x.PopFloorMax && x.PopFloorMin <= floor && x.PopFloorMax >= floor).OrderBy(x=>x.PopFloorMin).ToList();
        }

        public int PopCount(int floor)
        {
            int baseCount = 1;
            Random random = new Random();
            baseCount += (int)(floor / 20);
            return baseCount+random.Next(3)>5 ?5: baseCount + random.Next(3);
        }

        public List<Creature> EncountCreatures(int floor)
        {
            List < Creature > encount=new List<Creature>();
            List<Creature> allCreatures = this.AllFloorCreatures(floor);
            int popCount = this.PopCount(floor);
            List<int> creatureMaps=new List<int>();
            Random random = new Random();
            for (int i = 0; i < allCreatures.Count();i++)
            {
                for(int j = 0; j <= allCreatures.Count - i; j++)
                {
                    creatureMaps.Add(i);
                }
            }
            for(int i = 0; i < popCount; i++)
            {
                encount.Add(allCreatures[creatureMaps[random.Next(creatureMaps.Count())]].Clone());
            }
            return encount;
        }

        public int EncCount(int floor)
        {
            int baseCount = 1;
            Random random = new Random();
            baseCount += (int)(floor / 10);
            return baseCount + random.Next(3);
        }
        public List<List<Creature>> FloorEncount(int floor)
        {
            int encCount = this.EncCount(floor);
            List<List<Creature>> allEncount=new List<List<Creature>>();
            if (floor % 10 == 0)
            {
                allEncount.Add(this.AllFloorCreatures(floor));
            }
            else
            {
                for (int i = 0; i < encCount; i++)
                {
                    allEncount.Add(this.EncountCreatures(floor));
                }
            }
            return allEncount;
        }

        public List<string> GetStatus(Chara c1)
        {
            List<string> status = new List<string>();
            int padSize = 25;
            int diff = 0;
            status.Add(this.GetDiff(c1.Name + " Lv" + c1.Level, out diff).PadRight(padSize - diff));
            status.Add(this.GetDiff("HP  :" + c1.HP + "(+" + c1.Equip.BonusParams[0] + ")",out diff).PadRight(padSize-diff));
            status.Add(this.GetDiff("攻撃 :" + c1.Attack +"(+"+ c1.Equip.BonusParams[1]+")", out diff).PadRight(padSize - diff));
            status.Add(this.GetDiff("防御 :" + c1.Deffence + "(+" + c1.Equip.BonusParams[2] + ")", out diff).PadRight(padSize - diff));
            status.Add(this.GetDiff("素早さ:" + c1.Speed + "(+" + c1.Equip.BonusParams[3] + ")", out diff).PadRight(padSize - diff));
            status.Add(this.GetDiff("装備アイテム:" + c1.Equip.Name, out diff).PadRight(padSize - diff));

            Console.WriteLine(string.Join(",",status));
            return status;
        }

        public string GetDiff(string str,out int diff)
        {
            int count=0;
            foreach(char c in str)
            {
                if(!Regex.IsMatch(new string(c,1), @"[ -~｡-ﾟ]"))
                {
                    count++;
                }
            }
            diff=count;
            return str;
        }

        public List<string> GetAllStatus(List<Chara> cList)
        {
            List<List<string>> allStatus = new List<List<string>>();
            List<string> strings = new List<string>();
            foreach (Chara c1 in cList)
            {
                allStatus.Add(this.GetStatus(c1));
            }

            for (int i = 0; i < allStatus[0].Count(); i++)
            {
                strings.Add("");
                for (int j= 0; j < allStatus.Count(); j++)
                {
                strings[i] += allStatus[j][i]+"\t";

                }
            }
            return strings;
        }
    }
}
