using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BabelRPG
{
    public partial class Form : System.Windows.Forms.Form
    {
        FormControl formControl;
        public Form()
        {
            InitializeComponent();
            this.formControl = new FormControl(this.FieldBox, this.MessageBox,new Button[] {this.button1,this.button2,this.button3, this.button4, this.button5, this.button6 },this.NameBox,new Game());
        }


        private void Form_Load(object sender, EventArgs e)
        {
            this.formControl.start1();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.formControl.ReLoadInput(1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.formControl.ReLoadInput(2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.formControl.ReLoadInput(3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.formControl.ReLoadInput(4);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.formControl.ReLoadInput(5);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.formControl.ReLoadInput(6);
        }
    }
}
