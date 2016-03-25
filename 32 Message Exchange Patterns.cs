using System;
using System.Windows.Forms;

namespace SampleClient
{
    /*32. Ders Message Exchange Patterns
     Mesaj exchange Pattern, Client ve WCF Service arasında iletişimi sağlayan Resuest ve Response mesajların bir birleri ile bağlantısını ayarlar. 3 Farklı türü var. Request-Reply(varsayılan), One-Way, Duplex
     Message Exchange Pattern -> Request-Reply 
     Client bir istek yapar ve Service'in cevabını alana kadar kitlenir. Yani Request ve Respose mesajı bütün olarak işletilir. Request-Reply kullanan method tetiklendiğinde, methodun dönüş türü void olsa bile Service işini tamamladığında, Client'a boş Bir Respone mesajı gönderir ve Client bu mesajı alana kadar kitlenir. Method hata verirse, hata sonucu Client'a gönerilir. Tüm WCF Binding'ler, MSMQ tabanlı Binding'lerin Request-Reply pattern'i desteklemesini bekler.
     */
    public partial class Form1 : Form
    {
        SampleService.SampleServiceClient client;
        public Form1()
        {
            InitializeComponent();
            client = new SampleService.SampleServiceClient();
        }

        private void btnRequestReplyOperation_Click
            (object sender, EventArgs e)
        {
            try
            {
                listBox1.Items.Add("Request-Reply Operation Started @ " + DateTime.Now.ToString());
                btnRequestReplyOperation.Enabled = false;
                listBox1.Items.Add(client.RequestReplyOperation());
                btnRequestReplyOperation.Enabled = true;
                listBox1.Items.Add("Request-Reply Operation Completed @ " + DateTime.Now.ToString());
                listBox1.Items.Add("");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRequestReplyOperation_ThrowsException_Click
            (object sender, EventArgs e)
        {
            try
            {
                listBox1.Items.Add("Request-Reply Throws Exception Operation Started @ "
                    + DateTime.Now.ToString());
                client.RequestReplyOperation_ThrowsException();
                listBox1.Items.Add("Request-Reply Throws Exception Operation Completed @ "
                    + DateTime.Now.ToString());
                listBox1.Items.Add("");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }
    }
}
