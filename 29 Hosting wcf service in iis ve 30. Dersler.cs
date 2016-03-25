using System;
using System.Windows.Forms;

namespace HelloClient
{
    /*29. Ders Hosting wcf service in IIS
     Solution'a yeni Web Site projesi ekleyeceğiz. Add> New Web Site > WCF Service'e. Eklenen Proje Host olacak. Proje içindeki App_Code klasörü içinde Service dosyaları barındırır. Bizim servizimiz hazır olduğu için bunları kullamayacağız yani dosyaları silebiliriz. Sonra HelloService projesini referanslara ekliyoruz.
     .svc uzantılı dosyayı açıp CodeBehaind Attribute'unu siliyoruz ve Service Attribute'una Service Class'ının tam adını veriyoruz. Yine Config'deki service ayarları aynen geçerli. Son olarak oluşturduğumuz projesi IIS'de ekliyoruz. Wsdl sayfasını görüntülemek için IIS'de Projeye sağ tık > swich to Content view > .svc uzatılı dosyaya sağ tıklayarak tarayıcıda açıyoruz. 
     .svc dosyasında ServiceHost yönergesinden bulunur ve bu yönerge relative adresini belirlememiz için kullanılır. Bu dosyada Service kodlarını da yazabiliriz. Derste ayrı bir assebmly'de kullanıyoruz. Bu yüzden yönergenin Service Attribute'unu kullanarak assembly'i referans olarak dosyaya ekledik. 
     Not: .svc içindeki ServiceHost yönergesi gerektiğinde ServiceHost'un bir örneğini oluşturmakla sorumludur. Bu yüzden kod yazma gerekliliğimiz ortadan kalkar.
     */
    /* 30. Ders 
     IIS Host avantajları
     1. Service'i tutmak için kod yazmak zorunda değiliz. .svc dosyası içindeki <ServiceHost /> yönergesi gerektiğinde Service hostun bir örneğini oluştur.
     2. IIS Host Automatic message base activation'ı destekler. Yani Host istek üzerinde Service'ı otomatik olarak aktifleştirir.
     3. Automatic process recycling: İşlem sağlıklı değilse ve istediği hazırlamak için çok zaman alıyorsa, IIS otomatik oalrak yenilenir.
     Dezavantajları
     IIS 5.1 veya 6 kullanıyorsak, sadece HTTP iletişimini kullanırız. Yani HTTP ile ilgili binding'leri.
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
