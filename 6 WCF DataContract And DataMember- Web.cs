using System;

namespace Client
{
    /*6.Ders DataContract ve DataMember
      <wsdl:type> içindeki ilk <xs:schema> elementi içinde methodların Request ve Response Soap mesajlarında kullanacakları parametreler serileştirilir. Fakat her parametrenin bir .net Type'ı var ve bu xml dosyası tarafından tanınmıyor. <wsdl:type> içindeki 2. <xs:schema> ise Simple Type'ların serileştirildiği şemadır. Bu şema ilk şema içine <xs:import>(using) elementi ile çağarılır ve parametrelerin türleri belirlenir. Service methodları Complex Type parametre kullanıyorsa, Type'ında serileştirilmesi gerekir.
      .Net türlerinin XML formatındaki temsilini oluşturma işine serileştirme denir. Varsayılan olarak Complex Type'lar serileştirilmez. Fakat Wcf Service Soap mesajlarda kullanılacak tüm Complex Type'ları varsayılan olarak DataContractSerializer Class'ını kullanarak serileştirir. Bu Class Complex Type'ların sadece public Property'lerini serileştirir. Complex Type'a Serializable Attribute'unun uyguladığımızda, bu Attribute Complex Type'ın tüm Private Field'larını serileştirir. Her iki kullanımda da hangi üyelerin serileştirilip serileştirilemeyeceğini kontrol edemeyiz. DataContract Attribute'u ve DataMember Attribute'u Complex Type'ları serileştme işleminde bize tam kontrol sağlar. 3 kullanımda da serileştirme işi bir <xs:complexType> içinde yapılır ve bu elementi 1. şemadaki method parametrelerine eklediğimizde, element içinde oluşturulmuş tüm üyeleride method parametrelerine eklemiş oluruz. Yani DataContract ve Member sadece serileştirilecek Complex Type üyeleri üzerinde bazı işlemler yapma imkanı verir. Fakat hepsi Complext Type üyelerini bir <xs:ComplexType> içine serileştirir.
     DataContract'ın NameSpace Property'si http://pragimtech.com/2013/07/07/Employee --> oluşturulacak <xs:schema>nın NameSpace'ini verir. Service'in başka bir service ile karşılmaması için Namespace değeri olarak domain adı verilir. Service'ın oluşturulma tarihide eklenebiliriz. Bu url geçerli değildir.
      DataMember Attribute'unun Property'ler -> Name,Order,isRequired,EmitDefultValue(bool) 
      
      1. Not:Seralizable Attribute'u Field'ları serileştiriliyorsa, bu Class güvenliğini bozar.
      2. Not: Bu ders ile ServiceContract -> OperationContract -> DataContract -> DataMember Attribute'larını görmüş olduk.
     */
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void btnSave_Click(object sender, EventArgs e)
        {
            EmployeeService.Employee employee = new EmployeeService.Employee();
            employee.Id = Convert.ToInt32(txtID.Text);
            employee.Name = txtName.Text;
            employee.Gender = txtGender.Text;
            employee.DateOfBirth = Convert.ToDateTime(txtDateOfBirth.Text);

            EmployeeService.EmployeeServiceClient client = new EmployeeService.EmployeeServiceClient();
            client.SaveEmployee(employee);
            lblMessage.Text = "Employee saved";
        }

        protected void btnGetEmployee_Click(object sender, EventArgs e)
        {
            EmployeeService.EmployeeServiceClient client = new EmployeeService.EmployeeServiceClient();
            EmployeeService.Employee employee = client.GetEmployee(Convert.ToInt32(txtID.Text));

            txtName.Text = employee.Name;
            txtGender.Text = employee.Gender;
            txtDateOfBirth.Text = employee.DateOfBirth.ToShortDateString();

            lblMessage.Text = "Employee retrieved";
        } 
    }
}