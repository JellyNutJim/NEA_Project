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
			bool validName = true;

			if (!generalFunctions.checkFileNameValid(requestedFileName))
			{
				validName = false;
			}

			if (validName)
			{
				Filename_Holder.Text = requestedFileName;
				Close();
			}
		}
	}
}
