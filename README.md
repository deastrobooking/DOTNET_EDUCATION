Here’s the extracted code (ready to paste) plus a super-quick build guide for the Ashland Soccer homework. Source: 

# Quick build guide (ASP.NET Web Forms)

1. **Create project & page**

* Project: `ashlandSoccer`
* Add folder: `App_Code`
* Add page: `Default.aspx` (Title: “Ashland Soccer”)
* Add controls to `Default.aspx`:

  * `Label` → `ID="lblErrorMessage"` (Text empty, `BorderStyle=None`)
  * `GridView` → `ID="gvDisplay"`
  * (for Part 2) `DropDownList` x3 → `ddField`, `ddTeam`, `ddStatus` (set `AutoPostBack="true"`)
  * (debug only) `ListBox` x3 → `lbField`, `lbTeam`, `lbStatus` (make `Visible="false"` before turning in)
  * (optional) `Button` → `btnSubmit` (you can remove once AutoPostBack is on)

2. **Bring in DB helper**

* Copy `myDatabaseConnection.cs` from your Homework 2/3 into `App_Code`. 

3. **Wire up Default.aspx.cs**
   Add namespaces:

```csharp
using System;
using System.Web.UI.WebControls;
```

Page_Load (Part 1 baseline “show SCHEDULE”):

```csharp
protected void Page_Load(object sender, EventArgs e)
{
    lblErrorMessage.Text = "";
    string sqlStatement;

    if (!IsPostBack)
    {
        // (Filled in Part 2)
    }

    // Start simple (Part 1)
    // sqlStatement = "SELECT * from schedule";
    // myDatabaseConnection.executeSQL(sqlStatement, ref gvDisplay, ref lblErrorMessage);

    // Progression of SELECTs (uncomment one at a time while learning)
    // sqlStatement = "SELECT Week, Time_Slot, Field, Home, Visitor, Status from SCHEDULE ORDER BY SCHEDULE.WEEK";
    // sqlStatement = "SELECT DATES.PLAY_DATE, Time_Slot, Field, Home, Visitor, Status from SCHEDULE, DATES WHERE DATES.WEEK = SCHEDULE.WEEK ORDER BY SCHEDULE.WEEK";
    // sqlStatement = "SELECT DATES.PLAY_DATE, GameTime.Game_Time, Field, Home, Visitor, Status from SCHEDULE, DATES, GameTime WHERE DATES.WEEK = SCHEDULE.WEEK AND GameTime.TIME_SLOT = SCHEDULE.TIME_SLOT ORDER BY SCHEDULE.WEEK";
    // sqlStatement = "SELECT DATES.PLAY_DATE, GAMETIME.GAME_TIME, FIELDS.FIELD_NAME, Home, Visitor, Status from SCHEDULE, DATES, GAMETIME, FIELDS WHERE DATES.WEEK = SCHEDULE.WEEK AND GAMETIME.TIME_SLOT = SCHEDULE.TIME_SLOT AND FIELDS.FIELD = SCHEDULE.FIELD ORDER BY SCHEDULE.WEEK";
    // sqlStatement = "SELECT DATES.PLAY_DATE, GAMETIME.GAME_TIME, FIELDS.FIELD_NAME, Home, Visitor, STATUS.FULL_STATUS from SCHEDULE, DATES, GAMETIME, FIELDS, STATUS WHERE DATES.WEEK = SCHEDULE.WEEK AND GAMETIME.TIME_SLOT = SCHEDULE.TIME_SLOT AND FIELDS.FIELD = SCHEDULE.FIELD AND STATUS.STATUS = SCHEDULE.STATUS";
    // sqlStatement = "SELECT DATES.PLAY_DATE, GAMETIME.GAME_TIME, FIELDS.FIELD_NAME, TEAMNAME.TEAM_NAME, Visitor, STATUS.FULL_STATUS from SCHEDULE, DATES, GAMETIME, FIELDS, STATUS, TEAMNAME WHERE DATES.WEEK = SCHEDULE.WEEK AND GAMETIME.TIME_SLOT = SCHEDULE.TIME_SLOT AND FIELDS.FIELD = SCHEDULE.FIELD AND STATUS.STATUS = SCHEDULE.STATUS AND TEAMNAME.TEAM_NO = SCHEDULE.HOME ORDER BY SCHEDULE.WEEK";
    // sqlStatement = "SELECT DATES.PLAY_DATE, GAMETIME.GAME_TIME, FIELDS.FIELD_NAME, TEAMNAME.TEAM_NAME, TEAMS_1.TEAM_NAME, STATUS.FULL_STATUS from SCHEDULE, DATES, GAMETIME, FIELDS, STATUS, TEAMNAME, TEAMNAME AS TEAMS_1 WHERE DATES.WEEK = SCHEDULE.WEEK AND GAMETIME.TIME_SLOT = SCHEDULE.TIME_SLOT AND FIELDS.FIELD = SCHEDULE.FIELD AND STATUS.STATUS = SCHEDULE.STATUS AND TEAMNAME.TEAM_NO = SCHEDULE.HOME AND TEAMS_1.TEAM_NO = SCHEDULE.VISITOR ORDER BY SCHEDULE.WEEK";

    // Clean, multi-line final SELECT (Part 1 end-state)
    sqlStatement  = "SELECT DATES.PLAY_DATE, GAMETIME.GAME_TIME, FIELDS.FIELD_NAME,";
    sqlStatement += " TEAMNAME.TEAM_NAME, TEAMS_1.TEAM_NAME, STATUS.FULL_STATUS";
    sqlStatement += " from SCHEDULE, DATES, GAMETIME, FIELDS, STATUS, TEAMNAME, TEAMNAME AS TEAMS_1";
    sqlStatement += " WHERE DATES.WEEK = SCHEDULE.WEEK";
    sqlStatement += " AND GAMETIME.TIME_SLOT = SCHEDULE.TIME_SLOT";
    sqlStatement += " AND FIELDS.FIELD = SCHEDULE.FIELD";
    sqlStatement += " AND STATUS.STATUS = SCHEDULE.STATUS";
    sqlStatement += " AND TEAMNAME.TEAM_NO = SCHEDULE.HOME";
    sqlStatement += " AND TEAMS_1.TEAM_NO = SCHEDULE.VISITOR";
    sqlStatement += " ORDER BY SCHEDULE.WEEK";

    myDatabaseConnection.executeSQL(sqlStatement, ref gvDisplay, ref lblErrorMessage);
}
```

