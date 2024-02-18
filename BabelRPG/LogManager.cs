using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabelRPG
{
    internal class LogManager
    {
        public bool EndFlg {
            get
            {
                return this.Page == this.MaxPage;
            }
        }
        public string Log;
        public int Index=0;
        public int MaxIndex;
        public int Page=0;
        public int MaxPage=0;
        public int Range;
        List<List<string>> AllPages=new List<List<string>>();

        public LogManager(string log,int range)
        {
            this.Log = log;
            this.Range = range;
            List<string> sList = log.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
            this.MaxPage = sList.Count / range + (sList.Count() % range == 0 ? 0 : 1)-1;
            this.MaxIndex = sList.Count - 1;
            int index = 0;
            for(int i=0;i<=MaxPage;i++)
            {
                List<string> temp=new List<string>();
                for(int j=0; j < range; j++)
                {
                    temp.Add(sList[index]);
                    if (index == this.MaxIndex) break;
                    index++;
                }
                this.AllPages.Add(temp);
            }
        }  

        public string GetPage()
        {
            return string.Join("\r\n", this.AllPages[Page]);
        }
        

    }
}
