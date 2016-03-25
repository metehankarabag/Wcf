using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client
{
	/*9 How to Enable Tracing And Message Logging
      Bu işlemler tamamen Client tarafından yapılır. Client uygulamasını kullanan her kullancının Service ile iletişimini sağlayan Soap mesajlarını kaydetmek ve görüntülemek için kullanılır. Bu durum kullanıcı içi tehlikeli olabilir. Service ile Client arasında iletişimi sağlayan mesajları şifrelemeliyiz. 
      Client uygulamasında izleme ve mesajları kaydetme işlemini açmak için -> Web.config > sağ tık > Edit Wcf Service'e şeç.(ilk seferde görünmez). Görünmüyorsa > Tools > Edit Wcf Confıguration > Client uygulamasının Config dosyasını yükle. 
      Yükledikten sonra açılan pencerede --> Diahnostics > Log Auto Flush, Message Loging ve Tracing'i > aç. Message Loging'i açtığımızda Client'ların yaptığı kayıtları kaydeden dosya uygulamaya eklenir. Tracing'de bir dosya ekliyor. Fakat Mesajların dosyaya yazılabilmesi için izin vermemiz gerekiyor. Bu yüzden --> Diahnostics'in alt klasöründeki > Message Logingg > Log Entire Message > True.
      Dosyayı açtıktan sonra bir istekte bulunup dosyayı açtığımızda, Actvity sekmesineki gerçekleşen Action görünür seçtikten sonra, Description sekmesinde Service'e yapılan Request ve Response için oluşturulan Soap mesajı görünür. (1. istek 2. cevap) Birinine tıkladıktan sonra alt kısımdaki Formatted, XML, Message sekmeleri kullanarak mesajın içeriği ile bilgileri görebilriiz. Message sekmesinde <MessageLogTraceRecord> Elementi var ve bu elementin içinde de 2 element daha var -> <Addressing> ve <s:Envelope>(soap Envelope)
     <Addressing> Elementi içinde 2 element var. <Action>, <To>
     <s:Envelope> Elementi içinde de 2 element var. <s:Header, <s:Body>
     Not: Eklenen dosyaları çift tıklayarak, visualStudio SDK tools klasörü içindeki > Service Trace Viewer ile veya Visual Studio Command Promt ile açlabiliriz. -> Comman Promt'a > SVCTRACEVIEWER yaz.
     */
    /* Part 9
	 TRACING ve MESSAGE LOGING'i nasıl açağımızı öğrenecez. 
     2 yolu var 
     1. CLIENT projesinde web.config dosyasına sağ tıkla 
	 EDİT WCF CONFIGURATION'u seç yoksa 
	 Tools menüsünden şeç ve uygulamadaki web.config dosyasını göster sonra açılır.
     Diahnostics seçmesinde Log auto Flush, message looging ve Tracing'i aç. (Son 2 si uygulamaya 2 dosya ekleyecek.)
     Sonra Diahnostics'in alt klasöründeki Message Logingg'e tıkla Log Entire Message ye true değeri ver.
     
     Bu dosyalara açmak için üzerine çift tıklayabilir veya visualStudio SDK tools altındaki Service Trace Viewer'i açabiliriz. Visual Studio Command Promt ile açmak için SVCTRACEVIEWER yaz enter la
     
     */
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void btnGetEmployee_Click(object sender, EventArgs e)
        {
            EmployeeService.EmployeeServiceClient client = new EmployeeService.EmployeeServiceClient();
            EmployeeService.Employee employee = client.GetEmployee(Convert.ToInt32(txtID.Text));

            if (employee.Type == EmployeeService.EmployeeType.FullTimeEmployee)
            {
                txtAnnualSalary.Text = ((EmployeeService.FullTimeEmployee)employee).AnnualSalary.ToString();
                trAnnualSalary.Visible = true;
                trHourlPay.Visible = false;
                trHoursWorked.Visible = false;
            }
            else
            {
                txtHourlyPay.Text = ((EmployeeService.PartTimeEmployee)employee).HourlyPay.ToString();
                txtHoursWorked.Text = ((EmployeeService.PartTimeEmployee)employee).HoursWorked.ToString();
                trAnnualSalary.Visible = false;
                trHourlPay.Visible = true;
                trHoursWorked.Visible = true;
            }
            ddlEmployeeType.SelectedValue = ((int)employee.Type).ToString();

            txtName.Text = employee.Name;
            txtGender.Text = employee.Gender;
            txtDateOfBirth.Text = employee.DateOfBirth.ToShortDateString();
            lblMessage.Text = "Employee retrieved";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            EmployeeService.EmployeeServiceClient client = new EmployeeService.EmployeeServiceClient();
            EmployeeService.Employee employee = null;

            if (((EmployeeService.EmployeeType)Convert.ToInt32(ddlEmployeeType.SelectedValue)) == EmployeeService.EmployeeType.FullTimeEmployee)
            {
                employee = new EmployeeService.FullTimeEmployee
                {
                    Id = Convert.ToInt32(txtID.Text),
                    Name = txtName.Text,
                    Gender = txtGender.Text,
                    DateOfBirth = Convert.ToDateTime(txtDateOfBirth.Text),
                    Type = EmployeeService.EmployeeType.FullTimeEmployee,
                    AnnualSalary = Convert.ToInt32(txtAnnualSalary.Text),
                };
                client.SaveEmployee(employee);
                lblMessage.Text = "Employee saved";
            }
            else if (((EmployeeService.EmployeeType)Convert.ToInt32(ddlEmployeeType.SelectedValue)) == EmployeeService.EmployeeType.PartTimeEmployee)
            {
                employee = new EmployeeService.PartTimeEmployee
                {
                    Id = Convert.ToInt32(txtID.Text),
                    Name = txtName.Text,
                    Gender = txtGender.Text,
                    DateOfBirth = Convert.ToDateTime(txtDateOfBirth.Text),
                    Type = EmployeeService.EmployeeType.PartTimeEmployee,
                    HourlyPay = Convert.ToInt32(txtHourlyPay.Text),
                    HoursWorked = Convert.ToInt32(txtHoursWorked.Text),
                };
                client.SaveEmployee(employee);
                lblMessage.Text = "Employee saved";
            }
            else
            {
                lblMessage.Text = "Please select Employee Type";
            }
        }

        protected void ddlEmployeeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEmployeeType.SelectedValue == "-1")
            {
                trAnnualSalary.Visible = false;
                trHourlPay.Visible = false;
                trHoursWorked.Visible = false;
            }
            else if (ddlEmployeeType.SelectedValue == "1")
            {
                trAnnualSalary.Visible = true;
                trHourlPay.Visible = false;
                trHoursWorked.Visible = false;
            }
            else
            {
                trAnnualSalary.Visible = false;
                trHourlPay.Visible = true;
                trHoursWorked.Visible = true;
            }
        }
    }
}