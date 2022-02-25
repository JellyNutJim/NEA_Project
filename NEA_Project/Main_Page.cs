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

namespace NEA_Project
{
	public partial class Main_Page : Form
	{
        public LinkedList<Bitmap> letters;

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
            Current_Status_Label.Visible = false;
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

        // The following code relates to button on click functions ------------------------------------------------------------------------------------------------------------

        //Called when the remove background button is selected.
        private void Remove_BG_Btn_Click(object sender, EventArgs e)
		{
			//Console.WriteLine("Remove bg triggered");

			//Checks whether an image is actually present within the input_Img_Display picturebox.
			//If there is, call the removeBG with the image.
			//Else display an error to the user.
			if (Input_Img_Display.Image != null)
			{
				Bitmap bitmappedImage = new Bitmap(Input_Img_Display.Image);

                Current_Status_Label.Visible = true;
                Current_Status_Label.Text = "Loading...";
                Loading_Bar.Maximum = 5;
                Loading_Bar.Style = ProgressBarStyle.Blocks;
                Loading_Bar.Value = 0;

				codeCaller.RemoveBG(bitmappedImage, Loading_Bar, Current_Status_Label);
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
            Current_Status_Label.Text = "Done!";
		}

        //Called when the split letters button is selected.
        private void Split_Letters_Btn_Click(object sender, EventArgs e)
        {
            Loading_Bar.Maximum = 5;
            Loading_Bar.Style = ProgressBarStyle.Blocks;
            Loading_Bar.Value = 0;

            createLetter tempLetters = split();
            if (tempLetters != null)
            {
                int width = 0;
                int height = 0;
                int length = 0;

                foreach (Bitmap letter in tempLetters.letters)
                {
                    length++;
                    width += letter.Width;
                    height += letter.Height;
                }

                Bitmap allLetters = new Bitmap(width, height);
                letters = tempLetters.letters;
                LinkedList<Bitmap> drawletters = letters;


                //Draw the new letters into the result picturebox.
                /*int start = 0;
                using (Graphics g = Graphics.FromImage(allLetters))
                {
                    for (int i = 0; i < length; i++)
                    {
                        g.DrawImage((Image)(drawletters.First.Value), start, 0);
                        start += drawletters.First.Value.Width;
                        drawletters.RemoveFirst();
                    }
                }*/

                Result_Img_Display.Image = allLetters;
                Result_Img_Display.SizeMode = PictureBoxSizeMode.Zoom;
                Current_Status_Label.Text = "Done!";
            }
        }

        //Called when the Convert to text button is selected.
        //Firstly, if the background has not already been removed, it will remove the background.
        //If it has already been called then it will set the Input_Img_Display to that image.
        private void Convert_To_Text_Btn_Click(object sender, EventArgs e)
		{
            split();
		}

        //Called when the download button is selected.
        //Firstly,  the option of
        private void Download_Btn_Click(object sender, EventArgs e)
        {
            string selectedDownloadOption = Select_Download_Type_CB.Text;

            switch (selectedDownloadOption)
            {
                case "Single Image":
                    if (Result_Img_Display.Image != null)
                    {
                        FolderBrowserDialog getFolderLocation = new FolderBrowserDialog();

                        if (getFolderLocation.ShowDialog() == DialogResult.OK)
                        {
                            string folderLocation = getFolderLocation.SelectedPath;
                            try
                            {
                                Result_Img_Display.Image.Save(folderLocation + @"\Download.png");
                                MessageBox.Show("Download Successful");
                            }
                            catch
                            {
                                MessageBox.Show("Download Failed");
                            }
                        }        
                    }
                    else
                    {
                        MessageBox.Show("There must be an image to download.");
                    }
                    break;

                case "Multiple Images":
                    if (letters.First != null)
                    {
                        FolderBrowserDialog getFolderLocation = new FolderBrowserDialog();

                        if (getFolderLocation.ShowDialog() == DialogResult.OK)
                        {
                            string folderLocation = getFolderLocation.SelectedPath;
                            int num = 1;
                            try
                            {
                                foreach (Bitmap letter in letters)
                                {
                                    letter.Save(folderLocation + $@"\letter{num}.png");
                                    num++;
                                }
                                MessageBox.Show("Download Successful");
                            }
                            catch
                            {
                                MessageBox.Show("Download Failed");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please use the split letters function first.");
                    }



                    break;
                case "Text File":
                    break;
                default:
                    MessageBox.Show("Please select a download option");
                    break;
            }

        }

        // The following code contains functions that will be made use of multiple times by various button interactions. --------------------------------------------

        private createLetter split()
        {
            //An image with a removed background is not present
            if (Input_Img_Display.Image != null)
            {
                Bitmap bitmappedImage = new Bitmap(Input_Img_Display.Image);

                Current_Status_Label.Visible = true;
                codeCaller.RemoveBG(bitmappedImage, Loading_Bar, Current_Status_Label);

                //Splits image into chracters.
                Bitmap image = new Bitmap(BackgroundEdit.finalImage);
                createLetter newLetter = new createLetter(image);
                return newLetter;
            }
            else
            {
                //Display error to user.
                //Occurs when no image is present in either the result or input picture box.
                Image_Error_Display.BringToFront();
                Image_Error_Display.Text = "Please enter an image first";
            }

            return null;
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void Save_To_DB_Btn_Click(object sender, EventArgs e)
        {

        }
    }
}
