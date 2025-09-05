using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TasksTrackerWebApp.Authintication
{
    public partial class SigninPage : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                errorMsg.Text = "";
            }

        }

        protected void LoginBtn_Click(object sender, EventArgs e)
        {
            string email = emailtxt.Text.Trim();
            string password = passwrdtxt.Text;

            //string connStr = ConfigurationManager.ConnectionStrings["TaskTrackerConStr"].ConnectionString;

            string connStr = ConfigurationManager.ConnectionStrings["TaskTrackerDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                //string query = "SELECT passwordHash, role FROM users WHERE email = @Email";
                string query = "SELECT PasswordHash, Role FROM [Users] WHERE Email = @Email";


                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        string storedHash = reader.GetString(0);
                        string role = reader.GetString(1);
                        string message = "تم تسجيل الدخول بنجاح!";
                        string script = $"alert('{message}');";
                        if (BCrypt.Net.BCrypt.Verify(password, storedHash))
                        {
                         
                            Session["email"] = email;
                            Session["role"] = role;

                            string redirectUrl = role == "admin"
                                          ? ResolveUrl("~/AdminDashboard/AdminHome.aspx")
                                          : ResolveUrl("~/userHome.aspx");
                            Response.Redirect(redirectUrl);
                            return;
                        }
                    }
                    else errorMsg.Text = "البريد الإلكتروني أو كلمة المرور غير صحيحة";

                }
            }
          
        }

    }
}



