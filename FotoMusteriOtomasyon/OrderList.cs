using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FotoMusteriOtomasyon
{
    public partial class OrderList : Form
    {
        public OrderList()
        {
            InitializeComponent();
        }
        DataTable tablo = new DataTable();

        public void OrderList_Load(object sender, EventArgs e)
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
                if (Convert.ToDateTime(parca[6]) >= DateTime.Today)
                {

                    tablo.Rows.Add(parca[0], parca[1], parca[2], parca[3], parca[4], parca[5], parca[6], parca[7], parca[8], parca[9]);
                }

            }

            if (Properties.Settings.Default.maxsize == true)
            {
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            else
            {
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            }
            dataGridView1.Sort(dataGridView1.Columns[6], ListSortDirection.Ascending);
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
            double kalan = Convert.ToDouble(dataGridView1.CurrentRow.Cells[8].Value.ToString()) - Convert.ToDouble(dataGridView1.CurrentRow.Cells[9].Value.ToString());
            f.label21.Text = kalan + "₺";
            if (kalan == 0)
            {
                f.button1.Enabled = false;
                f.button1.Text = "ÖDEME ALINDI\n✓";
            }
            f.Show();
        }
        StringFormat strFormat;
        ArrayList arrColumnLefts = new ArrayList();
        ArrayList arrColumnWidths = new ArrayList();
        int iCellHeight = 0;
        int iTotalWidth = 0;
        int iRow = 0;
        bool bFirstPage = false;
        bool bNewPage = false;
        int iHeaderHeight = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            try
            {
                PrintPreviewDialog onizleme = new PrintPreviewDialog();
                onizleme.Document = printDocument1;
                printDocument1.PrinterSettings.PrinterName = Properties.Settings.Default.PrinterPreference2;
                onizleme.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Yazıcı Tercihinde Hata Oluştu Ayarlardan Yazıcı Tercihlerinizi Kontrol Ediniz: \n {0}", ex.Message), "Yazdırma Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            { }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                int iLeftMargin = e.MarginBounds.Left;
                int iTopMargin = e.MarginBounds.Top;
                bool bMorePagesToPrint = false;
                int iTmpWidth = 0;
                bFirstPage = true;

                if (bFirstPage)
                {
                    foreach (DataGridViewColumn GridCol in dataGridView1.Columns)
                    {
                        iTmpWidth = (int)(Math.Floor((double)((double)GridCol.Width /
                                       (double)iTotalWidth * (double)iTotalWidth *
                                       ((double)e.MarginBounds.Width / (double)iTotalWidth))));

                        iHeaderHeight = (int)(e.Graphics.MeasureString(GridCol.HeaderText,
                                    GridCol.InheritedStyle.Font, iTmpWidth).Height) + 11;


                        arrColumnLefts.Add(iLeftMargin);
                        arrColumnWidths.Add(iTmpWidth);
                        iLeftMargin += iTmpWidth;
                    }
                }

                while (iRow <= dataGridView1.Rows.Count - 1)
                {
                    DataGridViewRow GridRow = dataGridView1.Rows[iRow];

                    iCellHeight = GridRow.Height + 5;
                    int iCount = 0;

                    if (iTopMargin + iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
                    {
                        bNewPage = true;
                        bFirstPage = false;
                        bMorePagesToPrint = true;
                        break;
                    }
                    else
                    {
                        if (bNewPage)
                        {

                            e.Graphics.DrawString("Çekim Listesi", new Font(dataGridView1.Font, FontStyle.Bold),
                                    Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Top -
                                    e.Graphics.MeasureString("Çekim Listesi", new Font(dataGridView1.Font,
                                    FontStyle.Bold), e.MarginBounds.Width).Height - 13);
                            String strDate = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();
                            e.Graphics.DrawString(strDate, new Font(dataGridView1.Font, FontStyle.Bold),
                                    Brushes.Black, e.MarginBounds.Left + (e.MarginBounds.Width -
                                    e.Graphics.MeasureString(strDate, new Font(dataGridView1.Font,
                                    FontStyle.Bold), e.MarginBounds.Width).Width), e.MarginBounds.Top -
                                    e.Graphics.MeasureString("Çekim Listesi", new Font(new Font(dataGridView1.Font,
                                    FontStyle.Bold), FontStyle.Bold), e.MarginBounds.Width).Height - 13);


                            iTopMargin = e.MarginBounds.Top;
                            foreach (DataGridViewColumn GridCol in dataGridView1.Columns)
                            {
                                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray),
                                    new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight));

                                e.Graphics.DrawRectangle(Pens.Black,
                                    new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight));

                                e.Graphics.DrawString(GridCol.HeaderText, GridCol.InheritedStyle.Font,
                                    new SolidBrush(GridCol.InheritedStyle.ForeColor),
                                    new RectangleF((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight), strFormat);
                                iCount++;
                            }
                            bNewPage = false;
                            iTopMargin += iHeaderHeight;
                        }
                        iCount = 0;

                        foreach (DataGridViewCell Cel in GridRow.Cells)
                        {
                            if (Cel.Value != null)
                            {
                                e.Graphics.DrawString(Cel.Value.ToString(), Cel.InheritedStyle.Font,
                                            new SolidBrush(Cel.InheritedStyle.ForeColor),
                                            new RectangleF((int)arrColumnLefts[iCount], (float)iTopMargin,
                                            (int)arrColumnWidths[iCount], (float)iCellHeight), strFormat);
                            }

                            e.Graphics.DrawRectangle(Pens.Black, new Rectangle((int)arrColumnLefts[iCount],
                                    iTopMargin, (int)arrColumnWidths[iCount], iCellHeight));

                            iCount++;
                        }
                    }
                    iRow++;
                    iTopMargin += iCellHeight;
                }


                if (bMorePagesToPrint)
                    e.HasMorePages = true;
                else
                    e.HasMorePages = false;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                strFormat = new StringFormat();
                strFormat.Alignment = StringAlignment.Near;
                strFormat.LineAlignment = StringAlignment.Center;
                strFormat.Trimming = StringTrimming.EllipsisCharacter;

                arrColumnLefts.Clear();
                arrColumnWidths.Clear();
                iCellHeight = 0;
                iRow = 0;
                bFirstPage = true;
                bNewPage = true;

                iTotalWidth = 0;
                foreach (DataGridViewColumn dgvGridCol in dataGridView1.Columns)
                {
                    iTotalWidth += dgvGridCol.Width;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
