using System;
using System.ServiceModel;
using System.Windows.Forms;

namespace ReportClient
{
    /* 34. Ders Duplex Message Exchange Pattern
      Bu Pattern Request-Replay ve OneWay gibi OperationContract Attribute'una uygulanmaz.Duplex Message Exchange Pattern, ServiceContract Attribute'unun CallbackContract Property'sine uygulanır. Property parametre olarak bir Interface'in Type'ını vermeliyiz. Yani bu Pattern'i kullanabilmemiz için biri Service Interface'i olan 2 Interface'e ihtiyacımız var. 2. Interface'e ServiceContract uygulamıyoruz. Çünkü Duplex Message Exchange Pattern'in çalışma mantığında Client Service'in bir methodunu tetiklerken Service de Client'ın bir metodunu tetikler. Yani Client, Service Interface'inden bir methodu tetilediğinde, Service bu methodu işlerken, 2. Interface'ın bir üyesini tetikler. Bu yüzden 2. Interface üyesi Client'da olmalıdır. 2. Interface'e ServiceContract Attribute'unu uygularsak. Bu Interface üyeleri de Client'ın tetikleyeceği methodlara eklenir. Fakat Service'in, uygulaması Client'da belirlenmiş methodu tetikleyebilmesi için Soap mesajları kullanması gerekir. Bu yüzden methodları serileştirmek için 2. Interface üyelerine OperationContract Attribute'unu uygulamalıyız. 
      Bu Pattern'i hem Request-Reply hemde OneWay kullanan methodlar ile birlikte kullanabiliriz. Fakat Client'ın tetiklediği method Request-Reply kullandığımızda Deadlock oluşuyor. (Bununla baya uğraştım 45. dersi izle). Çünkü Client uygulaması varsayılan olarak ilk işini bitirmeden 2. işi çalıştırmaz. Yani Client'ın tetiklediği method Request-Reply kullanıyorsa, Client boş bir Xml mesajı alana kadar kitlenir ve başka isteği işleyemez. Fakat tetiklenen metod Service'de işlenirken, Service, Client'a bir istek gönderecek, Client bu isteği işleyemeyeceğinden sıraya sokar. Service'in tetiklediği method da Request-Replay kullanıyorsa, 2 durum oluşur. Service'den yapılan istek Client'a işlenemeyeceği için Service ilerleyemez. Fakat istek Client'da işlense, Service örneği ilk isteği işlemekle meşgul olduğu için kendi yaptığı isteğin cevabını alamaz. Bu yüzden method tetiklendiğinde Service'de "would Deadlock" hatası alırız. Yani Service isteği hiç göndermez. Bu Sorunu çözmek 2 şey yapmamız gerekir. Birinci Client'ın CallBack isteğini işleme sokmak için kendi isteğini cevabını beklemeksini önlemek. İkinci Service'in, Client'dan aldığı isteği işlerken, Client'a yaptığı isteğin cevabını örneğe almasını sağlamak. Bu iş için Client'da aşağıdaki Attribute'u, Service Class'da iste InstanceContextMode.Reentrant Property'sini kullanmalıyız. Yada OperationContract'lara OneWay uygulamalıyız. Böylece sevap Xml'i kullanılmaz. Deadlock oluşmaz.
      Service'de Property'i kullandıktan sonra, Client'da Attribute'u kullanmassak hata almayız. Fakat Client isteği işleyip cevabı döndüremyeceği için uygulama donar. Bu sorunu çözmek için Service'de Property'i kullandıktan sonra, Client'ın tetiklediği methodu OneWay yapabiliriz. Böylece Client isteğini yaptıkdan sonra hemen serberst kalır. Yada > Client'daki uygulama Class'ına  [CallbackBehavior(UseSynchronizationContext = false)] Attribute'u uygulayıp, sonra Service'in tetiklediği methoda OneWay yapabiliriz. Service iki isteği işleyemez ama Service'e cevap gelmeyeceği için sorun olmaz.
     Client'ın Callback Interface'ine uygulama tanımlayabilmesi için ise Proxy Class'a ekelenen 2. Interface'i kullanarak bir Class'ı türetmelidir.
     Service'in Client methodunu 2. Interface'in bir üyesini tetikleyebilmesi için örneğini kurması gerekir. Bu yüzden türettiği bir Class'ı kullanmalı fakat bu mümkün olmayacak. Bu yüzden OperationContext.Current.GetCallbackChannel<>() generic methodu kullanıyoruz. Type olarak Interface'i verip methodunu tetikleyebiliyoruz.
     Not: CallbackBehavior Attribute'unun UseSynchronizationContext Property'si, sadece Client'dan gelen istek için Client'ı serbest bırakır, Client kullanıcısı Client'ın yaptığı isteğin cevabını alana kadar Client'ı kullanamaz. Client OneWay Operation kullansaydı, Client donmaz, kullanıcı işlemlerine devam edebilirdir. Yani Client'da yapılan işlemler işlem bütünlüğü gerektirmiyorsa, OneWay kullanmak daha iyidir.
    */
    //[CallbackBehavior(UseSynchronizationContext = false)]
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

        public void Progress(int percentageComplated)
        {
            textBox1.Text = percentageComplated.ToString() + "% coplated";
            System.Threading.Thread.Sleep(600);
        }
    }
}
