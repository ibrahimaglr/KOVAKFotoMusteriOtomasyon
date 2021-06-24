using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FotoMusteriOtomasyon
{
    public partial class CustomerRegistration : Form
    {
        public CustomerRegistration()
        {
            InitializeComponent();
        }

        private void CustomerRegistration_Load(object sender, EventArgs e)
        {
            label9.Text = dateTimePicker1.Value.ToShortDateString() + " " + dateTimePicker1.Text.Substring(0, dateTimePicker1.Text.Length - 3);
            var ürün = File.ReadLines(Application.StartupPath + @"\Menus\MenuNames.save");
            foreach (var ür in ürün)
            {

                comboBox1.Items.Add(ür + "₺");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //boş alan kalmasın
            if (textBox1.Text != "" && textBox2.Text != "" && textBox5.Text != "" && textBox3.Text != "" && textBox4.Text != "" && richTextBox1.Text != "")
            {
                OrderPreview f = new OrderPreview();
                var dosyalar = File.ReadLines(Application.StartupPath + "\\OrderList\\Orders.save");
                foreach (var dosya in dosyalar)
                {
                    string[] parca = dosya.Split('½');
                    if (parca[6] == label9.Text)
                    {
                        f.label21.Visible = true;
                    }

                }
                f.label13.Text = textBox1.Text;
                f.label14.Text = textBox2.Text;
                f.label8.Text = textBox5.Text;
                f.label12.Text = textBox3.Text;
                f.label10.Text = textBox4.Text;
                f.label11.Text = richTextBox1.Text;
                f.label9.Text = dateTimePicker1.Value.ToShortDateString() + " " + dateTimePicker1.Text.Substring(0, dateTimePicker1.Text.Length - 3);
                f.richTextBox1.Text = "";
                foreach (var objj in listBox1.Items)
                {
                    if (objj == listBox1.Items[listBox1.Items.Count - 1])
                    {
                        f.richTextBox1.Text += objj.ToString();
                    }
                    else
                    {
                        f.richTextBox1.Text += objj.ToString() + " + ";
                    }
                }
                f.label18.Text = label8.Text;
                f.button3.Click += new EventHandler(result1);
                f.Show();
            }
            else
            {
                label11.Visible = true;
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
                button1.Enabled = false;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message + "\nLÜTFEN ÜRÜN SEÇİNİZ");

            }

        }

        void result1(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            listBox1.Items.Clear();
            richTextBox1.Clear();
            comboBox1.SelectedItem = null;
            label8.Text = "0₺";
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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            label9.Text = dateTimePicker1.Value.ToShortDateString() + " " + dateTimePicker1.Text.Substring(0, dateTimePicker1.Text.Length - 3);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button6.Enabled = true;
            button1.Enabled = true;
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
            button6.Enabled = false;
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        void reslt()
        {
            if (label11.Visible == true)
            {
                label11.Visible = false;
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            reslt();

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            reslt();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            reslt();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            reslt();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            reslt();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            reslt();
        }
    }
}
