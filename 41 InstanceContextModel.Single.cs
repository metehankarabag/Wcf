using System;
using System.Windows.Forms;

namespace SampleFormClient
{
    
    /* 41. Ders InstanceContextModel -> Single 
     Service'in tek bir örneği oluşturulur ve Service kapatılana kadar Service'ı kullanan tüm Client'lar bu örneği kullanır. Bu yüzden bir Client'ın, Service Class'ının üyeleri üzerinde yaptığı işlemlerin etkisi diğer Client'larda da geçerlidir. Service Class'ının int Field'ı her istekte bir artıyor. Bir uygulamada değeri 10'a çıkarttığımızda, başka bir uygulamada tekrar aynı örneğe istekte bulunacağımız için değer 11 olur. Tüm Client'ların ortak veriler üzerinde çalışması gerektiğinde kullanılır. Client'lardan aynı anda gelen isteklerin aynı anda çalıştırılması soruna neden olur. Bu sorunu çözmek için Service'in aynı anda tek bir Thread'in(isteğin) çalışmasını sağlayabiliriz. Bu durumda Service örneğini aynı anda sadece bir isteğin işlenmesi çıktı sorununu çözer. Fakat Client'lar bir birlerini beklemek zorunda kalır.
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
