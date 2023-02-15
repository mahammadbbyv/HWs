using Microsoft.AspNetCore.Http;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System.Text.Json;
//using Newtonsoft.Json;
using System.Text;
using System.ComponentModel;
namespace Introduction
{
    public partial class Form1 : Form
    {
        public List<Person> Persons { get; set; } = new();
        public bool change { get; set; } = false;
        public int last { get; set; } = 0;
        public Form1()
        {
            InitializeComponent();
        }

        public Form1(List<Person> Persons)
        {
            InitializeComponent();
            this.Persons = Persons;
            change = true;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            Regex re = new("^(?<firstchar>(?=[A-Za-z]))((?<alphachars>[A-Za-z])|(?<specialchars>[A-Za-z]['-](?=[A-Za-z]))|(?<spaces> (?=[A-Za-z])))*$");
            Regex re2 = new("^[1-9][0-9]*");
            
            if (re.IsMatch(nameTextBox.Text) && re.IsMatch(surnameTextBox.Text) && re2.IsMatch(ageTextBox.Text))
            {
                Persons[last].Name = nameTextBox.Text;
                Persons[last].Surname = surnameTextBox.Text;
                Persons[last].Age = Convert.ToInt32(ageTextBox.Text);

                peopleListBox.Items.Add(Persons[last]);

                nameTextBox.Text = "";
                surnameTextBox.Text = "";
                ageTextBox.Text = "";
                imgLabel.Text = "Image";
                last++;
            }
            else
            {
                MessageBox.Show("Enter valid info", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void peopleListBox_DoubleClick(object sender, EventArgs e)
        {
            int index = peopleListBox.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Choose element wabbu lubba dub dub", "ERORR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                InfoForm form = new(Persons[index], Persons);
                this.Hide();
                form.ShowDialog();
                this.Show();
            }
        }

        private void imgButton_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.jfif";

            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                Persons.Add(new Person());
                Persons[last].ImagePath = dialog.FileName;
            }
            imgLabel.Text = dialog.FileName;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
                FileStream fs = new("data.json", FileMode.OpenOrCreate);
                StreamWriter ws = new(fs);
                ws.Write(JsonSerializer.Serialize(peopleListBox.Items));
                ws.Close();
                fs.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (change)
            {
                change = false;
                Close();
            }
            else
            {
                FileStream fs = new("data.json", FileMode.Open, FileAccess.Read);
                StreamReader sr = new(fs);
                Persons = JsonSerializer.Deserialize<List<Person>>(sr.ReadToEnd());
                last = Persons.Count;
                for(int i = 0; i < Persons.Count; i++)
                {
                    peopleListBox.Items.Add(Persons[i]);
                }
                sr.Close();
                fs.Close();
            }
        }

        private void Form1_VisibleChanged(object sender, EventArgs e)
        {
            peopleListBox.Items.Clear();
            FileStream fs = new("data.json", FileMode.Open, FileAccess.Read);
            StreamReader sr = new(fs);
            Persons = JsonSerializer.Deserialize<List<Person>>(sr.ReadToEnd());
            for (int i = 0; i < Persons.Count; i++)
            {
                peopleListBox.Items.Add(Persons[i]);
            }
            sr.Close();
            fs.Close();
        }
    }
}