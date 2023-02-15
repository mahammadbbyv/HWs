using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Introduction;
using System.Text.Json;

namespace Introduction
{
    public partial class InfoForm : Form
    {
        private Person person = new();
        public List<Person> Persons { get; set; } = new();
        private bool changeform { get; set; } = false;
        public InfoForm()
        {
            InitializeComponent();
        }

        public InfoForm(Person person)
        {
            InitializeComponent();
            this.person = person;
            nameLabel.Text = person.Name;
            surnameLabel.Text = person.Surname;
            ageLabel.Text = person.Age.ToString();

            pictureBox.Image = Image.FromFile(person.ImagePath);
        }
        public InfoForm(Person person, bool changeform)
        {
            this.changeform = changeform;
            InitializeComponent();
            this.person = person;
            nameLabel.Text = person.Name;
            surnameLabel.Text = person.Surname;
            ageLabel.Text = person.Age.ToString();

            pictureBox.Image = Image.FromFile(person.ImagePath);
        }
        public InfoForm(Person person, List<Person> persons)
        {
            this.Persons = persons;
            this.changeform = changeform;
            InitializeComponent();
            this.person = person;
            nameLabel.Text = person.Name;
            surnameLabel.Text = person.Surname;
            ageLabel.Text = person.Age.ToString();

            pictureBox.Image = Image.FromFile(person.ImagePath);
        }

        private void InfoForm_Load(object sender, EventArgs e)
        {
            if (changeform)
            {
                changeform = false;
                this.Close();
            }
        }

        private void nameLabel_Click(object sender, EventArgs e)
        {
            ChangeForm form = new(person, "name");
            this.Hide();
            form.ShowDialog();
            this.Show();
            nameLabel.Text = person.Name;
        }

        private void surnameLabel_Click(object sender, EventArgs e)
        {
            ChangeForm form = new(person, "surname");
            this.Hide();
            form.ShowDialog();
            this.Show();
            surnameLabel.Text = person.Surname;
        }

        private void ageLabel_Click(object sender, EventArgs e)
        {
            ChangeForm form = new(person, "age");
            this.Hide();
            form.ShowDialog();
            this.Show();
            ageLabel.Text = person.Age.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Persons.Remove(person);
            FileStream fs = new("data.json", FileMode.OpenOrCreate);
            StreamWriter sw = new(fs);
            sw.Write(JsonSerializer.Serialize(Persons));
            sw.Close();
            fs.Close();
            this.Close();
        }
    }
}
