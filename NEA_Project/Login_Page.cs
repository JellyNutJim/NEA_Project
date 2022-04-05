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
        private DBTool tool;
        private int fails;

        public login_page()
        {
            /*
            Main_Page mp = new Main_Page(12);
            mp.Show();

            DB_Save_Page dp = new DB_Save_Page("This is a test", 12);
            dp.Show();
            */

            InitializeComponent();
            tool = new DBTool();
            fails = 0;
        }

        //Checks if the users log in details are corrrect, and then forwards them to the main page.
        public void Login_Btn_Click(object sender, EventArgs e)
        {
            logIntoAccount();
        }

        //Function called when a new account creation is requested.
        //Before data is checked agaisnt database, the basic standards are checked. (E.g. whether a capital is present)
        private void createAccount_Btn_Click(object sender, EventArgs e)
        {
            createNewAccount();
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
        
        private void logIntoAccount()
		{
            //Get user entered values.
            String attemptedUserName = user_Name_Entry.Text;
            String attemptedPassword = password_Entry.Text;
            bool userExists = false;

            if (attemptedUserName == "" || attemptedPassword == "")
            {
                MessageBox.Show("Please enter both a username and password first.");
            }
            else
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
                    //Create unique hash for the user.
                    string attemptedHash = createHash(attemptedUserName, attemptedPassword);

                    //Get users encrypted password.
                    string user_Actual_Hash = tool.get_String_From_Table(attemptedUserName);

                    //Compares the password the user entered to the one stored within the database.
                    //The "fail" is returned whenever the database was unable to retrieve the account details.
                    if (attemptedHash == user_Actual_Hash)
                    {
                        //The correct login details have been entered.
                        //We can now load the User_ID and open the main page.

                        int User_ID = Convert.ToInt32(tool.get_Int_From_Table(attemptedUserName));
                        Console.WriteLine("The users User_Id is:\n" + User_ID);

                        this.Hide();
                        Main_Page mp = new Main_Page(User_ID);
                        mp.Show();
                        MessageBox.Show("Login  Successful");
                    }
		    //The else statements show the user an error message bassed on the particular error they encountered.
                    else if (user_Actual_Hash == "fail")
                    {
                        fails += 1;
                        MessageBox.Show("Account does not exist");
                    }
                    else
                    {
                        fails += 1;
                        MessageBox.Show("Incorrect Password");
                    }
                }
                else
                {
                    fails += 1;
                    MessageBox.Show("This account does not exist.");
                }
            }

            if (fails >= 5)
            {
                MessageBox.Show("Too many failed log in attempts, please try again later.");
                Application.Exit();
            }
        }

        private void createNewAccount()
		{
            //Get user entered values from the text boxes.
            String requestedUserName = user_Name_Entry.Text;
            String requestedPassword = password_Entry.Text;
	    
		//Makes sure that there are values present in both text boxes before continuing.
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
			    //If there is a match, the username already exists.
                            if (requestedUserName == user_Name)
                            {
                                MessageBox.Show("Username already exist, please choose a different name");
                                exists = true;
                                break;
                            }
                        }
                        //If the username does not exist, and the passowrd meets the format requirements, then a new account can be created. and added to the database.
                        if (!exists)
                        {
                            addAccountToDatabase(requestedUserName, requestedPassword);
			    //The user is then automatically logged in.
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a username greater than 7 characters,\nand less than 64 characters.");
                }
            }
        }
	
	//This function validates that a username is within a valid size.
        private bool usernameFormatCheck(string username)
		{
            if (username.Length <= 5 || username.Length > 64)
			{
                return false;
			}

            return true;
		}

	//This function validates that a password is within a valid size and meets the basic format requirements.
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
            string regexSpecialChar = "[!£$%^&*()-=+_/@'|{}#~;:<>,./?@¬`]";

            //Each check has been seperated into a different statement.
            //While this is not as efficient as just finding the first error and then breaking, it allows me to give the user a detailed log of what went wrong.
            //This log not only includes basic error reports (upper case, numbers etc) but also whether the username already exists.

            //Checks if the password is at least 10 characters long.
            if (requestedPassword.Length < 10)
            {
                containsError = true;
                errors += "Password must be at least 10 characters long\n";
            }
		
	    //Checks if the password is no longer than 64 characters.
            if (requestedPassword.Length > 64)
			{
                containsError = true;
                errors += "Password cannot be longer than 64 characters";
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
                MessageBox.Show($"Your password does not meet the following criteria:\n{errors}");
                return false;
			}

            //No format errors in password.
            return true;
        }

        private void addAccountToDatabase(string username, string password)
		{
		
	    //Creates a new entry in the User_Data table containing the username, and a unqiue hash.
	    //A unqiue User_ID is also generated, although this happens automatically through my SQL table statement.
            tool.add_New_User_Data(username, createHash(username, password));
            MessageBox.Show("Account creation successful!");

            //Get the newly generated User_ID
            int User_ID = tool.get_Int_From_Table(username);
		
	    //Close the login page and open a new main page passing the new User_ID as an argument.
            this.Hide();
            Main_Page mp = new Main_Page(User_ID);
            mp.Show();


        }

        //Creates a unique composite hash using both the username and password.
	//Returns the same value every time for an identical set of inputs.
        private string createHash(string username, string password)
		{
            string longString;
            string shortString;
            string passAndUser = username + password;
	    
	    //It is unlikely that the user and password will be the same length,
	    //Therefore we save the longer value to 'longString' and the shorter value to 'shortString'
            if (username.Length > password.Length)
            {
                longString = username;
                shortString = password;
            }
            else
            {
                longString = password;
                shortString = username;
            }
		
            LinkedList<char> charList = new LinkedList<char>();
		
	    //Combine the two strings into a single string with alternating letters.
	    //This only combines as many letters as are in the short string.
            int i;
            for (i = 0; i < shortString.Length; i++)
            {
                charList.AddLast(shortString[i]);
                charList.AddLast(longString[i]);
            }
		
	    //Any excess letters frm the long string are added to the begining and end of the char list
	    //in a alternating pattern.
            for (i = i; i < longString.Length; i++)
            {
                //If i is even.
                if (i % 2 == 0)
                {
   		    //Place the letter at the start of the list. 
                    charList.AddFirst(longString[i]);
                }
                else
                {
		    //Place the letter at the end of the list. 
                    charList.AddLast(longString[i]);
                }
            }

            //Contains the binary eqiuvilent of each character in the charList
            string[] toHash = new string[charList.Count()];

            //Contains the binary equivilent of each character in both the username and password.
            string[] key = new string[charList.Count()];

            //Contains the result of the XOR function.
            string[] hash = new string[charList.Count()];
		
	    //The follwing two loops convert toHash and the key into a binary sequence.
	    //Each character is converted to ascii.
            int d = 0;
            int maxLength = 0;
            foreach (char c in charList)
            {
	    	//Convert to base 2
                toHash[d] = Convert.ToString(c, 2);
                if (toHash[d].Length > maxLength)
                {
                    maxLength = toHash[d].Length;
                }
                d++;
            }

            d = 0;
            foreach (char c in passAndUser)
            {
                key[d] = Convert.ToString(c, 2);
                if (key[d].Length > maxLength)
                {
                    maxLength = toHash[d].Length;
                }
                d++;
            }

            //Some of the binary sequences may be 7 or fewer bits, its important to increase their
	    //length so it is equal to that of the longest binary sequence. If this is not done then, the
            toHash = increaseLength(toHash, maxLength);
            key = increaseLength(key, maxLength);

            //XOR the two values together and save the result.
            int counter = 0;
            foreach (string binaryString in toHash)
            {
                hash[counter] = XOR(binaryString, key[counter]);
                counter++;
            }
	    
	    //Convert the binary sequence into a hexadecimal sequence.
            d = 0;
            maxLength = 0;
            foreach (string c in hash)
            {
	    	//Convert to base 16.
                hash[d] = Convert.ToString(binaryToDenary(c), 16);
		
		//The max length is also recorded,
                if (hash[d].Length > maxLength)
                {
                    maxLength = hash[d].Length;
                }
                d++;
            }
	    
            //Increase the length of each hexadecimal number to the same amount of characters.
	    //The returned value is saved into the hash array.
            hash = increaseLength(hash, maxLength);
	    
	    //Place each value in the hash array into a string.
            string finalHash = "";
            foreach (string c in hash)
            {
                finalHash += c;
            }

            return finalHash;
        }
        
	//Converts a denary number into its binary equivilent.
        private int binaryToDenary(string binary)
        {
            int denary = 0;
            double num = 0;

            for (int i = 0; i < binary.Length; i++)
            {
                if (binary[i] == '1')
                {
                    num += Math.Pow(2, i);
                }
            }

            //It's more efficient to use a double and during the for statement, and convert to int afterwards than it would be to use an int,
            //and therefore convert to an int each time it loops
            denary = Convert.ToInt32(num);

            return denary;
        }
	
	//Takes two strings that contain a binary sequence and performs an XOR logic operation.
        private string XOR(string str, string key)
        {
            //Loops through both of the strings and compares characters with the same index.
            string result = "";
            for (int i = 0; i < str.Length; i++)
            {
	    	//Compares the 4 possible value pairs for the binary digits.
                switch (str[i])
                {
                    case '0':
                        switch (key[i])
                        {
                            case '0':
                                result += '0';
                                break;
                            case '1':
                                result += '1';
                                break;
                        }
                        break;

                    case '1':
                        switch (key[i])
                        {
                            case '0':
                                result += '1';
                                break;
                            case '1':
                                result += '0';
                                break;
                        }
                        break;
                }
            }
            return result;
        }
	
	//Takes an array containing string of a varying size, and increases every string's length to be equivilent to
	//a specified integer.
        private static string[] increaseLength(string[] toLengthen, int requiredLength)
        {
            for (int i = 0; i < toLengthen.Length; i++)
            {
                while (toLengthen[i].Length < requiredLength)
                {
		    //The value being added to the string will either be a one or a zero.
		    //This depends on whether i is currently even or odd.
                    toLengthen[i] = toLengthen[i] + (i % 2);
                }
            }

            return toLengthen;
        }

    }
}
