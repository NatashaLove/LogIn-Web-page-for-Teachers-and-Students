using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Final2
{
    public partial class TeacherInfo : System.Web.UI.Page
    {
        public string teacher_id;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["login"] != null)
            {
                HttpCookie cookie = Request.Cookies["login"];

                teacher_id = cookie["teacher_id"].ToString();
                SqlDataSource1.SelectCommand = "SELECT * FROM [viewClasses] WHERE Teacher_ID= " + teacher_id;
            }
            else
            {
                Response.Redirect("./Default.aspx");
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}