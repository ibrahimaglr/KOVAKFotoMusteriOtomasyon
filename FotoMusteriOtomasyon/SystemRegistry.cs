using System;
using System.Drawing;
using System.Windows.Forms;

namespace FotoMusteriOtomasyon
{
    public partial class SystemRegistry : Form
    {
        public SystemRegistry()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox1.Text == " " && textBox4.Text == "" || textBox4.Text == " " && textBox5.Text == "" || textBox5.Text == " ")
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
                Properties.Settings.Default.Company = textBox4.Text;
                Properties.Settings.Default.Email = textBox5.Text;
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

        private void SistemKayıt_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog wallPapers = new OpenFileDialog();
            wallPapers.Filter = "Resim Dosyası |*.jpg;*.jfif;*.png;*.ico|Tüm Dosyalar |*.*";
            wallPapers.ShowDialog();
            if (wallPapers.FileName == "")
            {
                MessageBox.Show("BOŞ", "FotoMusteeOtom", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string FileString = wallPapers.FileName;
                Properties.Settings.Default.Path1 = FileString;
                Properties.Settings.Default.Save();
                pictureBox1.Image = Image.FromFile(Properties.Settings.Default.Path1);

            }
        }
    }
}
