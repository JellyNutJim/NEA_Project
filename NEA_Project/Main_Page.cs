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
	public partial class Main_Page : Form
	{
		public Main_Page()
		{
			InitializeComponent();

			//Allows files to be dropped into the input picture box.
			Input_Img_Display.AllowDrop = true;
		}

		private void Main_Page_Load(object sender, EventArgs e)
		{
			//Places the image's error display behind the 
			Image_Error_Display.SendToBack();
		}

		//Gets dragged image
		private void Input_Img_Display_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.Copy;
		}

		//Called when a file has been dropped onto the input picture box.
		private void Input_Img_Display_DragDrop(object sender, DragEventArgs e)
		{
			try
			{
				//Send the error display label behind the main image and resets the text value.
				Image_Error_Display.SendToBack();
				Image_Error_Display.Text = "No Error";

				foreach (string picture in ((string[])e.Data.GetData(DataFormats.FileDrop)))
				{
					Image img = Image.FromFile(picture);
					Input_Img_Display.Image = img;
					Input_Img_Display.SizeMode = PictureBoxSizeMode.Zoom;
				}
			}
			catch (Exception)
			{
				//Informs the user that the input was not in the correct format.
				Image_Error_Display.BringToFront();
				Image_Error_Display.Text = "Please enter a valid image";
			}
		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{
			
		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		private void label2_Click(object sender, EventArgs e)
		{

		}

		private void label2_Click_1(object sender, EventArgs e)
		{

		}

		//Called when the remove background button is selected.
		//	
		private void Remove_BG_Btn_Click(object sender, EventArgs e)
		{
			Console.WriteLine("Remove bg triggered");

			if (Input_Img_Display.Image != null)
			{
				Bitmap bitmappedImage = new Bitmap(Input_Img_Display.Image);
				process.RemoveBG(bitmappedImage);
			}
			else
			{
				//Display error to user.
				Image_Error_Display.BringToFront();
				Image_Error_Display.Text = "Please enter an image first";
			}

			Console.WriteLine("bruh");


			Result_Img.Image = (Image)(BackgroundEdit.finalImage);
			Result_Img.SizeMode = PictureBoxSizeMode.Zoom;
		}

		private void Result_Img_Click(object sender, EventArgs e)
		{

		}
	}
}
