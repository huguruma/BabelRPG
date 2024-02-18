using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using System.Text;

namespace BabelRPG
{
    internal class ItemMenuManager
    {
        private Dictionary<Item, int> Originals;
        private Dictionary<Item, int> MyItems;
        public int Index = 0;
        private int Range = 0;
        private int Top = 0;
        private int Bottom = 0;
        private bool BattleFlg;
        private Encoding SjisEnc = Encoding.GetEncoding("Shift_JIS");
        public bool IsBottom
        {
            get
            {
                return this.MyItems.Count() - 1 == this.Index;
            }
        }

        public ItemMenuManager(Dictionary<Item, int> myItems, int range,bool battleFlg)
        {   
            this.Originals = myItems;
            this.Range = range;
            this.Bottom = this.Range;
            this.BattleFlg = battleFlg;
            this.ReflectList(myItems);
        }
        
        public void ReflectList(Dictionary<Item, int> myItems)
        {
            this.MyItems = this.BattleFlg ? myItems.Where(x => x.Key.ItemType == 0).ToDictionary(x => x.Key, x => x.Value) : myItems.ToDictionary(x => x.Key, x =>x.Value );
        }

        public void SetIndex(bool UpDown)
        {
            if (UpDown) 
            { 
                this.Index--; 
            } else 
            {
                this.Index++; 
            }
            if (this.Index < this.Top) {
                this.Top--;
                this.Bottom--;
            }else if (this.Index > this.Bottom)
            {
                this.Top++;
                this.Bottom++;
            }
        }

        public string GetImage()
        {
            if (!this.Originals.ContainsKey(this.GetChoose())) this.Index -= 1;
            this.ReflectList(this.Originals);
            int diff = 0;
            List<string> sList = new List<string>();
            sList.Add( (this.Index==0 ? "△ ":"▲ ")+this.RemakeWide("アイテム名",20,'-') + (this.BattleFlg ? "":"アイテム種別")+ "--個数--");
            for (int i = this.Top; i <= this.Bottom; i++)
            {
                if (i >= this.MyItems.Count())
                {
                    sList.Add("");
                }
                else
                {
                    sList.Add(this.GetString(i));
                }
            }
            sList.Add((this.MyItems.Count() - 1 == this.Index ? "▽ " : "▼ ") + "----------------------------------------");

            return string.Join("\r\n",sList);
        }


        private string GetString(int i)
        {
            int diff = 0;
            string str = "";
            Item i1 = this.MyItems.Keys.ToList()[i];
            str+= this.Index==i ? "▶  ":"   ";
            string name = this.RemakeWide(i1.Name,20,' ');
            if (BattleFlg)
            {
                str += name + " " + this.MyItems[i1];
            }
            else
            {
                str += name + " "+ (i1.ItemType ==0 ? "使用アイテム" :"装備アイテム") +"  "+ this.MyItems[i1].ToString().PadLeft(3);
            }
            return str;
        }
        
        public Item GetChoose()
        {
            return this.MyItems.Keys.ToList()[this.Index];
        }

        public string GetDiscription()
        {
            Item i1 = this.GetChoose();
            string Bonus = i1.ItemType == 0 ? "" :
                string.Join(" ", 
                "HP:" + (i1.BonusParams[0]>=0?"+":"")+ i1.BonusParams[0], 
                "攻撃:" + (i1.BonusParams[1] >= 0 ? "+" : "") + i1.BonusParams[1], 
                "防御:" + (i1.BonusParams[2] >= 0 ? "+" : "") + i1.BonusParams[2], 
                "素早さ:" + (i1.BonusParams[3] >= 0 ? "+" : "") + i1.BonusParams[3]
                );
            return string.Join("\r\n", "【説明】", i1.Description, "\r\n"+(i1.ItemType == 0 ? "":"【装備ボーナス】"),Bonus);
        }

        public string GetDiff(string str, out int diff)
        {
            int count = 0;
            foreach (char c in str)
            {
                if (!Regex.IsMatch(new string(c, 1), @"[ -~｡-ﾟ]"))
                {
                    count++;
                }
            }
            diff = count;
            return str;
        }

        private string RemakeWide(string str,int num,char pad)
        {
            int bytes = this.SjisEnc.GetByteCount(str);
            return str + new string(pad, num - bytes);
        }
    }
}
