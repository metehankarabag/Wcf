using System;
using System.Windows.Forms;

namespace HelloClient
{
    /*22.Ders Configure WCF service endpoint dynamically in code
      EnPoint host'da belirlendiği için koduda host'da oluşturulur. Host oluşturmak için ServiceHost Class'ını kullanıyoruz. Bu Class ServiceHostBase Class'ından miras alır. 3 Constructor'u ve ServiceEndpoint türünde 2 overload'ı olan AddServiceEndpoint() methodu var. Başka metodlarda var.  
      AddServiceEndpoint() methodunun overload'larında kullanılan parametreler. 
     * Type implementedContract: Service Interface'inin Type'ını veriyoruz. 
     * Binding binding: Kullanılacak Binding'i belirliyoruz.
     * Uri/string address: Service addresinin son kısmını belirliyoruz.
     * Uri listenUri: Gelen mesajların dinlendiği adress gibi bişey yazıyor. -> kullanmamış 4 parametre bekleyen overload'larda kullanılan parametre
      
      HostBase Abstract Class'ının, ServiceDescription türündeki Description Read-Only Property'sine, ServiceDescription Class'ının KeyedByTypeCollection<IServiceBehavior> türündeki  Behaviors Read-Only Property'si uygulanmış. Bu Class bir çok Class'dan miras alıyor Collection<T> Class'ının Add() methodunu Property'e uygulamış. Method parametre olarak Generic Type örneği bekliyor. ServiceMetadataBehavior örneği vermişiz. Bu Class'ın HttpGetEnabled Property'sine True vererek get isteğine izin veriyoruz.
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
