using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace BabelRPG
{
    internal class PlayData
    {
        public string Name;
        public Chara Hero;
        public string JobType;
        public List<Chara> MyCharas = new List<Chara>();
        public Dictionary<Item, int> MyItems = new Dictionary<Item, int>();
        public ItemManager IMng;
        public CharaManager CMng;
        public int MaxFloor=1;
        private Dictionary<string, double[]> jobTypes =new Dictionary<string, double[]> 
        { 
            {"Attack",new double[] {3,5,2,1 } },
            {"Deffence",new double[] {3,2,4,2 } },
            {"Speed",new double[] {2,3,1,5 } },
        };

        public PlayData(bool isNewGame, ItemManager iMng, CharaManager cMng,string name ="",string jobType="")
        {
            this.IMng = iMng;
            this.CMng = cMng;
            if (isNewGame)
            {
                this.Name = name;
                this.JobType = jobType;
                this.MyCharas.Add(this.Hero=this.makeHero(this.Name, 600, "", jobType));
            }
            else 
            {
                string dataStr = File.ReadAllText("../../data/PlayData.csv");
                string[] sList = dataStr.Split(',');
                this.Name = sList[0];
                this.JobType = sList[3];
                this.MyCharas.Add(this.Hero=this.makeHero(this.Name, int.Parse(sList[1]), sList[2], sList[3]));
                this.MaxFloor = int.Parse(sList[4]);
                this.SetMyItems();
                this.SetMyChara();
            }
        }

        public void SetMyItems()
        {
            List<string> itemsStr = File.ReadAllLines("../../data/MyItems.csv").ToList();
            string[] sList;
            foreach (string str in itemsStr)
            {
                sList = str.Split(',');
                this.MyItems.Add(this.IMng.AllItems.Find(x => x.Name == sList[0]), int.Parse(sList[1]));
            }
        }

        public void SetMyChara()
        {
            List<string> itemsStr = File.ReadAllLines("../../data/MyCharas.csv").ToList();
            string[] sList;
            Chara c1;
            Item i1 = new Item();
            foreach (string str in itemsStr)
            {
                sList = str.Split(',');
                c1=this.CMng.AllCreatures.Find(x => x.Name == sList[0]).Clone();
                c1.Exp = int.Parse(sList[1]);
                if (sList[2] != "") 
                {
                    i1= IMng.AllItems.Find(x => x.Name == sList[2]).Clone(); 
                }
                c1.Equip = i1;
                this.MyCharas.Add(c1);
            }
        }

        private Chara makeHero(string name,int exp,string equip,string jobType)
        {
            Item i1 = new Item();
            if (equip != "")
            {
                i1 = IMng.AllItems.Find(x => x.Name == equip).Clone();
            }
            return new Chara(name, exp,
                100, 50, 50, 50, i1, this.jobTypes[jobType]);
        }

        public void SaveData()
        {
            string playDataList = string.Join(",",new string[] { this.Name, this.Hero.Exp.ToString(),this.Hero.Equip.Name,this.JobType,this.MaxFloor.ToString()});
            List<Item> keyList = this.MyItems.Keys.ToList();
            List<string> ItemDataList = keyList.Select(x => string.Join(",",new string[] { x.Name, this.MyItems[x].ToString() }) ).ToList();
            this.MyCharas.RemoveAt(0);
            List<string> CharaDataList = this.MyCharas.Select(x => string.Join(",", new string[] { x.Name, x.Exp.ToString(), x.Equip.Name })).ToList();
            File.WriteAllText("../../data/PlayData.csv", playDataList);
            File.WriteAllText("../../data/MyItems.csv", ItemDataList.ToString());
            File.WriteAllText("../../data/MyCharas.csv", CharaDataList.ToString());
        }

        public void GetItem(string itemName)
        {
            Item i1;
            if ((i1=this.MyItems.Keys.ToList().Find(x => x.Name == itemName)) != null){
                this.MyItems[i1]+=1;
            }
            else
            {
                this.MyItems[this.IMng.AllItems.Find(x => x.Name == itemName)] = 1;
            }
        }

        public void UseItem(string itemName)
        {
            Item i1;
            if ((i1 = this.MyItems.Keys.ToList().Find(x => x.Name == itemName)) != null)
            {
                this.MyItems[i1] -= 1;
                if(this.MyItems[i1] == 0)
                {
                    this.MyItems.Remove(i1);
                }
            }
            
        }

        public string ShowAllStatus()
        {
            return string.Join("\r\n",this.CMng.GetAllStatus(this.MyCharas));
        }

        public string ShowStatus(int index)
        {
            return string.Join("\r\n", this.CMng.GetStatus(this.MyCharas[index]));
        }

    }
}
