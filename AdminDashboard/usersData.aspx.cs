using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TasksTrackerWebApp
{
    public partial class usersData : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            populateUsersList();
        }

        protected void populateUsersList()
        {
            CRUD myCrud = new CRUD();
            String sqlCMDforUsersList = @"select * from users";

            SqlDataReader dr = myCrud.getDrPassSql(sqlCMDforUsersList);
           
            usersGrid.DataSource = dr;
            usersGrid.DataBind();
        }
        protected void usersGrid_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}