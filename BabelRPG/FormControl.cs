using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BabelRPG
{
    internal class FormControl
    {
        private string Head = "";
        private string Body = "";
        private string MsgWin = "";
        private string Attension = "";
        private int Input = 0;

        private TextBox Field;
        private TextBox Win;
        private Button[] Buttons;
        private TextBox NameBox;
        private Game GameController;
        private delegate void Next();
        Next next;
        //home
        private ItemMenuManager IMM;
        private Chara Choose;


        public FormControl(TextBox field, TextBox win, Button[] buttons, TextBox nameBox,Game gameController)
        {
            this.Field = field;
            this.Win = win;
            this.Buttons = buttons;
            this.NameBox = nameBox;
            this.GameController = gameController;
        }

        public void ReLoadInput(int num)
        {
            this.Input = num;
            this.next();
        }

        private void DisplayGame()
        {
            this.Field.Text = this.Head + "\r\n\r\n" + this.Body;
            this.Win.Text = this.MsgWin + "\r\n" + this.Attension;
            this.Attension = "";
        }
        private void DisplayField()
        {

            this.Field.Text = this.Head + "\r\n\r\n" + this.Body;
        }

        private void DisplayMsgWin()
        {
            this.Win.Text = this.MsgWin;
        }

        private void SetButton(string b1,string b2="",string b3="",string b4="",string b5="",string b6 = "")
        {
            string[] strList = { b1, b2, b3, b4, b5, b6 };
            for(int i= 0; i < strList.Length; i++)
            {
                if (strList[i] == "")
                {
                    this.Buttons[i].Enabled = false;
                }
                else
                {
                    this.Buttons[i].Enabled = true;
                }
                this.Buttons[i].Text=strList[i];         
            }
        }
        //画面制御------------------------------------------------------------------------------------------------------

        public void T()
        {
            this.Head = "";
            this.Body = "";
            this.MsgWin = "";

            this.DisplayGame();
            this.SetButton("");
            this.next = this.T;
        }
        public void C()
        {
            switch (this.Input)
            {
                case 1:
                    break;
            }
        }
        //--------------------------------------------------------------------------
        //--------------------------------------------------------------------------
        //スタート画面--------------------------------------------------------------
        public void start1()
        {
            this.Head = "【メニュー】";
            this.Body = "▶New Game\r\n▶Continue";
            this.MsgWin = "選択してください。";

            this.DisplayGame();
            this.SetButton("NewGame","Continue");
            this.next = this.startC1;
        }

        public void start2()
        {
            this.Head += ">>新規作成";
            this.Body = "既にあるデータは削除されます。よろしいですか？";
            this.MsgWin = "選択してください。";

            this.DisplayGame();
            this.SetButton("はい","いいえ");
            this.next = this.startC2;
        }

        public void start2_1()
        {
            this.Body = "あなたの名前を教えて下さい。";
            this.MsgWin = "テキストボックスに主人公の名前を入力してください。";
            this.NameBox.Visible = true;

            this.DisplayGame();
            this.SetButton("入力完了", "やっぱり\r\nやめる");
            this.next = this.startC2_1;
        }


        public void start2_2()
        {
            this.Body = "主人公の戦闘スタイルを選択してください。\r\n\r\n▶攻撃型\r\n▶防御型\r\n▶スピード型\r\n";
            this.MsgWin = "選択してください。";

            this.DisplayGame();
            this.SetButton("攻撃型", "防御型","スピード型", "やっぱり\r\nやめる");
            this.next = this.startC2_2;
        }

        public void start2_3()
        {
            this.GameController.SetData(true);
            this.Body = "こちらのデータで開始します。よろしいですか？\r\n\r\n"
                + this.GameController.PData.ShowAllStatus();

            this.DisplayGame();
            this.SetButton("はい", "いいえ");
            this.next = this.startC3;
        }



        public void start3()
        {
            this.Head += ">>続きから";
            this.GameController.SetData(false);
            this.Body = "こちらのデータで開始します。よろしいですか？\r\n\r\n"
                + this.GameController.PData.ShowAllStatus();

            this.DisplayGame();
            this.SetButton("はい", "いいえ");
            this.next = this.startC3;
        }



        public void startC1()
        {
            switch (this.Input)
            {
                case 1:
                    this.start2();
                    break;
                case 2:
                    this.start3();
                    break;
            }
        }

        public void startC2()
        {
            switch (this.Input)
            {
                case 1:
                    this.start2_1();
                    break ;
                case 2:
                    this.start1();
                    break;
            }
        }

        public void startC2_1()
        {
            this.NameBox.Visible = false;
            switch (this.Input)
            {
                case 1:
                    if (this.NameBox.Text == "")
                    {
                        this.Attension = "※名前を空白にはできません。";
                        this.start2_1();
                    }
                    else
                    {
                        this.GameController.Name=this.NameBox.Text;
                        this.NameBox.Text = "";
                        this.start2_2();
                    }
                    break;
                case 2:
                    this.start1();
                    break;
            }
        }

        public void startC2_2()
        {
            switch (this.Input)
            {
                case 1:
                    this.GameController.JobType = "Attack";
                    this.start2_3();
                    break;
                case 2:
                    this.GameController.JobType = "Deffence";
                    this.start2_3();
                    break;
                case 3:
                    this.GameController.JobType = "Speed";
                    this.start2_3();
                    break;
                case 4:
                    this.GameController.Name = "";
                    this.GameController.JobType = "";
                    this.start1();
                    break;

            }
        }

        public void startC3()
        {
            switch (this.Input)
            {
                case 1:
                    this.home1();
                    break;
                case 2:
                    this.start1();
                    break;
            }
        }

        //準備画面--------------------------------------------------------------
        public void home1()
        {
            this.Head = "【準備拠点】";
            this.Body = "▶バベルの塔へ (過去最高:"+this.GameController.PData.MaxFloor+"F)\r\n\tバベルの塔へ出発します。一度挑戦すると登頂する、フロアをクリアする、\r\n\tもしくは敗北したタイミングでしか返ってこれません。" +
                "\r\n\tアイテムの確認など十分準備してから挑みましょう。\r\n\r\n" +
                "▶ステータス確認\r\n\t味方のステータスを確認できます。キャラを選択するとアイテムを使用したり、装備できます。\r\n\r\n" +
                "▶アイテム一覧\r\n\t持っているアイテムを一覧表示します。アイテムを選択すると使用したり、装備できます。\r\n\r\n" +
                "▶セーブ\r\n\tプレイ状況をセーブします。\r\n\r\n"+
                "▶タイトルへ\r\n";
           this.MsgWin = "選択してください。";

            this.DisplayGame();
            this.SetButton("バベルの\r\n塔へ", "ステータス\r\n確認", "アイテム\r\n一覧", "セーブ","タイトルへ");
            this.next = this.homeC1;
        }

        public void home2()
        {
            this.Body = "バベルの塔へ出発します。よろしいですか？";
            this.MsgWin = "選択してください。";

            this.DisplayGame();
            this.SetButton("はい","いいえ");
            this.next = this.homeC2;
        }

        public void home3()
        {
            this.Head = "【準備拠点】>>ステータス確認";
            this.Body = this.GameController.PData.ShowAllStatus(); ;
            this.MsgWin = "アイテムなどを使用する場合はキャラを選択。";

            string[] names = this.GameController.PData.MyCharas.Select(x => x.Name).ToArray() ;
            int charaCount = this.GameController.PData.MyCharas.Count();
            this.DisplayGame();
            this.SetButton(names[0], charaCount == 1? "戻る" : names[1], 
                charaCount == 2 ? "戻る" : charaCount <2?"": names[2], 
                charaCount == 3 ? "戻る" : charaCount < 3 ? "" : names[3], 
                charaCount==4?"戻る":"");
            this.next = this.homeC3;
        }

        public void home3_1()
        {
            this.Head = "【準備拠点】>>ステータス確認>>" + this.Choose.Name;
            this.Body = (string.Join("\r\n", this.GameController.CMng.GetStatus(this.Choose))); ;
            this.MsgWin = "選択してください。";

            this.DisplayGame();
            this.SetButton("アイテム",this.Choose.Equip.Name==""?"":"装備を外す","戻る");
            this.next = this.homeC3_1;
        }

        public void home3_2()
        {
            this.Body = string.Join("\r\n", (string.Join("\r\n", this.GameController.CMng.GetStatus(this.Choose))),"\r\n",this.IMM.GetImage());
            this.MsgWin = this.IMM.GetDiscription();
            Item i1 = this.IMM.GetChoose();

            this.DisplayGame();
            this.SetButton(i1.ItemType==0?"使う":"装備する",this.IMM.Index==0?"":"▲",this.IMM.IsBottom?"":"▼","戻る");
            this.next = this.homeC3_2;
        }

        public void home3_3()
        {
            this.MsgWin = this.IMM.GetChoose().Name + "を"+(this.IMM.GetChoose().ItemType==0?"使用":"装備")+"します。よろしいですか？";

            this.DisplayGame();
            this.SetButton("はい","いいえ");
            this.next = this.homeC3_3;
        }

       

        public void home3_4()
        {
            this.MsgWin = this.GameController.PData.UseItem(Choose, this.IMM.GetChoose());
            this.Body = string.Join("\r\n", (string.Join("\r\n", this.GameController.CMng.GetStatus(this.Choose))), "\r\n", this.IMM.GetImage());

            this.DisplayGame();
            this.SetButton("OK");
            this.next = this.home3_2;
        }


        public void home3_5()
        {
            this.MsgWin = "装備を外します。よろしいですか？";

            this.DisplayGame();
            this.SetButton("はい", "いいえ");
            this.next = this.homeC3_4;
        }

        public void home3_6()
        {
            this.MsgWin = this.GameController.PData.UseItem(Choose, new Item());
            this.Body = string.Join("\r\n", this.GameController.CMng.GetStatus(this.Choose));

            this.DisplayGame();
            this.SetButton("OK");
            this.next = this.home3_1;
        }

        public void home4()
        {
            this.Head = "【準備拠点】>>【アイテム一覧】";
            this.Body = string.Join("\r\n",this.IMM.GetImage(), this.IMM.GetDiscription());
            this.MsgWin = "アイテムを選択";
            Item i1 = this.IMM.GetChoose();

            this.DisplayGame();
            this.SetButton(i1.ItemType == 0 ? "使う" : "装備する", this.IMM.Index == 0 ? "" : "▲", this.IMM.IsBottom ? "" : "▼", "戻る");
            this.next = this.homeC4;
        }

        public void home4_1()
        {
            this.Body =string.Join("\r\n", this.IMM.GetImage(), this.IMM.GetDiscription());
            this.MsgWin = string.Join("\r\n",(this.IMM.GetChoose().ItemType==0?"使用":"装備")+"するキャラを選択。\r\n", this.GameController.PData.ShowAllStatus());

            string[] names = this.GameController.PData.MyCharas.Select(x => x.Name).ToArray();
            int charaCount = this.GameController.PData.MyCharas.Count();
            this.DisplayGame();
            this.SetButton(names[0], charaCount == 1 ? "戻る" : names[1], charaCount == 2 ? "戻る" : charaCount < 2 ? "" : names[2], charaCount == 3 ? "戻る" : charaCount < 3 ? "" : names[3], charaCount == 4 ? "戻る" : "");
            this.next = this.homeC4_1;
        }

        public void home4_2()
        {
            this.MsgWin = string.Join("\r\n", this.IMM.GetChoose().Name + "を" + this.Choose.Name + "に" + (this.IMM.GetChoose().ItemType == 0 ? "使用" : "装備") + "します。よろしいですか？\r\n", this.GameController.PData.ShowAllStatus());  
            this.DisplayGame();
            this.SetButton("はい", "いいえ");
            this.next = this.homeC4_2;
        }

        public void home4_3()
        {
            this.MsgWin = string.Join("\r\n", this.GameController.PData.UseItem(Choose, this.IMM.GetChoose()) + "\r\n", this.GameController.PData.ShowAllStatus());
           
            this.DisplayGame();
            this.SetButton("OK");
            this.next = this.home4;
        }

        public void Save()
        {
            this.MsgWin = "プレイ状況をセーブしますか?";

            this.DisplayGame();
            this.SetButton("はい","いいえ");
            this.next = this.SaveC;
        }

        public void Save2()
        {
            this.MsgWin = "セーブしました。";

            this.DisplayGame();
            this.SetButton("OK");
            this.next = this.home1;
        }

        public void BackToTitle()
        {
            this.MsgWin = "セーブしてタイトルへ戻りますか？";

            this.DisplayGame();
            this.SetButton("はい","いいえ");
            this.next = this.BackToTitleC;
        }

        public void BackToTitle2()
        {
            this.MsgWin = "セーブしました。タイトルへ戻ります。";

            this.DisplayGame();
            this.SetButton("OK");
            this.GameController.PData = null; 
            this.next = this.start1;
        }


        public void homeC1()
        {
            switch (this.Input)
            {
                case 1:
                    this.home2();
                    break;
                case 2:
                    this.home3();
                    break;
                case 3:
                    if (this.GameController.PData.MyItems.Count() == 0)
                    {
                        this.Attension = "※まだアイテムを一つも持っていません。";
                        this.home1();
                    }
                    else
                    {
                        this.IMM = new ItemMenuManager(this.GameController.PData.MyItems, 15, false);
                        this.home4();
                    }
                    break;
                case 4:
                    this.Save();
                    break;
                case 5:
                    this.BackToTitle();
                    break;
            }
        }


        public void homeC2()
        {
            switch (this.Input)
            {
                case 1:
                    this.GameController.SetBattleManager();
                    this.Tower1();
                    break;
                case 2:
                    this.home1();
                    break;
            }
        }

        public void homeC3()
        {
            int charaCount = this.GameController.PData.MyCharas.Count();
            int choose = charaCount < this.Input? 5:this.Input;
            switch (choose)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    this.Choose = this.GameController.PData.MyCharas[this.Input - 1];
                    this.home3_1();
                    break;
                case 5:
                    this.Choose = null;
                    this.home1();
                    break;
            }
        }

        public void homeC3_1()
        {
            switch (this.Input)
            {
                case 1:
                    if (this.GameController.PData.MyItems.Count() == 0)
                    {
                        this.Attension = "※まだアイテムを一つも持っていません。";
                        this.home3_1();
                    }
                    else
                    { 
                    this.IMM = new ItemMenuManager(this.GameController.PData.MyItems, 5, false);
                    this.home3_2();
                    }
                    break;
                case 2:
                    this.home3_5();
                    break;
                case 3:
                    this.home3();
                    break;
            }
        }

        public void homeC3_2()
        {
            switch (this.Input)
            {
                case 1:
                    this.home3_3();
                    break;
                case 2:
                    this.IMM.SetIndex(true);
                    this.home3_2();
                    break;
                case 3:
                    this.IMM.SetIndex(false);
                    this.home3_2();
                    break;
                case 4:
                    this.home3_1();
                    break;
            }
        }

        public void homeC3_3()
        {
            switch (this.Input)
            {
                case 1:
                    this.home3_4();
                    break;
                case 2:
                    this.home3_2();
                    break;
            }
        }

        public void homeC3_4()
        {
            switch (this.Input)
            {
                case 1:
                    this.home3_6();
                    break;
                case 2:
                    this.home3_1();
                    break;
            }
        }


        public void homeC4()
        {
            switch (this.Input)
            {
                case 1:
                    this.home4_1();
                    break;
                case 2:
                    this.IMM.SetIndex(true);
                    this.home4();
                    break;
                case 3:
                    this.IMM.SetIndex(false);
                    this.home4();
                    break;
                case 4:
                    this.home1();
                    break;
            }
        }

        public void homeC4_1()
        {
            int charaCount = this.GameController.PData.MyCharas.Count();
            int choose = charaCount < this.Input ? 5 : this.Input;
            switch (choose)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    this.Choose = this.GameController.PData.MyCharas[this.Input - 1];
                    this.home4_2();
                    break;
                case 5:
                    this.Choose = null;
                    this.home4();
                    break;
            }
        }

        public void homeC4_2()
        {
            switch (this.Input)
            {
                case 1:
                    this.home4_3();
                    break;
                case 2:
                    this.home4_1();
                    break;
            }
        }

        public void SaveC()
        {
            switch (this.Input)
            {
                case 1:
                    this.GameController.PData.SaveData();
                    this.Save2();
                    break;
                case 2:
                    this.home1();
                    break;
            }
        }

        public void BackToTitleC()
        {
            switch (this.Input)
            {
                case 1:
                    this.GameController.PData.SaveData();
                    this.BackToTitle2();
                    break;
                case 2:
                    this.home1();
                    break;
            }
        }

        //バベルの塔-----------------------------------------------------------------------

        public void Tower1()
        {
            this.Head = "【バベルの塔"+(this.GameController.NowFloor+1).ToString().PadLeft(3)+"F】";
            this.Body = "";
            this.MsgWin = this.GameController.PData.Name+(this.GameController.PData.MyCharas.Count()==1 ?"":"達")+"は薄暗い通路をあるいている…。";

            this.DisplayGame();
            this.SetButton("次へ");
            if (this.GameController.BMng.BattleCount < this.GameController.BMng.AllEncount.Count())
            {
                this.next = this.Tower2;
            }
            else
            {
                this.next = this.NextFloor;
            }
        }

        public void Tower2()
        {
            this.Body = this.GameController.BMng.GetImage();
            this.MsgWin = "モンスター達に遭遇した！";

            this.DisplayGame();
            this.SetButton("次へ");
            this.next = this.Tower3;
        }

        public void Tower3()
        {
            this.MsgWin = "行動を選択してください";

            this.DisplayGame();
            this.SetButton("攻撃","アイテム","ステータス確認","逃げる");
            this.next = this.TowerC1;
        }

        public void Tower4()
        {
            this.MsgWin = "攻撃対象を選択してください。";

            string[] names = this.GameController.BMng.AllEncount[this.GameController.BMng.BattleCount].Select(x => x.Name).ToArray();
            int charaCount = this.GameController.BMng.AllEncount[this.GameController.BMng.BattleCount].Count();
            this.DisplayGame();
            this.SetButton(names[0], 
                charaCount == 1 ? "戻る" : names[1], 
                charaCount == 2 ? "戻る" : charaCount < 2 ? "" : names[2], 
                charaCount == 3 ? "戻る" : charaCount < 3 ? "" : names[3], 
                charaCount == 4 ? "戻る" : charaCount < 4 ? "" : names[4],
                charaCount == 5 ? "戻る" : ""
               );

            this.next = this.TowerC1_1;
        }

        public void TowerTurnEnd()
        {


            this.Body = this.GameController.BMng.GetImage();
            this.MsgWin = this.GameController.LMng.GetPage();

            this.DisplayGame();
            this.SetButton("次へ");
            if (this.GameController.LMng.EndFlg)
            {
                if (this.GameController.BMng.BattleNow)
                {
                    this.next = Tower3;
                }
                else
                {
                    if (this.GameController.BMng.BattleResult)
                    {
                        this.GameController.BMng.BattleCount++;
                        this.next=this.Tower1;
                    }
                    else
                    {
                        this.next=this.TowerResult;
                    }

                }

            }
            else
            {
                this.GameController.LMng.Page++;
                this.next = this.TowerTurnEnd;
            }
        }


        public void TowerResult()
        {
            this.Head = "【バベルの塔】";
            this.Body = "Result\r\n到達最終階  :"+(this.GameController.NowFloor+1)+"F   "+(this.GameController.PData.MaxFloor<this.GameController.NowFloor?"★記録更新":"")+
                "\r\n過去最高記録:"+this.GameController.PData.MaxFloor+"F";

            if (this.GameController.PData.MaxFloor < this.GameController.NowFloor)this.GameController.PData.MaxFloor=this.GameController.NowFloor+1;

            foreach(Chara c1 in this.GameController.PData.MyCharas)
            {
                c1.RecentHP = c1.HP;
            }

            this.GameController.NowFloor = 0;

            this.DisplayGame();
            this.SetButton("次へ");
            this.next = this.home1;
        }

        public void NextFloor()
        {
            this.Body = "フロアクリア！！";
            this.MsgWin = "次の階への階段を見つけた！このまま次の階へ行きますか…?";

            this.DisplayGame();
            this.SetButton("進む","撤退する");
            this.next = this.NextFloorC;
        }

        public void NextFloor2()
        {
            this.MsgWin = "次の階へ進む前に準備をしますか?";

            this.DisplayGame();
            this.SetButton("そのまま進む", "準備して進む","やっぱりやめる");
            this.next = this.NextFloorC2;
        }

        public void NextFloor3()
        {
            this.Body = "フロアクリア！！";
            this.MsgWin = "本当に撤退しますか？";

            this.DisplayGame();
            this.SetButton("はい", "いいえ");
            this.next = this.NextFloorC3;
        }


        public void TowerC1()
        {
            switch (this.Input)
            {
                case 1:
                    this.GameController.Command = 1;
                    this.Tower4();
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
            }
        }

        public void TowerC1_1()
        {
            int charaCount = this.GameController.BMng.AllEncount[this.GameController.BMng.BattleCount].Count();
            int choose = charaCount < this.Input ? 6 : this.Input;
            switch (choose)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                    this.GameController.SetLogManager(this.Input);
                    this.TowerTurnEnd();
                    break;
                case 6:
                    this.Tower3();
                    break;
            }
        }

        public void NextFloorC()
        {
            switch (this.Input)
            {
                case 1:
                    this.NextFloor2();
                    break;
                case 2:
                    this.NextFloor3();
                    break;
            }
        }

        public void NextFloorC2()
        {
            switch (this.Input)
            {
                case 1:
                    this.GameController.NowFloor++;
                    this.GameController.SetBattleManager();
                    this.Tower1();
                    break;
                case 2:
 //                   this.TowerPrepare();
                    break;
                case 3:
                    this.NextFloor3();
                    break;
            }
        }

        public void NextFloorC3()
        {
            switch (this.Input)
            {
                case 1:
                    this.TowerResult();
                    break;
                case 2:
                    this.NextFloor2();
                    break;
            }
        }
    }
}
