using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BabelRPG
{
    internal class Game
    {
        public ItemManager IMng;
        public CharaManager CMng;
        public PlayData PData;
        public BattleManager BMng;
        public LogManager LMng;
        public int NowFloor=0;
        public string Name = "";
        public string JobType = "";
        public int Command = 0;

        public Game()
        {
            this.IMng = new ItemManager();
            this.CMng = new CharaManager();
            IMng.SetAllItems();
            CMng.SetAllCreatures();
            CMng.SetDropItems();
        }

        public void SetData(bool isNewGame,string name="",string jobType="")
        {
            if (isNewGame)
            {
                this.PData = new PlayData(true, this.IMng, this.CMng,this.Name,this.JobType);
            }
            else
            {
                this.PData = new PlayData(false, this.IMng, this.CMng);
            }
        }
        
        public void SetBattleManager()
        {    
            this.BMng=new BattleManager(this.IMng,this.CMng,this.PData,this.NowFloor);
        }

        public void SetLogManager(int input,string i1="")
        {
            this.LMng = new LogManager(this.BMng.BattlePhase(this.Command, input-1,i1), 10);
        }
        public void MainFrame()
        {

        }

       

        
    }
}
