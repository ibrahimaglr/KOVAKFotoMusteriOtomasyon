using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FotoMusteriOtomasyon
{
    public partial class OrderDetails : Form
    {
        public OrderDetails()
        {
            InitializeComponent();
        }
        public string[] variable;
        int result = 0;
        private int result2 = 0;
        ChangeOrder Form = new ChangeOrder();
        private void button1_Click(object sender, EventArgs e)
        {
            TopMost = false;
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
                i++;
            }
            string dosya = label13.Text + "½" + label14.Text + "½" + label8.Text + "½" + label12.Text + "½" + label10.Text +
                           "½" + label11.Text + "½" + label9.Text.Substring(0, label9.Text.Length - 3) + "½" + label15.Text + "½"
                           + label18.Text.Substring(0, label18.Text.Length - 1) + "½" + label19.Text.Substring(0, label19.Text.Length - 1);
            for (int j = 0; j < i; j++)
            {

                if (variable[j] == dosya)
                {
                    result = j;
                }
            }
            string[] parca = variable[result].Split('½');
            string[] items = parca[7].Split('₺');
            Form.textBox1.Text = parca[0];
            Form.textBox2.Text = parca[1];
            Form.textBox5.Text = parca[2];
            Form.textBox3.Text = parca[3];
            Form.textBox4.Text = parca[4];
            Form.richTextBox1.Text = parca[5];
            Form.dateTimePicker1.Value = Convert.ToDateTime(parca[6] + ":00");
            for (int j = 0; j < items.Length - 1; j++)
            {
                if (j > 0)
                {
                    Form.listBox1.Items.Add(items[j].Substring(2, items[j].Length - 2) + "₺");
                }
                else
                {
                    Form.listBox1.Items.Add(items[j] + "₺");
                }
            }
            Form.label8.Text = parca[8] + "₺";
            Form.textBox6.Text = parca[9];
            Form.button3.Click += new EventHandler(changesave);
            Form.FormClosing += new FormClosingEventHandler(koruma);
            Form.Show();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            TopMost = false;
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
                i++;
            }
            string dosya = label13.Text + "½" + label14.Text + "½" + label8.Text + "½" + label12.Text + "½" + label10.Text +
                           "½" + label11.Text + "½" + label9.Text.Substring(0, label9.Text.Length - 3) + "½" + label15.Text + "½"
                           + label18.Text.Substring(0, label18.Text.Length - 1) + "½" + label19.Text.Substring(0, label19.Text.Length - 1);
            for (int j = 0; j < i; j++)
            {

                if (variable[j] == dosya)
                {
                    result = j;
                }
            }
            string[] parca = variable[result].Split('½');
            string[] items = parca[7].Split('₺');
            Form.textBox1.Text = parca[0];
            Form.textBox2.Text = parca[1];
            Form.textBox5.Text = parca[2];
            Form.textBox3.Text = parca[3];
            Form.textBox4.Text = parca[4];
            Form.richTextBox1.Text = parca[5];
            Form.dateTimePicker1.Value = Convert.ToDateTime(parca[6] + ":00");
            for (int j = 0; j < items.Length - 1; j++)
            {
                if (j > 0)
                {
                    Form.listBox1.Items.Add(items[j].Substring(2, items[j].Length - 2) + "₺");
                }
                else
                {
                    Form.listBox1.Items.Add(items[j] + "₺");
                }
            }
            Form.label8.Text = parca[8] + "₺";
            Form.textBox6.Text = parca[9];
            Form.button3.Click += new EventHandler(changesave);
            Form.FormClosing += new FormClosingEventHandler(koruma);
            Form.Show();
        }

        void koruma(object sender, FormClosingEventArgs e)
        {
            this.Close();
            MainWindow.activiteRules = true;
        }

        void changesave(object sender, EventArgs e)
        {
            string toplam = "";
            foreach (var objj in Form.listBox1.Items)
            {
                if (objj == Form.listBox1.Items[Form.listBox1.Items.Count - 1])
                {
                    toplam += objj.ToString();
                }
                else
                {
                    toplam += objj.ToString() + " + ";
                }
            }
            string tarih = Form.dateTimePicker1.Value.ToShortDateString() + " " +
                           Form.dateTimePicker1.Text.Substring(0, Form.dateTimePicker1.Text.Length - 3);
            variable[result] = Form.textBox1.Text + "½" + Form.textBox2.Text + "½" + Form.textBox5.Text + "½" + Form.textBox3.Text + "½" + Form.textBox4.Text +
                               "½" + Form.richTextBox1.Text + "½" + tarih + "½" + toplam + "½"
                               + Form.label8.Text.Substring(0, Form.label8.Text.Length - 1) + "½" + Form.textBox6.Text;

            using (System.IO.StreamWriter veri = new System.IO.StreamWriter(Application.StartupPath + "\\OrderList\\Orders.save"))
                veri.Write("");
            {
                foreach (object obj in variable)
                {
                    using (System.IO.StreamWriter veri = new System.IO.StreamWriter(Application.StartupPath + "\\OrderList\\Orders.save", true))
                        veri.WriteLine(obj.ToString());
                }
            }
            this.Close();
            Form.Close();
            MainWindow.activiteRules = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bu Kaydı Silmek istediğinize emin misiniz?", "FotoOtomasyon", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
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
                    i++;
                }
                string dosya = label13.Text + "½" + label14.Text + "½" + label8.Text + "½" + label12.Text + "½" + label10.Text +
                               "½" + label11.Text + "½" + label9.Text.Substring(0, label9.Text.Length - 3) + "½" + label15.Text + "½"
                               + label18.Text.Substring(0, label18.Text.Length - 1) + "½" + label19.Text.Substring(0, label19.Text.Length - 1);
                for (int j = 0; j < i; j++)
                {

                    if (variable[j] == dosya)
                    {
                        result2 = j;
                        variable[j] = "DELETE";
                    }
                }
                using (System.IO.StreamWriter veri = new System.IO.StreamWriter(Application.StartupPath + "\\OrderList\\Orders.save"))
                    veri.Write("");
                {
                    foreach (object obj in variable)
                    {
                        if (obj.ToString() == "DELETE")
                        {
                            MessageBox.Show("KAYIT SİLİNDİ");
                        }
                        else
                        {
                            using (System.IO.StreamWriter veri = new System.IO.StreamWriter(Application.StartupPath + "\\OrderList\\Orders.save", true))
                                veri.WriteLine(obj.ToString());
                        }

                    }
                }
                this.Close();
                Form.Close();
                MainWindow.activiteRules = true;
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
            foreach (var obj in label15.Text.Split('+'))
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
    }
}
