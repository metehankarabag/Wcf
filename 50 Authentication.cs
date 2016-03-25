using System;
using System.Windows.Forms;

namespace SimpleClient
{
    /*50. Ders Authentication
      Authentication'ı bir çok Binding kullanır. Örneğin wsHttpBinding ve NetTcpBinding, varsayılan olarak Windows Authentication kullanır. Beinding'in kullanacağı güvelik ayarlarını belirlemek için host config'de <Security> elementinin Mode Attribute'unu kullanırız. 47. derste gördük. Bu Attribute'a verdiğimiz değer Soap mesajın koruma kapsamını belirler. Mode Attribute'una Non,Transport,Message, TransportWithMessageCredential değerlerini verebiliriz. 
      Binding'in kullanacağı Authentication ayarını belirlemek için <Security> elementi içinde kullanabileceğimiz <transport /> ve <message /> elementlerin clientCredentialType Attribute'u belirler. Derste <Security> elementinin mode Attribute'una göre kullanılacak elementi belirliyor ama ben ikisinide denedim çalıştı.
      <transport /> elementini kullanırken, ClientCredentialType Attribute'una verebileceğimiz değerler -> None, Windows, Certificate
      <message /> elementini kullanırken, ClientCredentialType Attribute'una verebileceğimiz değerler -> None, Windows, Certificate, UserName, IssuedToken
     
      Service Class'ında ServiceSecurityContext Class'ı tüm kullanıcı bilgilerini barındıran Class'dır. ServiceSecurityContext türünde Anonymous ve Current, WindowsIdentity türünde WindowsIdentity, IIdentity Interface'i türünde PrimaryIdentity, boolean dönen isAnonymous Property'leri var ve hepsi Read-Only'dir. (iki tane daha var yazmadım.)  IIdentity Interface'inin 3 Read-Only Property'si var. Bu Property'leri kullanarak kullancını bilgilerini alabiliyoruz. Demek ki bir yerde bu Interface örneği türettiği bir Type aracılığı ile oluşturulup doldurulmuş. Property'ler -> IsAuthenticated, AuthenticationType, Name
     */
    /* 51. Ders Message Confıdentiality And Integrity With Transport Security
      47. derste Message Security'inin mesaja Confidentiality, Integrity sağlamak için otomatik olarak Encription ve Singing uyguladığını gördüe. Bu durum Transport Security kullanan netTcpBinding için de geçerlidir. Fakat mesajları kaydettiğimiz log dosyasını açtığımızda, netTcpBinging kullanırken taşınan mesajların Text olarak sunulduğunu görürürüz. Çünkü netTcpBinding Transport Layer'da bu işi gerçekleştirir ve bir yere mesajı göndermeden önce mesajı çözer.
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
            MessageBox.Show(client.GetUserName());
        }
    }
}
