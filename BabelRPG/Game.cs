using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelRPG
{
    internal class Game
    {
        public ItemManager IMng;
        public CharaManager CMng;
        public PlayData PData;
        private int NowFloor=0;
        public string Name = "";
        public string JobType = "";


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

        public void MainFrame()
        {

        }

       

        
    }
}
