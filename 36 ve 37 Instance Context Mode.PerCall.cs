using System;

namespace SampleClient
{
    /* 36. Ders Instance Context Mode (Instance Mode) nedir?
     Client Service'e bir istekte bulunduğunda Service CLass'ın bir örneği oluşturulur.  Instance Context Mode bu örneğin Server'da ne kadar kalacağını belirler. 3 Seçenek var.
     1. PerCall: İsteği yapan Client aynı olsun olmasın, her istek için Service Class'ın yeni bir örneği oluşturulur.
     2. PerSessıon: Her yeni Client Sessıon'ı için Service Class'ın bir örneği olşturulur ve Session süresi boyunca hatırlanır.
     3. Single: Service Class'ın sadece bir örneği oluşturulur ve Host yeni den başlatılana kadar aynı örnek tüm Client'lar tarafından kullanılır.
     Service Class'ına uyguladığımız ServiceBehavior Attribute'unun InstansContextMode Property'sine InstanceContextMode enumun 3 değerinden birini vererek, Service örneğinin ne kadar saklanacağını belirleriz.
    */

    /*1 -> 37. Ders PerCall
    Service'deki method Service Class'ının üyesi olan bir int Field'ın değerini her çağarıda bir arttırıyor. Fakat PerCall kullandığımızda her çağrı için yeni bir service oluşturulduğu için ne kadar istek yaparsak yapalım hep 1 değerini alıyoruz.
    PerCall'ın avantajları: Hafıza daha iyi kullanır. Çünkü Service nesneleri hemen silinir. 2. Uyumluluk sorunu yok. 3. Uygulama daha kolay ölçeklenebilir(düzenlenebilir). 
     Dezavantajları: Çağrılar arasında durum hatırlanmaz. Yani Service örneğindeki değerleri kaydetmemiz gerekir. Performas sorun olur. Çünkü her seferinden Service Class'ın yeni bir örneği oluşturuyor.
    */
    class Program
    {
        static void Main(string[] args)
        {
            SimpleService.SampleServiceClient client = new SimpleService.SampleServiceClient();
            int number = client.incrementNumber();
            Console.WriteLine("Number after first call = "+ number);
            number = client.incrementNumber();
            Console.WriteLine("Number after second call = " + number);
            number = client.incrementNumber();
            Console.WriteLine("Number after thrid call = " + number);
            Console.ReadLine();
        }
    }
}
