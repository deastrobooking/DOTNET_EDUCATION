<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogIn.aspx.cs" Inherits="LogIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Info Validation</title>
    <style type="text/css">
        #buttonDiv {
            text-align: center;
        }
        .registration-header {
            text-align: center;
            font-size: 24px;
            font-weight: bold;
            color: #333;
            margin: 20px 0;
        }
        .validation-table {
            margin: 20px auto;
            border-collapse: collapse;
        }
        .validation-table td {
            padding: 10px;
            border: 1px solid #ccc;
        }
        .error-message {
            color: red;
            font-weight: bold;
            margin: 10px 0;
            text-align: center;
        }
        .success-message {
            color: green;
            font-weight: bold;
            margin: 10px 0;
            text-align: center;
        }
        body {
            font-family: Arial, sans-serif;
            background-color: #f5f5f5;
        }
        .container {
            max-width: 600px;
            margin: 0 auto;
            background-color: white;
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
            margin-top: 50px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <div class="registration-header">Registration Page</div>
        
        <table class="validation-table" border="1" cellpadding="1" cellspacing="1">
            <tr>
                <td><asp:Label ID="lblEmail1" runat="server" Text="Enter Email:"></asp:Label></td>
                <td><asp:TextBox ID="txtEMail1" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblEmail2" runat="server" Text="Confirm Email:"></asp:Label></td>
                <td><asp:TextBox ID="txtEMail2" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblDOB1" runat="server" Text="Enter Date of Birth (MM/DD/YYYY):"></asp:Label></td>
                <td><asp:TextBox ID="txtDOB1" runat="server" placeholder="MM/DD/YYYY"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblDOB2" runat="server" Text="Confirm Date of Birth:"></asp:Label></td>
                <td><asp:TextBox ID="txtDOB2" runat="server" placeholder="MM/DD/YYYY"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2">
                    <div id="buttonDiv">
                        <asp:Button ID="btnSubmit" runat="server" Text="Validate" OnClick="btnSubmit_Click" />
                    </div>
                </td>
            </tr>
        </table>
        
        <div class="error-message">
            <asp:Label ID="lblErrorMessage" runat="server" Text=""></asp:Label>
        </div>
        
    </div>
    </form>
</body>
</html>