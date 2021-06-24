using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FotoMusteriOtomasyon
{
    public partial class OrderPreview : Form
    {
        public OrderPreview()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult onay = MessageBox.Show("Kayıt Oluştur? " + Environment.NewLine + " ",
                "FotoKayıtOtom", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (onay == DialogResult.Yes)
            {
                string dosya_dizini = Application.StartupPath + "\\OrderList\\" + "Orders.save";

                if (File.Exists(@dosya_dizini) == true)
                {
                    using (System.IO.StreamWriter veri = new System.IO.StreamWriter(dosya_dizini, true))
                    {
                        string itemlr = label13.Text + "½" + label14.Text + "½" + label8.Text + "½" + label12.Text + "½" + label10.Text +
                                        "½" + label11.Text + "½" + label9.Text + "½" + richTextBox1.Text + "½" + label18.Text.Substring(0, label18.Text.Length - 1) + "½" +
                                        label19.Text.Substring(0, label19.Text.Length - 1);
                        veri.WriteLine(itemlr);
                    }
                    Result form = new Result();
                    form.Show();
                    this.Close();
                }
                else
                {
                    FileStream fs = File.Create(dosya_dizini);
                    fs.Close();
                    using (System.IO.StreamWriter veri = new System.IO.StreamWriter(dosya_dizini, true))
                    {
                        string itemlr = label13.Text + "½" + label14.Text + "½" + label8.Text + "½" + label12.Text + "½" + label10.Text +
                                        "½" + label11.Text + "½" + label9.Text + "½" + richTextBox1.Text + "½" + label18.Text.Substring(0, label18.Text.Length - 1) + "½" +
                                        label19.Text.Substring(0, label19.Text.Length - 1);
                        veri.WriteLine(itemlr);
                    }

                    Result form = new Result();
                    form.Show();
                    this.Close();

                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                button3.Enabled = true;
                if (Convert.ToDouble(label19.Text.Substring(0, label19.Text.Length - 1)) <= 0)
                {
                    MessageBox.Show("Önödeme Tutarı Girilmedi.", "FotoMüşteri", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }

                if (label21.Visible == true)
                {
                    MessageBox.Show("AYNI SAATTE BAŞKA ÇEKİMLERDE MEVCUT", "FotoMüşteri", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            else
            {
                button3.Enabled = false;

            }
        }
        System.Drawing.Printing.PrintDocument pd = new System.Drawing.Printing.PrintDocument();
        private void button4_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            try
            {
                pd.PrinterSettings.PrinterName = Properties.Settings.Default.PrinterPreference;
                printPreviewDialog1.Document = pd;
                pd.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
                printPreviewDialog1.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Yazıcı Tercihinde Hata Oluştu Ayarlardan Yazıcı Tercihlerinizi Kontrol Ediniz: \n {0}", ex.Message), "Yazdırma Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        Font başlık = new Font("Verdana", 10, FontStyle.Bold);
        Font ürün = new Font("Verdana", 7, FontStyle.Bold);
        Font içerik = new Font("Verdana", 7);
        SolidBrush sb = new SolidBrush(Color.Black);
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            StringFormat st = new StringFormat();
            StringFormat sd = new StringFormat();
            sd.LineAlignment = StringAlignment.Center;
            e.Graphics.DrawString(Properties.Settings.Default.Company.ToUpper(), başlık, sb, 10, 40, sd);
            e.Graphics.DrawString("Fiş Kesim Tarihi: " + DateTime.Now, içerik, sb, 20, 70, st);
            e.Graphics.DrawString("-----------------------------------------------", başlık, sb, 0, 75, st);
            e.Graphics.DrawString("ÇEKİM TARİHİ: " + label9.Text, içerik, sb, 30, 80 + 1 * 15, st);
            e.Graphics.DrawString("AD: " + label13.Text, ürün, sb, 30, 80 + 2 * 15, st);
            e.Graphics.DrawString("SOYAD: " + label14.Text, ürün, sb, 30, 80 + 3 * 15, st);
            int count = 4;
            foreach (var obj in richTextBox1.Text.Split('+'))
            {
                e.Graphics.DrawString(obj, içerik, sb, 30, 80 + count * 15, st);
                count++;
            }
            e.Graphics.DrawString("-----------------------------------------------", başlık, sb, 0, 80 + (count) * 15, st);
            e.Graphics.DrawString("Toplam Alınacak Ücret: " + label18.Text, içerik, sb, 10, 80 + (count + 1) * 15, st);
            e.Graphics.DrawString("Ödenen Ücret: " + label19.Text, içerik, sb, 52, 80 + (count + 2) * 15, st);
            e.Graphics.DrawString("Kalan Ödeme: " + Convert.ToString(Convert.ToDouble(label18.Text.Substring(0, label18.Text.Length - 1)) - Convert.ToDouble(label19.Text.Substring(0, label19.Text.Length - 1))) + "₺"
                , ürün, sb, 43, 85 + (count + 3) * 15, st);
            this.TopMost = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button2.Enabled = true;
            try
            {
                if (Convert.ToDouble(textBox1.Text) > Convert.ToDouble(label18.Text.Substring(0, label18.Text.Length - 1)))
                {
                    MessageBox.Show("Sipariş Tutarından fazla ödeme alınamaz", "Kovak Foto Müşteri Otomasyonu",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Text = label18.Text.Substring(0, label18.Text.Length - 1);
                }

            }
            catch (Exception)
            { }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            button2.Enabled = false;
            try
            {
                if (textBox1.Text.Last() == Convert.ToChar(","))
                {

                    textBox1.Text += 0;
                }


            }
            catch (Exception)
            { }
            if (textBox1.Text == "")
            {
                textBox1.Text += 0;
            }
            label19.Text = textBox1.Text + "₺";
        }
        DataTable tablo = new DataTable();
        CustomOrderList f = new CustomOrderList();
        private void label21_Click(object sender, EventArgs e)
        {
            //AYNI TARİHTEKİ ÇEKİMLERE GİT label9.text
            this.TopMost = false;
            {
                //SEÇİLİ TARİHİN ÇEKİMLERİNİ GÖSTER
                tablo.Clear();

                var dosyalar = File.ReadLines(Application.StartupPath + "\\OrderList\\Orders.save");
                foreach (var dosya in dosyalar)
                {
                    string[] parca = dosya.Split('½');
                    if (parca[6] == label9.Text)
                    {
                        tablo.Rows.Add(parca[0], parca[1], parca[2], parca[3], parca[4], parca[5], parca[6], parca[7],
                            parca[8], parca[9]);
                    }
                }
                f.dataGridView1.DataSource = null;
                f.dataGridView1.DataSource = tablo;
                f.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                f.dataGridView1.Sort(f.dataGridView1.Columns[6], ListSortDirection.Ascending);
                f.textBox1.TextChanged += new EventHandler(rse);
                f.Show();
            }


        }
        void rse(object sender, EventArgs e)
        {
            try
            {
                DataView dv = tablo.DefaultView;
                dv.RowFilter = "Ad LIKE '" + f.textBox1.Text + "%'"
                               + "OR Soyad LIKE '" + f.textBox1.Text + "%'"
                               + "OR Tc LIKE '" + f.textBox1.Text + "%'"
                               + "OR Telefon LIKE '" + f.textBox1.Text + "%'"
                               + "OR Email LIKE '" + f.textBox1.Text + "%'"
                               + "OR Adres LIKE '" + f.textBox1.Text + "%'"
                               + "OR Paket LIKE '" + f.textBox1.Text + "%'";
                f.dataGridView1.DataSource = dv;
            }
            catch (Exception)
            { }
        }

        private void OrderPreview_Load(object sender, EventArgs e)
        {
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

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }
    }
}
