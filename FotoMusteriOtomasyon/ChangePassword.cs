using System;
using System.Windows.Forms;

namespace FotoMusteriOtomasyon
{
    public partial class ChangePassword : Form
    {
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox1.Text == " ")
            {
                label5.Visible = true;
                label5.Text = "KULLANICI ADI GEÇERSİZ";
                checkBox1.Checked = true;
                textBox2.PasswordChar = '\0';
                textBox3.PasswordChar = '\0';

            }
            else if (textBox2.Text == " " || textBox2.Text == "")
            {
                label5.Visible = true;
                label5.Text = "TERCİH EDİLEN ŞİFRE GEÇERSİZ";
                checkBox1.Checked = true;
                textBox2.PasswordChar = '\0';
                textBox3.PasswordChar = '\0';
            }
            else if (textBox2.Text != textBox3.Text)
            {
                label5.Visible = true;
                label5.Text = "ŞİFRELER UYUMSUZ";
                checkBox1.Checked = true;
                textBox2.PasswordChar = '\0';
                textBox3.PasswordChar = '\0';
            }
            else
            {
                Properties.Settings.Default.Kullanıcı = textBox1.Text;
                Properties.Settings.Default.Sifre = textBox2.Text;
                Properties.Settings.Default.Save();
                Login f = new Login();
                f.Show();
                this.Hide();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.PasswordChar = '\0';
                textBox3.PasswordChar = '\0';
            }
            else
            {
                textBox2.PasswordChar = '●';
                textBox3.PasswordChar = '●';
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

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            result1();
        }
    }
}
