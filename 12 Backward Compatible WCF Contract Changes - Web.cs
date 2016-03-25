using System;

namespace Client
{
	/* 12. Ders Backward Compatible WCF Contract Changes
      Client Proxy Class'ları kullandıktan sonra Wsdl dökümanının değiştirilmesi Client'ın hata almasına neden olur. Bu yüzden Service'de değişiklik yapmayı istiyorsak, eski kullanıcıların uyguluğunu bozmamamız gerekir.
      
      Service Contract'da(Interface'de)
      1. Bir methoda yeni parametre eklersek veya silersek, Client bu durumdan etkilenmez. Çünkü beklenen değer gelmese varsayılan değer kullanılır ve beklenmeyen bir veri gelirse görmezden gelinir.
      2. Bir parametrenin türünü değiştirdiğimizde, Client'ın gönderdiği değer bu türe çevirilmeye çalışılır. Başarılı olunursa sorun yok, başarısız olursa hata alır.
      3. Yeni bir method eklersek,Client bu durumdan etkilenmez. 
      4. Client'da mevut olan bir methodu silersek, kullanıcı methodu kullanmaya çalıştığında hata alır.
      
      DataContract Attribute'unun uygulandığı Class'da
      1. Non-Required bir üye eklersek, Client bu durumdan etkilenmez. Fakat Required bir alan eklersek kullanıcı değer gönderemeyeceği için hata alır.
      2. Non-Required bir üyeyi silerek, Client etkilenmez. Fakat Required bir alanı silersek, kullanıcı
       
     */
    /*13. Ders IExtensibleDataObject
      Non-Required bir Fiedl'ı DataContract'dan sildiğimizde, Client bu alaana veri gönderebilir. Client'ın dönderdiği veriler ile bir Complext Type nesnesi oluşturulduğunda, Property'i sildiğimiz gelen değer kullanılamaz. Bu veriler için DataCOntract uyguladığımız Class'ı IExtensionDataObject Interface'ini kullanıyoruz. Interface'ın ExtensionDataObject dönen Property'si var. ExtensionDataObject Xml'den alınan ve Service'de kullanılmayan tüm verilerin tutulduğu Class'dır. Bu Property ile bu Class nesnesinden değerleri alabiliyoruz. 
      Derste
      -> Kullanıcının gönderdiği değerler ile oluşrulan employee nesnesi bir Employee field'ına alıp, kullanıcı kaydettiği veriyi görmek istediğinde geri gösteriyoruz. Bu sadece son eklenen employee için geçerli bir alınan değerleri bir Employee liste alanına ekleseydik tüm eklenenler için geçerli olurdu.
      Not: ServiceBehavior Attribute'unun ınstanseContextMode özelliğine Single verdik kullandık. Bunun anlamı tüm Client'ların aynı Service örneği kullanacak.
     */
    /*14. Ders IExtensibleDataObject:
      ExtensibleDataObject verilerini hafızada tutar. Bir sürü bilinmeyen veri gönderilirse, tüm veriler hafızayı doldurur.
      Bundan kurtulmak için 2 yol var
      1. ServiceBehavior attributesini servis uygulama sınıfına uygulayıp  IgnoreExtensionDataObject özelliğine true vermek.
      2. Host'daki CONFIG dosyasında ServiceBehavior elementindeki <dataContractSerializer /> elementinde  aynı işi yapmak.
     
     Bu işi yaptığımızda ExtensionData property'si doldurulmayacaktır.
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