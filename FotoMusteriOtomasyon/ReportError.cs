using System;
using System.Management;
using System.Net.Mail;
using System.Windows.Forms;

namespace FotoMusteriOtomasyon
{
    public partial class ReportError : Form
    {
        public ReportError()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            //mail gönder
            string processorInfo = null;
            string processorSerial = null;
            string osSerial = null;
            string osVersionInfo = null;
            string OSinfo = null;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * FROM WIN32_Processor");
            ManagementObjectCollection mObject = searcher.Get();
            foreach (ManagementObject obj in mObject)
            {
                processorSerial = obj["name"].ToString();
            }
            processorInfo = processorSerial;
            ManagementObjectSearcher osInfo = new ManagementObjectSearcher("Select * From Win32_OperatingSystem");
            foreach (ManagementObject osInfoObj in osInfo.Get())
            {
                osSerial = (string)osInfoObj["Caption"];
                osVersionInfo = (string)osInfoObj["Version"];
                OSinfo = osSerial + " - " + osVersionInfo;
            }

            string x = Convert.ToString(DateTime.Now) + "\n" + "Kullanıcı:" + Properties.Settings.Default.Company + "\n" + "İletişim: " + Properties.Settings.Default.Email + "\n" +
                       "İşletim Sistemi: " + OSinfo + "\n" + "İşlemci: " + processorInfo + "\n\n\n" + "Bug: \n" + richTextBox1.Text;
            Gonder("Foro Müşteri Otomasyon ReportError", x);
        }
        public bool Gonder(string konu, string icerik)
        {
            MailMessage ePosta = new MailMessage();
            ePosta.From = new MailAddress("cammiccam@yandex.com");
            ePosta.To.Add("ibrahimaglar@hotmail.com");
            ePosta.Subject = konu;
            ePosta.Body = icerik;
            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new System.Net.NetworkCredential("cammiccam@yandex.com", "123456Aa.");
            smtp.Port = 587;
            smtp.Host = "smtp.yandex.com";
            smtp.EnableSsl = true;
            object userState = ePosta;
            bool kontrol = true;
            try
            {
                smtp.Send(ePosta);
                smtp.Dispose();
                Result result = new Result();
                result.label2.Text = "İŞLEM BAŞARILI...";
                result.Show();
            }
            catch (SmtpException ex)
            {
                kontrol = false;
                MessageBox.Show("HATA KODU: xx01 \n" + ex.Message);
            }
            MessageBox.Show("En Kısa Sürede Sizinle İletişime Geçilecektir.", "FotoMusteriOtom", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Cursor = Cursors.Default;
            this.Close();
            return kontrol;
        }
    }
}
