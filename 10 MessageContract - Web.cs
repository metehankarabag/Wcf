using System;

namespace EmployeeClient
{
    /*10. Ders MessageContract -> MessageHeader -> MessageBodyMember Attibutes
      Soap mesajlar Body ve Head olarak 2 kısımdan oluşur ve Service methodlarının kullandığı tüm parametreler her zaman Soap Body'e eklenir. Service methodlarının parametrelerinde kullandıkları Complex Type'ları serileştirmenin farklı yollarını görmüştük. En çok kontrol sağlayan yöntem Complex Type'a DataContract ve DataMember Attribute'larını kullanarak serileştirmekti. Bu Attriute'ları kullanarak, parametre adlarını, serileştirilme sırasını değiştirme ve bazı üylerin serileştirilmesini engelleyip method kullanımdan çıkartma gibi işlemler yapabiliriz. Fakat bu Attribute'ları kullandığımız Class'ın herhangi bir üyesini Soap mesajın Head bölümüne ekleyemeyiz. Bunun nedeni Complext Type üyelerinin bir bütün halinde <xs:complexType> elementi içinde yüklenmesidir. Bu elementi method parametrelerinde kullandığımızda, Complex Type üyelerinin hepsi ya Head'e yada Body'e eklenir. Bu Type'a MessageContract Attribute'unu uygulasaydık, Soap Head'a da parametre ekleyebilirdik. Çünkü Bu Attbiute'u uyguladığımız Complex Type'ın üyeleri ayrı ayrı serileştirilir. Complex Type'ın serileştirilecek üyelerini belirlemek için MessageHeader veya MessageBodyMember Attibute'larından birini kullanırız. Bu Attribute'lar parametrelerin Soap mesajda alacakları yeri belirler. Complext Type üyelerinin tek tek serileştirildiğini MessageHeader veya MessageBodyMember Attibute'larının Namespace özelliğinden anladım. MessageContract kullanmadan önce tüm Service methodları aynı parametreleri kullansa bile serileştirme ayrı ayrı yapılıyordu. Fakat MessageContract kullandığımızda parametre olarak Complext Type'ı alan soap mesajları için aynı elementi kullanır.
     MessageContract ne zaman kulanılır.
     Service'i kullanılabilmesi için kullanıcının lisans göndermesi gerekiyorsa, bu değeri taşıması için methoda parametre ekleyebiliriz fakat bu yanlış bir mantıktır. Çünkü istenilen parametrenin method kullanımı ile alakası yok. Bu yüzden böyle değerleri Soap head bölümünde taşımak için MessageConract kullanırız.
     Ayrıca MeesageHeader ve messageBodyMember elementlerinin ProtectionLevel Property'sini kullanarak, verilere farklı koruma ayarları uygulamak için kullanırız.
            
      MessageContract Attribute'unun Property'leri: isWrapper, WrapperName, WrapperNameSpace, ProtectionLevel(kullanmadı)
      DataContract Attribute'unun Property'leri: isReferance, Name, NameSpace
      MessageBodyMember: Name, NameSpace,Order,ProtectionLevel
      MessageHeader: Ancor,MustUnderstand,Name, NameSpace,ProtectionLevel,Relay
      DataMember: EmitDefaultValue, isRequired,Name,Order.
    */
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void btnGetEmployee_Click(object sender, EventArgs e)
        {
            EmployeeService.IEmployeeService client = new EmployeeService.EmployeeServiceClient();
            EmployeeService.EmployeeRequest request = new EmployeeService.EmployeeRequest("XYZ120FABC", Convert.ToInt32(txtID.Text));
            EmployeeService.EmployeeInfo employee = client.GetEmployee(request);

            if (employee.Type == EmployeeService.EmployeeType.FullTimeEmployee)
            {
                txtAnnualSalary.Text = employee.AnnualSalary.ToString();
                trAnnualSalary.Visible = true;
                trHourlPay.Visible = false;
                trHoursWorked.Visible = false;
            }
            else
            {
                txtHourlyPay.Text = employee.HourlyPay.ToString();
                txtHoursWorked.Text = employee.HoursWorked.ToString();
                trAnnualSalary.Visible = false;
                trHourlPay.Visible = true;
                trHoursWorked.Visible = true;
            }
            ddlEmployeeType.SelectedValue = ((int)employee.Type).ToString();

            txtName.Text = employee.Name;
            txtGender.Text = employee.Gender;
            txtDateOfBirth.Text = employee.DOB.ToShortDateString();
            lblMessage.Text = "Employee retrieved";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            EmployeeService.IEmployeeService client = new EmployeeService.EmployeeServiceClient();
            EmployeeService.EmployeeInfo employee = new EmployeeService.EmployeeInfo();

            if (ddlEmployeeType.SelectedValue == "-1")
            {
                lblMessage.Text = "Please select Employee Type";
            }
            else
            {
                if (((EmployeeService.EmployeeType)Convert.ToInt32(ddlEmployeeType.SelectedValue)) == EmployeeService.EmployeeType.FullTimeEmployee)
                {
                    employee.AnnualSalary = Convert.ToInt32(txtAnnualSalary.Text);
                    employee.Type = EmployeeService.EmployeeType.FullTimeEmployee;
                }
                else if (((EmployeeService.EmployeeType)Convert.ToInt32(ddlEmployeeType.SelectedValue)) == EmployeeService.EmployeeType.PartTimeEmployee)
                {
                    employee.HourlyPay = Convert.ToInt32(txtHourlyPay.Text);
                    employee.HoursWorked = Convert.ToInt32(txtHoursWorked.Text);
                    employee.Type = EmployeeService.EmployeeType.PartTimeEmployee;
                }

                employee.ID = Convert.ToInt32(txtID.Text);
                employee.Name = txtName.Text;
                employee.Gender = txtGender.Text;
                employee.DOB = Convert.ToDateTime(txtDateOfBirth.Text);

                client.SaveEmployee(employee);
                lblMessage.Text = "Employee saved";
            }
        }

        protected void ddlEmployeeType_SelectedIndexChanged
            (object sender, EventArgs e)
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