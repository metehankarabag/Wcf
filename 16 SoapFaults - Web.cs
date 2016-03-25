using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CalculatorClient
{
    /* Part 16 Calculator kullandık.
     SOAP FAULT'lar XML formatındadır ve platformlardan bağımsızdır. Tipik SOAP FAUL - FAULT CODE - FAULT REASON - DETAİLS ELEMENT'leri içerir.
     DETAİLS ELEMENT istenilen XML'e eklenebilir.
     IncludeExceptionDetailInFalut özelliğine host'un Config'de veya Servis uygulama sınıfında true verirsek, DETAILS ELEMENT SOAP FAULT'da görünür.
     
     SOAP 1.1'da  FAULT REASON -  FAULT STRING içinde görünüyor ve basicHttpBinding SOAP 1.1 kullanır. Diğer tüm bağlantılar 1.2 kullanır.
     Şimdi Host config'de   wsHttpBining'i deneyeceğiz. Bingding'i değiştirdiğimizde oluşturulmuş PROXY'nin güncellenmesi gerekir.
     
     Bunu deneyip dahayı aldıktan sonra, Service trace Viewer  SOAP FAULT'u göstermez. Bunun nedeni wsHttpBinding için mesaj güvenliği açılır. 
     Güvenlik modunu wsHttpBinding'de kapatmak için NONE a ayarlamalıyız.
     Bunu yapmak için config'de <bindings> bölümünü açmalıyız.
     Güvenlik ayarını değiştirdiğimizde PROXY'i güncellemeliyiz.
     1.2 mesajı biraz daha farklıdır.
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