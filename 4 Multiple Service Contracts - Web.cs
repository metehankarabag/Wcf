using System;

namespace CompanyClient
{
    /*4. Ders > Multiple ServiceContract(Interface)
       Wsdl'da Service Interface'ini tanımlayan <wsdl:PortType> elementidir. Service Class'ına bir den fazla interface uygulayıp Interface'lere ServiceContract Attribute'unu uyguladığımızda birden fazla Interface kullanmış olur ve her interface için farklı bir <wsdl:PortType> elementi oluşturulur. Interface'leri EndPoint oluştururken kullandığımız için Client sadece kullandığı end point'deki Interface'in methodlarını çalıştırabilir. Tüm Interface'ler Proxy Class oluşturmak için kullanılır fakat endpoint'in bağlan türünün gerekliliğini karşılamıyorsa methodları kullanamaz.
     Wsdl dökümanında End point'lerde belirlediğimiz her farklı bağlantı türü için bir <wsdl:Binding> elementi oluşturur ve bu elementin Type Attribute'unun bir <wsdl:PortType> elementi belirlediğini biliyoruz. Yani her endpoint kendine özgü interface methodları kullanıma açtığı için Client sadece kullandığı Binding'in kullandığı Interface methodlarını kullanabilir.
     */
    /*5.Ders > Client'ın işleyişini bozmadan Service Interface'de değişiklikler yapma.
        Bir Proxy'i oluşturduktan sonra Service Interface'ini düzenleyip Host'u tekrar çalıştırdığımızda Wsdl'daki yapı değişir. Fakat Daha önce eski yapıya göre oluşturulmuş Proxy Class'lar değişmeyeceği için Cliet hata alır. ServiceContract ve OperationContract Attribute'ların Property'lerini kullanarak çalışma zamanında değerlerin değişmesini engelleyebilriz. Name Property'sine eski Interface adı verilip, Interface adı değiştirildiğinde Client bozulmaz.
      */
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Button1_Click(object sender, EventArgs e)
        {
            
            CompanyService.MyCompanyPublicServiceClient client1 = new CompanyService.MyCompanyPublicServiceClient("BasicHttpBinding_IMyCompanyPublicService");
            Label1.Text = client1.GetPublicInformation();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            CompanyService.MyCompanyConfidentialServiceClient client2 = new CompanyService.MyCompanyConfidentialServiceClient("NetTcpBinding_IMyCompanyConfidentialService");
            Label2.Text = client2.GetCofidentialInformation();
        }
    }
}