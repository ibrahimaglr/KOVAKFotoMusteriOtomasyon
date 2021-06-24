using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FotoMusteriOtomasyon
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                Properties.Settings.Default.OtoDoldur = true;
            }
            else
            {
                Properties.Settings.Default.OtoDoldur = false;
            }
            Properties.Settings.Default.Save();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            try
            {
                if (Properties.Settings.Default.OtoDoldur == true)
                {
                    checkBox1.Checked = true;
                }
                if (Properties.Settings.Default.maxsize == true)
                {
                    checkBox2.Checked = true;
                }
                label2.Text = Properties.Settings.Default.Company.ToUpper();
                label3.Text = Properties.Settings.Default.Email;
                pictureBox1.Image = Image.FromFile(Properties.Settings.Default.Path1);

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message + "\n AYARLARI YAPILANDIRIN!!!");

            }

            try
            {
                comboBox1.DataSource = System.Drawing.Printing.PrinterSettings.InstalledPrinters.OfType<string>().ToArray();
                comboBox1.DisplayMember = "PrinterName";
                if (Properties.Settings.Default.PrinterPreference != "")
                {
                    this.comboBox1.SelectedItem = Properties.Settings.Default.PrinterPreference;
                }

                comboBox2.DataSource = System.Drawing.Printing.PrinterSettings.InstalledPrinters.OfType<string>().ToArray();
                comboBox2.DisplayMember = "PrinterName";
                if (Properties.Settings.Default.PrinterPreference2 != "")
                {
                    this.comboBox2.SelectedItem = Properties.Settings.Default.PrinterPreference2;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message + "\n AYARLARI YAPILANDIRIN!!!");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChangePassword f = new ChangePassword();
            f.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CompanyInformation f = new CompanyInformation();
            f.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ProductSettings f = new ProductSettings();
            f.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default.PrinterPreference = comboBox1.SelectedValue.ToString();
                Properties.Settings.Default.Save();
            }
            catch (Exception)
            { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default.PrinterPreference2 = comboBox2.SelectedValue.ToString();
                Properties.Settings.Default.Save();
            }
            catch (Exception)
            { }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                Properties.Settings.Default.maxsize = true;
            }
            else
            {
                Properties.Settings.Default.maxsize = false;
            }
            Properties.Settings.Default.Save();
        }
    }
}
