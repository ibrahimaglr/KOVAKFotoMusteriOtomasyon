using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FotoMusteriOtomasyon
{
    public partial class CurrentReceipts : Form
    {
        public CurrentReceipts()
        {
            InitializeComponent();
        }
        DataTable tablo = new DataTable();
        private void CurrentReceipts_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker2.Value = DateTime.Today;
            tablo.Columns.Add("AD", typeof(string));//0
            tablo.Columns.Add("SOYAD", typeof(string));//1
            tablo.Columns.Add("TARİH", typeof(DateTime));//6
            tablo.Columns.Add("PAKET", typeof(string));//7
            tablo.Columns.Add("ÜCRET", typeof(Double));//8
            tablo.Columns.Add("ÖDENEN", typeof(Double));//9
            label1.Text = dateTimePicker1.Value.ToShortDateString() + " " + dateTimePicker1.Text.Substring(0, dateTimePicker1.Text.Length - 3);
            label3.Text = dateTimePicker2.Value.ToShortDateString() + " " + dateTimePicker2.Text.Substring(0, dateTimePicker2.Text.Length - 3);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            double toplam = 0;
            double toplam2 = 0;
            tablo.Clear();
            if (checkBox1.Checked)
            {
                var dosyalar = File.ReadLines(Application.StartupPath + "\\OrderList\\Orders.save");
                foreach (var dosya in dosyalar)
                {
                    string[] parca = dosya.Split('½');
                    toplam2 += Convert.ToDouble(parca[8]);
                    toplam += Convert.ToDouble(parca[9]);
                    tablo.Rows.Add(parca[0], parca[1], parca[6], parca[7],
                        parca[8], parca[9]);
                }
                dataGridView1.DataSource = tablo;
            }
            else if (checkBox2.Checked)
            {
                var dosyalar = File.ReadLines(Application.StartupPath + "\\OrderList\\Orders.save");
                foreach (var dosya in dosyalar)
                {
                    string[] parca = dosya.Split('½');
                    if (Convert.ToDouble(parca[8]) > Convert.ToDouble(parca[9]))
                    {
                        toplam2 += Convert.ToDouble(parca[8]);
                        toplam += Convert.ToDouble(parca[9]);
                        tablo.Rows.Add(parca[0], parca[1], parca[6], parca[7],
                            parca[8], parca[9]);
                    }
                }
                dataGridView1.DataSource = tablo;
            }
            else
            {
                var dosyalar = File.ReadLines(Application.StartupPath + "\\OrderList\\Orders.save");
                foreach (var dosya in dosyalar)
                {
                    string[] parca = dosya.Split('½');
                    if (Convert.ToDateTime(parca[6]) >= dateTimePicker1.Value && Convert.ToDateTime(parca[6]) <= dateTimePicker2.Value)
                    {
                        toplam2 += Convert.ToDouble(parca[8]);
                        toplam += Convert.ToDouble(parca[9]);
                        tablo.Rows.Add(parca[0], parca[1], parca[6], parca[7],
                            parca[8], parca[9]);
                    }

                }
                dataGridView1.DataSource = tablo;
            }
            label2.Text = String.Format("Alınan:{0}₺", toplam);
            label5.Text = String.Format("Alınacak:{0}₺", toplam2);
            label4.Text = String.Format("Fiş Sayısı:{0}", dataGridView1.Rows.Count);
            label6.Text = String.Format("Kalan:{0}₺", toplam2 - toplam);
            this.Cursor = Cursors.Default;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //formu yeniden
            // dataGridView1.DataSource = null;
            tablo.Clear();
            dataGridView1.DataSource = tablo;
            label2.Text = "Alınan:0₺";
            label4.Text = "Fiş Sayısı:0";
            label5.Text = "Alınacak:0₺";
            label6.Text = "Kalan:0₺";
            checkBox1.Checked = false;
            checkBox2.Checked = false;
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            //kayıtları aç mainwindow listboxtaki kodalr
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
                    string dosya = dataGridView1.CurrentRow.Cells[0].Value.ToString() + " " +
                                   dataGridView1.CurrentRow.Cells[1].Value.ToString() + " " +
                                   dataGridView1.CurrentRow.Cells[2].Value.ToString() + " " +
                                   dataGridView1.CurrentRow.Cells[3].Value.ToString() + " " +
                                   dataGridView1.CurrentRow.Cells[4].Value.ToString() + " " +
                                   dataGridView1.CurrentRow.Cells[5].Value.ToString();
                    string kars = deg[0] + " " + deg[1] + " " + deg[6] + ":00" + " " + deg[7] + " " + deg[8] + " " + deg[9];
                    for (int j = 0; j <= i; j++)
                    {
                        if (kars == dosya)
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
                            f.FormClosing += F_FormClosing;
                            //f.button2.Click += new EventHandler(changesave);
                            //f.button3.Click += new EventHandler(changesave);
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

        private void F_FormClosing(object sender, FormClosingEventArgs e)
        {
            button1.PerformClick();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            label1.Text = dateTimePicker1.Value.ToShortDateString() + " " + dateTimePicker1.Text.Substring(0, dateTimePicker1.Text.Length - 3);
            dateTimePicker2.MinDate = dateTimePicker1.Value;
            //ttimepicker2 seçili tarihten sonrasını görebiliyor
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            label3.Text = dateTimePicker2.Value.ToShortDateString() + " " + dateTimePicker2.Text.Substring(0, dateTimePicker2.Text.Length - 3);
            dateTimePicker1.MaxDate = dateTimePicker2.Value;
            //timepicker1 seçili tarihten öncesini görebiliyor
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
                label1.ForeColor = Color.DarkCyan;
                label3.ForeColor = Color.DarkCyan;
            }
            else
            {
                dateTimePicker1.Enabled = true;
                dateTimePicker2.Enabled = true;
                label1.ForeColor = Color.Black;
                label3.ForeColor = Color.Black;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
                label1.ForeColor = Color.DarkCyan;
                label3.ForeColor = Color.DarkCyan;
                checkBox1.Checked = false;
                checkBox1.Enabled = false;
            }
            else
            {
                dateTimePicker1.Enabled = true;
                dateTimePicker2.Enabled = true;
                label1.ForeColor = Color.Black;
                label3.ForeColor = Color.Black;
                checkBox1.Checked = false;
                checkBox1.Enabled = true;
            }
        }
    }
}
