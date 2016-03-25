using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace SimpleFormClient
{
    /*43 ConcurrencyMode (Aynı Anda olan modu)
      ServiceBehaviors Attribute'unun ConcurrencyMode Property'si, bir Service örneğinin aynı anda kaç istek işleyebileceğini belirler. Bu Property, ConcurrencyMode Enumundan Single, Multiple, Reentrant değerlerini alabilir. Varsayılanı Single'dır. Yani bir Service örneğin aynı anda sadece bir isteği işleyebilir. İsteğin işlenmesi demek uygulama kodunun çalıştırılması demek. Uygulama kodları Thread'ler ile çalıştırılır. Yani her istek bir Thread aracılığı ile işlenir. Bir Service örneği aynı anda sadece bir istek işleyebiliyorsa, uygulama kodunu aynı anda sadece bir Thread çalıştırabilir. İstek işlenirken, Thread Service örneği ile birlikte kilitlenir ve aynı Service örneğini kullanmak zorunda olan diğer istekler sıralanır. Yani diğer isteği yapan Client'lar beklemek zorunda kalır. Service örneği aynı anda iki isteği işleyebilirse, ServiceThroughPut'u olumlu etkilenir. Fakat Service TroughPut'unu doğru değerlendirebilmemiz için isteği yapan Client'ı ve Service'in Service örneği oluşturma mantığını da hesaba katmalıyız. Yani Service ThroughPut'u ConcurrencyMode'u, InstanceContextMode'u ve kullanılan Binding'in Session'ı destekleyip desteklememesiyle ilişkilidir. 
      ConcurrencyMode.Single'iken, (tablo aşağıda)
      -InstanceContextMode'da Single'sa, tek Service örneği var demektir. Yani tüm Client'lar aynı örneğe istek yapacağı için tüm istekleri sıraya sokulur. Bu durum en kötü Service ThruoghPut'una neden olur. Client'ların bir birlerini beklemek zorunda kalır. InstanceContextMode.PerSession'sa, her Service için bir örnek var demektir. Yani farklı Client'lardan aynı anda gelen isteklerin aynı anda işlenebilir. Fakat bir Client'dan aynı anda gelen istekler sıraya sokulur. Yani ThroughPut'u Client'lar arasında olumlu etki olur. Fakat Client'lar kendi içinde ne olumlu nede olumsuz etkilenir. Çünkü Client kendi işini bekler. InstanceContextMode.PerCall'sa, her istek için bir örnek oluşturulduğu için aynı anda yapılan tüm istekler aynı anda işlenebilir. Tüm Client'lar olumlu etkilenir. 
      Not: InstanceContextMode.PerSession'iken, kullanılan Binding Session'ı desteklemiyorsa, PerSession, PerCall gibi çalıştığı için Service tüm istekleri aynı anda işleyebilir. 
      Not: InstanceContextMode.PerCall'iken, kullanılan Binding Session'u destekliyorsa, PerSession'ın gibi bir Client'dan aynı anda yapılan istekleri, aynı anda işleyemez. (Bunun nedenini anlamadım. Session kullanılsa bile her istek farklı bir örnek oluşturmaya devam ediyor. Yani PerCall, PerSession gibi çalışmıyor.)
     
      ThroughPut birim zamanda yapılan işe denir. Service aynı anda ne kadar fazla isteği işleyebilirse, birim zamandaki çıktısı o kadar fazla olur. Fakat yukarıda da göründüğü gibi bu etkinin Client'larda aynı karşılığı aynı olmayabilir. Aynı ThroughPut olumlu etkilensede, Thread'ler uygulama kodundaki Shared Resource(ortak field)'a aynı anda bağlanmak isterse sorun olabilir. Service örneği de bir Shared Resource'dur. Aynı örneği birden fazla Thread çalıştırırsa, sorun çıkabilir.
      Not: Thread.CurrentThread.ManagedThreadId Service'i çalıştıran Thread'in adını verir.
    */
    /*44. Concurrency Mode - Multiple 
    InstanceContextMode'un ve Bingding, Session'u destekleyip desteklememesinin, hiç önemli yok. Her şartda Service birden fazla Thread'ı aynı ayna çalıştırabilir. Bu durumda Shared Resources sorunları ile karşılaşabiliriz. C# Video Tutorial'da nasıl çözeceğimizi gördük.
    */
    public partial class Form1 : Form
    {
        SimpleService.SimpleServiceClient client;

        public Form1()
        {
            InitializeComponent();
            client = new SimpleService.SimpleServiceClient();
        }

        private void btnGetEvenNumbers_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void btnGetOddNumbers_Click(object sender, EventArgs e)
        {
            backgroundWorker2.RunWorkerAsync();
        }

        private void btnClearResults_Click(object sender, EventArgs e)
        {
            listBoxEvenNumbers.DataSource = null;
            listBoxOddNumbers.DataSource = null;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = client.GetEvenNumbers();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            listBoxEvenNumbers.DataSource = (int[])e.Result;
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = client.GetOddNumbers();
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            listBoxOddNumbers.DataSource = e.Result;
        }
    }
}
/*   Instance        | Concorency | BINDING SUPPORTS  | Concurrent calls PROCESSED             | Throughput Impact
     Context Mode    |     Mode   | SESSION           |                                        | 
     ------------------------------------------------------------------------------------------------------------------------------------
     1. PERCALL      | Single     | NO                | YES                                    | POSITIVE
     ------------------------------------------------------------------------------------------------------------------------------------
     3. PERSESSION   | Single     | NO                | YES                                    | POSITIVE --> Binding Session'ı desteklemiyorsa Service Percall olarak çalışır.
     ------------------------------------------------------------------------------------------------------------------------------------
     5. Single       | Single     | NO                | NO                                     | NEGATIVE -Tüm CLIENT' istekleri için
     ------------------------------------------------------------------------------------------------------------------------------------
  
     2. PERCALL      | Single     | YES               | NO                                     | NEGATIVE            
     ------------------------------------------------------------------------------------------------------------------------------------
     4. PERSESSION   | Single     | YES               | YES - Farklı CLIENT'ler arasında.      | POSITIVE - CLIENT'lar arasında
                     |            |                   | NO - Aynı CLIENT ile gelen isteklerde  | NEGATIVE - Aynı CLIENT'ın isteklerinde
     ------------------------------------------------------------------------------------------------------------------------------------
     6. Single       | Single     | YES               | NO                                     | NEGATIVE -Tüm CLIENT' istekleri için           
*/