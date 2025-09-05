using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TasksTrackerWebApp
{
    public partial class CreateTask : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCategories();
        LoadUsers();
        BindTaskRepeater();
    }
}

private void LoadCategories()
{
    string query = "SELECT CategoryId, Name FROM dbo.Categories ORDER BY Name";
    using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TaskTrackerDB"].ConnectionString))
    using (var cmd = new SqlCommand(query, conn))
    {
        conn.Open();
        ddlCategory.DataSource = cmd.ExecuteReader();
        ddlCategory.DataTextField = "Name";
        ddlCategory.DataValueField = "CategoryId";
        ddlCategory.DataBind();
        ddlCategory.Items.Insert(0, new ListItem("--اختر التصنيف--", ""));
    }
}

private void LoadUsers()
{
    string query = @"
                SELECT 
                    UserId, 
                    FirstName + ' ' + LastName AS UserName,
                    Email
                FROM 
                    dbo.Users 
                ORDER BY 
                    FirstName, LastName";

    using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TaskTrackerDB"].ConnectionString))
    using (var cmd = new SqlCommand(query, conn))
    {
        conn.Open();
        ddlUsers.DataSource = cmd.ExecuteReader();
        ddlUsers.DataTextField = "UserName";
        ddlUsers.DataValueField = "UserId";
        ddlUsers.DataBind();
        ddlUsers.Items.Insert(0, new ListItem("--إسناد إلى--", ""));
    }
}

protected void btnCreateTask_Click(object sender, EventArgs e)
{
    if (Page.IsValid)
    {
        try
        {
            int taskId = SaveTaskToDatabase();

            if (taskId > 0)
            {
                SendTaskNotification(taskId);
                lblStatus.Text = "Task created successfully!";
                lblStatus.ForeColor = System.Drawing.Color.Green;
                ClearForm();
                BindTaskRepeater();
            }
        }
        catch (Exception ex)
        {
            lblStatus.Text = $"Error: {ex.Message}";
            lblStatus.ForeColor = System.Drawing.Color.Red;
        }
    }
}

private int SaveTaskToDatabase()
{
    string query = @"
                INSERT INTO dbo.Tasks (
                    Title, Description, DueDate, Priority, Status,
                    CategoryId, UserId, CreatedAt
                ) VALUES (
                    @Title, @Description, @DueDate, @Priority, @Status,
                    @CategoryId, @UserId, GETDATE()
                );
                SELECT SCOPE_IDENTITY();";

    using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TaskTrackerDB"].ConnectionString))
    using (var cmd = new SqlCommand(query, conn))
    {
        cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
        cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
        cmd.Parameters.AddWithValue("@DueDate", DateTime.Parse(txtDueDate.Text));

        cmd.Parameters.AddWithValue("@Priority", ddlPriority.SelectedValue);
        cmd.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue);
        cmd.Parameters.AddWithValue("@CategoryId", int.Parse(ddlCategory.SelectedValue));
        cmd.Parameters.AddWithValue("@UserId", int.Parse(ddlUsers.SelectedValue));

        conn.Open();
        return Convert.ToInt32(cmd.ExecuteScalar());
    }
}

private string GetAssignedUserEmail(int userId)
{
    string query = "SELECT Email FROM dbo.Users WHERE UserId = @UserId";
    using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TaskTrackerDB"].ConnectionString))
    using (var cmd = new SqlCommand(query, conn))
    {
        cmd.Parameters.AddWithValue("@UserId", userId);
        conn.Open();
        return cmd.ExecuteScalar()?.ToString();
    }
}

private string GetNotifierEmail()
{
    string query = "SELECT Email FROM dbo.Users WHERE Role = 'Notifier'";
    using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TaskTrackerDB"].ConnectionString))
    using (var cmd = new SqlCommand(query, conn))
    {
        conn.Open();
        return cmd.ExecuteScalar()?.ToString();
    }
}

