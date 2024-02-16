using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BabelRPG
{
    internal class Chara 
    {
        public string Name { get; set; }
        public int Exp { get; set; }
        public int BaseHP { get; private set; }
        public int BaseAttack { get; private set; }
        public int BaseDeffence { get; private set; }
        public int BaseSpeed { get; private set; }
        public Item Equip { get; set; }
        public double[] LevelBonusMagnif { get; private set; } = new double[4] {1,1,1,1};

        public int RecentHP = 0;

        public Chara( string name, int exp, int baseHP, int baseAttack, int baseDeffence, int baseSpeed, Item equip, double[] levelBonusMagnif)
        {
            this.Name = name;
            this.Exp = exp;
            this.BaseHP = baseHP;
            this.BaseAttack = baseAttack;
            this.BaseDeffence = baseDeffence;
            this.BaseSpeed = baseSpeed;
            this.Equip = equip;
            this.LevelBonusMagnif = levelBonusMagnif;
            RecentHP = 0;
        }

        public int Level
        {
            get
            {
                int level = 0;
                int exp = this.Exp;
                int needed = 100;
                do
                {
                    level++;
                    exp -= needed;
                    needed = (int)Math.Floor((double)needed * 1.3);
                } while (needed < exp);
                return level;
            }
        }

        public int HP
        {
            get
            {
                return this.BaseHP + (int)Math.Floor(LevelBonusMagnif[0] * (double)this.Level) + this.Equip.BonusParams[0];
            }
        }

        public int Attack
        {
            get
            {
                return this.BaseAttack + (int)Math.Floor(LevelBonusMagnif[1] * (double)this.Level) + this.Equip.BonusParams[1];
            }
        }

        public int Deffence
        {
            get
            {
                return this.BaseDeffence + (int)Math.Floor(LevelBonusMagnif[2] * (double)this.Level) + this.Equip.BonusParams[2];
            }
        }

        public int Speed
        {
            get
            {
                return this.BaseSpeed + (int)Math.Floor(LevelBonusMagnif[3] * (double)this.Level) + this.Equip.BonusParams[3];
            }
        }


    }
}