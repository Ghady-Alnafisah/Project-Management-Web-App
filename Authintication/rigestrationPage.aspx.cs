

using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace TasksTrackerWebApp
{
    public partial class rigestrationPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        }

        protected void SignUpSubmitBtn_Click(object sender, EventArgs e)
        {
            string selectedRole = roleList.SelectedValue;



            string connStr = ConfigurationManager.ConnectionStrings["TaskTrackerDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {

                string query = @"INSERT INTO [Users] (FirstName, LastName, Email, PasswordHash, Role)
                                   VALUES (@FirstName, @LastName, @Email, @PasswordHash, @Role)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {


                    cmd.Parameters.AddWithValue("@FirstName", fNametxt.Text.Trim());
                    cmd.Parameters.AddWithValue("@LastName", lNametxt.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", emailtxt.Text.Trim());
                    cmd.Parameters.AddWithValue("@PasswordHash", BCrypt.Net.BCrypt.HashPassword(passwrdtxt.Text));
                    cmd.Parameters.AddWithValue("@Role", selectedRole);


                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }

            // Alert and redirect
            string message = "تم إنشاء الحساب بنجاح!";
            string redirectUrl = selectedRole == "admin"
                ? ResolveUrl("~/AdminDashboard/AdminHome.aspx")
                : ResolveUrl("~/userHome.aspx");

            string script = $"alert('{message}'); window.location='{redirectUrl}';";
            ClientScript.RegisterStartupScript(this.GetType(), "SignupSuccess", script, true);
        }


    }
}
