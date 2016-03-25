using System;
using System.Windows.Forms;

namespace SampleFormClient
{
    /*38. InstanceContextMode -> PerSession
      PerSession'ı kullandığımızda, her Client için bir örnek oluşturulur ve her Client kendi örneği üzerinde çalışır. Bu sadece Service Class'ına ait üyelerin değerleri istekler arasında kaybolmaz. Session'lar Client'ların Service'de tanımlanması için kullanır. Yani her Client sadece bir Session kullanılır. Service Class örneği, Client'ın kullandığı Session'ın süresi bitene kadar hafızada saklanır. Bu Service'in fazla hafıza kullanmasına neden olur. Bir Client'dan aynı anda iki istek gelirse ve istekler aynı anda örnek üzerinde çalıştırılırsa sorun olabilir.
     PerCall ve PerSession InstanceContextMode değerlerinin Service'lerin güçlü ve zayfı yanları vardır. Yani biri birinden iyi değildir. Uygulamanın ihtiyaçlarına uygu olanı seçmemiz gerekir. PerSession Object Orianted programlama için uygundur. PerCall SOA (Servis Orianted Arhcitedture) için uygundur. Genellikle, (Performance/ölçeklenebilirlik) hariç tüm durumlar eşittir. PerSession Service daha iyi performas verir. Çünkü Service Class örneği daha az oluşturulur. PerCall daha kolay ölçeklenebilirlik sağlar. Çünkü her örnek bir istekten sorumludur.
    */
    /*39. Ders PerSession Questions
      1. Tüm Binding türleri Session destekler mi? -> desteklemez. 
         Örneğin BasicHttpBinding. Service'ın kullandığı Binding Session'ı desteklemiyorsa, Service'i PerSession olarak ayarlasak bile PerCall gibi davranır. Çünkü Client tanımlanamadığı için her istek farklı bir Client'dan geliyormuş gibi algılanır ve her istek için bir örnek oluşturulmak zorunda kalınır. PerCall ile Session'ı birlikte kullanırsak, Client tanımlanmasına rağmen tüm istekler farklı örnekte işlenir. Bu yüzden bir değişiklik olmaz.
    2. Session süresini nasıl ayarlanır? 
        Config dosyasındaki <binding> elementinin ReceiveTimeout Attribute'unu kullanarak, Binding'i kullanan Session'un zaman aşımı süresini Saat:Dk:Sn şeklinde belirleyebiliriz. Session süresi dolduğunda silinir ve Client hata alır.(CommunicationExcetion). Bunun nedeni Wcf'e yapılan bağlantının kapanmasıdır. Bu yüzden Channel hata durumunu alır. Yani Client'ın tekrar Wcf Service'e bağlanabilmesi için yeni bir Proxy Class örneği oluşturması gerekir. Yani Session ile birlikte service verisi de silinir.
      
     */
    /*40.Ders How to Retrieve The Sessionid In WCF And In The Client
       OperationContext Sealed Class'ı Client ile Service arasında gerçekleşen olayların tüm bilgisine ulaşabileceğimiz Class'dır. Class'ın, çoğu mesajlarla ilgili olan 14 tane Read-Only 3 tane normal Property'si var. OperationContext türündeki Current Property'sini kullandıktan sonra OperationContext Class'ının Read-Only SessionId'ile Id'i almış fakat neden direk almamış? --> Burda bir Programlama mantığı var. Şimdi uğraşamayacam. Basit gibi görünüyor.
       Client'da Session ID'i almak için proxyClass.InnerChannel.SessionId
        
     Client'i çalıştırdığımızda, HOST'da ve Client'da 2 farklı SessionId değerler tutulduğunu görürüz. Bunun nedeni binding'in ReliableSessionId kullanmamasıdır. Bu Id'leri bir birleri ile ilişkili olasını sağlar. Net.Tcp binding'de varsayılan olarak Disable'dır. <reliableSessionId /> elementini binding içinde kullanıp enabled Attribute'una True verdiğimizde düzelir.
     Not: wsHttpBinding'de bu elementin hangi değeri aldığının bir önemi yok her zaman SessionId aynı olur.
     Not: Wcf'de farklı türlerde Session'lar vardır. Bunlar sonraki derslerde anlatılacak.
    */
    public partial class Form1 : Form
    {
        SampleService.SampleServiceClient host;
        public Form1()
        {
            InitializeComponent();
            host = new SampleService.SampleServiceClient();
        }

        private void button1_Click(object sender, EventArgs e) 
        {
            MessageBox.Show(host.incrementNumber().ToString() + "\nSession ID : " + host.InnerChannel.SessionId); 
        }
    }
}
