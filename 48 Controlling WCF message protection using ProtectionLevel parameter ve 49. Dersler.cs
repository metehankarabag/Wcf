using System;
using System.Windows.Forms;

namespace HelloClient
{
    /*48 Controlling WCF message protection using ProtectionLevel parameter
      ProtectionLevel bazı Attribute'ların Named parametresidir ve Soap mesajların üyelerine farklı güvenlik ayarları uygulamak için kullanılır. Property değer olarak  ProtectionLevel Enumun'dan None,Sign ve EncryptAndSign bekler. Bu Attribute'u kullanmadığımızda tüm mesaja kullanılan Binding'in ayarları uygulanır.
      Property'i barındıran Attribute'lar ve mesaj üzerindeki etkileri
      ServiceContractAttribute   1. seviye --> Interface'e uygulandığı için tüm methodlara uygulanır. Yani methodlar serileştiriliken tüm parametreler uygulanır.
      OperationContractAttribute 2. seviye --> Sadece bir methodun serileştirilen tüm parametreleri uygulanır.
      FaultContractAttribute     3. seviye --> Hata olduğuna kullanılacak methodun serileştirilen tüm parametrelerine belirler.
      MessageContractAttribute             --> Direk mesajı oluşturduğumuz için mesajın tüm parametrelerine uygulanır. Mesajı kullanan her method bu ayarları kullanmış olur.
      MessageHeaderAttribute     4. seviye --> Oluşturulan mesajın Header'da kullanılan parametresine uyguların.
      MessageBodyMemberAttribute 
      Not: 2. ile 3. seviye arasındaki var OperationContract'in Request ve Response Soap mesajına etki etmesinden kaynaklanıyor sanırım.(Bu dersi verenin sıralaması)
      Not: WsHttpBinding, varsayılan olarak mesaja -> Encryption ve Signing uygular. Encryption Confidentiality ve Signing, Integrity sağlar.
      Not: PropectinnLevel Enum'u .Net.Security isim uzayındadır.
    */

    /*49. Ders Bindings and the impact on message protection
      Binding Securty'i desteklemiyorsa, ProtectionLevel Property'sini bir Attribute'a uygularsak, InvalideOperationException hatası alırız.  The request message must be protected. This is required by an operation of the contract. The protection must be provided by the binding.
      Varsayılan olarak basicHttpBinding'de Security kapalıdır. 
    */
    public partial class Form1 : Form
    {
        HelloService.HelloServiceClient client;
        public Form1()
        {
            InitializeComponent();
            client = new HelloService.HelloServiceClient();
        }

        private void btnGetMessage_Click(object sender, EventArgs e)
        {
            MessageBox.Show(client.GetMessageWithoutAnyProtection());
        }

        private void btnGetSignedMessage_Click(object sender, EventArgs e)
        {
            MessageBox.Show(client.GetSignedMessage());
        }

        private void btnGetSignedEncryptedMessage_Click(object sender, EventArgs e)
        {
            MessageBox.Show(client.GetSignedAndEncryptedMessage());
        }
    }

}
