using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FotoMusteriOtomasyon
{
    public partial class ProductSettings : Form
    {
        public ProductSettings()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox2.Enabled = true;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            button3.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //item ekleme yeri buraya text1 ürün text2 fiyat
            if (textBox2.Text == "")
            {
                textBox2.Text = "0";
            }
            listBox1.Items.Add(textBox1.Text + " " + textBox2.Text + "₺");
            textBox1.Clear();
            textBox2.Clear();
            textBox2.Enabled = false;
            button3.Enabled = false;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //silme işlemi buraya
            button1.Enabled = false;
            button2.Enabled = false;
            listBox1.Items.Remove(listBox1.SelectedItem);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //değiştirme işlemi buraya
            button1.Enabled = false;
            button2.Enabled = false;
            textBox1.Clear();
            textBox2.Clear();
            try
            {
                string[] parc = listBox1.SelectedItem.ToString().Split(' ');
                listBox1.Items.Remove(listBox1.SelectedItem);
                for (int i = 0; i < parc.Length - 1; i++)
                {
                    textBox1.Text += parc[i].ToString() + " ";
                }
                textBox2.Text = parc.Last().Substring(0, parc.Last().Length - 1);
            }
            catch (Exception)
            { }


        }

        private void ProductSettings_Load(object sender, EventArgs e)
        {
            var ürün = File.ReadLines(Application.StartupPath + @"\Menus\MenuNames.save");
            foreach (var ür in ürün)
            {

                listBox1.Items.Add(ür + "₺");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (System.IO.StreamWriter veri = new System.IO.StreamWriter(Application.StartupPath + @"\Menus\MenuNames.save"))
                veri.Write("");
            {
                foreach (object obj in listBox1.Items)
                {
                    using (System.IO.StreamWriter veri = new System.IO.StreamWriter(Application.StartupPath + @"\Menus\MenuNames.save", true))
                        veri.WriteLine(obj.ToString().Substring(0, obj.ToString().Length - 1));
                }
            }
            this.Close();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
