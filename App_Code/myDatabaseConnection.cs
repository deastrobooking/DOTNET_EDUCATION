using System;
using System.Data;
using System.Data.SqlClient;
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

    /// <summary>
    /// Fills a DropDownList with data from a database table, with a corresponding ListBox for index values
    /// </summary>
    /// <param name="dropdownId">The DropDownList control to populate</param>
    /// <param name="listboxId">The ListBox control to store corresponding index values</param>
    /// <param name="tableName">The database table name</param>
    /// <param name="field">The field name to display in the dropdown</param>
    /// <param name="index">The field name to store as the index value</param>
    /// <param name="lblErrorMessage">Label control for error messages</param>
    public static void fillDropDownList(DropDownList dropdownId, ListBox listboxId, string tableName, 
   string field, string index, ref Label lblErrorMessage)
    {
    // Add the "*" option to show all records
        dropdownId.Items.Add("*");
        
        DataRow dr;
        DataTable dt = new DataTable();
  string sqlCommand = "SELECT " + field + ", " + index + " FROM " + tableName + " ORDER BY " + field;

        try
        {
    myConnection.Open();

            try
            {
       SqlDataAdapter da = new SqlDataAdapter(sqlCommand, myConnection);
    da.Fill(dt);

         // Populate both the dropdown (display values) and listbox (index values)
              for (int i = 0; i < dt.Rows.Count; i++)
          {
    dr = dt.Rows[i];
   dropdownId.Items.Add(dr[field].ToString().Trim());
    listboxId.Items.Add(dr[index].ToString().Trim());
     }
         
        // Set default selection to "*" (show all)
             dropdownId.SelectedIndex = 0;
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
        finally
        {
            try
            {
myConnection.Close();
       }
            catch (Exception ex)
       {
    lblErrorMessage.Text += ex.ToString();
            }
        }
    }
}