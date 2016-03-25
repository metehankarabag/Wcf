using System;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Windows.Forms;
using RemotingService.IHelloRemotingService;

namespace RemotingService.HelloRemotingServiceClient
{
    /*2. Ders Windows Application için .Net Remoting Service oluşturma
      Asmx Web Service, Client'ın Service'ı tanıması için Wsdl dökümanı sunar. .Net remoting Service böyle bir arayüz kullanmaz. Bu yüzden .net Remoting Service, biri Service Class'ını, diğeri Service Interface'ını barındıran 2 Class Library uygulamasından oluşur. Interface'i barındıran uygulamanın dll'ini Client'a verilir ve Service tanımlanmış olunur. Fakat Interface tek başına kullanılamaz Web Service'de de olduğu gibi Proxy Class'lar oluşturmamız gerekir. Client uygulamasının, Well-Known object olarak internete yüklenmiş Service Class'ın bir örneğini kullanarak Proxy Class'ları oluşturabilmesi için Activator Sealed Class'ının object dönen GetObject() methodu kullanması gerekir. Bu yüzden Client'a Service adresini vermeliyiz. Methodun 2 ve 3 parametreli 2 overload'ı var. Bu method internetteki adrese Well-Known type olarak yüklediğimiz Service Class'ını alır ve Proxy Class'ı oluşturur. Fakat object döner. Client bu adresden nesneyi alıp Interface örneğini kurar. Fakat bir Class methodunun başka bir ortamdan tetiklenebilmesi için Remotable olması gerekir. Bu yüzden Service Class'ının ya MarshalByRefObject Class'ından türemesi yada Serializable Attribute'unun uygulanması gerekir.
      Client'ın Service Class'ını kullanabilmesi için Class'ın çalıştırılması ve internete yüklenmesi gerekir. Bu işi yapan bir Host uygulamasına ihtiyacımız var. Çünkü Class Libray'ı projesi çalıştıramayız. Service'in nerede tutulduğu, kullanılacak bağlantı ve mesaj protokolü gibi Service'in çalışması ile ilgili işler Host uygulamasında yapılır. Yani Serice'in tüm çalıştırılma ayarları host'da belirlenir. Host'un Service Class'ını internete yükleyebilmesi için Service uygulamasının dll'ini alması gerekir. Host uygulaması çalıştırıldığında Service Class'ına internet üzerinden erişilebilir. Host uygulamasında 2 şey yaptık.
      1 TCP Protocol'ü kullanan bir Url oluşturmak için TcpChannel Class'ını kullanmamız gerekiyor. Class Constuctor'una parametre olarak Port numarası veriyoruz.
      2 Service Class'ının bir örneğini internette barındırmak için RemotingConfiguration Static Class'ının Void dönen RegisterWellKnownServiceType() methodunu kullanıyoruz. Methodun 2 overload'ı var. 1. overload 3 parametrelidir ve 1. parametre olarak Service Class'ının Type'ını, 2. Service nesnesine tutulacağı url'nin son kısmını alır, 3. parametre nesnenin modunu belirler. Method bu parametreleri kullanarak WellKnownClientTypeEntry oluşturur ve belirlenen adrese Well-Known type olarak Service Class'ını kaydeder. 2. overload WellKnownClientTypeEntry nesnesi alır. (Wdsl'daki işlere benzer işleri host'da yaptık)

        Method parametreleri
        1. parametre -> Well-Known nesnenin Type'ını istiyor. Bu bizim Service Class'ı ve yüklü değil. Fakat Interfacei kullanabiliriz. --> Interface object = new Well-KnownType();
        2. parametrede nesnenin alınacağı Url. Nesne internette yüklü olduğu için path olarak Url vermemiz gerekiyor.
        3. parametre nesnenin durumu -> 2. overload'da kullanılıyor.
        Not: Method object döndüğü için Interface türüne Cast etmemiz gerekir.
        Not: .net Remoting Service kullanarak Web Service kullanabiliriz. Fakat Client .net platform'unda oluşturulmamışsa, işler karışır. Çünkü Service'de .net'e ait hiç bir type'ı kullanmamamız gerekir.
      */
    public partial class Form1 : Form
    {
        IHelloRemtingService client;
        public Form1()
        {
            InitializeComponent();
            TcpChannel channel = new TcpChannel();
            ChannelServices.RegisterChannel(channel);
            client = (IHelloRemtingService)Activator.GetObject(typeof(IHelloRemtingService), "tcp://localhost:8080/GetMessage");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = client.GetMessage(textBox1.Text);
        }
    }
}