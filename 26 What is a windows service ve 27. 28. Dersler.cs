using System;
using System.Windows.Forms;

namespace HelloClient
{
    /*26. Ders What is a windows service
     Windows Servis, windows sisteminde çalışan diğer programlar gibidir. Farkları arkaplanda çalışır, System çalışmaya başladığında otomatik başlayabilir, kullanıcı arayüzü olmaz.
     Windows servislerin kullanım alanları: Event logging(Event Log Service), Providing Security(Windows Firewall Service), Error Reporting(Windows Error Reporting Service)
     Windows Service'ı bilgisayar açıkken servisin her zaman kalmasını istiyorsak kullanırız. 
      
     27. Ders Hosting a wcf service in a windows service
      Wcf Service'ı Windows Service'de tutabilmemiz için Add new Project penceresini kullanarak Windows Service'i projesi oluturmalıyız. Uygulama içinde eklenen dosyaya tıkladığımızda Design penresi açılır. F7 ile kod kısmına geçebiliriz. Host'u başlatmak için yazdığımız kodlarda bir fark yok. Fakat Design'da sağ tıklayıp Add Installer'i şeçiyoruz. Bu bir cs dosyası daha ekler. Bu Projenin Design penceresini açtığımızda serviceProcessInstaller ve serviceInstaller görünür. Bunlar oluşturacağız Windows service'ın çalışma ayarlarını belirlemek için kullanılır. (gerekli olduğunda bakarım 5:00 dk) Service'ı Pc'ye yüklemek için Windows Service projesinin exe'i tam yolu ile kopyalayıp Command promt'u açıyoruz > cd/ -enter > installutil -i "yolumuzde boşluk olduğu için tırnaklar içinde yapıştırıyoruz." -enter. Servisimiz yüklendi. silmek için -u kullanıyoruz.
     Not: instalutil /? tüm kullanabileceğimiz komutları gösterir.
     */
    /*Part 28 
     Windows service avantajları
     1. Bir kullnıcının bilgisayarada oturum açma gereliği olmadan, sistem çalıştığından windows service'nin otomatik çalışması sağlanabilir. Bunun anlamı windows servisin barındırdığı WCF Service otomatik olarak çalışır.
     2. Bir hata olduğunda, windows service otomatik yeniden çalıştırabilir ve kurtararabiliriz.
        * Bunu ayarlamak için servise sağ tıkla > Property > Recovery'i seç > hatalar sonunda yapılması gereken işlemler var.
     3. Tüm binding'leri ve transport'ları destekler.
     
     Dezavantajları
     1. Windows Servis oluşturmak için kod yazmayı içerir.
     2. WCF servisi barındıran Windows Servis üretim sunucusunda geliştirilmelidir.
     3. Windows Servisin içinde çalıştığı işlemleri bğlamamız gerektiği için, WCF Service ile Debug modda çalışmak zordur. 
        * İlişkilendirme işlemini Debug - Attach to Process'den servisi.exe yi seçep attact a tıklayayara yapıyoruz.
       
     Debug modda break point kullandığımızda f11 yazdığımız methodun içine girer.(metodun yazıldığı yere gideriz.)
     
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
