namespace Lab3
{
    public partial class Lab03 : Form
    {
        public Lab03()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bai01 f = new Bai01();
            f.ShowDialog();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Bai02 f = new Bai02();
            f.ShowDialog();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Bai03 f = new Bai03();
            f.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Bai04 f = new Bai04();
            f.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Bai06 f = new Bai06();
            f.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Bai05 f = new Bai05();
            f.Show();
        }
    }
}
