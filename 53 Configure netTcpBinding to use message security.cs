using System;
using System.Windows.Forms;

namespace SimpleClient
{
    /*53 Configure netTcpBinding to use message security
     Bu dersde netTcpBinding'in varsayılan <security mode="Transport" /> ayarını Message'a çevirdik. Bu kolay çünkü geçen derste IIS'de bağlantı türünü değiştirdik. Bunu neden yaptığımız anlamadım.
     */
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCallService_Click(object sender, EventArgs e)
        {
            SimpleService.SimpleServiceClient client = new SimpleService.SimpleServiceClient();
            MessageBox.Show(client.GetMessage("Metehan"));
        }
    }
}
