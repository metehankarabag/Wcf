using System;
using System.Windows.Forms;

namespace HelloWindowsClient
{
    /*3. Ders WCF
      Wcf Service'i de .Net Remoting Service gibi Service Class'ı ve Service Interface'inden oluşur. Wcf Service Client ile iletişimini Soap mesajlar ile sağlar ve Wsdl dökümanı kullanıyr. Yani Wcf service kullanıyorsa Service Interface'ını CLient'a vermemize gerek yok. Bu sadeye bu iki dosya tek bir uygulamada olabilir. Web Service'de Wsdl dökümanı oluşturulurken Service Class'ı kullanılırken, wcf Service'de ise Service Interface'ı kullanılır. Bir Class Library projesine Wcf Service eklediğimizde projeye 2 Class dosyası eklenir. (Service Class'ı ve Interface'i). Service Class'ına miras veren birden fazla Interface olabilir. Wsdl oluşturulurken hangisinin dikkate alınacağını belirlemek için ServiceContract Attribute kullanılır. Tüm Interface'lere uygulayabiliriz. Interface üyesi olan methodların Wsdl'a eklenmesi için ise OperationContract Atrribute'u kullanılır. 
      ServiceContract uygulanmış Interface'ler Wsdl'da <wsdl:PortType> elementi temsil edilir. Bu Xml elemetinin Name Attribute'u Interface'in adını alır. Yani Wsdl'in <wsdl:PortType> elementinin c#'daki karlışığı bir Type'dır.(Class,Interface gibi..). Service Methodlarını temsil eden Xml elementi ise <wsdl:Operation>'dır. OperationContact uygulanmış her method içinde ayrı ayrı kullanılır. Methodların Soap mesajlarda kullanacağı parametreler <wsdl:Message> elementleri ile belirlenir. Bu yüzden <wsdl:Operation> içindeki <wsdl:input> ve <wsdl:output> elementlerinin Message özelliği bir <wsdl:message> elementini gösterir. Method parametrelerinin serileştirildiği element <wsdl:types> elementidir ve <wsdl:message> hangi methodun hangi soap mesajlarda hangi parametreleri kullanacağına bu element içindekilere göre belirler. Service'i çalıştırmak için yine bir Host uygulamasına ihtiyacımız var. Kısaca Host, Service çalıştırmak ve tüm çalıştırma ayarlarını belirlemek için kullanılır. -> Aktifleştirilek Service Class'ı, Service Interface'i, Service'in tutulacağı yeri ve kullanacağı bağlantı türü,vs...  Wsdl oluşturulurken bu ayarlar kullanılır. Host'a Service Class'ını ve Service ayarlarını kullanabilmemiz için System.MvcModel ve Service'ı oluşturduğumuz uygulamanın referansını eklemeliyiz. Wcf Service'in tüm ayarları Host'un Config dosyasında yapılır. 
      Service'e hem Web hemde Windows Client için uygun bağlantıyı türü eklemek için Config dosyasında <endpoint> elementini kullanırız. <endpoint> elementi Wsdl'da <wsdl:Binding> ile temsil edilir. <wsdl:Binding> elementinin Type Attribute'u kullanılacak <wsdl:PortType>'ı belirler.(Interface'i). <wsdl:Binding> içinde <soap:binding> ve <wsdl:Operation> elementleri var. <soap:binding> elementinin Transport Attribute'u kullanılacak bağlantı türünü belirler ve Soap kullanılan mesaj protokoludür. <wsdl:Operation> kullanılan PortType'in tüm methodlarını için kullanılır ve bu elementin en içinde kullanılan <soap:body> elementinin use Attribute'u metodun kullandığı Soap mesajlarının nasıl şifreleceğini belirler.(literal)-> şifreleme yok.
     Tüm Service ayarlarını belirledikten sonra System.ServiceModel.ServiceHost Class'ının uygulamanın host'u çalıştırabilmesi kullanmamız gerekiyor. Bu Class'ın Constructor'ı 2 parametre bekliyor. 1. Service Class'ının Type'ı, 2. parametre optional-> base adres'ler belirleyebileceğimiz Uri dizisi. ServiceHost Class'ının Open() methodu ile Servisi çalıştırabiliriz. Nesneyi Using bloğunda kullanarak otomatik kapanmasını sağlayabiliriz.
      CLient uygulaması aynı.
      1. Not: Client'da Proxy Class'larını kullanırken Class'ını parametresiz kurucusu ile kullanabiliriz. Fakat birden fazla End point belirlemişsek, hata verir. Bu yüzden Class Constructor'unun 2. overload'ını kullanarak kullanmayı istediğimiz Endpoint'in adını vermeliyiz. Sanırım kullanmayı istemediğimiz endpoint'leri silerek'de aynı işi yapabiliriz.
    */
    public partial class Form1 : Form
    {
        public Form1() { InitializeComponent(); }

        private void button1_Click(object sender, EventArgs e)
        {
            HelloService.HelloServiceClient client = new HelloService.HelloServiceClient("NetTcpBinding_IHelloService");
            label1.Text = client.GetMessage(textBox1.Text);
        }
    }
}
/*
<system.serviceModel>
    <services>
      <service name="HelloService.HelloService" behaviorConfiguration="mexBehaviour">
            name: Nesnesi oluşturulacak Service Class'ının tam adı.
        <endpoint address="HelloService" binding="basicHttpBinding" contract="HelloService.IHelloService"> 
         address ATTRIBUTE: Url'in son kısmını belirleyen string'i alır.
         binding ATTRIBUTE: EndPoint'in kullanacağı Url türünü ve Client'a gönderilecek mesaj türünü belirler.
         contract ATTRIBUTE: ServiceContract Attribute uyguladığımız Service Interface'inin tam adını alır.
        </endpoint>
        <endpoint address="HelloService" binding="netTcpBinding" contract="HelloService.IHelloService">
        </endpoint>
        <endpoint address="mex" binding="mexHttpBinding" contract="IMetadataExchange" /> -->Proxy Class'ın kurulması için WSDL'in linki.
        <host> --> Base Addresi belirlediğimiz yer.
          <baseAddresses>
            <add baseAddress="http://localhost:8080/" /> --> End point'lerde kullandığımız Binding türü için Binding'in kullanabileceği Base adresi belirlemek zorundayız.
            <add baseAddress="net.tcp://localhost:8090"/>
          </baseAddresses>
        </host>
      </service> --> Bir service tanımladık. bakla bir Service tanımlamak için gene oluşturabiliriz. 
    </services>
    
    <behaviors>
      <serviceBehaviors> 
        <behavior name="mexBehaviour"> --> Uygulandığı Service'in davranış özelliklerini belirlediğimiz yer geçerli olabilmesi için name'de belirlediğimiz değeri Service elementinin behaviorConfiguration Attribute'una parametre olarak vermeliyiz.
          <serviceMetadata httpGetEnabled="true" /> --> Servisin Get istegi ile kullanılabileceğini belirler.
        </behavior>
      </serviceBehaviors>
    </behaviors>
</system.serviceModel>
 */
