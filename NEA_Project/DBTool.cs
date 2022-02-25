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
	class DBTool
	{
		SqlConnection connection;
		string connectionString;

		public DBTool()
		{
			connectionString = ConfigurationManager.ConnectionStrings["NEA_Project.Properties.Settings.NEA_DataConnectionString"].ConnectionString;
		}

		//Returns all usernames currently existing within the datbase.
		public LinkedList<string> check_User_Name()
		{
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
						Console.WriteLine(reader.GetString(1));
						user_Names.AddLast(reader.GetString(1));
					}
					reader.Close();
				}
			}

			return user_Names;
		}

		public void add_New_User_Data(string newUserName, string newUserPassword)
		{
			/*string query = "INSERT INTO File_Data values('chddd', 'jddeez');";

			using (connection = new SqlConnection(connectionString))
			{
				using (SqlCommand cmd = new SqlCommand(query, connection))
				{
					connection.Open();
					//cmd.Parameters.AddWithValue("@FN", "'deez'");
					//cmd.Parameters.AddWithValue("@FF", "'deez'");
					//cmd.CommandType = CommandType.Text;
					cmd.ExecuteNonQuery();
				}
			}*/



			string query = "INSERT INTO User_Data(User_Name, User_Password) VALUES (@User_Name, @User_Password)";

			using (connection = new SqlConnection(connectionString))
			using (SqlCommand command = new SqlCommand(query, connection))
			{
				command.CommandType = CommandType.Text;
				command.Parameters.AddWithValue("@User_Name", newUserName);
				command.Parameters.AddWithValue("@User_Password", newUserPassword);

				Console.WriteLine(command.CommandText);

				connection.Open();
				command.ExecuteNonQuery();
			}

			foreach (string bruh in check_User_Name())
			{
				Console.WriteLine(bruh);
			}
		}
	}
}
