using System;
using System.Windows.Forms;

namespace DownloadClient
{
    /*35. Ders Sending Large Messages In WCF Using MTOM(Message Transmission Optimization Mechanism)
     WCF de varsayılan şifreleme mekanizması base64'ün ile şifrelenen Text'dir. Bunun 2 dez avantajı var. Biri Approximately olarak %33 şişirir(bloat) ve fazladan şifreleme ve bozma işlemleri gerçekleşir. Büyük Bindary mesajları göndermek için WCF'de tercih edilen yöntem MTOM mesaj şifrelemesidir. MTOM interoperabe bir standardır. Base64 şifrelemesi yapmaz. Daha az alan kullanır ve tüm mesaj gönderme işlemini önemli ölçüde hızlandırır. Text Message Encoding ile Bindary data base64 ile şifrelenip SOAP mesajına gömülür. MTOM ile bindary data MIME(Multipurpose Internet Mail Extensions) eklentisi olarak Soap mesaja eklenir. Service'in Soap mesajlarıda taşıyacağı Bindary Data'yı nasıl şifreleceğine kullanılan bağlantı türünün ayarlarının belirlenidği <bindings> elementi içinde yapılır. Host Config'de <bindings> elementi içinde hangi binding türünü kullanıyorsak o türün elementini oluşturduktan sonra <binding> elementi elementini istediğimiz kadar kullanabiliriz. Bu elementin Name özelliğindeki değeri kullanarak Endpoint ile ilişkilendirmek için Endpoint'in bindingConfiguration Attiribute'unu değeri veriyoruz. <binding> elementnin messageEncoding Attribute'u verinin nasıl şifreleneceğini belirler. WsHttpBinding kullandığımız için varsayılan Text'dir. Soap mesaj ile gönderilen veri çok fazla ise hata verir. Bu hatayı düzelmek için binding elementinde MaxReceivedMessageSize Attribute'unu ve <binding> içinde kullandığımız <readerQuotas> elementinin mazArrayLength Attribute'unu kullanarak gönderilecek veri miktarını arttırabiliriz. İkisinede 700000 değeri verdi. Bu özellik servis atağı önlemek içindir. 
     
     Şimdi Fiddler'i aracılığı ile hangisinin daha hızlı olduğunu anlayacağız. Oluşan son bağlantıya tıkla > sağ tarafdan Inspectors >  Entity başlığı altında Base64 şifrelemesi kullanmışsa soap+xml yazdığını görürürüz. 
     Inspectors > alt daki TextView sekmesine tıkla > Bindary mesaj şifrelenmiş şekilde Soap Body içine yerleştirilirdiği görünür.
     Inspectors'de Statistic'e geç > hemen altta Bytes Recieved karşısındaki sayı servis ile client arasındaki trafik sayısıdır. Text şifreleme sonucu - 14.204
     Mesaj şifreleme mekanizmasını MTOM yaptıktan sonra  Inspectors sekmesinden >entity ye ve Miscallaneous a bak > Mime eklentisi olduğu görünür.
     TextView'a bak > Sonra <s:envelope> elementi MIME eklentisi içinde oluşturulur. Sağ altda view node pad'e tıkladığımızda tüm mesajın bu ekletinin içinde ama soap'ın dışında olduğu görünür.
     */
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DownloadService.DownloadServiceClient client = new DownloadService.DownloadServiceClient();
            DownloadService.File file = client.DownloadDocument();
            System.IO.File.WriteAllBytes(@"D:\2_Uygulamalar\7_WCF\35 Sending large messages in WCF using MTOM\DownloadClient\" + file.Name, file.Content);
            MessageBox.Show(file.Name + " is Downloaded");
        }
    }
}
