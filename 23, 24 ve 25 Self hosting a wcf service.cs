using System;
using System.Windows.Forms;

namespace HelloClient
{
    /*23. Ders Diffent types of Hosting
     Self-hosting, Windows Service, Internet Information Services(IIS), Windows Activation Services(WAS)
     */

    /* 24. Ders Self hosting a wcf service in console application  -- 25. Ders WindowsForm ile
     Herhangi bir .net uyuglaması ile bir WCF Service'i hostalamaya Self hosting denir.  WCF Service Configuration Editor kullanarak Service'ı belirlemiş.
     Self hosting avantajları
     1. Oluşturması çok kolay sadece bir kaç satır kod kullanıyoruz.
     2. WCF Service'i barındıran dağınık işlemleri bağlamak zorunda olmadığımız için Debug modda çalışmak çok kolay. Yani servisi ve client'i debug modda çalıştırıp. Servise break point kolaysa işlemleri servisde takipedebiliriz.
     3. Tüm Binding ve transport protokolleri destekler.
     4. Hostu açmak ve kapamak çok esnek.
     
     Dezavantajları
     1. Servis sadece host çalışır durumdaysa çalışır.
     2. automatic message based activation'u desteklemez. IIS Hosting destekler.
     3. Ayarlanmış kodlar yazamamız gereklir.
     
      Self-hosting sadece geliştirme ve ispat için kullanılır.
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
