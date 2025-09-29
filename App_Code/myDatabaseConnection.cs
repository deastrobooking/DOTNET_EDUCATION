using System;
using System.Data.SqlClient;
//using System.Linq
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for myDatabaseConnection
/// University DB Management System
/// Software uses C# and ASP.NET to connect to a 3rd-tier SQL Database 
/// And perform basic SQL commands and operations on the database through a web interface. 
/// </summary>
public class myDatabaseConnection
{
    public myDatabaseConnection()
    {
        // TODO: Add constructor logic here
    }

    // Provided 3rd-tier connection string
    public static SqlConnection myConnection = new SqlConnection(
        "Data Source=SQL5025.myWindowsHosting.com;" +
        "Initial Catalog=DB_A28DC6_mccSupport;" +
        "User Id=DB_A28DC6_mccSupport_admin;" +
        "Password=passw0rd;");

    // Reusable executor
    public static void executeSQL(string sqlCommand, ref GridView gvDisplay, ref Label lblErrorMessage)
    {
        gvDisplay.Visible = false;
        lblErrorMessage.Text = "";

        try
        {
            myConnection.Open();
            SqlCommand myCommand = new SqlCommand(sqlCommand, myConnection);

            try
            {
                // NOTE: SELECT must be UPPERCASE to be detected
                if (sqlCommand.StartsWith("SELECT"))
                {
                    gvDisplay.DataSource = myCommand.ExecuteReader();
                    gvDisplay.DataBind();
                    gvDisplay.Visible = true;
                }
                else
                {
                    myCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.ToString();
            }
        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = ex.ToString();
        }

        try
        {
            myConnection.Close();
        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = ex.ToString();
        }
    }
}