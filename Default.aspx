<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ashland Soccer</title>
 <style>
        body {
   font-family: Arial, sans-serif;
      margin: 0;
            padding: 0;
    background-color: #f5f5f5;
        }
        .container { 
            max-width: 1200px;
         margin: 20px auto;
  padding: 20px;
      background-color: white;
    box-shadow: 0 2px 4px rgba(0,0,0,0.1);
 }
        .soccer-banner { 
            background: linear-gradient(135deg, #1e7e34 0%, #4CAF50 100%);
            color: white;
     padding: 30px; 
  text-align: center;
     border-radius: 8px;
      margin-bottom: 30px;
    box-shadow: 0 4px 6px rgba(0,0,0,0.1);
        }
.soccer-banner h1 {
          margin: 0;
         font-size: 2.5em;
     text-shadow: 2px 2px 4px rgba(0,0,0,0.3);
        }
        .soccer-banner p {
            margin: 10px 0 0 0;
     font-size: 1.2em;
   opacity: 0.9;
        }
  .filter-section {
   background-color: #f8f9fa;
            padding: 20px;
      border-radius: 5px;
      margin-bottom: 20px;
        display: flex;
            gap: 20px;
            align-items: flex-end;
   flex-wrap: wrap;
        }
        .filter-group {
            display: flex;
flex-direction: column;
   min-width: 200px;
        }
        .filter-group label {
         font-weight: bold;
 margin-bottom: 5px;
   color: #333;
        }
        .filter-group select {
     padding: 8px;
            border: 1px solid #ddd;
    border-radius: 4px;
    font-size: 14px;
        }
        .error { 
color: red; 
      font-weight: bold; 
            padding: 10px;
            background-color: #fee;
            border-radius: 4px;
         margin-bottom: 15px;
  }
      .gridview {
   width: 100%;
    border-collapse: collapse;
    margin-top: 20px;
     }
     .gridview th {
         background-color: #1e7e34;
     color: white;
   padding: 12px;
            text-align: left;
            font-weight: bold;
        }
        .gridview td {
     padding: 10px;
       border-bottom: 1px solid #ddd;
        }
 .gridview tr:hover {
            background-color: #f5f5f5;
        }
        .hidden {
          display: none;
 }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        
        <!-- Soccer Banner -->
        <div class="soccer-banner">
 <h1>? Ashland Soccer League ?</h1>
          <p>Game Schedule & Results</p>
        </div>
   
        <!-- Error Message Display -->
        <asp:Label ID="lblErrorMessage" runat="server" CssClass="error" Text="" Visible="false" />
  
        <!-- Filter Section -->
    <div class="filter-section">
  <div class="filter-group">
          <label for="ddField">Field</label>
              <asp:DropDownList ID="ddField" runat="server" AutoPostBack="true" />
</div>
     <div class="filter-group">
             <label for="ddTeam">Team Name</label>
            <asp:DropDownList ID="ddTeam" runat="server" AutoPostBack="true" />
         </div>
   <div class="filter-group">
   <label for="ddStatus">Status</label>
       <asp:DropDownList ID="ddStatus" runat="server" AutoPostBack="true" />
   </div>
   </div>
        
        <!-- Hidden ListBoxes for Index Tracking (debugging - hidden from view) -->
        <asp:ListBox ID="lbField" runat="server" CssClass="hidden" Visible="false" />
        <asp:ListBox ID="lbTeam" runat="server" CssClass="hidden" Visible="false" />
        <asp:ListBox ID="lbStatus" runat="server" CssClass="hidden" Visible="false" />
        
        <!-- GridView for displaying schedule -->
        <asp:GridView ID="gvDisplay" runat="server" 
 CssClass="gridview" 
       AutoGenerateColumns="true" 
              HeaderStyle-BackColor="#1e7e34" 
HeaderStyle-ForeColor="White" />
        
    </div>
    </form>
    
</body>
</html>