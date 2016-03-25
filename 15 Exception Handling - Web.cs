using System;

namespace CalculatorClient
{
    /* 15. Ders Exception Handling
     Client'ın gönderdiği değerler WCF Service'de hataya neden olursa, Service hatayı Soap Fault içinde serileştirir ve Soap Fault'u Client'a gönderir. Bu yüzden Client'da FaultExcetion hatası alırız. Yani gerçek hatayı almayız. Bunun nedeni sistem güvenliği sağlamaktır. Debugging amacı ile hatayı Client'a göndermeyi istiyorsak, IncludeExceptionDetailInFaults özelliğini kullanabiliriz. 2 yolu var.
     1. Host Config'de <behavior> içinde IncludeExceptionDetailInFaults özelliğine true vermek.
     2. Service Class'ına uyguladığımız ServiceBehavior Attribute'unda aynı Property'i kullanmak
          
     Görüşmelerde sorulan sorular
     WCF'de bir hata olursa ne olur? veya SOAP FAULT nedir?
     WCF service hataları istemciye nasıl yansılıtır?
     */
    /* 16. Ders Soap faults
     SOAP Fault yapısında Fault Code- Fault Reason - Details elementlerini içeren Soap mesajlarıdır. Details Element istenilen XML'e de olabilir de olmayabilirde Bunu IncludeExceptionDetailInFalut Property'sinin aldığı değer belirler. True verisek hata detayının Client'a gönderilebilmesi için Soap mesajına eklenir. 
     
     SOAP 1.1'da  Fault Reason - Fault Strıng içinde görünüyor ve basicHttpBinding SOAP 1.1 kullanır. Diğer tüm bağlantı türleri 1.2 kullanır. Kullandığımız bağlantı türünü wsHttpBining yaptık ve Soap 1.2 kullanıldı. Binding'i değiştirdiğimizde oluşturulmuş PROXY'nin güncellenmesi gerekir. Bunu deneyip hatayı aldıktan sonra, Service Trace Viewer'da  SOAP FAULT'u göstermeyiz. Çünkü wsHttpBinding için mesaj güvenliği engeller. Güvenlik modunu wsHttpBinding'de kapatmak için NONE a ayarlamalıyız.
     Bunu yapmak için config'de <bindings> <security mode="None">'u Confige'e eklemeliyiz. Güvenlik ayarını değiştirdiğimizde de PROXY'i güncellemeliyiz
    */
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void btnDivide_Click(object sender, EventArgs e)
        {
            int numerator = Convert.ToInt32(txtNumerator.Text);
            int denominator = Convert.ToInt32(txtDenominator.Text);
            CalculatorService.CalculatorServiceClient client =
                new CalculatorService.CalculatorServiceClient();
            lblResult.Text = client.Divide(numerator, denominator).ToString();
        } 
    }
}