(Progression and final query pulled directly from the assignment.) 

4. **Part 2: Dynamic dropdowns (DDD)**

Add these calls inside `Page_Load` under `if (!IsPostBack)` to populate menus:

```csharp
if (!IsPostBack)
{
    myDatabaseConnection.fillDropDownList(ddTeam,  lbTeam,  "TEAMNAME", "TEAM_NAME",   "TEAM_NO", ref lblErrorMessage);
    myDatabaseConnection.fillDropDownList(ddField,  lbField, "FIELDS",   "FIELD_NAME",  "FIELD",    ref lblErrorMessage);
    myDatabaseConnection.fillDropDownList(ddStatus, lbStatus,"STATUS",   "FULL_STATUS", "STATUS",   ref lblErrorMessage);
}
```



**Handler to rebuild query on change**
(If you keep a Submit button, put this in `btnSubmit_Click`; otherwise rely on AutoPostBack of the three DDLs and reuse this body from their `SelectedIndexChanged` events.)

```csharp
protected void RequeryAndBind()
{
    lblErrorMessage.Text = "";
    string sqlStatement = "";
    sqlStatement  = "SELECT DATES.PLAY_DATE, GAMETIME.GAME_TIME, FIELDS.FIELD_NAME,";
    sqlStatement += " TEAMNAME.TEAM_NAME, TEAMS_1.TEAM_NAME, STATUS.FULL_STATUS";
    sqlStatement += " from SCHEDULE, DATES, GAMETIME, FIELDS, STATUS, TEAMNAME, TEAMNAME AS TEAMS_1";
    sqlStatement += " WHERE DATES.WEEK = SCHEDULE.WEEK";
    sqlStatement += " AND GAMETIME.TIME_SLOT = SCHEDULE.TIME_SLOT";
    sqlStatement += " AND FIELDS.FIELD = SCHEDULE.FIELD";
    sqlStatement += " AND STATUS.STATUS = SCHEDULE.STATUS";
    sqlStatement += " AND TEAMNAME.TEAM_NO = SCHEDULE.HOME";
    sqlStatement += " AND TEAMS_1.TEAM_NO = SCHEDULE.VISITOR";

    // Status filter
    if (ddStatus.SelectedIndex > 0)
    {
        sqlStatement += " AND SCHEDULE.STATUS = '" + lbStatus.Items[ddStatus.SelectedIndex - 1] + "'";
    }
    // Field filter
    if (ddField.SelectedIndex > 0)
    {
        sqlStatement += " AND SCHEDULE.FIELD = " + lbField.Items[ddField.SelectedIndex - 1];
    }
    // Team filter (either home or visitor)
    if (ddTeam.SelectedIndex > 0)
    {
        sqlStatement += " AND (SCHEDULE.HOME = " + lbTeam.Items[ddTeam.SelectedIndex - 1] +
                        " OR SCHEDULE.VISITOR = " + lbTeam.Items[ddTeam.SelectedIndex - 1] + ")";
    }

    sqlStatement += " ORDER BY SCHEDULE.WEEK";
    myDatabaseConnection.executeSQL(sqlStatement, ref gvDisplay, ref lblErrorMessage);
}
```