private void SendTaskNotification(int taskId)
{
    try
    {
        string taskTitle = txtTitle.Text;
        string assignedTo = ddlUsers.SelectedItem.Text;
        string dueDate = txtDueDate.Text;
        string priority = ddlPriority.SelectedValue;

        // Send to assigned user
        string assignedUserEmail = GetAssignedUserEmail(int.Parse(ddlUsers.SelectedValue));
        if (!string.IsNullOrEmpty(assignedUserEmail))
        {
            string subject = $"New Task Assigned: {taskTitle}";
            string body = $@"
                        <h3>New Task Assignment</h3>
                        <p><strong>Title:</strong> {taskTitle}</p>
                        <p><strong>Due Date:</strong> {dueDate}</p>
                        <p><strong>Priority:</strong> {priority}</p>
                        <p><a href='http://yoursite.com/TaskDetails.aspx?TaskId={taskId}'>View Task Details</a></p>
                    ";
            SendEmailDirectly(assignedUserEmail, subject, body);
        }

        // Send to notifier
        string notifierEmail = GetNotifierEmail();
        if (!string.IsNullOrEmpty(notifierEmail))
        {
            string notifierSubject = $"SYSTEM NOTIFICATION: New Task Created - {taskTitle}";
            string notifierBody = $@"
                        <h3>New Task Created in System</h3>
                        <p><strong>Title:</strong> {taskTitle}</p>
                        <p><strong>Assigned To:</strong> {assignedTo}</p>
                        <p><strong>Due Date:</strong> {dueDate}</p>
                        <p><strong>Priority:</strong> {priority}</p>
                        <p><a href='http://yoursite.com/TaskDetails.aspx?TaskId={taskId}'>View Task</a></p>
                    ";
            SendEmailDirectly(notifierEmail, notifierSubject, notifierBody);
        }
    }
    catch (Exception ex)
    {
        System.Diagnostics.Debug.WriteLine($"Email sending error: {ex.Message}");
    }
}

private void SendEmailDirectly(string toEmail, string subject, string body)
{
    try
    {
        using (var smtp = new SmtpClient("smtp.gmail.com", 587))
        {
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(
                "tasknotifier6@gmail.com",
                "zydpmrdpsmqbhvsp"
            );

            using (var msg = new MailMessage())
            {
                msg.From = new MailAddress("tasknotifier6@gmail.com");
                msg.To.Add(toEmail);
                msg.Subject = subject;
                msg.Body = body;
                msg.IsBodyHtml = true;

                smtp.Send(msg);
                lblStatus.Text = "Email sent successfully!";
                lblStatus.ForeColor = System.Drawing.Color.Green;
            }
        }
    }
    catch (Exception ex)
    {
        lblStatus.Text = $"Failed: {ex.Message}";
        lblStatus.ForeColor = System.Drawing.Color.Red;

        // Log detailed error
        System.Diagnostics.Debug.WriteLine($"SMTP ERROR: {ex.ToString()}");
    }
}


protected void btnTestEmail_Click(object sender, EventArgs e)
{
    try
    {
        using (MailMessage mail = new MailMessage())
        {
            mail.From = new MailAddress("tasknotifier6@gmail.com");
            mail.To.Add("yourpersonalemail@example.com");
            mail.Subject = "SMTP Test Email";
            mail.Body = "This is a test email from TaskTracker";

            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                smtp.Credentials = new NetworkCredential("tasknotifier6@gmail.com", "gnuf ytbb vhdz jlgo");
                smtp.EnableSsl = true;
                smtp.Send(mail);
                lblStatus.Text = "Test email sent successfully! Check your inbox.";
                lblStatus.ForeColor = System.Drawing.Color.Green;
            }
        }
    }
    catch (Exception ex)
    {
        lblStatus.Text = $"Test email failed: {ex.Message}";
        lblStatus.ForeColor = System.Drawing.Color.Red;
    }
}


protected void btnTest_Click(object sender, EventArgs e)
{
    SendEmailDirectly(
        "redwansara4@gmail.com", // Your  email of choice for testing -- the email you want the notifictaions sent to
        "SMTP Test",
        "<h3>This is a working test</h3>"
    );
}

private void ClearForm()
{
    txtTitle.Text = "";
    txtDescription.Text = "";
    txtDueDate.Text = "";
    ddlPriority.SelectedIndex = 0;
    ddlStatus.SelectedIndex = 0;
    ddlCategory.SelectedIndex = 0;
    ddlUsers.SelectedIndex = 0;
}


private void BindTaskRepeater(string searchTerm = "")
{
    string query = @"
            SELECT 
                t.TaskId, t.Title, t.Description, t.DueDate, 
                t.Priority, t.Status, c.Name AS CategoryName,
                u.FirstName + ' ' + u.LastName AS UserName
            FROM 
                dbo.Tasks t
                INNER JOIN dbo.Categories c ON t.CategoryId = c.CategoryId
                INNER JOIN dbo.Users u ON t.UserId = u.UserId
            WHERE 
                @SearchTerm = '' 
                OR t.Title LIKE '%' + @SearchTerm + '%'
                OR t.Description LIKE '%' + @SearchTerm + '%'
                OR c.Name LIKE '%' + @SearchTerm + '%'
                OR u.FirstName + ' ' + u.LastName LIKE '%' + @SearchTerm + '%'
            ORDER BY 
                t.DueDate DESC";

    using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TaskTrackerDB"].ConnectionString))
    using (var cmd = new SqlCommand(query, conn))
    {
        cmd.Parameters.AddWithValue("@SearchTerm", string.IsNullOrWhiteSpace(searchTerm) ? "" : searchTerm.Trim());

        conn.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        rptTasks.DataSource = dt;
        rptTasks.DataBind();
    }
}

