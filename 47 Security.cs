using System;
using System.Windows.Forms;

namespace HelloClient
{
    /*47 Security
      Service güvenliği tamamen Service ile Client'ın iletişimini sağlayan Soap mesajların güvenliğini sağlamak için kullanılır. Bu güvenliği sağlamak için 4 farklı başlığı inceleyebiliriz. 1. Authentication, 2. Authorization, 3. Confidentiality, 4. Integtrity. İlk ikiyi biliyoruz. 
      3. Confidentiality: Mesaj Service ile Client arasında taşınırken, başka birinin mesajı okumasını engellemek için kullanılır. Mesajı şifreleyerek Confidentiality'i sağlarız.
      4. Integtrity: Mesaj Service ile Client arasında taşınırken, birinin mesaj içeriğini değiştirmesini engellemek için kullanılır. Mesajı imzalayarak Integtrity'i sağlarız.
      
      Mesajın taşımasını yapan kanalın kullandığı Binding türü kullanılan güvenlik ayarlarını belirler. Her Binding'in varsayılan güvenlik şeması farklı olabilir. Örneğin NetTcpBinding'in varsayılan güvenlik şeması Transport'iken, WsHttpBinding'in message'dır. Yani mesaj güvenliğini 2 katmanda korunur. 1. Mesajın baştan sonra kadar korunma. 2. mesajı sadece mesajı taşıyan kanalda koruma
      Transport Securty: Point to Point güvenlik sağlar. Yani mesaj sadece taşınma anında korunur. Mesaj bir yere iletildikten sonra korunmaz. Yani Transport Security, güvenliği Transport Layer'da sağlar. PROXY, Load Balancer gibi bir aracıya mesaj içeriğine gönderilirse mesaj burda korunmaz.
      Message Securty: Tüm Soap mesajı güvenlik kimliği ile şifreleyerek mesajın kendisini kormaya denir. Mesajın kendisi korunduğu için And to And güvenlik sağlar. 
      
      Her protokolün kendine özgü kanal günvenliği sağlama yöntemi vardır. Örneğin TcpBinding, kanal güvenliğini sağlamak için TLS(Transport Layer Security) kullanır. Bu uygulama işletim sistemi tarafından sağlanır. HttpBinding, HTTP üzerinde SSL(Secure Sockets Layer) ekleyerek aynı işi yapar. 
      
      PROTOCOL'lerin varsayılan güvenlik ayarları
      http://msdn.microsoft.com/en-us/library/ms731092(v=vs.110).aspx
      Message Security ve Transport Security arasındaki farklar
      http://msdn.microsoft.com/en-us/library/ms733137.aspx
     */
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGetMessage_Click(object sender, EventArgs e)
        {
            HelloService.HelloServiceClient client = new HelloService.HelloServiceClient();
            MessageBox.Show(client.GetMessage(textBox1.Text));
        }
    }

}
