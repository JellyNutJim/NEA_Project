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
        DBTool tool;

        public login_page()
        {
            //TEMP ---------------------------------------------- TESTING
            this.Hide();
            Main_Page dbs = new Main_Page(9);
            dbs.Show();
            //TEMP ---------------------------------------------- TESTING

            InitializeComponent();
            tool = new DBTool();
        }

        //Checks if the users log in details are corrrect, and then forwards them to the main page.
        public void Login_Btn_Click(object sender, EventArgs e)
        {
            //Get user entered values.
            String attemptedUserName = userNameEntry.Text;
            String attemptedPassword = passwordEntry.Text;
            bool userExists = false;

            if (attemptedUserName == "" || attemptedPassword == "")
			{
                MessageBox.Show("Please enter both a username and password first.");
			}
			else
			{
                //Checks if the login credentials meet the basic format requirements.
                //This is the first check we do, because if this check fails then the database does not have to be opened for no reason.
                if (usernameFormatCheck(attemptedUserName) && passwordFormatCheck(attemptedPassword))
                {
                    //Gets each user name present in the User_Data table.
                    //The attempted username is then compared agaisnt these to check if it exists.
                    foreach (string user_Name in tool.check_Table_For_Values())
                    {
                        if (attemptedUserName == user_Name)
                        {
                            userExists = true;
                            break;
                        }
                    }

                    //Attempted username is present.
                    if (userExists)
                    {
                        //Encrypt password
                        attemptedPassword = EncodePasswordToBase64(attemptedPassword);

                        //Get users encrypted password.
                        string user_Actual_Password = tool.get_String_From_Table(attemptedUserName);
                        Console.WriteLine(user_Actual_Password);

                        //Compares the password the user entered to the one stored within the database.
                        //The "fail" is returned whenever the database was unable to retrieve the account details.
                        if (attemptedPassword == user_Actual_Password)
                        {
                            //The correct login details have been entered.
                            //We can now load the User_ID and open the main page.

                            int User_ID = Convert.ToInt32(tool.get_Int_From_Table(attemptedUserName));

                            this.Hide();
                            Main_Page mp = new Main_Page(User_ID);
                            mp.Show();
                            MessageBox.Show("Login  Successful");
                        }
                        else if (user_Actual_Password == "fail")
                        {
                            MessageBox.Show("Account does not exist");
                        }
                        else
                        {
                            MessageBox.Show("Incorrect Password");
                        }
                    }
                    else
                    {
                        MessageBox.Show("This account does not exist.");
                    }
                }
                else
                {
                    MessageBox.Show("Login credentials are not in the correct format");
                }
            }  
        }

        //Function called when a new account creation is requested.
        //Before data is checked agaisnt database, the basic standards are checked. (E.g. whether a capital is present)
        private void createAccount_Btn_Click(object sender, EventArgs e)
        {
            //Get user entered values.
            String requestedUserName = userNameEntry.Text;
            String requestedPassword = passwordEntry.Text;

            if (requestedUserName == "" || requestedPassword == "")
			{
                MessageBox.Show("Please enter both a username and password first.");
			}
			else
			{
                //Checks if both the username and password match the basic format criteria.
                if (usernameFormatCheck(requestedUserName))
                {
                    if (passwordFormatCheck(requestedPassword))
                    {
                        //Username and password meets basic format requirements.

                        //Checks if username already exists within the datbase.
                        bool exists = false;
                        foreach (string user_Name in tool.check_Table_For_Values())
                        {
                            Console.WriteLine(user_Name);
                            if (requestedUserName == user_Name)
                            {
                                MessageBox.Show("Username already exist, please choose a different name");
                                exists = true;
                                break;
                            }
                        }
                        //If the username does not exist, and the passowrd meets the format requirements, then a new account can be created.
                        if (!exists)
                        {
                            createAccount(requestedUserName, requestedPassword);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a username greater than 7 characters.");
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

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

		private void button1_Click_1(object sender, EventArgs e)
		{

		}

		private void file_DataBindingNavigatorSaveItem_Click(object sender, EventArgs e)
		{


		}

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

        private bool usernameFormatCheck(string username)
		{
            if (username.Length <= 5)
			{
                return false;
			}

            return true;
		}

        private bool passwordFormatCheck(string password)
		{
            string requestedPassword = password;

            //Creates the regex strings, as well as creates an intial error variable.
            bool containsError = false;
            string errors = "";

            //The regex searches have been seperated so the user can receive a specifc error message telling them what the iussue was.
            string regexUpperCase = "[A-Z]";
            string regexLowerCase = "[a-z]";
            string regexNumber = "[0-9]";
            string regexSpecialChar = "[!£$%^&*()-=+_/@'{}#~;:<>,./?@¬`]";

            //Each check has been seperated into a different statement.
            //While this is not as efficient as just finding the first error and then breaking, it allows me to give the user a detailed log of what went wrong.
            //This log not only includes basic error reports (upper case, numbers etc) but also whether the username already exists.

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

            if (containsError)
			{
                //Format errors are present in the password, these errors are shown to the user.
                MessageBox.Show($"Your password does not meet the following criteria:\n {errors}");
                return false;
			}

            //No format errors in password.
            return true;
        }

        private void createAccount(string username, string password)
		{
            //Create userID hash
            string user_ID_Hash = getHash(username, password);

            tool.add_New_User_Data(username, EncodePasswordToBase64(password));
            MessageBox.Show("Account creation successful!");

            //Get this User_ID
            int User_ID = tool.get_Int_From_Table(username);

            this.Hide();
            Main_Page mp = new Main_Page(User_ID);
            mp.Show();


        }

        private string getHash(string username, string password)
		{
            return "t";

            //Sets l to the length of the longest string.
            
		}

        //Temp encoding function
        private string EncodePasswordToBase64(string password)
        {
            byte[] encData_byte = new byte[password.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
            string encodedData = Convert.ToBase64String(encData_byte);
            return encodedData;
        }

        /*private string DecodeFrom64(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }*/

    }
}
