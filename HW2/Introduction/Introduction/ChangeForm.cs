using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Introduction
{
    public partial class ChangeForm : Form
    {
        private Person person = new();
        private string? changeItem = "";
        public ChangeForm()
        {
            InitializeComponent();
        }
        public ChangeForm(Person ToChange, string? changeItem) 
        {
            InitializeComponent();
            this.person = ToChange;
            this.changeItem = changeItem;
        }

        private void ChangeForm_Load(object sender, EventArgs e)
        {
            label1.Text = changeItem;
            if (changeItem == "name") { textBox1.Text = person.Name; }else if(changeItem == "surname") { textBox1.Text = person.Surname; } else if (changeItem == "age") { textBox1.Text = person.Age.ToString(); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (changeItem == "name") { person.Name = textBox1.Text; } else if (changeItem == "surname") {person.Surname = textBox1.Text  ; } else if (changeItem == "age") {person.Age  = Convert.ToInt32(textBox1.Text); }
            InfoForm form = new(person, true);
            form.ShowDialog();
            form.Close();
            this.Close();
        }
    }
}
