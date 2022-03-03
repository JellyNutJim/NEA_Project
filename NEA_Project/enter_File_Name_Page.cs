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

			this.Filename_Holder = filename_Holder;
		}

		private void name_Entry_Btn_Click(object sender, EventArgs e)
		{
			string requestedFileName = filename_Entry_TextBox.Text;
			string[] bannedFileNames = { "CON", "PRN", "AUX", "NUL", "LST", "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9", "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9" };
			bool validName = true;

			if (requestedFileName.Last() == ' ' || requestedFileName.Last() == '.')
			{
				validName = false;
				MessageBox.Show("Please do not end your filename with a space or period.");
			}
			else
			{
				foreach (string bannedName in bannedFileNames)
				{
					if (requestedFileName.ToUpper() == bannedName)
					{
						validName = false;
						MessageBox.Show("This file name is reserved for specific windows commands\nPlease choose a different name.");
						break;
					}
				}
			}

			if (validName)
			{
				Filename_Holder.Text = requestedFileName;
				Close();
			}
		}
	}
}
