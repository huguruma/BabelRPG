using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BabelRPG
{
    internal class Creature:Chara
    {
        public Dictionary<string,double> DropItems { get; }
        public int PopFloorMin { get; }
        public int PopFloorMax { get; }

        public int CatchPer = 0;

        public Creature(string name, int exp,  int baseHP, int baseAttack, int baseDeffence, int baseSpeed, Item equip, double[] levelBonusMagnif, Dictionary<string, double> dropItems, int popFloorMin, int popFloorMax, int catchPer) : base(name, exp, baseHP, baseAttack, baseDeffence, baseSpeed, equip, levelBonusMagnif)
        {
            DropItems = dropItems;
            PopFloorMin = popFloorMin;
            PopFloorMax = popFloorMax;
            CatchPer = catchPer;
        }

        public Creature Clone()
        {
            return (Creature)MemberwiseClone();
        }
    }
}
