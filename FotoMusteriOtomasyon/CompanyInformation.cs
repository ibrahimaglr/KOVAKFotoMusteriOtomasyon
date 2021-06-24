using System;
using System.Drawing;
using System.Windows.Forms;

namespace FotoMusteriOtomasyon
{
    public partial class CompanyInformation : Form
    {
        public CompanyInformation()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "" || textBox4.Text == " " && textBox1.Text == "" || textBox1.Text == " ")
            {
                label5.Visible = true;
                label5.Text = "KULLANICI ADI GEÇERSİZ";
            }
            else
            {
                Properties.Settings.Default.Company = textBox4.Text;
                Properties.Settings.Default.Email = textBox1.Text;
                Properties.Settings.Default.Save();
                MainWindow.activiteRules = true;
                this.Close();
            }

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
        public void result1()
        {
            if (label5.Visible == true)
            {
                label5.Visible = false;
            }
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            result1();
        }

        private void CompanyInformation_Load(object sender, EventArgs e)
        {
            textBox1.Text = Properties.Settings.Default.Email;
            textBox4.Text = Properties.Settings.Default.Company;
            pictureBox1.Image = Image.FromFile(Properties.Settings.Default.Path1);
        }
    }
}
