using System;
using System.ServiceModel;
using System.Windows.Forms;

namespace CalculatorClient
{
    /*20. Ders Centralized exception handling By Implementing IErrorHandler
     Asp.net'de, Global.asax dosyasının Application_Error() Event'ında, tüm hataları uygulama düzeyinde yakalayıp isteğimiz işlemi yapabilirz. Aynı iş WCF'de 3 adımda yapılır. İlk önce IErrorHandler Interface'ınden türeyen bir Class oluşturmamız gerekiyor. Interface'ın 2 methodu var.
     ProvideFault(): Bu method Service'de herhangi bir .net Excetion olduğunda, hata işlenmemişse(kanımca), bu method otomatik olarak tetiklenir. Method içinde FaultExcetion oluşturup methodu tetikleyen .net hatasını değiştiriyoruz. HandleError'den önce çalışır.  .Net hatalarını göstermemizin 2 nedeni var. Biri .Net hatasının sadece .net uygulamalarda çalışması. diğeri Client'ın kullandığı kanalı bozması.
     HandleError(): Bu gerçek hatayı veri tabanı veya başka bir yere kaydetmek için kullanılır. Böylece bir sorun çıktığında bundan haberimiz olacak.
     
     2. adım bir hata olduğunda  GlobalErrorHandler sınıfını kullanmayı isteidğimiz WCF'nin bilmesine izin vermek için  bir Servis davranış özniteliği oluştur.
     Oluşturacağımız sınıf Attribet, sınıfından ve IserviceBehavior arayüzünden türemek zorunda.
      
     ApplyDispatchBehavior methodu GlobalErrorHandlar sınıfı ile WCF'i ilişkilendirmek için kod yazacağımız methoddur.
      
     */
    public partial class Form1 : Form
    {
        CalculatorService.CalculatorServiceClient client = null;
        public Form1()
        {
            InitializeComponent();
            client = new CalculatorService.CalculatorServiceClient();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int numerator = Convert.ToInt32(textBox1.Text);
                int Denominator = Convert.ToInt32(textBox2.Text);
                label3.Text = client.Divide(numerator,Denominator).ToString();

            }

            catch (FaultException faultException)
            {

                label3.Text = faultException.Message;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            client = new CalculatorService.CalculatorServiceClient();
        }
    }
}
