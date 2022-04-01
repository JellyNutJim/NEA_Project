using System;
using System.IO;
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
			//Gets the local path of the database on the users computer.
			//This returns the path of a temporary file created during debugging.
			//Therefore we have to slightly alter the string so it points to the permanent database.
			string generatedDatabasePath = Path.GetFullPath(@"NEA_Project");
			generatedDatabasePath = generatedDatabasePath.Remove(generatedDatabasePath.Length - 21);
			generatedDatabasePath += "NEA_Data.mdf";

			//Creates a connection string using the generated database path.
			connectionString = ($"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={generatedDatabasePath};Integrated Security=True");
		}

		// ------------------------------------------------------------------------------------------------------------------------------ Login_Page related database code.

		//Checks a specific colomn in a table.
		//Be default this function returns all the names that exist within the User_Data table.
		public LinkedList<string> check_Table_For_Values(string query = "SELECT User_Name FROM User_Data")
		{
			//This list will contain all existing names within the database.
			LinkedList<string> user_Names = new LinkedList<string>();

			//Using statement automatically closes the database connection.
			using (connection = new SqlConnection(connectionString))
			{
				SqlCommand cmd = new SqlCommand(query, connection);
				connection.Open();

				//Sql reader queries the database.
				using (SqlDataReader reader = cmd.ExecuteReader())
				{	
					//The while loop will repeat for every value that was fetched.
					//E.g. every username in the database.
					while (reader.Read())
					{
						user_Names.AddLast(reader.GetString(0));
					}
					reader.Close();
				}
			}
			return user_Names;
		}

		//By default, this function returns the password for a specfic username, but it can also any other string from any table.
		public string get_String_From_Table(string commanValue, string query = "SELECT User_Hash FROM User_Data WHERE User_Name = @commaoValueToEnter;")
		{
			string fetchedData = "";

			using (connection = new SqlConnection(connectionString))
			{
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					command.CommandType = CommandType.Text;

					//I am using the addwithvalue function to input certain values into the query.
					//While I could have simply placed theses values into the original query string, it would not have had SQL injection protection.
					//Substitution is a lot safer and less prone to being attacked.
					command.Parameters.AddWithValue("@commonValueToEnter", commanValue);

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

					//Open the connection and excute reader.
					connection.Open();
					using (SqlDataReader reader = command.ExecuteReader())
					{
						try
						{
							//Opens the reader.Read Method.
							//This while loop will only run once as we are only getting one value from the table.
							while (reader.Read())
							{
								//Gets an int from the table reader, and then defines fetchedData with said int. 
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

		//Adds a new user entry to the User_Data table.
		public void add_New_User_Data(string newUserName, string newUserHash)
		{
			string query = "INSERT INTO User_Data(User_Name, User_Hash) VALUES (@User_Name, @User_Hash)";

			using (connection = new SqlConnection(connectionString))
			using (SqlCommand command = new SqlCommand(query, connection))
			{
				command.CommandType = CommandType.Text;
				//A new User_ID value is auto generated by the datatable.
				command.Parameters.AddWithValue("@User_Name", newUserName);
				command.Parameters.AddWithValue("@User_Hash", newUserHash);
				
				//Open the connection and excute the query.
				connection.Open();
				command.ExecuteNonQuery();
			}
		}

		// ------------------------------------------------------------------------------------------------------------------------------ DB_Management page related database code.

		//Adds the new file to the Saved_Files table.
		public bool add_New_File(int User_ID, string File_Name, string File_Type, string File_Binary, string compression_String, int compressed_File_Size, DateTime date_Of_Creation)
		{
			//The SQL query. The binary which is saved as a string is casted to a varbinay(max) type so it is more space efficient in the database.
			string query = "INSERT INTO Saved_Files(User_ID, File_Name, File_Type, Saved_File, Compression_String, Compressed_File_Size, Date_Of_Creation) VALUES(@UserID, @FileName, @FileType, CAST(@SavedFile as VARBINARY(MAX)), @CompressionString, @CompressedFileSize, @DateOfCreation);";
			try
			{
				using (connection = new SqlConnection(connectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.CommandType = CommandType.Text;

						//Enter the parameters into the query.
						command.Parameters.AddWithValue("@UserID", User_ID);
						command.Parameters.AddWithValue("@FileName", File_Name);
						command.Parameters.AddWithValue("@FileType", File_Type);
						command.Parameters.AddWithValue("@SavedFile", File_Binary);
						command.Parameters.AddWithValue("@CompressionString", compression_String);
						command.Parameters.AddWithValue("@CompressedFileSize", compressed_File_Size);
						command.Parameters.AddWithValue("@DateOfCreation", date_Of_Creation);

						//Open the connection and excute the query.
						connection.Open();
						command.ExecuteNonQuery();
					}
				}

				return true;
			} 
			catch (Exception e)
			{
				Console.WriteLine(e);
				return false;
			}
		}
		
		//This function will return the general data about files linked to a specific user.
		//The data returned does not include the contents of the file of the compression string used to decompress that file.
		public LinkedList<SavedFileData> get_All_Files(int User_ID)
		{
			//The SQL query.
			string query = "SELECT File_Name, File_Type, Compressed_File_Size, Date_Of_Creation FROM Saved_Files WHERE User_ID = @UserID";
			
			//At his point during the execution of the function the amount of files this specific user has is unkown.
			//Therefore I am using a linked list as it is a dynamic data type that can increase in size with each
			//piece of file data added.
			LinkedList<SavedFileData> data = new LinkedList<SavedFileData>();
			
			//Create a connect and created a new command object.
			using (connection = new SqlConnection(connectionString))
			{
				using (SqlCommand command = new SqlCommand(query, connection))
				{	
					//The only type of information being retreived is in a text format.
					command.CommandType = CommandType.Text;
					
					//Enter the user_ID as a parameter.
					command.Parameters.AddWithValue("@UserID", User_ID);
					
					//Open the connection and create a new sql reader to retreive the data.
					connection.Open();
					using (SqlDataReader reader = command.ExecuteReader())
					{
						//Allows the reader to read data from the database, the while loop will repeat for however many
						//files belong to this user.
						while (reader.Read())
						{
							//Store the values needed as variables.
							string File_Name = reader.GetString(0);
							string File_Type = reader.GetString(1);
							int C_File_Size = reader.GetInt32(2);
							DateTime DOC = reader.GetDateTime(3);
							
							//Create a new Saved_File_Data object and add it to the end of the linked list.
							data.AddLast(new SavedFileData(File_Name, File_Type, C_File_Size, DOC));
						}
						reader.Close();
					}
				}
			}
				return data;
		}
		
		//Gets the actual data of a given file that belongs to a specific user.
		public string[] get_Saved_File(int User_ID, string File_Name)
		{
			//The SQL query that will fetched the file binary and the compression string.
			//The binary is casted to a nvarchar.
			string query = "SELECT CAST(Saved_File as NVARCHAR(max)), Compression_String FROM Saved_Files WHERE User_ID = @UserID AND File_Name = @FileName";
			
			//The data will be returned as an array with the binary in the first element, and the compression string in the second.
			string[] fileAndCS = new string[2];

			using (connection = new SqlConnection(connectionString))
			{
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					//Enter the parameters into the query.
					command.Parameters.AddWithValue("@UserID", User_ID);
					command.Parameters.AddWithValue("@FileName", File_Name);

					connection.Open();
					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							//Assign the array with the binary string, and the compression string.
							fileAndCS[0] = reader.GetString(0);
							fileAndCS[1] = reader.GetString(1);
						}
					}
				}
			}
					return fileAndCS;
		}
	}
}
