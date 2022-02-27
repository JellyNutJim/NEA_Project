using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace NEA_Project
{
	//This class contains all the datebase related functions.
	//This includes fetching existing data and submitting new data to the database.

	//My project makes use of the database on two main occasions.
	// 1: When a user is creating or loggin into an account.
	// 2: When a user is saving or loading their saved data from the database.

	class DBTool
	{
		SqlConnection connection;
		string connectionString;

		public DBTool()
		{
			connectionString = ("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Oscar Hoodsmith\\Documents\\NEA\\code\\NEA_Project\\NEA_Project\\NEA_Data.mdf;Integrated Security=True");
		}

		// ------------------------------------------------------------------------------------------------------------------------------ Login_Page related database code.

		//Returns all usernames currently existing within the datbase.
		public LinkedList<string> check_User_Name()
		{
			//This list will contain all existing names within the database.
			LinkedList<string> user_Names = new LinkedList<string>();

			//Using statement automatically closes the database connection.
			using (connection = new SqlConnection(connectionString))
			{
				SqlCommand cmd = new SqlCommand("SELECT * FROM User_Data", connection);
				connection.Open();

				//Sql reader queries the database.
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						user_Names.AddLast(reader.GetString(1));
					}
					reader.Close();
				}
			}
			return user_Names;
		}

		//By default, this function returns the password for a specfic username, but it can also any other string from any table.
		public string get_String_From_Table(string commanValue, string query = "SELECT User_Password FROM User_Data WHERE User_Name = @commanValueToEnter;")
		{
			string fetchedData = "";

			using (connection = new SqlConnection(connectionString))
			{
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					command.CommandType = CommandType.Text;

					//I am using the addwithvalue function to input certain values into the query.
					//While I could have simplt placed theses values into the original query string, it would not have had sql injection protection.
					//Substitution is a lot safer and less prone to being attacked.
					command.Parameters.AddWithValue("@commanValueToEnter", commanValue);

					connection.Open();
					using (SqlDataReader reader = command.ExecuteReader())
					{
						try
						{
							while (reader.Read())
							{
								fetchedData = reader.GetString(0);
							}
						}
						catch (Exception e)
						{
							Console.WriteLine(e);
							return ("fail");
						}
					}
				}
			}
			return fetchedData;
		}
		
		//By default, returns the User_ID for a specific username.
		public int get_Int_From_Table(string commanValue, string query = "SELECT User_ID FROM User_Data WHERE User_Name = @commanValueToEnter;")
		{
			int fetchedData = 0;

			using (connection = new SqlConnection(connectionString))
			{
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					command.CommandType = CommandType.Text;

					//I am using the addwithvalue function to input certain values into the query.
					//While I could have simplt placed theses values into the original query string, it would not have had sql injection protection.
					//Substitution is a lot safer and less prone to being attacked.
					command.Parameters.AddWithValue("@commanValueToEnter", commanValue);

					connection.Open();
					using (SqlDataReader reader = command.ExecuteReader())
					{
						try
						{
							while (reader.Read())
							{
								fetchedData = reader.GetInt32(0);
							}
						}
						catch (Exception e)
						{
							Console.WriteLine(e);
							return (0);
						}
					}
				}
			}
			return fetchedData;
		}

		public void add_New_User_Data(string newUserName, string newUserPassword)
		{
			string query = "INSERT INTO User_Data(User_Name, User_Password) VALUES (@User_Name, @User_Password)";

			using (connection = new SqlConnection(connectionString))
			using (SqlCommand command = new SqlCommand(query, connection))
			{
				command.CommandType = CommandType.Text;
				command.Parameters.AddWithValue("@User_Name", newUserName);
				command.Parameters.AddWithValue("@User_Password", newUserPassword);

				connection.Open();
				command.ExecuteNonQuery();
			}
		}



		// ------------------------------------------------------------------------------------------------------------------------------ DB_Management page related database code.
	}
}
