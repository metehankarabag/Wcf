using System;
using System.ServiceModel;
using System.Windows.Forms;

namespace CalculatorClient
{
    /*18. Ders Throwing Fault Exceptions From
      WCF service bir hata olduğunda ya FaultException/FaultException<T> atmalıdır. Bunun 2 nedeni var.
      1. İşlenmemiş .net Exception gerçekleştiğinde kanal Faulted durumu alır. Bu yüzden Client'ın yeni bir Proxy örneği oluşturması gerekir. Fakat FaultException'lar kanalı Faulted durumuna sokmaz. Clien'a bir .net hatası ve FaultException hatası gönderip denedik.
     FaultException Class 9 overload'ı var. Bu overload'lardaki parametreleri kullanarak Soap mesajlarındaki veriyi ayarlayabiliriz. 4 overload'dın 2. parametresi FaultCode türünde parametre bekliyor. Class Constructor'u parametre olarak FaultCode'un Name değerini istiyor gerçek .net hata adını girebiliriz.
      2. .Net exception'lar sadece .net uygulamalarda tanınabilir. Servisimiz interoperable olduğu için Java uygulamalarlada kullanılabilir ama Java .net exception'ı anlamaz.
     */
    /*19. Ders Creating And Throwing Strongly Typed SOAP Faults
      Soap Fault bizim Wsdl'de oluşdurduğumuz bir Soap mesajı değildir. Fakat bir Soap mesajı oluşturup hatayı yansıtmak için bu mesajı kullanabiliriz. Soap mesajında hatayı kullancıya gönderecek bir parametreleri oluşurmak için bir Class oluşturup parametrelerini serileştiriyoruz.(DataCOntract ve member kullanmış). Service methodların bu hata Class'ını kullanabilmesi için Interface'de methodlara FaultContract Attribute'unu uygulayıp parametre olarak'da oluşturduğumuz class'ın tpye'ını vermemiz gerekir. Class'ın Property değerlerini Service Class'da veriyoruz.
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
            //catch (FaultException<CalculatorService.DivideByZeroFault> faultException)
            //{
            //    label3.Text = faultException.Detail.Error + " - " + faultException.Detail.Details;
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            client = new CalculatorService.CalculatorServiceClient();
        }
    }
}
