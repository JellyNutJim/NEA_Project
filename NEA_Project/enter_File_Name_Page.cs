using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NEA_Project
{
	public partial class enter_File_Name_Page : Form
	{
		public Label Filename_Holder;

		public enter_File_Name_Page(Label filename_Holder)
		{
			InitializeComponent();

			//This is an invisibe label on the main page.
			//It is used to bring the value from this page to main page as we cannot simply return it. 
			this.Filename_Holder = filename_Holder;
		}

		//Called when the Entry  btn is clicked.
		private void name_Entry_Btn_Click(object sender, EventArgs e)
		{
			Console.WriteLine("name_Entry_Btn_Click has been called");

			//Get the filename from the textbox.
			string requestedFileName = filename_Entry_TextBox.Text;
			bool validName = true;

			Console.WriteLine(requestedFileName);

			//Check if the filename meets the correct format.
			if (!generalFunctions.checkFileNameValid(requestedFileName))
			{
				validName = false;
			}

			Console.WriteLine("validName contains: " + validName);

			//If the name is valed then define the invisisble label's text value to the requested filename.
			if (validName)
			{
				Filename_Holder.Text = requestedFileName;
				Console.WriteLine("Filename_Holder.text contains: " + Filename_Holder.Text);
				Close();
			}
		}
	}
}