protected void txtSearch_TextChanged(object sender, EventArgs e)
{
    BindTaskRepeater(txtSearch.Text.Trim());
}





protected void rptTasks_ItemCommand(object source, RepeaterCommandEventArgs e)
{
    int taskId = Convert.ToInt32(e.CommandArgument);

    if (e.CommandName == "SelectTask")
    {
        LoadTaskForEdit(taskId);
    }

    if (e.CommandName == "Edit" && Page.IsValid)
    {
        LoadTaskForEdit(taskId);

    }
    else if (e.CommandName == "Delete")
    {
        DeleteTask(taskId);
    }
}


private void LoadTaskForEdit(int taskId)
{
    string query = @"
        SELECT 
            TaskId, Title, Description, DueDate, Priority, Status,
            CategoryId, UserId
        FROM 
            dbo.Tasks
        WHERE 
            TaskId = @TaskId";

    using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TaskTrackerDB"].ConnectionString))
    using (var cmd = new SqlCommand(query, conn))
    {
        cmd.Parameters.AddWithValue("@TaskId", taskId);
        conn.Open();

        using (SqlDataReader reader = cmd.ExecuteReader())
        {
            if (reader.Read())
            {
                hdnTaskId.Value = reader["TaskId"].ToString();
                txtTitle.Text = reader["Title"].ToString();
                txtDescription.Text = reader["Description"].ToString();
                txtDueDate.Text = Convert.ToDateTime(reader["DueDate"]).ToString("yyyy-MM-dd");


                ddlPriority.SelectedValue = reader["Priority"].ToString();
                ddlStatus.SelectedValue = reader["Status"].ToString();
                ddlCategory.SelectedValue = reader["CategoryId"].ToString();
                ddlUsers.SelectedValue = reader["UserId"].ToString();


                btnCreateTask.Visible = false;
                btnUpdateTask.Visible = true;
                btnCancelUpdate.Visible = true;
            }
        }
    }
}



private void DeleteTask(int taskId)
{
    string query = "DELETE FROM dbo.Tasks WHERE TaskId = @TaskId";

    using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TaskTrackerDB"].ConnectionString))
    using (var cmd = new SqlCommand(query, conn))
    {
        cmd.Parameters.AddWithValue("@TaskId", taskId);
        conn.Open();
        int rowsAffected = cmd.ExecuteNonQuery();

        if (rowsAffected > 0)
        {
            lblStatus.Text = "Task deleted successfully!";
            lblStatus.ForeColor = System.Drawing.Color.Green;
            BindTaskRepeater();
        }
        else
        {
            lblStatus.Text = "Error deleting task.";
            lblStatus.ForeColor = System.Drawing.Color.Red;
        }
    }
}

private void UpdateTaskInDatabase(int taskId, DateTime? completedAt)
{
    string query = @"
        UPDATE dbo.Tasks 
        SET 
            Title = @Title,
            Description = @Description,
            DueDate = @DueDate,
            Priority = @Priority,
            Status = @Status,
            CategoryId = @CategoryId,
            UserId = @UserId,
            CompletedAt = @CompletedAt
        WHERE 
            TaskId = @TaskId";

    using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TaskTrackerDB"].ConnectionString))
    using (var cmd = new SqlCommand(query, conn))
    {
        cmd.Parameters.AddWithValue("@TaskId", taskId);
        cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
        cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
        cmd.Parameters.AddWithValue("@DueDate", DateTime.Parse(txtDueDate.Text));
        cmd.Parameters.AddWithValue("@Priority", ddlPriority.SelectedValue);
        cmd.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue);
        cmd.Parameters.AddWithValue("@CategoryId", int.Parse(ddlCategory.SelectedValue));
        cmd.Parameters.AddWithValue("@UserId", int.Parse(ddlUsers.SelectedValue));
        cmd.Parameters.AddWithValue("@CompletedAt", completedAt != null ? (object)completedAt : DBNull.Value);

        conn.Open();
        cmd.ExecuteNonQuery();
    }
}


