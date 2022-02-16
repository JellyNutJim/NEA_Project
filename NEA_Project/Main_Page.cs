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

		private void Input_Img_Display_DragEnter(object sender, DragEventArgs e)
		{
			//Gets dragged image
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

		private void Result_Img_Click(object sender, EventArgs e)
		{

		}

		//Called when the remove background button is selected.
		//	
		private void Remove_BG_Btn_Click(object sender, EventArgs e)
		{
			Console.WriteLine("Remove bg triggered");

			//Checks whether an image is actually present within the input_Img_Display picturebox.
			//If there is, call the removeBG with the image.
			//Else display an error to the user.
			if (Input_Img_Display.Image != null)
			{
				Bitmap bitmappedImage = new Bitmap(Input_Img_Display.Image);
				codeCaller.RemoveBG(bitmappedImage);
			}
			else
			{
				//Display error to user.
				Image_Error_Display.BringToFront();
				Image_Error_Display.Text = "Please enter an image first";
			}

			//Gets the final image (with the removed backkground) and sets the Result_Img
			//picture box image equal to this value.
			Result_Img_Display.Image = (Image)(BackgroundEdit.finalImage);
			Result_Img_Display.SizeMode = PictureBoxSizeMode.Zoom;
		}

		//Called when the Convert to text button is clicked.
		//Firstly, if the background has not already been removed, it will remove the background.
		//If it has already been called then it will set the Input_Img_Display to that image.
		private void Convert_To_Text_Btn_Click(object sender, EventArgs e)
		{
			//If removed bg exists
			if (Result_Img_Display.Image != null)
			{
				Console.WriteLine("Ghewe");
				Input_Img_Display.Image = Result_Img_Display.Image;
				Result_Img_Display.Image = null;
				convert();
			}
			else 
			{
				//An image with a removed background is not present
				if (Input_Img_Display.Image != null)
				{
					Bitmap bitmappedImage = new Bitmap(Input_Img_Display.Image);
					codeCaller.RemoveBG(bitmappedImage);

					//Sets the Input_Img_Display to the image with a removed background.
					Input_Img_Display.Image = (Image)(BackgroundEdit.finalImage);

					//Splits image into chracters.
					convert();


				}
				else
				{
					//Display error to user.
					Image_Error_Display.BringToFront();
					Image_Error_Display.Text = "Please enter an image first";
				}
			}
		}

		private void convert()
		{
			Bitmap image = new Bitmap(Input_Img_Display.Image);
			createLetter newLetter = new createLetter(image);
			/*int width = 0;
			int height = 0;
			int length = 0;

			foreach (Bitmap letter in newLetter.letters)
			{
				length++;
				width += letter.Width;
				height += letter.Height;
			}

			Bitmap allLetters = new Bitmap(width, height);
			LinkedList<Bitmap> temp = newLetter.letters;

			int start = 0;

			using (Graphics g = Graphics.FromImage(allLetters))
			{
				for (int i = 0; i < length; i++)
				{
					g.DrawImage((Image)(temp.First.Value), start, 0);
					start += temp.First.Value.Width;
					temp.RemoveFirst();
				}
			}*/

			Bitmap temp = newLetter.letters.Last.Value;

			for (int y = 0; y < temp.Height; y++)
			{
				for (int x = 0; x < temp.Width; x++)
				{
					if (temp.GetPixel(x, y) != Color.FromArgb(255, 0, 0, 0))
					{
						temp.SetPixel(x, y, Color.FromArgb(255, 40, 32, 69));
					}
				}
			}

					Input_Img_Display.Image = newLetter.letters.Last.Value;
		}
	}
}
