using System.Threading;
using System.Windows.Forms;

namespace SimpleClient
{
    /*46 Throttling
      Throttling, Service'in aynı anda(Concurrent) en fazla kaç tane (Max) Instance, Call ve Session kullanabileceğini belirlemek için kullanılır. Bu ayarlar Service ThroughtPut'u etkiler. InstanceContextMode Property'si, Service örneğinin kullanım şeklini belirlediğinden ve ConcurrencyMode Property'si, bir Service örneğinin aynı anda kaç tane istek(Thread) işleyebileceğini belirlediğinden bu ayarlarda ThroughtPut'u etkiler. Yani Throttling, sadece üsttekilerin Max değerlerini belirler ve çıktıyı sınırlamak/düzenlemek için kullanılır. Throttling ayarları Host Config'de <behavior> elementi içinde kullanılan <serviceThrottling> elementinin Attribute'ları ile belirlenir. Attribute'lar -> MaxConcurrentInstances, MaxConcurrentCalls, MaxConcurrentSessions'dır.  
      Örneğin ConcurrencyMode.Multiple ve InstanceContextMode.PerCall'iken, her örnek aynı anda birden fazla Thread(istek) işleyebilir fakat her istek(Thread) oluşturulacak yeni Service örneği üzerinde çalışacağı için her örnek sadece bir Thread(istek) ile çalışır. Yani Throttling'de MaxConcurrentInstances'a 2 değerini verirsek, Service aynı anda sadece 2 örneği oluşturulabileceğiden ve her örnekte sadece 1 Thread çalıştığından, Service aynı anda sadece 2 Thread'i(isteği) çalıştırabilir. Bu yüzden Throttling'in MaxConcurrentCalls Attribute'una veridğimiz değerin bir 2 ve 2'den büyük değerin bir etkisi olamaz. Fakat InstanceContextMode.Single yapsaydık, her istek için yeni bir örnek oluşturmayacağı için ve tüm Thread'ler işlenebilirdi.
     
      Not: MaxConcurrentSessions ayarı Service'de kullanılabilecek Session Type sayısını belirler. App,Transport,Reliable,Secure bazı türleri.
      Not: Varsayıla değerler
            Before WCF 4.0            | WCF 4.0 and later
            MaxConcurrentCalls: 16    | MaxConcurrentCalls: 16 * işlemçi sayısı
            MaxConcurrentSessions: 10 | MaxConcurrentSessions: (100) * işlemçi sayısı
          
                 MaxConcurrentInstances: MaxConcurrentCalls + MaxConcurrentSessions      
     */
    public partial class Form1 : Form
    {
        SimpleService.SimpleServiceClient Client;
        public Form1()
        {
            InitializeComponent();
            Client = new SimpleService.SimpleServiceClient();
            for (int i = 1; i <= 100; i++)
            {
                Thread thread = new Thread(Client.DoWork);
                thread.Start();
            }   
        }
    }
}
