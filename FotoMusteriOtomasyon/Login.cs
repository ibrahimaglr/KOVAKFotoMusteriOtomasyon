using System;
using System.Windows.Forms;

namespace FotoMusteriOtomasyon
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == Properties.Settings.Default.Kullanıcı && textBox2.Text == Properties.Settings.Default.Sifre)
            {
                MainWindow f = new MainWindow();
                f.Show();
                this.Hide();
            }
            else
            {
                label5.Visible = true;
                label5.Text = "KULLANICI ADI VEYA ŞİFRE HATALI";
                checkBox1.Checked = true;
                textBox2.PasswordChar = '\0';
                linkLabel1.Visible = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.PasswordChar = '\0';
            }
            else
            {
                textBox2.PasswordChar = '●';
            }
        }
        public void result1()
        {
            if (label5.Visible == true)
            {
                label5.Visible = false;
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            result1();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            result1();
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.OtoDoldur == true)
            {
                textBox1.Text = Properties.Settings.Default.Kullanıcı;
                textBox2.Text = Properties.Settings.Default.Sifre;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ChangePassword f = new ChangePassword();
            f.Show();
        }
    }
}