Then wire:

```csharp
protected void ddTeam_SelectedIndexChanged(object sender, EventArgs e)  => RequeryAndBind();
protected void ddField_SelectedIndexChanged(object sender, EventArgs e) => RequeryAndBind();
protected void ddStatus_SelectedIndexChanged(object sender, EventArgs e)=> RequeryAndBind();

// If using a button instead of AutoPostBack:
// protected void btnSubmit_Click(object sender, EventArgs e) => RequeryAndBind();
```

(Exact filter expressions and placement follow the assignment.) 

5. **Implement `fillDropDownList` in `myDatabaseConnection.cs`**

Add at top of file:

```csharp
using System.Data;
using System.Data.SqlClient;
```

Method signature and body:

```csharp
public static void fillDropDownList(
    DropDownList dropdownId,
    ListBox listboxId,
    string tableName,
    string field,
    string index,
    ref Label lblErrorMessage)
{
    dropdownId.Items.Clear();
    listboxId.Items.Clear();

    dropdownId.Items.Add("*");

    DataRow dr;
    DataTable dt = new DataTable();
    string sqlCommand = "SELECT " + field + ", " + index + " FROM " + tableName + " ORDER BY " + field;

    try
    {
        myConnection.Open(); // assumes same connection object as in executeSQL
    }
    catch (Exception ex)
    {
        lblErrorMessage.Text = ex.ToString();
    }

    try
    {
        SqlDataAdapter da = new SqlDataAdapter(sqlCommand, myConnection);
        da.Fill(dt);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dr = dt.Rows[i];
            dropdownId.Items.Add(dr[field].ToString());
            listboxId.Items.Add(dr[index].ToString());
        }
        dropdownId.SelectedIndex = 0;
    }
    catch (Exception ex)
    {
        lblErrorMessage.Text = ex.ToString();
    }
    finally
    {
        try { myConnection.Close(); } catch { /* ignore */ }
    }
}
```

(Structure and logic mirror the assignment’s walk-through.) 

---

# SQL snippets (copied from the assignment)

* Base:

```sql
SELECT * FROM SCHEDULE;
```

* Ordered minimal set:

```sql
SELECT Week, Time_Slot, Field, Home, Visitor, Status
FROM SCHEDULE
ORDER BY SCHEDULE.WEEK;
```

* Add dates:

