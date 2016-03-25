using System;
using System.Windows.Forms;

namespace SimpleClient
{
    /*52 Configure wsHttpBinding to use transport security
      Oluşturduğumuz Service'i kullanıma açmak için IIS Host kullanıyoruz.(Bunun nedenini anlmadım.) Service'e WsHttpBinding kullanan bir Endpoint var. WsBinding'in ayarları Securtiy mode = Message -> clientCredentialType = Windows'dur. Transport ve Basic'e çevireceğiz. Yani Transport Layer'da güvenliği sağlamayı istiyoruz. wsBingding Http Protokolü kullanıyor. Bunu https'e çeviriyoruz. Neden bu işi yapma gereği duyduğumuzu anlamadım (sanırım Transport security bunu gerektiriyor.)fakat Https kullanabilmemiz için bir ssl Certificate oluşturmamız gerekiyor.(Asp.net 101. Derste gördük.). Sanırım bu yüzden IIS Host kullanıyoruz. IIS'de SSL Certificate oluşturmak ve IIS'de uygulamanın Https kullanmasını sağlamak için 3 şey yapmamız gerekiyor.
       1. Certificate'i oluşturmak için --> IIS'de aç > Pc adını seç > sağ tık > Switch to Features View'a tıkla > Açılan pencerede > IIS > Service Certificate'i seç > Actions bölümündeki butonları kullanarak SSL Certificate'i oluşur.
       2. Default Web Sites'i seç > Action bölümü > Bindings > açılan pencereyi kullanarak oluşturduğumuz Certificate ile IIS'e Https ekle.
       3. Atık IIS'deki uygulamalar Https kullanabilir. Bir uygulamayı seç > Features penceresinden > IIS > SSL Settings > gerekli ayarları isteğe göre yap
     
     Artık Host uygulaması Service'ı Https kullanan bir adress ile sunacak. Host Config'de Security Mode'u Transport seçiyoruz. Şimdi farkettim ki bu sadece mesajı şifrelemek için kullanılıyor. Message ile arasındaki fark şifreleme kapsamı. ClientCretendialType Attribute'unada Basic veriyoruz. Uygulamayı çalıştırdığımızda hata alırız. Çünkü IIS'deki uygulamada Basic Authentication kapalı. Security settings for this Service required 'Basic' Authentication but it is not enabled for the IIS application that hosts this service.
      Tekrar çalıştırdığımızda farklı bir hata alırız. The HttpGetEnabled property of ServiceMetaDataBehavior is set to true and the HttpGetUrl property is a relative address, but there is no http based address. Either supply an http base address or set HttpGetUrl to an absolute address. Bunun nedeni Host Config'de HttpGetEnable Attribute'u yerine HttpGetEnable Attribute'una true değeri vermemizdir.
      Not: Client'da Proxy Class'ı silip yeniden kurmayı denediğimizde, Wsdl dökümanının localhost yazan yerine SSL Certıfıcate adını yazmassak hata alıyoruz      
      
      CLIENT'i çalıştırdığımızda hata alırız. User name is not provided. Specify username in ClientCredentials.
      Basic Authentication kullandığımız için WCF SERVICE'e ClientCredentials sağlamamız gerekir. Form1.cs'de yapıyoruz.
     */
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCallService_Click(object sender, EventArgs e)
        {
            SimpleService.SimpleServiceClient client = new SimpleService.SimpleServiceClient();
            client.ClientCredentials.UserName.UserName = "Metehan";
            client.ClientCredentials.UserName.Password = "1973842654";
            MessageBox.Show(client.GetMessage("Metehan"));
        }
    }
}
