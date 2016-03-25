using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Client
{	//Part 7 
    /*KnownType Attribute
      Method parametrelerinin Soap mesajlar da kullanılabilmesi için parametrelerin serileştirilmesi gerekir. Bu yüzden method parametrelerinde kullanılan Type'lar Soap mesajlarda da kullanılacağı için otomatik olarak serileştirilir. Method parametresinde kullanılan Complex Type'ın başka bir Complex Type'da üyesi varsa, bu Type'da serileştirilir. Fakat bu Type Enum gibi aracı ile kullanlılıyorsa yani direk Complext Type ile bir ilişkisi yoksa, serileştirilmez. Bu Complex Type'ları Serileştirmek için DataContract uyguladığımız Type'a KnownType Attribute'unu uygulayıp, Attribute'a parametre olarak serileştirilmesini istediğimiz Complex Type'ın Type'ını veriyoruz.
     */
    /*8.Ders Different ways of associating known types
      Known Type belirleme ile ilgili 4 farklı yolu var.
      1. DataContract Class'ında kullanmak: Known Type'ın tüm Service Interface'leri ve methodları tarafından tanınmasını sağlar 
      2. Bir Service Interface'ine uygulamak: Known Type'ın sadece o Service'in tüm methodları tarafından tanınmasını sağlar
      3. OperationContract uygulanmış methodlara uygulama: Known Type sadece uygulandığı method tarafından tanınır.
      3. Host Config dosyasında knownType elementini kullanma: Bu da tüm Service Interface'lerinde kullanımı sağlar.
        <dataContractSerializer>
            <declaredTypes>
             <add type="EmployeeService.Employee, EmployeeService, Version=1.0.0.0, Culture=Neutral, PublicKeyToken=null">
                <knownType type="EmployeeService.FullTimeEmployee, EmployeeService, Version=1.0.0.0, Culture=Neutral, PublicKeyToken=null"/>
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