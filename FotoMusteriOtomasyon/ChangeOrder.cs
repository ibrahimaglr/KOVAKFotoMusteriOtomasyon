using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FotoMusteriOtomasyon
{
    public partial class ChangeOrder : Form
    {
        public ChangeOrder()
        {
            InitializeComponent();
        }

        private void ChangeOrder_Load(object sender, EventArgs e)
        {
            label9.Text = dateTimePicker1.Value.ToShortDateString() + " " + dateTimePicker1.Text.Substring(0, dateTimePicker1.Text.Length - 3);
            var ürün = File.ReadLines(Application.StartupPath + @"\Menus\MenuNames.save");
            foreach (var ür in ürün)
            {

                comboBox1.Items.Add(ür + "₺");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string metin = comboBox1.Text;
            string[] sonDeger = metin.Split(' ');
            listBox1.Items.Add(metin);
            if (Convert.ToDouble(label8.Text.Substring(0, label8.Text.Length - 1)) <= 0)
            {
                label8.Text = (sonDeger.Last());
            }
            else if (Convert.ToDouble(label8.Text.Substring(0, label8.Text.Length - 1)) > 0)
            {
                try
                {
                    double result = (Convert.ToDouble(label8.Text.Substring(0, label8.Text.Length - 1)) + Convert.ToDouble(sonDeger.Last().Substring(0, sonDeger.Last().Length - 1)));
                    label8.Text = Convert.ToString(result) + "₺";
                }
                catch (Exception)
                {

                }

            }
        }
        ChangePrice frm = new ChangePrice();
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                frm.Location = new Point(Cursor.Position.X, Cursor.Position.Y - 100);
                string[] res = listBox1.SelectedItem.ToString().Split(' ');
                frm.textBox1.Text = res.Last().Substring(0, (res.Last().Length - 1));
                frm.button12.Click += new EventHandler(result);
                frm.Show();
                button6.Enabled = false;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message + "\nLÜTFEN ÜRÜN SEÇİNİZ");

            }
        }
        void result(object sender, EventArgs e)
        {
            int seciliolan = listBox1.SelectedIndex;
            string[] parca = listBox1.SelectedItem.ToString().Split(' ');
            listBox1.Items[seciliolan] = "";
            if (frm.textBox1.Text == "")
            {
                for (int i = 0; i < parca.Length - 1; i++)
                {
                    listBox1.Items[seciliolan] += parca[i].ToString() + " ";
                }

                listBox1.Items[seciliolan] += "0₺";
            }
            else
            {
                for (int i = 0; i < parca.Length - 1; i++)
                {
                    listBox1.Items[seciliolan] += parca[i].ToString() + " ";
                }

                listBox1.Items[seciliolan] += frm.textBox1.Text + "₺";
            }
            frm.Hide();
            double result1 = 0;
            foreach (var obj in listBox1.Items)
            {
                string[] parc = obj.ToString().Split(' ');
                result1 += Convert.ToDouble(parc.Last().Substring(0, parc.Last().Length - 1));
            }

            label8.Text = result1.ToString() + "₺";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Remove(listBox1.SelectedItem);
            double result1 = 0;
            foreach (var obj in listBox1.Items)
            {
                string[] parc = obj.ToString().Split(' ');
                result1 += Convert.ToDouble(parc.Last().Substring(0, parc.Last().Length - 1));
            }

            label8.Text = result1.ToString() + "₺";
            button1.Enabled = false;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button6.Enabled = true;
            button1.Enabled = true;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                label9.Text = dateTimePicker1.Value.ToShortDateString() + " " + dateTimePicker1.Text.Substring(0, dateTimePicker1.Text.Length - 3);
            }
            catch (Exception)
            { }

        }

        public void ChangeOrder_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToDouble(textBox6.Text) > Convert.ToDouble(label8.Text.Substring(0, label8.Text.Length - 1)))
            {
                MessageBox.Show("Sipariş Tutarından fazla ödeme alınamaz", "Kovak Foto Müşteri Otomasyonu",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox6.Text = label8.Text.Substring(0, label8.Text.Length - 1);
            }
        }
    }
}
