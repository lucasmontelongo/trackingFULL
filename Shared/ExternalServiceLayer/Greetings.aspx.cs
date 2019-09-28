using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExternalServiceLayer
{
    public partial class Greetings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ServiceReference1.Service1Client soap = new ServiceReference1.Service1Client();
            Label1.Text = soap.Greeting(TextBox1.Text);
        }
    }
}