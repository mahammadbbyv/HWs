namespace WinFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Mahammad Babayev", "My full name is:", MessageBoxButtons.OK);
            MessageBox.Show("16", "My age is:", MessageBoxButtons.OK);
            MessageBox.Show("I'm Dumbass", $"average({39/3})", MessageBoxButtons.OK);
        }
    }
}