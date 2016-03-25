using System;
using System.Windows.Forms;

namespace HelloClient
{
    /*21. Ders Bindings
     Binding Client ile Servis arasındaki bağlantı kurallarını belirler. Binding Endpoint'ler ile birlikte belirlenir ve her Endpoint'e farklı bir binding türü uygulayabiliriz. Kullandığımız Binding türünün ayarlarını, Host Config'de <Bindings> elementi içinde uygun Binding elementini oluşturup bu element içinde belirliyoruz.
     Binding 3 şeyi belirler.
     1. Transport Protocol: Http, TCP, NamedPipe, MSMQ
     2. Message Encoding: text/XML, bindary..
     3. Protocols: Reliable message, Transaction support
     */
	 
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HelloService.HelloServiceClient client = new HelloService.HelloServiceClient();
            label2.Text = client.GetMessage(textBox1.Text);
        }
    }
}
