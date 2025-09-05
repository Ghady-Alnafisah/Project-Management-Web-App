using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TasksTrackerWebApp
{
    public partial class TasksPage : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["TaskTrackerDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Load tasks with all statuses selected by default
                LoadTasks();
            }
        }

        private void LoadTasks()
        {
            try
            {
                var selectedStatuses = cblStatus.Items.Cast<ListItem>()
                    .Where(li => li.Selected)
                    .Select(li => li.Value.ToLower()) // Normalize to lowercase
                    .ToList();

                string query = "SELECT * FROM TASKS";

                if (selectedStatuses.Any())
                {
                    query += " WHERE LOWER(Status) IN (" +
                           string.Join(",", selectedStatuses.Select(s => $"'{s}'")) + ")";
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlDataAdapter da = new SqlDataAdapter(query, con))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    TasksGridView.DataSource = dt;
                    TasksGridView.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<div class='alert alert-danger'>Error: " + ex.Message + "</div>");
            }
        }






        protected void TaskButton_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "EditTask")
            {
                string taskId = e.CommandArgument.ToString();
                Response.Redirect($"EditTask.aspx?TaskId={taskId}");
            }
        }

        protected void cblStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTasks(); // Refresh when status selection changes
        }

        protected void TasksGridView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
