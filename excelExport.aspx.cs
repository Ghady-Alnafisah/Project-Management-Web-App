using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Data.SqlClient; 

namespace TaskTrackerAUTH
{
    public partial class excelExport : System.Web.UI.Page
    {
        private static DataTable taskTable;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTasks();
                BindGrid();
            }
        }

     

private void LoadTasks()
    {
        taskTable = new DataTable();

        string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["TaskTrackerDB"].ConnectionString;

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = "SELECT * FROM TASKS";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(taskTable);
            }
        }
    }


    private void BindGrid()
        {
            DataView dv = taskTable.DefaultView;

            switch (ddlSort.SelectedValue)
            {
                case "TitleAsc": dv.Sort = "Title ASC"; break;
                case "TitleDesc": dv.Sort = "Title DESC"; break;
                case "DueDateAsc": dv.Sort = "DueDate ASC"; break;
                case "DueDateDesc": dv.Sort = "DueDate DESC"; break;
                case "CreatedAsc": dv.Sort = "CreatedDate ASC"; break;
                case "CreatedDesc": dv.Sort = "CreatedDate DESC"; break;
            }

            gvTasks.DataSource = dv;
            gvTasks.DataBind();
        }

        protected void ddlSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            BindGrid(); // Ensure GridView is populated
            ExportGridToExcel(gvTasks);
        }

        private void ExportGridToExcel(GridView gv)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Tasks.xls");
            Response.ContentType = "application/vnd.ms-excel";
            Response.Charset = "";

            using (StringWriter sw = new StringWriter())
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                gv.RenderControl(hw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            // Required for exporting GridView
        }
    }
}