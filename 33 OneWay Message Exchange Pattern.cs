using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SampleClient
{
    /* Part 33
      OperationContract Attribute'unun isOneWay Property'sine true verdiğimiz methodlar, OneWay kullanır. Client işlemleri, Service işlemlerinden bağımsız çalışır. Yani Client, Service'e istek gönderir ve cevabını beklemeden çalışmaya devam eder. Bu yüzden Service'de bir hata olduğunda Client hatayı bir daha isteği yapana kadar göremez. Yani ilk işlemi hata olduğu için gerçekleşmez ama Client'a cevab gönderilmediği için Client bunu anlayalmaz. OperationContract uyguladığımız method OneWay kullanacaksa, dönüş türü void olmak zorunda ve herhangi bir output parametresi veya referans parametresi kullanmaz. Çünkü One may operation Client'a bir şey dönmez.
     OneWay çağrıları senkronize olamayan çağrılarla aynı değildir. Bir OneWay çağrısı Service'de işletilirken, Client'dan başka bir istekte bulunursa, çağrı sıraya sokulur. Yani Service tarafı düzenli çalışır ve Client serbetst kalır. Yinede işletilmek için bekleyen istek sayısı Service sıralama limitini aştarsa, OneWay çağrısı Client'ı engelleyebilir. 
     */
    public partial class Form1 : Form
    {
        SampleService.SampleServiceClient client;
        public Form1()
        {
            InitializeComponent();
            client = new SampleService.SampleServiceClient();
        }

        private void btnRequestReplyOperation_Click(object sender, EventArgs e)
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

        private void btnOneWayOperation_Click(object sender, EventArgs e)
        {
            try
            {
                listBox1.Items.Add("OneWay Operation Started @ " + DateTime.Now.ToString());
                btnOneWayOperation.Enabled = false;
                client.OneWayOperation();
                btnOneWayOperation.Enabled = true;
                listBox1.Items.Add("OneWay Operation Completed @ " + DateTime.Now.ToString());
                listBox1.Items.Add("");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnOneWayOperation_ThrowException_Click(object sender, EventArgs e)
        {
            try
            {
                listBox1.Items.Add("Request-Reply Throws Exception Operation Started @ " + DateTime.Now.ToString());
                client.OneWayOperation_ThrowsException();
                listBox1.Items.Add("Request-Reply Throws Exception Operation Completed @ " + DateTime.Now.ToString());
                listBox1.Items.Add("");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
