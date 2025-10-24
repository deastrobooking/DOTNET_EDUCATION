using System;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblErrorMessage.Text = "";
        string sqlStatement;

        // Populate dropdowns only on first load (not on postback)
        if (!IsPostBack)
      {
            myDatabaseConnection.fillDropDownList(ddTeam, lbTeam, "TEAMNAME", "TEAM_NAME", "TEAM_NO", ref lblErrorMessage);
 myDatabaseConnection.fillDropDownList(ddField, lbField, "FIELDS", "FIELD_NAME", "FIELD", ref lblErrorMessage);
   myDatabaseConnection.fillDropDownList(ddStatus, lbStatus, "STATUS", "FULL_STATUS", "STATUS", ref lblErrorMessage);
}

  // Build the base SQL query with all joins
        sqlStatement = "SELECT DATES.PLAY_DATE, GAMETIME.GAME_TIME, FIELDS.FIELD_NAME,";
        sqlStatement += " TEAMNAME.TEAM_NAME, TEAMS_1.TEAM_NAME, STATUS.FULL_STATUS";
     sqlStatement += " FROM SCHEDULE, DATES, GAMETIME, FIELDS, STATUS, TEAMNAME, TEAMNAME AS TEAMS_1";
      sqlStatement += " WHERE DATES.WEEK = SCHEDULE.WEEK";
        sqlStatement += " AND GAMETIME.TIME_SLOT = SCHEDULE.TIME_SLOT";
     sqlStatement += " AND FIELDS.FIELD = SCHEDULE.FIELD";
        sqlStatement += " AND STATUS.STATUS = SCHEDULE.STATUS";
        sqlStatement += " AND TEAMNAME.TEAM_NO = SCHEDULE.HOME";
        sqlStatement += " AND TEAMS_1.TEAM_NO = SCHEDULE.VISITOR";

     // Apply filters based on dropdown selections (skip index 0 which is "*")
      if (ddStatus.SelectedIndex > 0)
        {
    sqlStatement += " AND SCHEDULE.STATUS = '" + lbStatus.Items[ddStatus.SelectedIndex - 1].Text + "'";
    }

     if (ddField.SelectedIndex > 0)
  {
            sqlStatement += " AND SCHEDULE.FIELD = " + lbField.Items[ddField.SelectedIndex - 1].Text;
   }

        if (ddTeam.SelectedIndex > 0)
        {
       sqlStatement += " AND (SCHEDULE.HOME = " + lbTeam.Items[ddTeam.SelectedIndex - 1].Text +
   " OR SCHEDULE.VISITOR = " + lbTeam.Items[ddTeam.SelectedIndex - 1].Text + ")";
}

     // Order by week
        sqlStatement += " ORDER BY SCHEDULE.WEEK";

   // Execute the query
        myDatabaseConnection.executeSQL(sqlStatement, ref gvDisplay, ref lblErrorMessage);
        
        // Show error message only if there's actual error content
     if (!string.IsNullOrEmpty(lblErrorMessage.Text))
        {
    lblErrorMessage.Visible = true;
        }
    }
}