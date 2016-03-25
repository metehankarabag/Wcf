using System;
using System.Windows.Forms;

namespace SampleFormClient
{
    /*42. Ders SessionMode Enumeration
     ServiceContract ve OperationContract Attribute'unun Property'sidir. SessionMode Enumundan -> Allowed,NotAllowed, Required değerlerini alabilir. Bu Property'nin amacı methodun veya methodların Service ile iletişiminde kullanacakları binding'in Session kullanımını belirlemek.
      Allowed: Binding Session'ı destekliyorsa, methodlar bağlantıyı kullanabilir. Desteklemiyorsa hata oluşmaz.
      Required: Binding Session'ı destekliyorsa, method binding'i kullanabilir. Fakat Binding Session'ı desteklemiyorsa hata oluşur.
      NotAllowed: Method Session kullanan Binding'ler ile çalıştırılamaz.
      Not: Service InstanceContextMode ile SessionMode'un mümkün olan tüm birleşimi Msdn'de, birleşimlerin etkileri ile birlikte gösterilmiştir.
      http://msdn.microsoft.com/en-us/library/system.servicemodel.sessionmode(v=vs.110).aspx 
      InstanceContextMode ile SessionMode arasında ne bağlantı var çözemedim. --> Fazla bir durum yok. Aslolan Binding'in Session'ı destekleyip desteklemesi.
     */

    public partial class Form1 : Form
    {
        SampleService.SampleServiceClient host;
        public Form1() { InitializeComponent(); host = new SampleService.SampleServiceClient(); }

        private void button1_Click(object sender, EventArgs e) { MessageBox.Show(host.incrementNumber().ToString() + "\nSession ID : " + host.InnerChannel.SessionId); }
    }
}
