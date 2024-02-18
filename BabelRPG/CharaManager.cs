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
        private Encoding SjisEnc = Encoding.GetEncoding("Shift_JIS");

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

            status.Add(this.RemakeWide((this.RemakeWide(c1.Name,8,' ') + " Lv" + c1.Level),30,' '));
            status.Add(this.RemakeWide(("HP          :" + c1.RecentHP +"/"+c1.HP+ "("+ (c1.Equip.BonusParams[0] >= 0 ? "+" : "") + c1.Equip.BonusParams[0] + ")"), 30, ' '));
            status.Add(this.RemakeWide(("攻撃        :" + c1.Attack +"("+ (c1.Equip.BonusParams[1] >= 0 ? "+" : "") + c1.Equip.BonusParams[1]+")"), 30, ' '));
            status.Add(this.RemakeWide(("防御        :" + c1.Deffence + "(" + (c1.Equip.BonusParams[2] >= 0 ? "+" : "")+ c1.Equip.BonusParams[2] + ")"), 30, ' '));
            status.Add(this.RemakeWide(("素早さ      :" + c1.Speed + "(" + (c1.Equip.BonusParams[3] >= 0 ? "+" : "")+ c1.Equip.BonusParams[3] + ")"), 30, ' '));
            status.Add(this.RemakeWide(("装備アイテム:" + c1.Equip.Name), 30, ' '));

            return status;
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

        private string RemakeWide(string str, int num, char pad)
        {
            int bytes = this.SjisEnc.GetByteCount(str);
            return str + new string(pad, num - bytes);
        }

    }


}