```sql
SELECT DATES.PLAY_DATE, Time_Slot, Field, Home, Visitor, Status
FROM SCHEDULE, DATES
WHERE DATES.WEEK = SCHEDULE.WEEK
ORDER BY SCHEDULE.WEEK;
```

* Add game time:

```sql
SELECT DATES.PLAY_DATE, GameTime.Game_Time, Field, Home, Visitor, Status
FROM SCHEDULE, DATES, GameTime
WHERE DATES.WEEK = SCHEDULE.WEEK
  AND GameTime.TIME_SLOT = SCHEDULE.TIME_SLOT
ORDER BY SCHEDULE.WEEK;
```

* Add field names:

```sql
SELECT DATES.PLAY_DATE, GAMETIME.GAME_TIME, FIELDS.FIELD_NAME, Home, Visitor, Status
FROM SCHEDULE, DATES, GAMETIME, FIELDS
WHERE DATES.WEEK = SCHEDULE.WEEK
  AND GAMETIME.TIME_SLOT = SCHEDULE.TIME_SLOT
  AND FIELDS.FIELD = SCHEDULE.FIELD
ORDER BY SCHEDULE.WEEK;
```

* Add full status:

```sql
SELECT DATES.PLAY_DATE, GAMETIME.GAME_TIME, FIELDS.FIELD_NAME, Home, Visitor, STATUS.FULL_STATUS
FROM SCHEDULE, DATES, GAMETIME, FIELDS, STATUS
WHERE DATES.WEEK = SCHEDULE.WEEK
  AND GAMETIME.TIME_SLOT = SCHEDULE.TIME_SLOT
  AND FIELDS.FIELD = SCHEDULE.FIELD
  AND STATUS.STATUS = SCHEDULE.STATUS;
```

* Add home team name:

```sql
SELECT DATES.PLAY_DATE, GAMETIME.GAME_TIME, FIELDS.FIELD_NAME,
       TEAMNAME.TEAM_NAME, Visitor, STATUS.FULL_STATUS
FROM SCHEDULE, DATES, GAMETIME, FIELDS, STATUS, TEAMNAME
WHERE DATES.WEEK = SCHEDULE.WEEK
  AND GAMETIME.TIME_SLOT = SCHEDULE.TIME_SLOT
  AND FIELDS.FIELD = SCHEDULE.FIELD
  AND STATUS.STATUS = SCHEDULE.STATUS
  AND TEAMNAME.TEAM_NO = SCHEDULE.HOME
ORDER BY SCHEDULE.WEEK;
```

* Add visitor team with alias:

```sql
SELECT DATES.PLAY_DATE, GAMETIME.GAME_TIME, FIELDS.FIELD_NAME,
       TEAMNAME.TEAM_NAME, TEAMS_1.TEAM_NAME, STATUS.FULL_STATUS
FROM SCHEDULE, DATES, GAMETIME, FIELDS, STATUS, TEAMNAME, TEAMNAME AS TEAMS_1
WHERE DATES.WEEK = SCHEDULE.WEEK
  AND GAMETIME.TIME_SLOT = SCHEDULE.TIME_SLOT
  AND FIELDS.FIELD = SCHEDULE.FIELD
  AND STATUS.STATUS = SCHEDULE.STATUS
  AND TEAMNAME.TEAM_NO = SCHEDULE.HOME
  AND TEAMS_1.TEAM_NO = SCHEDULE.VISITOR
ORDER BY SCHEDULE.WEEK;
```

(All SQL pulled verbatim from the assignment.) 

---

# Turn-in checklist (from the brief)

* Everything works with dropdown filters.
* Add a soccer banner; label each dropdown (“Field”, “Team Name”, “Status”).
* Remove `Response.Write` cruft and extra `using` statements.
* Hide the 3 list boxes (`Visible="false"`).
* Replace `App_Code/myDatabaseConnection.cs` on the server with your new one (keep a backup).
* Homework 3 should still work unchanged. 

If you want, I can package this into a `.md` quick-start with all snippets ready to paste into VS—just say the word.
