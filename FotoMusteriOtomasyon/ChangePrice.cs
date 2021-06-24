using System;
using System.Windows.Forms;

namespace FotoMusteriOtomasyon
{
    public partial class ChangePrice : Form
    {
        public ChangePrice()
        {
            InitializeComponent();
        }

        private void ChangePrice_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text += 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text += 2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text += 3;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text += 4;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text += 5;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text += 6;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text += 7;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text += 8;
        }
        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text += 9;
        }
        private void button10_Click(object sender, EventArgs e)
        {
            textBox1.Text += 0;
        }
        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = textBox1.Text.Substring(0, (textBox1.Text.Length - 1));
            }
            catch (Exception)
            { }

        }

        public void button12_Click(object sender, EventArgs e)
        {

        }

        private void ChangePrice_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }
        private void button13_Click(object sender, EventArgs e)
        {

        }
    }
}
