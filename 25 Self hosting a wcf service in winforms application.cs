using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HelloClient
{
    /*Part 25 Form1.cs'de
    Console'dan tek farkı arayüzü var ama yinede sadece test için kullanılır.
     */
    public partial class Form1 : Form
    {
        HelloService.HelloServiceClient client;
        public Form1()
        {
            InitializeComponent();
            client = new HelloService.HelloServiceClient();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                if (client.State == System.ServiceModel.CommunicationState.Faulted)
                {
                    client = new HelloService.HelloServiceClient();
                }

                label2.Text = client.GetMessage(textBox1.Text);
            }
            catch (Exception ex)
            {

                label2.Text = ex.Message;
            }
        }
    }
}
