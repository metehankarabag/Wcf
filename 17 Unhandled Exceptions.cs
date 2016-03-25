using System;
using System.Windows.Forms;
using System.ServiceModel;

namespace CalculatorClient
{
    /*17. Ders Unhandled Exceptions
     WCF Service'de bir hata alırsak, Service'in kullandığı kanal Faulted durumu alır ve kanalı hataya düşüren Session tekrar kullanılamaz. Yani bu işlem bir kez gerçekleştiğinde, Service'i çalıştırmak için kullandığımız Proxy Class örneğini tekrar kullanamayız. Yeniden bağlantı kurmak için PROXY CLASS'un yeni bir örneğini kullanmak zorundayız. Bu durum basicHttpBinding için geçerli değil. Çünkü basicHttpBinding'in Session kullanmaz. Yani bir hata olduğunda, sadece servis kanalı sorumludur. Client'daki PROXY örneği hala çalışır. Çünkü bacisHttpBinding ile kanal SESSION'ı koruyamaz. Binding türünü wsBinding'e çevirdikten sonra bir hata aldığımızda (Hata Service'den kaynaklanıyorsa hatayı try ile yakalasak bile) bir daha Proxy Class'ın aynı örneğini kullanamayız. wsHttpBinding'in Security Session'ları olduğu için CommunicationObjectFaultedException hatası aldığımızda, kanal bozmuştur ve kullandığımız Session bunu sürekli hatırlar. Yeni bir prox örneği oluşturmak için Proxy Class örneğini oluşturan kodların programda tekrar çalışması gerekiyor. Bunu kodu Button Clik Event'ında çalıştırdık.
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
