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
        private int Input = 0;

        private TextBox Field;
        private TextBox Win;
        private Button[] Buttons;
        private Game GameController;
        private delegate void Next();
        Next next;

        public FormControl(TextBox field, TextBox win, Button[] buttons, Game gameController)
        {
            this.Field = field;
            this.Win = win;
            this.Buttons = buttons;
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
            this.Win.Text = this.MsgWin;
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
        //--------------------------------------------------------------------------
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
            this.Body = "既にあるデータは削除されます。よろしいですか？";
            this.MsgWin = "選択してください。";

            this.DisplayGame();
            this.SetButton("はい","いいえ");
            this.next = this.startC2;
        }

        public void start3()
        {
            this.GameController.SetData(false);
            this.Body = "こちらのデータで開始します。よろしいですか？\r\n"
                + this.GameController.PData.ShowAllStatus();

            this.DisplayGame();
            this.SetButton("はい", "いいえ");
            this.next = this.startC2;
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
                    this.T();
                    break;
                case 2:
                    this.start1();
                    break;
            }
        }






    }
}
