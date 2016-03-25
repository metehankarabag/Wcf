using System;

namespace HelloWebClient
{
    /*1. Ders What is WCF(Windows Communication Foundation)
      Interoperable ve Distributed Application'lar oluşturmak için kullanılan bir Microsoft platformudur. Uygulamayı oluşturan parçaların birden fazla bilgisayarda çalıştığı uygulamalara Distributed uygulamalar denir.Connected System'ler de denir. Herhangi bir platformdaki uygulama ile birlikte çalışabilecek uygulamaya ise Interoperable uygulama denir.
      Distributed Application: 2 avantajı var. 
      > 1. oluşturduğumuz uygulama ile birlikte çalışan başka uygulamaları kullanabiliriz. Örneğin kurumsal bir şirketin E-ticaret sitesi ödeme işlemleri için PayPal Servce'lerini kullanabilir. Service'ler Paypal bilgisayarlarında, e-ticaret sitesi bizim bilgisayarımızda çalışan uygulamadır. 
      > 2. Uygulamamızı birden fazla katmanda oluşturarak her uygulamayı farklı bir bilgisayar da çalıştırabiliriz. Bu hem uygulama performansını hemde uygulama ölçeklenebilirliğini attırır.  Yani uygulamanın gelecekteki ithiyaçlara göre yeniden düzenlenebilirliğini arttırır. Katmanlar: 1. Presentation tier, 2. Business tier, 3. Data Access tier.
      
      Interoperable Application: Uygulama çıktısının tüm platformlarda çalışabilecek yapıda olan uygulamalara denir. Örneğin Web Service'ler Interoperable'dır fakat .Net remoting Service'ler değildir. Çünkü .net remoting Service tüm ortamlarda çalışabilecek bir çıktı vermez fakat Web Service Xml formatın da bir çıktı verir.
      
      2 Client uygulamasında kullanmak için bir servis oluşturmayı istiyoruz. 
      > 1. Uygulama Java ile yazılmış bir Web uygulaması. Servis'in bu uygulama ile birlikte çalışabilmesi için XML formatında çıktı vermesi ve çıktıyı HTTP Protokolu ile Client'a göndermesi gerekir. WCF kullanmadan bu işi yapabilmemiz için Web Service kullanarak servisi oluşturmalıyız. 
      > 2. Uygulama Windows uygulaması servisin bu uygulama ile birlikte (performanslı) çalışabilmesi için Bindary formatındaki veriyi Tcp protokolü ile göndermesi gerekir. Bu işi yapabilmemiz için ise .Net Remoting Service teknolojisini kullanmalıyız. 
      
      Distributed uygulamalar oluşturmak için WCF'den önce -> Enterprise Services, Dot Net Remoting, Web Services,Msmq gibi teknolojiler kullanılıyordu. Yani WCF'den her farklı uygulama türü için aynı işi yapan farklı servis oluşturmamız gerekiyordu. Bu teknolojiler bir birinden tamamen bağımsız olduğu için kullanabilmemiz için hepsini öğrenmemiz gerekir. WCF bu tür farklı teknolojilerin toplandığı bir platformdur. WCF'de Client uygulamasına uygun formatta veri ve protokol kullanarak, birden fazla end point oluşturup tüm uygulamalar ile birlikte çalışabilecek bir servis oluşturabiliriz. 
      Gelecek derste bu uygulamaları yapacağız. Sonra Wcf'i kullanmaya başlayacağız.
     */
	 /*2. Ders Web App için Asmx Web Service 
	   Asmx Web Service derslerinde yazdım.
		E:\2_Uygulamalar\7_WCF\_Asp.Net Web Service\1 Introduction To Asp Net Web Services\_1_IntroductionToWebServices\CalculatorWebService.asmx.cs
	 */
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Button1_Click(object sender, EventArgs e)
        {
            HelloWebService.HelloWebServiceSoapClient client = new HelloWebService.HelloWebServiceSoapClient();

            Label1.Text = client.GetMessage(TextBox1.Text);
        }
    }
}