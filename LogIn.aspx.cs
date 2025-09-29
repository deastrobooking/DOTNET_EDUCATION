using System;
using System.Globalization;
using System.Web;
using System.Web.UI;

public partial class LogIn : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            // Display captured data for debugging (commented out for production)
            // Response.Write(this.txtEMail1.Text + "<br />");
            // Response.Write(this.txtEMail2.Text + "<br />");
            // Response.Write(this.txtDOB1.Text + "<br />");
            // Response.Write(this.txtDOB2.Text + "<br />");
            // Response.Write(this.btnSubmit.Text + "<br />");
            // Response.Write("Enter Validation Code Here <br />");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        // Initialize error flag for validation
        Boolean errorFlag = false;
        lblErrorMessage.Text = ""; // Clear previous error messages

        //--------------------------------------------------------------------
        // Business Rule 1: Emails & DOBs must match.
        // Business Rule 2: Email must contain '@' and a '.' after '@'.
        // Business Rule 3: DOB format must be MM/DD/YYYY.
        //--------------------------------------------------------------------

        // Business Rule 1 - Match Check for Date of Birth
        if ((txtDOB1.Text).Trim() != (txtDOB2.Text).Trim())
        {
            txtDOB1.Text = "";
            txtDOB2.Text = "";
            txtDOB1.Focus();
            lblErrorMessage.Text = "Date of Birth entries do not match. Please try again.";
            errorFlag = true;
        }

        // Business Rule 1 - Match Check for Email
        if ((txtEMail1.Text).Trim() != (txtEMail2.Text).Trim())
        {
            txtEMail1.Text = "";
            txtEMail2.Text = "";
            txtEMail1.Focus();
            lblErrorMessage.Text = "Email entries do not match. Please try again.";
            errorFlag = true;
        }

        // Business Rule 2 - Email Format Validation
        if (!errorFlag)
        {
            string[] partsOfAddress = (txtEMail1.Text).Split('@');

            // Check if email has exactly one @ and at least one . after @
            if ((partsOfAddress.Length != 2) || (!partsOfAddress[1].Contains(".")))
            {
                txtEMail1.Text = "";
                txtEMail2.Text = "";
                txtEMail1.Focus();
                lblErrorMessage.Text = "Invalid email format. Email must contain '@' and '.' after '@'. Please try again.";
                errorFlag = true;
            }
        }

        // Business Rule 3 - Date Format Validation (MM/DD/YYYY)
        if (!errorFlag)
        {
            try
            {
                // Parse the date using exact format MM/dd/yyyy
                DateTime checkDate = DateTime.ParseExact(txtDOB1.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                
                // Additional check: Make sure the date is reasonable (not in the future, not too old)
                if (checkDate > DateTime.Now)
                {
                    txtDOB1.Text = "";
                    txtDOB2.Text = "";
                    txtDOB1.Focus();
                    lblErrorMessage.Text = "Date of birth cannot be in the future. Please try again.";
                    errorFlag = true;
                }
                else if (checkDate < DateTime.Now.AddYears(-150))
                {
                    txtDOB1.Text = "";
                    txtDOB2.Text = "";
                    txtDOB1.Focus();
                    lblErrorMessage.Text = "Please enter a valid date of birth. Please try again.";
                    errorFlag = true;
                }
            }
            catch
            {
                txtDOB1.Text = "";
                txtDOB2.Text = "";
                txtDOB1.Focus();
                lblErrorMessage.Text = "Invalid date format. Please use MM/DD/YYYY format. Please try again.";
                errorFlag = true;
            }
        }

        // If no errors, create cookie and redirect to Default.aspx
        if (!errorFlag)
        {
            // Create and configure the userInfo cookie
            HttpCookie cookie = new HttpCookie("userInfo");
            cookie.Values.Add("eMail", txtEMail1.Text);
            cookie.Values.Add("DOB", txtDOB1.Text);
            cookie.Expires = DateTime.Now.AddDays(30); // Cookie expires in 30 days

            // Add cookie to response
            Response.Cookies.Add(cookie);

            // For testing - display cookie values (commented out for production)
            // txtEMail1.Text = Request.Cookies["userInfo"]["eMail"];
            // txtDOB1.Text = Request.Cookies["userInfo"]["DOB"];

            // Show success message and redirect
            lblErrorMessage.CssClass = "success-message";
            lblErrorMessage.Text = "Validation successful! Redirecting to main application...";
            
            // Redirect to Default.aspx
            Response.Redirect("Default.aspx");
        }
    }
}