private string GetUserFriendlyConstraintMessage(SqlException ex)
{
    if (ex.Message.Contains("CHK_Completion"))
    {
        return "Cannot complete task: " +
               "1. Due date must be in the past\n" +
               "2. Description must be provided\n" +
               "3. Other completion requirements not met";
    }
    return ex.Message;
}

private Task GetTaskById(int taskId)
{
    string query = "SELECT * FROM Tasks WHERE TaskId = @TaskId";
    using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TaskTrackerDB"].ConnectionString))
    using (var cmd = new SqlCommand(query, conn))
    {
        cmd.Parameters.AddWithValue("@TaskId", taskId);
        conn.Open();

        using (var reader = cmd.ExecuteReader())
        {
            if (reader.Read())
            {
                return new Task
                {
                    TaskId = (int)reader["TaskId"],
                    Title = reader["Title"].ToString(),
                    Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null,
                    DueDate = (DateTime)reader["DueDate"],
                    Priority = reader["Priority"].ToString(),
                    Status = reader["Status"].ToString(),
                    CategoryId = (int)reader["CategoryId"],
                    UserId = (int)reader["UserId"],
                    CreatedAt = (DateTime)reader["CreatedAt"],
                    CompletedAt = reader["CompletedAt"] != DBNull.Value ? (DateTime?)reader["CompletedAt"] : null
                };
            }
        }
    }
    return null;
}

public class Task
{
    public int TaskId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public string Priority { get; set; }
    public string Status { get; set; }
    public int CategoryId { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
}




protected void btnCancelUpdate_Click(object sender, EventArgs e)
{
    ClearForm();
    btnCreateTask.Visible = true;
    btnUpdateTask.Visible = false;
    btnCancelUpdate.Visible = false;
}


protected void btnUpdateTask_Click(object sender, EventArgs e)
{
    if (Page.IsValid)
    {
        try
        {
            int taskId = int.Parse(hdnTaskId.Value);
            var currentTask = GetTaskById(taskId);
            string newStatus = ddlStatus.SelectedValue;
            DateTime dueDate = DateTime.Parse(txtDueDate.Text);

            if (!IsValidStatusTransition(currentTask.Status, newStatus, dueDate))
            {
                return;
            }

            // Set completion timestamp only when completing
            DateTime? completedAt = newStatus == "Completed" ? (DateTime?)DateTime.Now : null;

            UpdateTaskInDatabase(taskId, completedAt);

            lblStatus.Text = "Task updated successfully!";
            lblStatus.CssClass = "alert alert-success";
            ClearForm();
            BindTaskRepeater();
            ResetFormButtons();
        }
        catch (FormatException)
        {
            lblStatus.Text = "Invalid date format. Please use YYYY-MM-DD.";
            lblStatus.CssClass = "alert alert-danger";
        }
        catch (SqlException sqlEx) when (sqlEx.Message.Contains("CHK_Completion"))
        {
            lblStatus.Text = "Cannot complete task. Please ensure:\n" +
                           "1. Due date is in the past\n" +
                           "2. Description is provided";
            lblStatus.CssClass = "alert alert-danger";
        }
        catch (Exception ex)
        {
            lblStatus.Text = $"Error: {ex.Message}";
            lblStatus.CssClass = "alert alert-danger";
        }
    }
}



private void ResetFormButtons()
{
    btnCreateTask.Visible = true;
    btnUpdateTask.Visible = false;
    btnCancelUpdate.Visible = false;
}

private bool IsValidStatusTransition(string currentStatus, string newStatus, DateTime dueDate)
{
    // Allow staying in same status
    if (currentStatus == newStatus)
    {
        return true;
    }

    // Special rules when transitioning TO Completed
    if (newStatus == "Completed")
    {
        if (dueDate > DateTime.Today)
        {
            lblStatus.Text = "Cannot complete task: Due date must be in the past";
            lblStatus.CssClass = "alert alert-danger";
            return false;
        }

        if (string.IsNullOrWhiteSpace(txtDescription.Text))
        {
            lblStatus.Text = "Cannot complete task: Description is required";
            lblStatus.CssClass = "alert alert-danger";
            return false;
        }
    }

    return true;
}



private Dictionary<string, List<string>> allowedTransitions = new Dictionary<string, List<string>>
{
    ["New"] = new List<string> { "In Progress", "Completed", "Archived" },
    ["In Progress"] = new List<string> { "New", "Completed", "Archived" },
    ["Completed"] = new List<string> { "New", "In Progress", "Archived" },
    ["Archived"] = new List<string> { "New", "In Progress", "Completed" }
};
    }
}


