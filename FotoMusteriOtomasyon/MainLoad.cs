using System;
using System.IO;
using System.Windows.Forms;

namespace FotoMusteriOtomasyon
{
    public partial class MainLoad : Form
    {
        public MainLoad()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            progressBar1.Value += 20;
            if (progressBar1.Value == 300)
            {
                label1.Text = "Yükleniyor...";
            }
            if (progressBar1.Value == 400)
            {
                timer1.Enabled = false;
                if (Properties.Settings.Default.Kullanıcı == "")
                {
                    SystemRegistry form = new SystemRegistry();
                    form.Show();
                }
                else
                {
                    Login f = new Login();
                    f.Show();
                }
                this.Hide();
            }
        }

        private void MainLoad_Load(object sender, EventArgs e)
        {
            try
            {
                label1.Text = "Dosyalar Kontrol Ediliyor...";
                string path = Application.StartupPath + "\\OrderList\\" + "Orders.save";
                string path2 = Application.StartupPath + @"\Menus\MenuNames.save";
                Directory.CreateDirectory("OrderList");
                progressBar1.Value += 40;
                Directory.CreateDirectory("Menus");
                progressBar1.Value += 40;
                if (File.Exists(path))
                { progressBar1.Value += 40; }
                else
                {
                    FileStream fs = File.Create(path);
                    fs.Close();
                    progressBar1.Value += 40;
                }
                if (File.Exists(path2))
                { progressBar1.Value += 40; }
                else
                {
                    FileStream fs = File.Create(path2);
                    fs.Close();
                    progressBar1.Value += 40;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
