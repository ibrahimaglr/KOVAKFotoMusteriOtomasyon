using System;
using System.Windows.Forms;

namespace FotoMusteriOtomasyon
{
    public partial class Result : Form
    {
        public Result()
        {
            InitializeComponent();
        }
        int result = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            result++;
            if (result == 2)
            {
                this.Close();
            }
        }

        private void Result_Activated(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }
    }
}
