using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FotoMusteriOtomasyon
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        static public bool activiteRules = false;
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Uygulamayı kapatmak istediğinize emin misiniz?", "FotoOtomasyon", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
            if (result == DialogResult.Yes)
            {
                try
                {
                    Environment.Exit(0);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }


            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ReportError f = new ReportError();
            f.Show();
        }
        public void FormOpenning(Form frm)
        {
            panel1.Controls.Clear();
            frm.MdiParent = Application.OpenForms["MainWindow"];
            frm.FormBorderStyle = FormBorderStyle.None;
            panel1.Controls.Add(frm);
            frm.Show();
            frm.Dock = DockStyle.Fill;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            button1.BackColor = Color.LightSkyBlue;
            button2.BackColor = Color.Transparent;
            button3.BackColor = Color.Transparent;
            button4.BackColor = Color.Transparent;
            button6.BackColor = Color.Transparent;
            CustomerRegistration result = new CustomerRegistration();
            FormOpenning(result);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.BackColor = Color.LightSkyBlue;
            button1.BackColor = Color.Transparent;
            button3.BackColor = Color.Transparent;
            button4.BackColor = Color.Transparent;
            button6.BackColor = Color.Transparent;
            OrderList result = new OrderList();
            FormOpenning(result);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.BackColor = Color.LightSkyBlue;
            button2.BackColor = Color.Transparent;
            button1.BackColor = Color.Transparent;
            button4.BackColor = Color.Transparent;
            button6.BackColor = Color.Transparent;
            OrderArchives result = new OrderArchives();
            FormOpenning(result);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button4.BackColor = Color.LightSkyBlue;
            button2.BackColor = Color.Transparent;
            button3.BackColor = Color.Transparent;
            button1.BackColor = Color.Transparent;
            button6.BackColor = Color.Transparent;
            CurrentReceipts result = new CurrentReceipts();
            FormOpenning(result);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            button6.BackColor = Color.LightSkyBlue;
            button2.BackColor = Color.Transparent;
            button3.BackColor = Color.Transparent;
            button4.BackColor = Color.Transparent;
            button1.BackColor = Color.Transparent;
            Settings result = new Settings();
            FormOpenning(result);
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            reload();
            tablo.Columns.Add("AD", typeof(string));//0
            tablo.Columns.Add("SOYAD", typeof(string));//1
            tablo.Columns.Add("TC", typeof(string));//2
            tablo.Columns.Add("TELEFON", typeof(string));//3
            tablo.Columns.Add("EMAİL", typeof(string));//4
            tablo.Columns.Add("ADRES", typeof(string));//5
            tablo.Columns.Add("TARİH", typeof(DateTime));//6
            tablo.Columns.Add("PAKET", typeof(string));//7
            tablo.Columns.Add("ÜCRET", typeof(Double));//8
            tablo.Columns.Add("ÖDENEN", typeof(Double));//9
        }

        void reload()
        {
            int count = 0;
            var dosyalar = File.ReadLines(Application.StartupPath + "\\OrderList\\Orders.save");
            foreach (var dosya in dosyalar)
            {
                count++;
            }

            listBox1.Items.Clear();
            DateTime[] orders = new DateTime[count];
            int i = 0;
            DateTime today = DateTime.Now;
            DateTime answer = today.AddDays(1);

            foreach (var dosya in dosyalar)
            {
                string[] parca = dosya.Split('½');
                try
                {
                    orders[i] = Convert.ToDateTime(parca[6]);
                    monthCalendar1.BoldedDates = orders;
                }
                catch (Exception)
                { }
                if (Convert.ToDateTime(parca[6]) >= DateTime.Today && Convert.ToDateTime(parca[6]) <= answer)
                {
                    listBox1.Items.Add(parca[0] + " " + parca[1] + " " + parca[6]);
                }
                i++;
            }
        }

        private void MainWindow_Activated(object sender, EventArgs e)
        {
            reload();
            if (activiteRules == true)
            {
                if (button2.BackColor == Color.LightSkyBlue)
                {
                    button2.PerformClick();
                }
                else if (button3.BackColor == Color.LightSkyBlue)
                {
                    button3.PerformClick();
                }
                else if (button6.BackColor == Color.LightSkyBlue)
                {
                    button6.PerformClick();
                }

                activiteRules = false;
            }

        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string[] variable;
                var dosyalar = File.ReadLines(Application.StartupPath + "\\OrderList\\Orders.save");
                int i1 = 0;
                int i = 0;
                foreach (var obj in dosyalar)
                {
                    i1++;
                }

                variable = new string[i1];
                foreach (var obj in dosyalar)
                {
                    variable[i] = obj;
                    string[] deg = obj.Split('½');
                    string dosya = listBox1.SelectedItem.ToString();

                    for (int j = 0; j <= i; j++)
                    {
                        if (deg[0] + " " + deg[1] + " " + deg[6] == dosya)
                        {
                            string[] parca = variable[i].Split('½');
                            OrderDetails f = new OrderDetails();
                            f.label13.Text = parca[0];
                            f.label14.Text = parca[1];
                            f.label8.Text = parca[2];
                            f.label12.Text = parca[3];
                            f.label10.Text = parca[4];
                            f.label11.Text = parca[5];
                            f.label9.Text = parca[6] + ":00";
                            f.label15.Text = parca[7];
                            f.label18.Text = parca[8] + "₺";
                            f.label19.Text = parca[9] + "₺";
                            double kalan = Convert.ToDouble(parca[8]) - Convert.ToDouble(parca[9]);
                            f.label21.Text = kalan + "₺";
                            if (kalan == 0)
                            {
                                f.button1.Enabled = false;
                                f.button1.Text = "ÖDEME ALINDI\n✓";
                            }

                            f.TopMost = true;
                            f.Show();
                            return;
                        }
                    }

                    i++;
                }
            }
            catch (Exception)
            { }
        }
        DataTable tablo = new DataTable();
        CustomOrderList frm = new CustomOrderList();
        int i = 0;
        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            i++;
            if (i % 2 == 0)
            {
                tablo.Clear();

                DateTime today = monthCalendar1.SelectionRange.Start;
                DateTime answer = today.AddDays(1);
                var dosyalar = File.ReadLines(Application.StartupPath + "\\OrderList\\Orders.save");
                foreach (var dosya in dosyalar)
                {
                    string[] parca = dosya.Split('½');
                    if (Convert.ToDateTime(parca[6]) >= today && Convert.ToDateTime(parca[6]) <= answer)
                    {
                        tablo.Rows.Add(parca[0], parca[1], parca[2], parca[3], parca[4], parca[5], parca[6], parca[7],
                            parca[8], parca[9]);
                    }

                }

                frm.dataGridView1.DataSource = null;
                frm.dataGridView1.DataSource = tablo;
                frm.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                frm.dataGridView1.Sort(frm.dataGridView1.Columns[6], ListSortDirection.Ascending);
                frm.textBox1.TextChanged += new EventHandler(rse);
                frm.Show();
            }
        }

        void rse(object sender, EventArgs e)
        {
            try
            {
                DataView dv = tablo.DefaultView;
                dv.RowFilter = "Ad LIKE '" + frm.textBox1.Text + "%'"
                               + "OR Soyad LIKE '" + frm.textBox1.Text + "%'"
                               + "OR Tc LIKE '" + frm.textBox1.Text + "%'"
                               + "OR Telefon LIKE '" + frm.textBox1.Text + "%'"
                               + "OR Email LIKE '" + frm.textBox1.Text + "%'"
                               + "OR Adres LIKE '" + frm.textBox1.Text + "%'"
                               + "OR Paket LIKE '" + frm.textBox1.Text + "%'";
                frm.dataGridView1.DataSource = dv;
            }
            catch (Exception)
            { }
        }
    }
}
