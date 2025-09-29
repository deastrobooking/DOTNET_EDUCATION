using System;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class _Default : System.Web.UI.Page
{
    readonly string userID = "chabotr564_";

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check for user authentication cookie - redirect to login if not found
        HttpCookie cookie = Request.Cookies["userInfo"];
        if (cookie == null)
        {
            Response.Redirect("LogIn.aspx");
            return;
        }

        // Handle reset button click (logout functionality)
        if (IsPostBack)
        {
            // Expire the cookie and redirect to login
            cookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(cookie);
            Response.Redirect("LogIn.aspx");
            return;
        }

        // Display welcome message with user information
        if (cookie != null)
        {
            // Bonus: Parse cookie values and display user email
            // Response.Write(cookie.Value + "<br />"); // For debugging
            string[] rawInput = cookie.Value.Split('&');
            if (rawInput.Length > 0)
            {
                string[] eMail = rawInput[0].Split('=');
                if (eMail.Length > 1)
                {
                    lblWelcomeBanner.Text = "Welcome! If " + eMail[1] + " is not your email address, please click on Reset";
                }
            }
        }

        if (!IsPostBack)
        {
            string filePath = Server.MapPath("~/App_Data");
            string file1 = filePath + "\\course.dat";
            string file2 = filePath + "\\student.dat";
            string file3 = filePath + "\\faculty.dat";
            string file4 = filePath + "\\location.dat";
            string file5 = filePath + "\\frank.dat";
            string file6 = filePath + "\\term.dat";
            string file7 = filePath + "\\course_section.dat";
            string file8 = filePath + "\\enrollment.dat";
            string file9 = filePath + "\\state.dat";

            readData(file1, lstCourse);
            readData(file2, lstStudent);
            readData(file3, lstFaculty);
            readData(file4, lstLocation);
            readData(file5, lstFrank);
            readData(file6, lstTerm);
            readData(file7, lstCourse_Section);
            readData(file8, lstEnrollment);
            readData(file9, lstState);

            // Initial state detection for all tables
            checkForTables("Course");
            checkForTables("Student");
            checkForTables("Faculty");
            checkForTables("Location");
            checkForTables("Frank");
            checkForTables("Term");
            checkForTables("Course_Section");
            checkForTables("Enrollment");
            checkForTables("State");
        }
        
        // Reset table name label outside IsPostBack
        lblTableName.Text = "";
    }

    private void readData(string fileName, ListBox listBoxName)
    {
        try
        {
            if (File.Exists(fileName))
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    while (sr.Peek() >= 0)
                    {
                        string line = sr.ReadLine();
                        listBoxName.Items.Add(line);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = "Error reading file " + fileName + ": " + ex.Message;
        }
    }

    // Helper: Change color and enabled state for a single button
    private void changeColor(Button button, int flag)
    {
        if (button == null) return;
        switch (flag)
        {
            case 0:
                button.BackColor = Color.White;
                button.Enabled = false;
                break;
            case 1:
                button.BackColor = Color.SkyBlue;
                button.Enabled = true;
                break;
        }
    }

    // Helper: Batch color/state for 4 related buttons
    protected void changeColor(string rootID, int bCFlag, int bLFlag, int bVFlag, int bDFlag)
    {
        string bC = "btnCreate";
        string bL = "btnLoad";
        string bV = "btnView";
        string bD = "btnDelete";
        Button button;

        bC += rootID;
        bL += rootID;
        bV += rootID;
        bD += rootID;

        button = (Button)FindControl(bC);
        changeColor(button, bCFlag);

        button = (Button)FindControl(bL);
        changeColor(button, bLFlag);

        button = (Button)FindControl(bV);
        changeColor(button, bVFlag);

        button = (Button)FindControl(bD);
        changeColor(button, bDFlag);
    }

    // Helper: Create table and set button states
    private void createTable(string tableName, string sqlCommand)
    {
        myDatabaseConnection.executeSQL(sqlCommand, ref gvDisplay, ref lblErrorMessage);
        changeColor(tableName, 0, 1, 0, 1);
        checkForError();
    }

    // Helper: View table
    private void viewTable(string tableName)
    {
        string sqlCommand = "SELECT * FROM " + userID + tableName;
        myDatabaseConnection.executeSQL(sqlCommand, ref gvDisplay, ref lblErrorMessage);
        lblTableName.Text = tableName;
    }

    // Helper: Drop table and set button states
    private void dropTable(string tableName)
    {
        string sqlCommand = "DROP TABLE " + userID + tableName;
        myDatabaseConnection.executeSQL(sqlCommand, ref gvDisplay, ref lblErrorMessage);
        changeColor(tableName, 1, 0, 0, 0);
        checkForError();
    }

    // Helper: Load data from ListBox into table
    private void createSqlInsertStatements(string listboxID, string tableName)
    {
        ListBox listBox = (ListBox)FindControl(listboxID);
        if (listBox == null)
        {
            lblErrorMessage.Text = "Error: Could not find listbox with ID: " + listboxID;
            return;
        }

        for (int index = 0; index < listBox.Items.Count; index++)
        {
            string[] fields = listBox.Items[index].Text.Split(',');
            string sqlInserts = "INSERT INTO " + userID + tableName + " VALUES (";

            for (int fieldIndex = 0; fieldIndex < fields.Length; fieldIndex++)
            {
                string currentField = fields[fieldIndex].Trim();
                int tempResult;
                
                if (Int32.TryParse(currentField, out tempResult))
                {
                    sqlInserts += currentField;
                }
                else
                {
                    string value = currentField.Replace("'", "''");
                    sqlInserts += "'" + value + "'";
                }

                if (fieldIndex < fields.Length - 1)
                    sqlInserts += ", ";
                else
                    sqlInserts += ")";
            }

            myDatabaseConnection.executeSQL(sqlInserts, ref gvDisplay, ref lblErrorMessage);
            
            if (lblErrorMessage.Text != "" && !lblErrorMessage.Text.Contains("Command Complete"))
                break;
        }
        
        // After load: View/Delete on
        changeColor(tableName, 0, 0, 1, 1);
        checkForError();
    }

    // Helper: Initial state detection for table
    private void checkForTables(string tableName)
    {
        string sqlCommand = "SELECT * FROM " + userID + tableName;
        myDatabaseConnection.executeSQL(sqlCommand, ref gvDisplay, ref lblErrorMessage);
        
        // Hide feedback during detection
        lblErrorMessage.Visible = false;
        gvDisplay.Visible = false;

        int status;
        if (lblErrorMessage.Text != "")          // error ? no table
            status = 0;
        else if (gvDisplay.Rows.Count == 0)      // empty table
            status = 1;
        else                                     // data exists
            status = 2;

        switch (status)
        {
            case 0: // Create on
                changeColor(tableName, 1, 0, 0, 0);
                break;
            case 1: // Load/Delete on
                changeColor(tableName, 0, 1, 0, 1);
                break;
            case 2: // View/Delete on
                changeColor(tableName, 0, 0, 1, 1);
                break;
        }
        
        // Reset visibility
        lblErrorMessage.Visible = true;
        gvDisplay.Visible = true;
        lblErrorMessage.Text = "";
    }

    // Helper: Show "Command Complete" if no error
    private void checkForError()
    {
        if (lblErrorMessage.Text == "")
            lblErrorMessage.Text = "Command Complete";
    }

    // ===== MINIMAL BUTTON HANDLERS =====

    // Course Operations
    protected void btnCreateCourse_Click(object sender, EventArgs e)
    {
        createTable("Course", "CREATE TABLE " + userID + "COURSE(COURSEID INTEGER UNIQUE, CALL_ID CHAR(8), COURSE_NAME CHAR(30), CREDITS INTEGER)");
    }

    protected void btnLoadCourse_Click(object sender, EventArgs e)
    {
        createSqlInsertStatements(lstCourse.ID, "COURSE");
    }

    protected void btnViewCourse_Click(object sender, EventArgs e)
    {
        viewTable("COURSE");
    }

    protected void btnDeleteCourse_Click(object sender, EventArgs e)
    {
        dropTable("COURSE");
    }

    // Student Operations
    protected void btnCreateStudent_Click(object sender, EventArgs e)
    {
        createTable("Student", "CREATE TABLE " + userID + "STUDENT (SID INTEGER UNIQUE, SLNAME CHAR(15), SFNAME CHAR(15), SMI CHAR(2), SADD CHAR(30), SCITY CHAR(20), SSTATE CHAR(2), SZIP CHAR(10), SPHONE CHAR(14), SDOB DATE, SCLASS CHAR(2), SPIN INTEGER, FID INTEGER)");
    }

    protected void btnLoadStudent_Click(object sender, EventArgs e)
    {
        createSqlInsertStatements(lstStudent.ID, "STUDENT");
    }

    protected void btnViewStudent_Click(object sender, EventArgs e)
    {
        viewTable("STUDENT");
    }

    protected void btnDeleteStudent_Click(object sender, EventArgs e)
    {
        dropTable("STUDENT");
    }

    // Faculty Operations
    protected void btnCreateFaculty_Click(object sender, EventArgs e)
    {
        createTable("Faculty", "CREATE TABLE " + userID + "FACULTY (FID INTEGER UNIQUE, FLNAME CHAR(15), FFNAME CHAR(15), FMI CHAR(2), LOCID INTEGER, FPHONE CHAR(14), FRANK CHAR(5), FPIN INTEGER, FIMAGE CHAR(20))");
    }

    protected void btnLoadFaculty_Click(object sender, EventArgs e)
    {
        createSqlInsertStatements(lstFaculty.ID, "FACULTY");
    }

    protected void btnViewFaculty_Click(object sender, EventArgs e)
    {
        viewTable("FACULTY");
    }

    protected void btnDeleteFaculty_Click(object sender, EventArgs e)
    {
        dropTable("FACULTY");
    }

    // Location Operations
    protected void btnCreateLocation_Click(object sender, EventArgs e)
    {
        createTable("Location", "CREATE TABLE " + userID + "LOCATION(LOCID INTEGER UNIQUE, BLDG_CODE CHAR(4), ROOM CHAR(4), CAPACITY INTEGER)");
    }

    protected void btnLoadLocation_Click(object sender, EventArgs e)
    {
        createSqlInsertStatements(lstLocation.ID, "LOCATION");
    }

    protected void btnViewLocation_Click(object sender, EventArgs e)
    {
        viewTable("LOCATION");
    }

    protected void btnDeleteLocation_Click(object sender, EventArgs e)
    {
        dropTable("LOCATION");
    }

    // Frank Operations
    protected void btnCreateFrank_Click(object sender, EventArgs e)
    {
        createTable("Frank", "CREATE TABLE " + userID + "FRANK (FRANK CHAR(5) UNIQUE, FRANKDESC CHAR(15))");
    }

    protected void btnLoadFrank_Click(object sender, EventArgs e)
    {
        createSqlInsertStatements(lstFrank.ID, "FRANK");
    }

    protected void btnViewFrank_Click(object sender, EventArgs e)
    {
        viewTable("FRANK");
    }

    protected void btnDeleteFrank_Click(object sender, EventArgs e)
    {
        dropTable("FRANK");
    }

    // Term Operations
    protected void btnCreateTerm_Click(object sender, EventArgs e)
    {
        createTable("Term", "CREATE TABLE " + userID + "TERM(TERMID INTEGER UNIQUE, TERM_DESC CHAR(12), STATUS CHAR(7))");
    }

    protected void btnLoadTerm_Click(object sender, EventArgs e)
    {
        createSqlInsertStatements(lstTerm.ID, "TERM");
    }

    protected void btnViewTerm_Click(object sender, EventArgs e)
    {
        viewTable("TERM");
    }

    protected void btnDeleteTerm_Click(object sender, EventArgs e)
    {
        dropTable("TERM");
    }

    // Course_Section Operations
    protected void btnCreateCourse_Section_Click(object sender, EventArgs e)
    {
        createTable("Course_Section", "CREATE TABLE " + userID + "COURSE_SECTION(CSEC_ID INTEGER UNIQUE, COURSEID INTEGER, TERMID INTEGER, SEC_NUM INTEGER, FID INTEGER, CSEC_DAY CHAR(6), CSEC_TIME DATE, LOCID INTEGER, MAX_ENRL INTEGER)");
    }

    protected void btnLoadCourse_Section_Click(object sender, EventArgs e)
    {
        createSqlInsertStatements(lstCourse_Section.ID, "COURSE_SECTION");
    }

    protected void btnViewCourse_Section_Click(object sender, EventArgs e)
    {
        viewTable("COURSE_SECTION");
    }

    protected void btnDeleteCourse_Section_Click(object sender, EventArgs e)
    {
        dropTable("COURSE_SECTION");
    }

    // Enrollment Operations
    protected void btnCreateEnrollment_Click(object sender, EventArgs e)
    {
        createTable("Enrollment", "CREATE TABLE " + userID + "ENROLLMENT (SID INTEGER, CSEC_ID INTEGER, GRADE CHAR(2), CONSTRAINT " + userID + "ENROLLMENT_UN UNIQUE (SID, CSEC_ID))");
    }

    protected void btnLoadEnrollment_Click(object sender, EventArgs e)
    {
        createSqlInsertStatements(lstEnrollment.ID, "ENROLLMENT");
    }

    protected void btnViewEnrollment_Click(object sender, EventArgs e)
    {
        viewTable("ENROLLMENT");
    }

    protected void btnDeleteEnrollment_Click(object sender, EventArgs e)
    {
        dropTable("ENROLLMENT");
    }

    // State Operations
    protected void btnCreateState_Click(object sender, EventArgs e)
    {
        createTable("State", "CREATE TABLE " + userID + "STATE (S_ABBREVIATION CHAR(3) UNIQUE, S_NAME CHAR(100))");
    }

    protected void btnLoadState_Click(object sender, EventArgs e)
    {
        createSqlInsertStatements(lstState.ID, "STATE");
    }

    protected void btnViewState_Click(object sender, EventArgs e)
    {
        viewTable("STATE");
    }

    protected void btnDeleteState_Click(object sender, EventArgs e)
    {
        dropTable("STATE");
    }
}