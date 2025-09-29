<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Hocus Pocus Set the Focus</title>
    <style>
        .container { margin: 20px; }
        .section { margin: 15px 0; border: 1px solid #ccc; padding: 10px; }
        .error { color: red; font-weight: bold; }
        .success { color: green; font-weight: bold; }
        .hidden { display: none; }
        .welcome-banner { 
            background-color: #f0f8ff; 
            padding: 15px; 
            border: 2px solid #4CAF50; 
            margin-bottom: 20px;
            font-size: 18px;
            font-weight: bold;
            text-align: center;
        }
        .reset-section {
            text-align: center;
            margin: 20px 0;
            padding: 15px;
            background-color: #ffe4e1;
            border: 1px solid #ff6b6b;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        
        <!-- Welcome Banner -->
        <div class="welcome-banner">
            <asp:Label ID="lblWelcomeBanner" runat="server" Text="Welcome to the University Database Management System!" />
        </div>
        
        <!-- Reset Cookie Section -->
        <div class="reset-section">
            <asp:Button ID="btnReset" runat="server" Text="Reset Login" BackColor="#ff6b6b" ForeColor="White" />
            <br />
            <small>Click to logout and return to login page</small>
        </div>
        
        <h2>ITWP 2300 Homework 4 - Data Validation, Cookies, and Page Redirection</h2>
        
        <!-- Error/Success Message Display -->
        <asp:Label ID="lblErrorMessage" runat="server" CssClass="error" EnableViewState="false" Text="" />
        <br />
        
        <!-- Database Title Label for Extra Credit -->
        <asp:Label ID="lblTableName" runat="server" Text="" Font-Bold="true" />
        <br />
        
        <!-- GridView for displaying query results -->
        <asp:GridView ID="gvDisplay" runat="server" Visible="false" 
                      CssClass="gridview" AutoGenerateColumns="true" 
                      HeaderStyle-BackColor="#507CD1" HeaderStyle-ForeColor="White" />
        <hr />
        
        <div class="section">
            <h3>Course Operations</h3>
            <asp:Button ID="btnCreateCourse" runat="server" Text="Create Course" OnClick="btnCreateCourse_Click" />
            <asp:Button ID="btnLoadCourse" runat="server" Text="Load Course" OnClick="btnLoadCourse_Click" />
            <asp:Button ID="btnViewCourse" runat="server" Text="View Course" OnClick="btnViewCourse_Click" />
            <asp:Button ID="btnDeleteCourse" runat="server" Text="Delete Course" OnClick="btnDeleteCourse_Click" />
            <br />
            <asp:ListBox ID="lstCourse" runat="server" Width="400px" Height="100px" Visible="false"></asp:ListBox>
        </div>
        
        <div class="section">
            <h3>Student Operations</h3>
            <asp:Button ID="btnCreateStudent" runat="server" Text="Create Student" OnClick="btnCreateStudent_Click" />
            <asp:Button ID="btnLoadStudent" runat="server" Text="Load Student" OnClick="btnLoadStudent_Click" />
            <asp:Button ID="btnViewStudent" runat="server" Text="View Student" OnClick="btnViewStudent_Click" />
            <asp:Button ID="btnDeleteStudent" runat="server" Text="Delete Student" OnClick="btnDeleteStudent_Click" />
            <br />
            <asp:ListBox ID="lstStudent" runat="server" Width="400px" Height="100px" Visible="false"></asp:ListBox>
        </div>
        
        <div class="section">
            <h3>Faculty Operations</h3>
            <asp:Button ID="btnCreateFaculty" runat="server" Text="Create Faculty" OnClick="btnCreateFaculty_Click" />
            <asp:Button ID="btnLoadFaculty" runat="server" Text="Load Faculty" OnClick="btnLoadFaculty_Click" />
            <asp:Button ID="btnViewFaculty" runat="server" Text="View Faculty" OnClick="btnViewFaculty_Click" />
            <asp:Button ID="btnDeleteFaculty" runat="server" Text="Delete Faculty" OnClick="btnDeleteFaculty_Click" />
            <br />
            <asp:ListBox ID="lstFaculty" runat="server" Width="400px" Height="100px" Visible="false"></asp:ListBox>
        </div>
        
        <div class="section">
            <h3>Location Operations</h3>
            <asp:Button ID="btnCreateLocation" runat="server" Text="Create Location" OnClick="btnCreateLocation_Click" />
            <asp:Button ID="btnLoadLocation" runat="server" Text="Load Location" OnClick="btnLoadLocation_Click" />
            <asp:Button ID="btnViewLocation" runat="server" Text="View Location" OnClick="btnViewLocation_Click" />
            <asp:Button ID="btnDeleteLocation" runat="server" Text="Delete Location" OnClick="btnDeleteLocation_Click" />
            <br />
            <asp:ListBox ID="lstLocation" runat="server" Width="400px" Height="100px" Visible="false"></asp:ListBox>
        </div>
        
        <div class="section">
            <h3>Frank Operations</h3>
            <asp:Button ID="btnCreateFrank" runat="server" Text="Create Frank" OnClick="btnCreateFrank_Click" />
            <asp:Button ID="btnLoadFrank" runat="server" Text="Load Frank" OnClick="btnLoadFrank_Click" />
            <asp:Button ID="btnViewFrank" runat="server" Text="View Frank" OnClick="btnViewFrank_Click" />
            <asp:Button ID="btnDeleteFrank" runat="server" Text="Delete Frank" OnClick="btnDeleteFrank_Click" />
            <br />
            <asp:ListBox ID="lstFrank" runat="server" Width="400px" Height="100px" Visible="false"></asp:ListBox>
        </div>

        <div class="section">
            <h3>Term Operations</h3>
            <asp:Button ID="btnCreateTerm" runat="server" Text="Create Term" OnClick="btnCreateTerm_Click" />
            <asp:Button ID="btnLoadTerm" runat="server" Text="Load Term" OnClick="btnLoadTerm_Click" />
            <asp:Button ID="btnViewTerm" runat="server" Text="View Term" OnClick="btnViewTerm_Click" />
            <asp:Button ID="btnDeleteTerm" runat="server" Text="Delete Term" OnClick="btnDeleteTerm_Click" />
            <br />
            <asp:ListBox ID="lstTerm" runat="server" Width="400px" Height="100px" Visible="false"></asp:ListBox>
        </div>
        
        <div class="section">
            <h3>Course Section Operations</h3>
            <asp:Button ID="btnCreateCourse_Section" runat="server" Text="Create Course Section" OnClick="btnCreateCourse_Section_Click" />
            <asp:Button ID="btnLoadCourse_Section" runat="server" Text="Load Course Section" OnClick="btnLoadCourse_Section_Click" />
            <asp:Button ID="btnViewCourse_Section" runat="server" Text="View Course Section" OnClick="btnViewCourse_Section_Click" />
            <asp:Button ID="btnDeleteCourse_Section" runat="server" Text="Delete Course Section" OnClick="btnDeleteCourse_Section_Click" />
            <br />
            <asp:ListBox ID="lstCourse_Section" runat="server" Width="400px" Height="100px" Visible="false"></asp:ListBox>
        </div>
        
        <div class="section">
            <h3>Enrollment Operations</h3>
            <asp:Button ID="btnCreateEnrollment" runat="server" Text="Create Enrollment" OnClick="btnCreateEnrollment_Click" />
            <asp:Button ID="btnLoadEnrollment" runat="server" Text="Load Enrollment" OnClick="btnLoadEnrollment_Click" />
            <asp:Button ID="btnViewEnrollment" runat="server" Text="View Enrollment" OnClick="btnViewEnrollment_Click" />
            <asp:Button ID="btnDeleteEnrollment" runat="server" Text="Delete Enrollment" OnClick="btnDeleteEnrollment_Click" />
            <br />
            <asp:ListBox ID="lstEnrollment" runat="server" Width="400px" Height="100px" Visible="false"></asp:ListBox>
        </div>
        
        <div class="section">
            <h3>State Operations</h3>
            <asp:Button ID="btnCreateState" runat="server" Text="Create State" OnClick="btnCreateState_Click" />
            <asp:Button ID="btnLoadState" runat="server" Text="Load State" OnClick="btnLoadState_Click" />
            <asp:Button ID="btnViewState" runat="server" Text="View State" OnClick="btnViewState_Click" />
            <asp:Button ID="btnDeleteState" runat="server" Text="Delete State" OnClick="btnDeleteState_Click" />
            <br />
            <asp:ListBox ID="lstState" runat="server" Width="400px" Height="100px" Visible="false"></asp:ListBox>
        </div>
        
    </div>
    </form>
    
</body>
</html>