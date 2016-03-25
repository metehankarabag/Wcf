using System;
using System.Windows.Forms;

namespace HelloClient
{
    /*31. Ders WAS hosting 
      IISHost sadece Http protokolünü destekler. Tcp gibi Http türünde olmayanları kullanmayı istiyorsak,  Windows Communication Foundation Non-HTTP Activation ve WAS bileşenlerini indirmemiz gerekir. WAS'ı IIS 7 ve üzeri versiyonlar destekler. 
      Http olmayan protokoller kullanabilmemiz için 2 şey yapmamız gerekir.
      1. Programs and features > Trun windows features on or off > Açılan pencerede Windows Process Activation Service'i yükle. Microsoft .Net Framework 3.5.1 altındaki 2. seçenek yükle
      2. IIS'de uygulamamız için Non-HTTP protokole izin vermeliyiz. IIS'de Projeye sağ tıkla > manage App > Advanced Setting > Açılan pendereceki behaviors sekmesinde virgül ile ayırarak istediğimiz bağlantı türünü ekleyebiliriz.
     
      Tüm işlemleri yapıp PROXY'i güncelledikten sonra 2 tane hata alabiliriz.
      1. Could not load 'System.ServiceModel.Activation.HTTPModule' from assembly 'System.ServiceModel, version = 3.0.0.0, culture=netural,publickeyToken=""'
        Command promt'a > aspnet_regiis.exe -iru yaz > çalıştır.
      2. {System.ServiceModel.EndpointNotFoundException: The message could not be dispatched because the service at the point address 'net.tcp://Metehan-Pc/WCF31/HelloService.svc/HelloService' is unavailable for the protocol of the address} alırsan 
        Command promt'a > cd\ enter - C:\Windows\Microfost.NET\Framework32\v4.0.30319 > enter > ServiceModelReg.exe -r yaz > çalıştır.
     
      Avantajları
      1. kod yazmıyoruz.
      2. işlem geri dönüşüm, otomatik mesaj aktivasyonu, idle time yönetimi gibi özellikleri var.
      3. Tüm Binding'leri destekliyor.
     */
    public partial class Form1 : Form
    {
        HelloService.HelloServiceClient client;
        public Form1()
        {
            InitializeComponent();
            client = new HelloService.HelloServiceClient();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                if (client.State == System.ServiceModel.CommunicationState.Faulted)
                {
                    client = new HelloService.HelloServiceClient();
                }

                label2.Text = client.GetMessage(textBox1.Text);
            }
            catch (Exception ex)
            {

                label2.Text = ex.Message;
            }
        }
    }
}
