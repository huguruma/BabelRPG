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
        //Home
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
        public void Start1()
        {
            this.Head = "【メニュー】";
            this.Body = "▶New Game\r\n▶Continue";
            this.MsgWin = "選択してください。";

            this.DisplayGame();
            this.SetButton("NewGame","Continue");
            this.next = this.StartC1;
        }

        public void Start2()
        {
            this.Head += ">>新規作成";
            this.Body = "既にあるデータは削除されます。よろしいですか？";
            this.MsgWin = "選択してください。";

            this.DisplayGame();
            this.SetButton("はい","いいえ");
            this.next = this.StartC2;
        }

        public void Start2_1()
        {
            this.Body = "あなたの名前を教えて下さい。";
            this.MsgWin = "テキストボックスに主人公の名前を入力してください。";
            this.NameBox.Visible = true;

            this.DisplayGame();
            this.SetButton("入力完了", "やっぱり\r\nやめる");
            this.next = this.StartC2_1;
        }


        public void Start2_2()
        {
            this.Body = "主人公の戦闘スタイルを選択してください。\r\n\r\n▶攻撃型\r\n▶防御型\r\n▶スピード型\r\n";
            this.MsgWin = "選択してください。";

            this.DisplayGame();
            this.SetButton("攻撃型", "防御型","スピード型", "やっぱり\r\nやめる");
            this.next = this.StartC2_2;
        }

        public void Start2_3()
        {
            this.GameController.SetData(true);
            this.Body = "こちらのデータで開始します。よろしいですか？\r\n\r\n"
                + this.GameController.PData.ShowAllStatus();

            this.DisplayGame();
            this.SetButton("はい", "いいえ");
            this.next = this.StartC3;
        }



        public void Start3()
        {
            this.Head += ">>続きから";
            this.GameController.SetData(false);
            this.Body = "こちらのデータで開始します。よろしいですか？\r\n\r\n"
                + this.GameController.PData.ShowAllStatus();

            this.DisplayGame();
            this.SetButton("はい", "いいえ");
            this.next = this.StartC3;
        }



        public void StartC1()
        {
            switch (this.Input)
            {
                case 1:
                    this.Start2();
                    break;
                case 2:
                    this.Start3();
                    break;
            }
        }

        public void StartC2()
        {
            switch (this.Input)
            {
                case 1:
                    this.Start2_1();
                    break ;
                case 2:
                    this.Start1();
                    break;
            }
        }

        public void StartC2_1()
        {
            this.NameBox.Visible = false;
            switch (this.Input)
            {
                case 1:
                    if (this.NameBox.Text == "")
                    {
                        this.Attension = "※名前を空白にはできません。";
                        this.Start2_1();
                    }
                    else
                    {
                        this.GameController.Name=this.NameBox.Text;
                        this.NameBox.Text = "";
                        this.Start2_2();
                    }
                    break;
                case 2:
                    this.Start1();
                    break;
            }
        }

        public void StartC2_2()
        {
            switch (this.Input)
            {
                case 1:
                    this.GameController.JobType = "Attack";
                    this.Start2_3();
                    break;
                case 2:
                    this.GameController.JobType = "Deffence";
                    this.Start2_3();
                    break;
                case 3:
                    this.GameController.JobType = "Speed";
                    this.Start2_3();
                    break;
                case 4:
                    this.GameController.Name = "";
                    this.GameController.JobType = "";
                    this.Start1();
                    break;

            }
        }

        public void StartC3()
        {
            switch (this.Input)
            {
                case 1:
                    this.Home1();
                    break;
                case 2:
                    this.Start1();
                    break;
            }
        }

        //準備画面--------------------------------------------------------------
        public void Home1()
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
            this.next = this.HomeC1;
        }

        public void Home2()
        {
            this.Body = "バベルの塔へ出発します。よろしいですか？";
            this.MsgWin = "選択してください。";

            this.DisplayGame();
            this.SetButton("はい","いいえ");
            this.next = this.HomeC2;
        }

        public void Home3()
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
            this.next = this.HomeC3;
        }

        public void Home3_1()
        {
            this.Head = "【準備拠点】>>ステータス確認>>" + this.Choose.Name;
            this.Body = (string.Join("\r\n", this.GameController.CMng.GetStatus(this.Choose))); ;
            this.MsgWin = "選択してください。";

            this.DisplayGame();
            this.SetButton("アイテム",this.Choose.Equip.Name==""?"":"装備を外す","戻る");
            this.next = this.HomeC3_1;
        }

        public void Home3_2()
        {
            this.Body = string.Join("\r\n", (string.Join("\r\n", this.GameController.CMng.GetStatus(this.Choose))),"\r\n",this.IMM.GetImage());
            this.MsgWin = this.IMM.GetDiscription();
            Item i1 = this.IMM.GetChoose();

            this.DisplayGame();
            this.SetButton(i1.ItemType==0?"使う":"装備する",this.IMM.Index==0?"":"▲",this.IMM.IsBottom?"":"▼","戻る");
            this.next = this.HomeC3_2;
        }

        public void Home3_3()
        {
            this.MsgWin = this.IMM.GetChoose().Name + "を"+(this.IMM.GetChoose().ItemType==0?"使用":"装備")+"します。よろしいですか？";

            this.DisplayGame();
            this.SetButton("はい","いいえ");
            this.next = this.HomeC3_3;
        }

        public void Home3_4()
        {
            this.MsgWin = this.GameController.PData.UseItem(Choose, this.IMM.GetChoose());
            this.Body = string.Join("\r\n", (string.Join("\r\n", this.GameController.CMng.GetStatus(this.Choose))), "\r\n", this.IMM.GetImage());

            this.DisplayGame();
            this.SetButton("OK");
            this.next = this.Home3_2;
        }


        public void Home3_5()
        {
            this.MsgWin = "装備を外します。よろしいですか？";

            this.DisplayGame();
            this.SetButton("はい", "いいえ");
            this.next = this.HomeC3_4;
        }

        public void Home3_6()
        {
            this.MsgWin = this.GameController.PData.UseItem(Choose, new Item());
            this.Body = string.Join("\r\n", this.GameController.CMng.GetStatus(this.Choose));

            this.DisplayGame();
            this.SetButton("OK");
            this.next = this.Home3_1;
        }

        public void Home4()
        {
            this.Head = "【準備拠点】>>【アイテム一覧】";
            this.Body = string.Join("\r\n",this.IMM.GetImage(), this.IMM.GetDiscription());
            this.MsgWin = "アイテムを選択";
            Item i1 = this.IMM.GetChoose();

            this.DisplayGame();
            this.SetButton(i1.ItemType == 0 ? "使う" : "装備する", this.IMM.Index == 0 ? "" : "▲", this.IMM.IsBottom ? "" : "▼", "戻る");
            this.next = this.HomeC4;
        }

        public void Home4_1()
        {
            this.Body =string.Join("\r\n", this.IMM.GetImage(), this.IMM.GetDiscription());
            this.MsgWin = string.Join("\r\n",(this.IMM.GetChoose().ItemType==0?"使用":"装備")+"するキャラを選択。\r\n", this.GameController.PData.ShowAllStatus());

            string[] names = this.GameController.PData.MyCharas.Select(x => x.Name).ToArray();
            int charaCount = this.GameController.PData.MyCharas.Count();
            this.DisplayGame();
            this.SetButton(names[0], charaCount == 1 ? "戻る" : names[1], charaCount == 2 ? "戻る" : charaCount < 2 ? "" : names[2], charaCount == 3 ? "戻る" : charaCount < 3 ? "" : names[3], charaCount == 4 ? "戻る" : "");
            this.next = this.HomeC4_1;
        }

        public void Home4_2()
        {
            this.MsgWin = string.Join("\r\n", this.IMM.GetChoose().Name + "を" + this.Choose.Name + "に" + (this.IMM.GetChoose().ItemType == 0 ? "使用" : "装備") + "します。よろしいですか？\r\n", this.GameController.PData.ShowAllStatus());  
            this.DisplayGame();
            this.SetButton("はい", "いいえ");
            this.next = this.HomeC4_2;
        }

        public void Home4_3()
        {
            this.MsgWin = string.Join("\r\n", this.GameController.PData.UseItem(Choose, this.IMM.GetChoose()) + "\r\n", this.GameController.PData.ShowAllStatus());
           
            this.DisplayGame();
            this.SetButton("OK");
            this.next = this.Home4;
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
            this.next = this.Home1;
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
            this.next = this.Start1;
        }


        public void HomeC1()
        {
            switch (this.Input)
            {
                case 1:
                    this.Home2();
                    break;
                case 2:
                    this.Home3();
                    break;
                case 3:
                    if (this.GameController.PData.MyItems.Count() == 0)
                    {
                        this.Attension = "※まだアイテムを一つも持っていません。";
                        this.Home1();
                    }
                    else
                    {
                        this.IMM = new ItemMenuManager(this.GameController.PData.MyItems, 15, false);
                        this.Home4();
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


        public void HomeC2()
        {
            switch (this.Input)
            {
                case 1:
                    this.GameController.SetBattleManager();
                    this.Tower1();
                    break;
                case 2:
                    this.Home1();
                    break;
            }
        }

        public void HomeC3()
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
                    this.Home3_1();
                    break;
                case 5:
                    this.Choose = null;
                    this.Home1();
                    break;
            }
        }

        public void HomeC3_1()
        {
            switch (this.Input)
            {
                case 1:
                    if (this.GameController.PData.MyItems.Count() == 0)
                    {
                        this.Attension = "※まだアイテムを一つも持っていません。";
                        this.Home3_1();
                    }
                    else
                    { 
                    this.IMM = new ItemMenuManager(this.GameController.PData.MyItems, 5, false);
                    this.Home3_2();
                    }
                    break;
                case 2:
                    this.Home3_5();
                    break;
                case 3:
                    this.Home3();
                    break;
            }
        }

        public void HomeC3_2()
        {
            switch (this.Input)
            {
                case 1:
                    this.Home3_3();
                    break;
                case 2:
                    this.IMM.SetIndex(true);
                    this.Home3_2();
                    break;
                case 3:
                    this.IMM.SetIndex(false);
                    this.Home3_2();
                    break;
                case 4:
                    this.Home3_1();
                    break;
            }
        }

        public void HomeC3_3()
        {
            switch (this.Input)
            {
                case 1:
                    this.Home3_4();
                    break;
                case 2:
                    this.Home3_2();
                    break;
            }
        }

        public void HomeC3_4()
        {
            switch (this.Input)
            {
                case 1:
                    this.Home3_6();
                    break;
                case 2:
                    this.Home3_1();
                    break;
            }
        }


        public void HomeC4()
        {
            switch (this.Input)
            {
                case 1:
                    this.Home4_1();
                    break;
                case 2:
                    this.IMM.SetIndex(true);
                    this.Home4();
                    break;
                case 3:
                    this.IMM.SetIndex(false);
                    this.Home4();
                    break;
                case 4:
                    this.Home1();
                    break;
            }
        }

        public void HomeC4_1()
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
                    this.Home4_2();
                    break;
                case 5:
                    this.Choose = null;
                    this.Home4();
                    break;
            }
        }

        public void HomeC4_2()
        {
            switch (this.Input)
            {
                case 1:
                    this.Home4_3();
                    break;
                case 2:
                    this.Home4_1();
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
                    this.Home1();
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
                    this.Home1();
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
            foreach (Chara c1 in this.GameController.PData.MyCharas)
            {
                if (c1.RecentHP == 0) c1.RecentHP++;
            }

            this.Body = this.GameController.BMng.GetImage();
            this.MsgWin = "モンスターに遭遇した！";

            this.DisplayGame();
            this.SetButton("次へ");
            this.next = this.Tower3;
        }

        public void Tower3()
        {
            this.Body = this.GameController.BMng.GetImage();
            this.MsgWin = "行動を選択してください";

            this.DisplayGame();
            this.SetButton("攻撃",this.GameController.PData.MyItems.Count==0?"":"アイテム","ステータス\r\n確認","逃げる");
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

        public void Tower5()
        {
            this.Body = string.Join("\r\n",this.GameController.BMng.GetImage(),"\r\n"+this.IMM.GetImage());
            this.MsgWin = this.IMM.GetDiscription();
            

            this.DisplayGame();
            this.SetButton("使う", this.IMM.Index == 0 ? "" : "▲", this.IMM.IsBottom ? "" : "▼", "戻る");
            this.next = this.TowerC2;
        }

        public void Tower5_1()
        {
            this.MsgWin = string.Join("\r\n", "使用するキャラを選択。\r\n\r\n", this.GameController.PData.ShowAllStatus());

            string[] names = this.GameController.PData.MyCharas.Select(x => x.Name).ToArray();
            int charaCount = this.GameController.PData.MyCharas.Count();
            this.DisplayGame();
            this.SetButton(names[0], charaCount == 1 ? "戻る" : names[1], charaCount == 2 ? "戻る" : charaCount < 2 ? "" : names[2], charaCount == 3 ? "戻る" : charaCount < 3 ? "" : names[3], charaCount == 4 ? "戻る" : "");
            this.next = this.TowerC2_1;
        }

        public void Tower6()
        {
            this.Body = string.Join("\r\n", "\r\n" + this.GameController.BMng.GetImage(), this.IMM.GetImage(), "\r\n"+this.GameController.PData.ShowAllStatus());
            this.DisplayGame();
            this.SetButton("戻る");
            this.next = this.Tower3;

        }

        public void Tower7()
        {
            this.MsgWin = "本当に逃げますか？";

            this.DisplayGame();
            this.SetButton("はい","いいえ");
            this.next = this.TowerC4;
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
                    if (this.GameController.BMng.Caught != null)
                    {
                        this.next = this.Caught;
                    }
                    else if (this.GameController.BMng.BattleResult)
                    {
                        this.GameController.BMng.BattleCount++;
                        this.next=this.Tower1;
                    }
                    else
                    {

                        this.GameController.BMng.Caught = null;
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
            this.next = this.Home1;
        }

        public void Caught()
        {
            this.GameController.BMng.Caught.RecentHP = this.GameController.BMng.Caught.HP;
            this.MsgWin = "おや？先ほど倒した" + this.GameController.BMng.Caught.Name + "が付いて来たいようだ。\r\n" + this.GameController.BMng.Caught.Name + "を連れていきますか？" +
                          "\r\n\r\n" + string.Join("\r\n", this.GameController.CMng.GetStatus(this.GameController.BMng.Caught));

            this.DisplayGame();
            this.SetButton("はい","いいえ");
            this.next = this.CaughtC;
        }

        public void Caught2()
        {
            this.MsgWin = "これ以上仲間を連れていけない！今いる仲間と入れ替えますか？\r\n" +
                "\r\n\r\n"+ string.Join("\r\n", this.GameController.CMng.GetStatus(this.GameController.BMng.Caught)); 

            this.DisplayGame();
            this.SetButton("はい","いいえ");
            this.next = this.CaughtC2;
        }

        public void Caught3()
        {
            string str = string.Join("\r\n", this.GameController.CMng.GetAllStatus(this.GameController.PData.MyCharas.Where((x, i) => i != 0).ToList()));
            this.Body = string.Join("\r\n", "\r\n" + this.GameController.BMng.GetImage(), this.IMM.GetImage(), "\r\n" + str);

            this.MsgWin = "入れ替える仲間を選んでください\r\n" +
                "\r\n\r\n" + string.Join("\r\n", this.GameController.CMng.GetStatus(this.GameController.BMng.Caught));

            string[] names = this.GameController.PData.MyCharas.Where((x, i) => i != 0).Select(x => x.Name).ToArray();
            int charaCount = this.GameController.PData.MyCharas.Count()-1;
            this.DisplayGame();
            this.SetButton(names[0],names[1],names[2], "やっぱり\r\nやめる");
            this.next = this.CaughtC3;
        }

        public void Caught4()
        {
            
            this.MsgWin = this.Choose.Name+"と" + this.GameController.BMng.Caught.Name+"を入れ替えますか？\r\n" +
                "\r\n\r\n" + string.Join("\r\n", this.GameController.CMng.GetStatus(this.GameController.BMng.Caught));

            this.DisplayGame();
            this.SetButton("はい", "いいえ");
            this.next = this.CaughtC4;
        }

        public void Caught5()
        {
            this.GameController.PData.MyCharas[this.GameController.PData.MyCharas.IndexOf(this.Choose)] = this.GameController.BMng.Caught;
            string str = string.Join("\r\n", this.GameController.CMng.GetAllStatus(this.GameController.PData.MyCharas.Where((x, i) => i != 0).ToList()));
            this.Body = string.Join("\r\n", "\r\n" + this.GameController.BMng.GetImage(), this.IMM.GetImage(), "\r\n" + str);
            this.MsgWin = this.Choose.Name + "の代わりに" + this.GameController.BMng.Caught.Name + "に付いて来てもらうことにした！";

            
            this.DisplayGame();
            this.SetButton("次へ");
            this.next = this.CaughtEndC;
        }

        public void Caught6()
        {
            this.GameController.PData.MyCharas.Add(this.GameController.BMng.Caught);
            this.Body = this.GameController.BMng.GetImage();
            this.MsgWin = this.GameController.BMng.Caught.Name +"を新たに仲間に加えた！";

            this.DisplayGame();
            this.SetButton("次へ");
            this.next = this.CaughtEndC;
        }

        public void Caught7()
        {
            
            this.MsgWin = "気持ちだけ受け取って先へ進むことにした！";

            this.DisplayGame();
            this.SetButton("次へ");
            this.next = this.CaughtEndC;
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
        public void NextFloor4()
        {
            this.MsgWin = "次の階へ進みますか?";

            this.DisplayGame();
            this.SetButton("進む","やっぱりやめる");
            this.next = this.NextFloorC4;
        }

        public void Prepare1()
        {
            this.Body = 
                "▶ステータス確認\r\n\t味方のステータスを確認できます。キャラを選択するとアイテムを使用したり、装備できます。\r\n\r\n" +
                "▶アイテム一覧\r\n\t持っているアイテムを一覧表示します。アイテムを選択すると使用したり、装備できます。\r\n\r\n" +
                "▶先へ進む\r\n";
            this.MsgWin = "選択してください。";

            this.DisplayGame();
            this.SetButton("ステータス\r\n確認", "アイテム\r\n一覧", "先へ進む");
            this.next = this.PrepareC1;
        }

        public void Prepare3()
        {
            this.Body = this.GameController.PData.ShowAllStatus(); ;
            this.MsgWin = "アイテムなどを使用する場合はキャラを選択。";

            string[] names = this.GameController.PData.MyCharas.Select(x => x.Name).ToArray();
            int charaCount = this.GameController.PData.MyCharas.Count();
            this.DisplayGame();
            this.SetButton(names[0], charaCount == 1 ? "戻る" : names[1],
                charaCount == 2 ? "戻る" : charaCount < 2 ? "" : names[2],
                charaCount == 3 ? "戻る" : charaCount < 3 ? "" : names[3],
                charaCount == 4 ? "戻る" : "");
            this.next = this.PrepareC3;
        }

        public void Prepare3_1()
        {
            this.Body = (string.Join("\r\n", this.GameController.CMng.GetStatus(this.Choose))); ;
            this.MsgWin = "選択してください。";

            this.DisplayGame();
            this.SetButton("アイテム", this.Choose.Equip.Name == "" ? "" : "装備を外す", "戻る");
            this.next = this.PrepareC3_1;
        }

        public void Prepare3_2()
        {
            this.Body = string.Join("\r\n", (string.Join("\r\n", this.GameController.CMng.GetStatus(this.Choose))), "\r\n", this.IMM.GetImage());
            this.MsgWin = this.IMM.GetDiscription();
            Item i1 = this.IMM.GetChoose();

            this.DisplayGame();
            this.SetButton(i1.ItemType == 0 ? "使う" : "装備する", this.IMM.Index == 0 ? "" : "▲", this.IMM.IsBottom ? "" : "▼", "戻る");
            this.next = this.PrepareC3_2;
        }

        public void Prepare3_3()
        {
            this.MsgWin = this.IMM.GetChoose().Name + "を" + (this.IMM.GetChoose().ItemType == 0 ? "使用" : "装備") + "します。よろしいですか？";

            this.DisplayGame();
            this.SetButton("はい", "いいえ");
            this.next = this.PrepareC3_3;
        }

        public void Prepare3_4()
        {
            this.MsgWin = this.GameController.PData.UseItem(Choose, this.IMM.GetChoose());
            this.Body = string.Join("\r\n", (string.Join("\r\n", this.GameController.CMng.GetStatus(this.Choose))), "\r\n", this.IMM.GetImage());

            this.DisplayGame();
            this.SetButton("OK");
            this.next = this.Prepare3_2;
        }


        public void Prepare3_5()
        {
            this.MsgWin = "装備を外します。よろしいですか？";

            this.DisplayGame();
            this.SetButton("はい", "いいえ");
            this.next = this.PrepareC3_4;
        }

        public void Prepare3_6()
        {
            this.MsgWin = this.GameController.PData.UseItem(Choose, new Item());
            this.Body = string.Join("\r\n", this.GameController.CMng.GetStatus(this.Choose));

            this.DisplayGame();
            this.SetButton("OK");
            this.next = this.Prepare3_1;
        }

        public void Prepare4()
        {
            this.Body = string.Join("\r\n", this.IMM.GetImage(), this.IMM.GetDiscription());
            this.MsgWin = "アイテムを選択";
            Item i1 = this.IMM.GetChoose();

            this.DisplayGame();
            this.SetButton(i1.ItemType == 0 ? "使う" : "装備する", this.IMM.Index == 0 ? "" : "▲", this.IMM.IsBottom ? "" : "▼", "戻る");
            this.next = this.PrepareC4;
        }

        public void Prepare4_1()
        {
            this.Body = string.Join("\r\n", this.IMM.GetImage(), this.IMM.GetDiscription());
            this.MsgWin = string.Join("\r\n", (this.IMM.GetChoose().ItemType == 0 ? "使用" : "装備") + "するキャラを選択。\r\n", this.GameController.PData.ShowAllStatus());

            string[] names = this.GameController.PData.MyCharas.Select(x => x.Name).ToArray();
            int charaCount = this.GameController.PData.MyCharas.Count();
            this.DisplayGame();
            this.SetButton(names[0], charaCount == 1 ? "戻る" : names[1], charaCount == 2 ? "戻る" : charaCount < 2 ? "" : names[2], charaCount == 3 ? "戻る" : charaCount < 3 ? "" : names[3], charaCount == 4 ? "戻る" : "");
            this.next = this.PrepareC4_1;
        }

        public void Prepare4_2()
        {
            this.MsgWin = string.Join("\r\n", this.IMM.GetChoose().Name + "を" + this.Choose.Name + "に" + (this.IMM.GetChoose().ItemType == 0 ? "使用" : "装備") + "します。よろしいですか？\r\n", this.GameController.PData.ShowAllStatus());
            this.DisplayGame();
            this.SetButton("はい", "いいえ");
            this.next = this.PrepareC4_2;
        }

        public void Prepare4_3()
        {
            this.MsgWin = string.Join("\r\n", this.GameController.PData.UseItem(Choose, this.IMM.GetChoose()) + "\r\n", this.GameController.PData.ShowAllStatus());

            this.DisplayGame();
            this.SetButton("OK");
            this.next = this.Prepare4;
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
                    this.GameController.Command = 2;
                    this.IMM = new ItemMenuManager(this.GameController.PData.MyItems, 6, true);
                    this.Tower5();
                    break;
                case 3:
                    break;
                case 4:
                    this.Tower7();
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

        public void TowerC2()
        {
            switch (this.Input)
            {
                case 1:
                    this.Tower5_1();
                    break;
                case 2:
                    this.IMM.SetIndex(true);
                    this.Tower5();
                    break;
                case 3:
                    this.IMM.SetIndex(false);
                    this.Tower5();
                    break;
                case 4:
                    this.Tower3();
                    break;
            }
        }


        public void TowerC2_1()
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
                    this.GameController.SetLogManager(this.Input,this.IMM.GetChoose().Name);
                    this.IMM = null;
                    this.TowerTurnEnd();
                    break;
                case 5:
                    this.Choose = null;
                    this.Tower5();
                    break;
            }
        }

        public void TowerC4()
        {
            switch (this.Input)
            {
                case 1:
                    this.GameController.Command = 4;
                    this.GameController.SetLogManager(this.Input);
                    this.TowerTurnEnd();
                    break;
                case 2:
                    this.Tower3();
                    break;
            }
        }

        public void CaughtC()
        {
            switch (this.Input)
            {
                case 1:
                    if (this.GameController.PData.MyCharas.Count == 4)
                    {
                        this.Caught2();
                    }
                    else
                    {
                        this.Caught6();
                    }
                    break;
                case 2:
                    this.Caught7();
                    break;
            }
        }

        public void CaughtC2()
        {
            switch (this.Input)
            {
                case 1:
                        this.Caught3();
                    break;
                case 2:
                    this.Caught7();
                    break;
            }
        }

        public void CaughtC3()
        {
            int charaCount = this.GameController.PData.MyCharas.Count()-1;
            int choose = charaCount < this.Input ? 4 : this.Input;
            switch (choose)
            {
                case 1:
                case 2:
                case 3:
                    this.Choose = this.GameController.PData.MyCharas[this.Input];
                    this.Caught4();
                    break;
                case 4:
                    this.Choose = null;
                    this.Caught7();
                    break;
            }
        }

        public void CaughtC4()
        {
            switch (this.Input)
            {
                case 1:
                    this.Caught5();
                    break;
                case 2:
                    this.Caught3();
                    break;
            }
        }


        public void CaughtEndC()
        {

            this.GameController.BMng.GotExp = new int[this.GameController.PData.MyCharas.Count].ToList();
            this.GameController.BMng.Caught = null;
            if (this.GameController.BMng.BattleResult)
            {
                this.GameController.BMng.BattleCount++;
                this.Tower1();
            }
            else
            {
                this.TowerResult();
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
                    this.Prepare1();
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
        public void NextFloorC4()
        {
            switch (this.Input)
            {
                case 1:
                    this.GameController.NowFloor++;
                    this.GameController.SetBattleManager();
                    this.Tower1();
                    break;
                case 2:
                    this.Prepare1();
                    break;
            }
        }



        public void PrepareC1()
        {
            switch (this.Input)
            {
                case 1:
                    this.Prepare3();
                    break;
                case 2:
                    if (this.GameController.PData.MyItems.Count() == 0)
                    {
                        this.Attension = "※まだアイテムを一つも持っていません。";
                        this.Prepare1();
                    }
                    else
                    {
                        this.IMM = new ItemMenuManager(this.GameController.PData.MyItems, 15, false);
                        this.Prepare4();
                    }
                    break;
                case 3:
                    this.NextFloor4();
                    break;
            }
               
        }

        public void PrepareC3()
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
                    this.Prepare3_1();
                    break;
                case 5:
                    this.Choose = null;
                    this.Prepare1();
                    break;
            }
        }

        public void PrepareC3_1()
        {
            switch (this.Input)
            {
                case 1:
                    if (this.GameController.PData.MyItems.Count() == 0)
                    {
                        this.Attension = "※まだアイテムを一つも持っていません。";
                        this.Prepare3_1();
                    }
                    else
                    {
                        this.IMM = new ItemMenuManager(this.GameController.PData.MyItems, 5, false);
                        this.Prepare3_2();
                    }
                    break;
                case 2:
                    this.Prepare3_5();
                    break;
                case 3:
                    this.Prepare3();
                    break;
            }
        }

        public void PrepareC3_2()
        {
            switch (this.Input)
            {
                case 1:
                    this.Prepare3_3();
                    break;
                case 2:
                    this.IMM.SetIndex(true);
                    this.Prepare3_2();
                    break;
                case 3:
                    this.IMM.SetIndex(false);
                    this.Prepare3_2();
                    break;
                case 4:
                    this.Prepare3_1();
                    break;
            }
        }

        public void PrepareC3_3()
        {
            switch (this.Input)
            {
                case 1:
                    this.Prepare3_4();
                    break;
                case 2:
                    this.Prepare3_2();
                    break;
            }
        }

        public void PrepareC3_4()
        {
            switch (this.Input)
            {
                case 1:
                    this.Prepare3_6();
                    break;
                case 2:
                    this.Prepare3_1();
                    break;
            }
        }


        public void PrepareC4()
        {
            switch (this.Input)
            {
                case 1:
                    this.Prepare4_1();
                    break;
                case 2:
                    this.IMM.SetIndex(true);
                    this.Prepare4();
                    break;
                case 3:
                    this.IMM.SetIndex(false);
                    this.Prepare4();
                    break;
                case 4:
                    this.Prepare1();
                    break;
            }
        }

        public void PrepareC4_1()
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
                    this.Prepare4_2();
                    break;
                case 5:
                    this.Choose = null;
                    this.Prepare4();
                    break;
            }
        }

        public void PrepareC4_2()
        {
            switch (this.Input)
            {
                case 1:
                    this.Prepare4_3();
                    break;
                case 2:
                    this.Prepare4_1();
                    break;
            }
        }
    }
}
