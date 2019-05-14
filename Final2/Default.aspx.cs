using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Final2
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //display error message
            //string error = Request.QueryString["error"]; // in [] - because it's an array in the method
            //// the "error" - from the query line below -?error - looks for it and displays the message -which is under the variable "error"
            //if (!(String.IsNullOrWhiteSpace(error)))
            //{
            //    lblError.Text = error;
                lblError.Visible = true;
            //    TextBox1.Text = "";
            //    TextBox2.Text = "";
            //}
        }

               private SqlConnection sqlConnection()
        {
            SqlConnection conn;

            try
            {
                conn = new SqlConnection(
                    // in sql connections - similar data - on the left side from the '='
                    "Data Source=184.168.194.55;" +
                    "Initial Catalog=classroom;" +
                    "Integrated Security=false;" +
                    "UID=profmorris;" +
                    "PWD=Lec2g#08"
                );
            }
            catch (ArgumentException ex)
            {
                lblError.Text = String.Format(ex.Message);
                conn = null;
            }

            return conn;


        }

        //after the button submit is clicked - a cryptography object is created to aff hash to the password
        protected void Button1_Click(object sender, EventArgs e)
        {
            MD5Cng md5 = new MD5Cng();
            StringBuilder sb = new StringBuilder();// to concat the hash and passw

            string passhash;
            string username = TextBox1.Text;

            byte[] hashbytes = md5.ComputeHash(Encoding.ASCII.GetBytes(TextBox2.Text));

            foreach (byte b in hashbytes)
            {
                sb.Append(b.ToString("X2"));
            }

            passhash = sb.ToString();

            SqlConnection connection = sqlConnection();

            if (connection != null)
            {
                try
                {
                    // check for connection state
                    if (connection.State == System.Data.ConnectionState.Closed)
                    {
                        connection.Open();
                        //lblError.Visible = true;
                        //lblError.Text += "Connected..";

                    }

                    string userCheckQuery = "SELECT COUNT(*) " +
                        " FROM Persons " +
                        " WHERE Person_UName = @uname " +
                        " AND Person_MD5 = @password ";

                    // command brings bck the result
                    SqlCommand sqlCommand = new SqlCommand(userCheckQuery, connection);

                    sqlCommand.Parameters.AddWithValue("@uname", username);
                    sqlCommand.Parameters.AddWithValue("@password", passhash);

                    if ((int)sqlCommand.ExecuteScalar() == 0)
                    {
                       // lblError.Visible = true;
                        lblError.Text += "User does not exist.";
                    }
                    else
                    {
                        // create initial cookie
                        HttpCookie cookie = new HttpCookie("login");
                        //gets the stud id of the required person
                        string studentQuery = "SELECT Student_ID " +
                            " FROM Students " +
                            " JOIN Persons ON Students.Person_ID = Persons.Person_ID " +
                            " WHERE Persons.Person_UName = @uname " +
                            " AND Persons.Person_MD5 = @password ";
                        //checking the query above in the database
                        SqlCommand studentCommand = new SqlCommand(studentQuery, connection);

                        studentCommand.Parameters.AddWithValue("@uname", username);
                        studentCommand.Parameters.AddWithValue("@password", passhash);

                        int student_id = (studentCommand.ExecuteScalar() != null) ? (int)studentCommand.ExecuteScalar() : 0;
                        //the same as: if id is NOT null then- id = (int)studentCommand.ExecuteScalar(); ELSE = 0!

                        if (student_id == 0)
                        {
                            //first checks if the id is student - then if it's teacher
                            string teacherQuery = "SELECT Employee_ID 'Teacher_ID' " +
                                " FROM Employees" +
                                " JOIN Persons ON Employees.Person_ID = Persons.Person_ID " +
                                " WHERE Persons.Person_UName = @uname " +
                                " AND Persons.Person_MD5 = @password ";

                            SqlCommand teacherCommand = new SqlCommand(teacherQuery, connection);

                            teacherCommand.Parameters.AddWithValue("@uname", username);
                            teacherCommand.Parameters.AddWithValue("@password", passhash);

                            //if id is not null - then = (int)teacherCommand.ExecuteScalar(); ELSE = 0!
                            int teacher_id = (teacherCommand.ExecuteScalar() != null) ? (int)teacherCommand.ExecuteScalar() : 0;

                            // close connection
                            connection.Close();

                            // create cookie for teacher_id
                            cookie["teacher_id"] = teacher_id.ToString();

                            // add cookie
                            Response.Cookies.Add(cookie);

                            // redirect to TeacherInfo.aspx
                            Response.Redirect("./TeacherInfo.aspx");
                        }
                        else
                        {
                            // close connection (recommended by the prof - to close connection asap -
                            //when we got all the info from the db - 
                            //the data will be stored in a variable in our program on the server
                            // I create the variable which saves the data  - so can close the connection when saved in variable
                            connection.Close();

                            // create cookie for student_id
                            cookie["student_id"] = student_id.ToString();

                            // add cookie
                            Response.Cookies.Add(cookie);

                            // redirect to StudentInfo.aspx
                            Response.Redirect("./StudentInfo.aspx");

                        }
                    }

                    connection.Close();
                }
                catch (Exception ex)
                {
                   // lblError.Visible = true;
                   
                    lblError.Text = String.Format("Caught this exception: " + ex.Message);
                }
            }

        }

    }
}