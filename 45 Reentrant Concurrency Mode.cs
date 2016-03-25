using System;
using System.ServiceModel;
using System.Windows.Forms;
using System.Diagnostics;

namespace ReportClient
{
    /*45. Ders Concurrency Mode Reentrant
      Daha önce Duplex Message Pattern'de, Service methodları Request-Reply düzeninde çalışırken, Deadlock oluşmasın diye kullanmıştık. Service örneği varsayılan olarak aynı anda bir Thread'ı çalıştırabilir. Bu yüzden Client, Service'e bir istek yaptığında Service örneği, Thread ile kilitlenir. Service, isteği işlerken Request-Reply kullanan bir Client methodunu tetiklerse, bu iş farklı bir Thred ile yapıldığı için ve methodun Response mesajı Thread'i Service örneğine geri döneceği için ve örnek başka bir Thread'le daha önce kilitlendiği için Deadlock oluşuyor. Bu durumu çözmek için ServiceBehavior Attribute'una ConcurrencyMode.Reentrant verebiliriz. Bu sadece Callback çağrılarını işlerken Thread'lerin sıraya girmesini sağlar. Yani Multiple gibi aynı anda birden fazla Thread çalıştırmaya izin vermez.(tahminim)
      Not: OneWay, Bu sorunu çözmek için daha iyi bir yoldur. Çünkü Service'in tetiklediği method OneWay kullanıyorsa, CallBack cevabı oluşturulmaz. Bu yüzden Service'e girmeye çalışan ikinci THREAD'de olmaz. Tabi bunu sadece Client'den Service'e cevap alma gerekliliği olmadığında kullanabiliriz.
     Not: Client'da CallbackBehavior Attribute'unun UseSynchronizationContext Property'sine False vermemiz Client'ın işlem bütünlüğü içinde çalışmasını engeller. Fakat Client'ın tetiklediği method Request-Reply kullanıyorsa, Client tetiklediği Service methodunun cevabını alana kadar kitlenir.
     */
    [CallbackBehavior(UseSynchronizationContext = false)]
    public partial class Form1 : Form, ReportService.IReportServiceCallback
    {
         
        public Form1()
        {
            InitializeComponent();
        }
        private void btnProcessReport_Click(object sender, EventArgs e)
        {
            InstanceContext instanceContext = new InstanceContext(this);
            ReportService.ReportServiceClient client = new ReportService.ReportServiceClient(instanceContext);
            client.ProcessReport();
        }

        public void Progress(int percentageComplete)
        {
            System.Threading.Thread.Sleep(100);
            textBox1.Text = percentageComplete.ToString() + " % completed";
        }
    }
}
