using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BabelRPG
{
    internal class BattleManager
    {
        ItemManager IMng;
        CharaManager CMng;
        PlayData PData;
        List<List<Creature>> AllEncount;
        Chara Caught = null;
        public int BattleCount = 0;
        public bool BattleNow = false;
        public List<int> GotExp = new List<int>();
        public List<string> GotItems = new List<string>();
        public bool EscapeFlg = false;

        public BattleManager(ItemManager iMng, CharaManager cMng, PlayData pData, int floor)
        {
            this.IMng = iMng;
            this.CMng = cMng;
            this.PData = pData;
            this.AllEncount = this.CMng.FloorEncount(floor);
        }

        public string BattlePhase(int input,int target=-1,string i1="")
        {
            this.BattleNow = true;
            if (!this.CheckBattleFinished())
            {
                if (input == 0 || input == 1 || input == 3)
                {
                    return Battle(input, target, i1);
                }
            }
            else
            {
                List<string> result = this.Result();
                switch (result[1])
                {
                    case "win":
                        this.BattleCount++;
                        return "Victory!!\r\n\r\n" + result[2];
                    case "lose":
                        this.BattleCount = -1;
                        return "You Lose... \r\n\r\n"+result[2];
                    case "esc":
                        this.BattleCount++;
                        return "バトルから離脱した! \r\n\r\n" + result[2];
                }
            }
            return "";
        }

        private List<string> texts(string head = "", string body = "", string msgBox = "")
        {
            return new List<string> { head, body, msgBox };
        }

        private string Battle(int command, int target = 0, string i1 = "")
        {
            string log = "";
            List<Chara> orders = this.PData.MyCharas.Concat(this.AllEncount[BattleCount]).OrderByDescending(x => x.Speed).ToList();
            Random rand = new Random();
            foreach (Chara c1 in orders)
            {
                if (this.CheckBattleFinished()) break;

                if (c1.RecentHP == 0)
                {
                    if (this.PData.MyCharas.Any(x => x == c1))
                    {
                        if (c1.Name == this.PData.Name)
                        {
                            switch (command)
                            {
                                case 1:
                                    log += this.Attack(c1, this.AllEncount[BattleCount][target]);
                                    break;
                                case 2:
                                    log += this.UseItemFor(this.PData.MyCharas[target], this.PData.MyItems.Keys.ToList().Find(x => x.Name == i1));
                                    break;
                                case 3:
                                    log += this.PData.Name + (this.PData.MyCharas.Count() > 1 ? "達" : "" + "は逃げ出した!\r\n");
                                    if (rand.Next(100) < Math.Pow(this.PData.MyCharas[0].Level / this.AllEncount[BattleCount][0].Level, 2) * 100)
                                    {
                                        this.EscapeFlg = true;
                                        log += "なんとか逃げ切れた!\r\n";
                                    }
                                    else
                                    {
                                        log += "逃げ切れなかった!\r\n";
                                    }
                                    break;
                            }

                        }
                        else
                        {
                            log += this.Attack(c1, this.AllEncount[BattleCount][rand.Next(this.AllEncount[BattleCount].Count())]);
                        }

                        foreach (Creature c2 in this.AllEncount[BattleCount].Where(x => x.RecentHP == 0))
                        {
                            this.AllEncount[BattleCount].Remove(c2);
                            foreach (string i2 in c2.DropItems.Keys)
                            {
                                if (rand.Next(10) < c2.DropItems[i2] * 10)
                                {
                                    this.GotItems.Add(i2);
                                    
                                    log += c2.Name + "は" + i2 + "をドロップした。\r\n";
                                }
                            }
                            int i = 0;
                            foreach (Chara c3 in this.PData.MyCharas)
                            {
                                GotExp[i] += c2.Exp / 3 * c2.Level / c3.Level;
                                i++;
                            }
                            if (this.Caught != null)
                            {
                                if (rand.Next(100) < c2.CatchPer && this.PData.MyCharas.Count() < 3)
                                {
                                    this.Caught = c2;
                                }
                            }
                        }
                    }
                    else
                    {
                        log += this.Attack(c1, this.PData.MyCharas.Where(x => x.RecentHP != 0).ToList()[rand.Next(this.PData.MyCharas.Where(x => x.RecentHP != 0).Count())], true);
                    }
                }
            }
            return log;
        }

        private string Attack(Chara c1, Chara c2, bool isEnemy = false)
        {
            Random random = new Random();
            int damage = 10 * c1.Attack / (c2.Deffence+1) * c1.Level / c2.Level * (90 + random.Next(11)) / 100;
            if (c2.RecentHP > damage)
            {
                c2.RecentHP -= damage;
            }
            else
            {
                c2.RecentHP = 0;
            }
            string log = c1.Name + "の攻撃！\r\n" + c2.Name + "は" + damage + "のダメージ！\r\n" + (c2.RecentHP == 0 ? (isEnemy ? "敵の" : "") + c2.Name + "は倒れた！\r\n" : "");

            return log;
        }

        private string UseItemFor(Chara c1, Item i1)
        {
            string log = "";

            log += this.PData.Name + "は" + c1.Name + "に" + i1 + "を使用。\r\n";
            if (c1.RecentHP > 0)
            {
                log+=this.PData.UseItem(c1,i1);
            }
            else
            {
                log += c1.Name + "は倒れているため効果がなかった。\r\n";
            }
            return log;
        }

        public bool CheckBattleFinished()
        {
            return this.AllEncount.Count() == 0 || this.PData.MyCharas[0].RecentHP == 0 || this.EscapeFlg;
        }

        public List<string> Result()
        {
            string result = "";
            string log = "";

            if (this.AllEncount.Count() == 0 ||this.EscapeFlg)
            {
                result = this.EscapeFlg ? "esc":"win";
                int i = 0;
                foreach(Chara c1 in this.PData.MyCharas)
                {
                    int pastHP = c1.HP;
                    log += c1.Name + "は" + this.GotExp[i] + "Exp獲得！\r\n";
                    c1.Exp += this.GotExp[i];
                    c1.RecentHP=c1.HP-pastHP;
                }
                foreach(string i1 in this.GotItems)
                {
                    log += i1 + "をゲット!\r\n";
                    this.PData.PutItem(i1);
                }
            }else if(this.PData.MyCharas[0].RecentHP == 0)
            {
                result = "lose";
                log += "戦闘に敗北した...\r\n";
               foreach(Chara c1 in this.PData.MyCharas)
                {
                    log += c1.Name + "は" + (int)c1.Exp/10 + "Exp失った...\r\n";
                    c1.Exp = c1.Exp - (int)(c1.Exp / 5);
                }
                log += "\r\n" + this.PData.Name + (this.PData.MyCharas.Count() > 1 ? "達" : "") + "は塔から撤退した...\r\n";
            }
            this.GotExp.Clear();
            this.GotItems.Clear();
            this.BattleNow = false;

            return new List<string> { result, log };
        }
    }
}
