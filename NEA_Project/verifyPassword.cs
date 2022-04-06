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
	public partial class verifyPassword : Form
	{
		Label passwordHolder;

		public verifyPassword(Label password_Holder)
		{
			InitializeComponent();

			//Defines the password holder to a label form element from the calling page.
			this.passwordHolder = password_Holder;
		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		private void Submit_Btn_Click(object sender, EventArgs e)
		{
			//Upon the user re-submiting the password, it is sent to the calling page by assinging it to 
			//the label element from that page.
			passwordHolder.Text = password_Input.Text;
			Close();
		}
	}
}
