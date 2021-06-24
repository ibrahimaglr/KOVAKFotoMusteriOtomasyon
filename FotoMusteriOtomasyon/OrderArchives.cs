using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace FotoMusteriOtomasyon
{
    public partial class OrderArchives : Form
    {
        public OrderArchives()
        {
            InitializeComponent();
        }

        DataTable tablo = new DataTable();

        private void OrderArchives_Load(object sender, EventArgs e)
        {
            tablo.Columns.Add("AD", typeof(string)); //0
            tablo.Columns.Add("SOYAD", typeof(string)); //1
            tablo.Columns.Add("TC", typeof(string)); //2
            tablo.Columns.Add("TELEFON", typeof(string)); //3
            tablo.Columns.Add("EMAİL", typeof(string)); //4
            tablo.Columns.Add("ADRES", typeof(string)); //5
            tablo.Columns.Add("TARİH", typeof(DateTime)); //6
            tablo.Columns.Add("PAKET", typeof(string)); //7
            tablo.Columns.Add("ÜCRET", typeof(string)); //8
            tablo.Columns.Add("ÖDENEN", typeof(Double)); //9
            dataGridView1.DataSource = tablo;
            dataGridView1.Columns[0].Width = 80;
            dataGridView1.Columns[1].Width = 70;
            dataGridView1.Columns[2].Width = 80;
            dataGridView1.Columns[3].Width = 90;
            dataGridView1.Columns[4].Width = 90;
            dataGridView1.Columns[5].Width = 140;
            dataGridView1.Columns[6].Width = 100;
            dataGridView1.Columns[7].Width = 150;
            dataGridView1.Columns[8].Width = 50;
            var dosyalar = File.ReadLines(Application.StartupPath + "\\OrderList\\Orders.save");
            foreach (var dosya in dosyalar)
            {
                string[] parca = dosya.Split('½');
                tablo.Rows.Add(parca[0], parca[1], parca[2], parca[3], parca[4], parca[5], parca[6], parca[7], parca[8],
                    parca[9]);
            }

            if (Properties.Settings.Default.maxsize == true)
            {
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            else
            {
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            }

            dataGridView1.Sort(dataGridView1.Columns[6], ListSortDirection.Descending);
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            OrderDetails f = new OrderDetails();
            f.label13.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            f.label14.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            f.label8.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            f.label12.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            f.label10.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            f.label11.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            f.label9.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            f.label15.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            f.label18.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString() + "₺";
            f.label19.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString() + "₺";
            double kalan = Convert.ToDouble(dataGridView1.CurrentRow.Cells[8].Value.ToString()) -
                           Convert.ToDouble(dataGridView1.CurrentRow.Cells[9].Value.ToString());
            f.label21.Text = kalan + "₺";
            if (kalan == 0)
            {
                f.button1.Enabled = false;
                f.button1.Text = "ÖDEME ALINDI\n✓";
            }

            f.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataView dv = tablo.DefaultView;
                dv.RowFilter = "Ad LIKE '" + textBox1.Text + "%'"
                               + "OR Soyad LIKE '" + textBox1.Text + "%'"
                               + "OR Tc LIKE '" + textBox1.Text + "%'"
                               + "OR Telefon LIKE '" + textBox1.Text + "%'"
                               + "OR Email LIKE '" + textBox1.Text + "%'"
                               + "OR Adres LIKE '" + textBox1.Text + "%'"
                               + "OR Paket LIKE '" + textBox1.Text + "%'";
                dataGridView1.DataSource = dv;
            }
            catch (Exception)
            {
            }
        }
    }
}
