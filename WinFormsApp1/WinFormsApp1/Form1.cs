namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Int32.Parse(textBox1.Text) < 1 || Int32.Parse(textBox1.Text) > 2000)
            {
                MessageBox.Show("Enter value more than 1 and less than 2000.", "Value error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (Int32.Parse(textBox1.Text) > 1 || Int32.Parse(textBox1.Text) < 2000)
            {
                int count = 0;
                Random rand= new();
                int val = rand.Next(1, 2000);
                while(val != Int32.Parse(textBox1.Text))
                {
                    count++;
                    val = rand.Next(1, 2000);
                }
                DialogResult result = MessageBox.Show($"Tries: {count}\nWould you like to continue?", "Success!", MessageBoxButtons.YesNo, MessageBoxIcon.None);
                if(result == DialogResult.No)
                {
                    this.Close();
                }
            }

        }
    }
}