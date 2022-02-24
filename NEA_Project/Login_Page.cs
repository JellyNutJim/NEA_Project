using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Drawing.Text;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace NEA_Project
{
    public partial class login_page : Form
    {
        public login_page()
        {
            InitializeComponent();
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //Checks if the users log in details are corrrect, and then forwards them to the main page.
        public void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Main_Page mp = new Main_Page();
            mp.Show();
        }

        private void login_page_Load(object sender, EventArgs e)
        {

        }

        private void password_input_Click(object sender, EventArgs e)
        {

        }

		private void label1_Click_1(object sender, EventArgs e)
		{
           
		}

        
        //Function called when a new account creation is requested.
        //Before data is checked agaisnt database, the basic standards are checked. (E.g. whether a capital is present)
		private void createAccount_btn_Click(object sender, EventArgs e)
		{
            //Get user entered values.
            String requestedUserName = userNameEntry.Text;
            String requestedPassword = passwordEntry.Text;

            //Opens up the databse base connection
            string path = Environment.CurrentDirectory;
            path += "\\userData.mdb";

            Console.WriteLine(path);

            OleDbConnection connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Persist Security Info=False;DataSource='/bin/Debug/User_Data.mdb';");

            OleDbCommand cmd = connection.CreateCommand();
            //connection.Open();

            //Creates the regex strings, as well as creates an intial error variable.
            bool containsError = false;
            string errors = "";

            //The regex searches have been seperated so the user can receive a specifc error message telling them what the iussue was.
            string regexUpperCase = "[A-Z]";
            string regexLowerCase = "[a-z]";
            string regexNumber = "[0-9]";
            string regexSpecialChar = "[!£$%^&*()-=+_/@'{}#~;:<>,./?@¬`]";

            //Basic checks for both username and password:

            //Each check has been seperated into a different statement.
            //While this is not as efficient as just finding the first error and then breaking, it allows me to give the user a detailed log of what went wrong.
            //This log not only includes basic error reports (upper case, numbers etc) but also whether the username already exists.
            //I may change this system in the future ---------------------------------------------------------------------

            //Username length Check
            if (requestedUserName.Length < 5)
			{
                containsError = true;
                errors += "Username must be at least 5 characters long\n";
			}

            //Password checks:

            //Checks if the password is at least 10 characters long.
            if (requestedPassword.Length < 10)
			{
                containsError = true;
                errors += "Password must be at least 10 characters long\n";
			}

            //Checks if password contains at least one capital letter.
            if (!(Regex.Match(requestedPassword, regexUpperCase).Success))
			{
                containsError = true;
                errors += "Password must contain at least one capital\n";
            }

            //Checks if password contains at least one lower case letter.
            if (!(Regex.Match(requestedPassword, regexLowerCase).Success))
			{
                containsError = true;
                errors += "Password must contain at least lower case letter\n";
            }

            //Checks if the password contains at least one number.
            if (!(Regex.Match(requestedPassword, regexNumber).Success))
			{
                containsError = true;
                errors += "Password must contain at least one number\n";
			}

            //Checks if the password contains at least one Special character.
            if (!(Regex.Match(requestedPassword, regexSpecialChar).Success))
			{
                containsError = true;
                errors += "Password must contain at one special character\n";
			}

            // NOTE TO SELF  ------ Change error logging system!!! ------------------------------------------------------------------------

            //Checks if any errors have occured and displays the appropriate error message to the user.
            if (containsError)
            {
                MessageBox.Show($"Errors found: \n{errors}");
            }
			else
			{
                //Database check

                //Check against stuff

                //Now the name and password has been cleared for use it can be added to the database and an account can be created.
                /*connection.Open();
                cmd.CommandText = $"INSERT INTO User_Data (UserID, UserName, UserPassword) VALUES('temp', '{requestedUserName}', '{requestedUserName}');";
                cmd.Connection = connection;

                cmd.ExecuteNonQuery();
                connection.Close();*/
            }
        }

		private void button1_Click_1(object sender, EventArgs e)
		{
            string connString = @"Server = RAINBOW3; DATABASE = UserInfo; Trusted_Connection = True;";
            SqlConnection conn = new SqlConnection(connString);

            string query = @"SELECT UserID
                             FROM UserInfo";

            SqlCommand cmd = new SqlCommand(query, conn);

            conn.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            string UserID = dr.GetString(0);
            Console.WriteLine(UserID);
		}
	}
}
