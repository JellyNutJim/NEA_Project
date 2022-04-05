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

namespace NEA_Project
{
	public partial class Main_Page : Form
	{
        public LinkedList<Bitmap> letters;
        public int User_ID;

        public Main_Page(int User_ID)
		{
			InitializeComponent();
            this.User_ID = User_ID;

			//Allows files to be dropped into the input picture box.
			Input_Img_Display.AllowDrop = true;

            //Hides the rich text box that will display any text that has been processed.
            Result_Text_Display.SendToBack();
            Result_Text_Display.ReadOnly = false;
            Result_Text_Display.Text = "";

            //A label used to store a users chosen filename when saving a file.
            filename_Holder.SendToBack();

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
					
					//Sets the image property of the Input_Image_Display to the input img.
					//This image will be reffered back to several times during the programs execution.
					Input_Img_Display.Image = img;
					
					//Set the size mode to zoom, so the image is scaled to the size of the picture box.
					//This makes the image significantly easier to see when the program is being used.
					Input_Img_Display.SizeMode = PictureBoxSizeMode.Zoom;
				}
			}
			catch (e)
			{
				//Informs the user that the input was not in the correct format.
				Image_Error_Display.BringToFront();
				Image_Error_Display.Text = "Please enter a valid image";
				MessageBox.Show("Please enter a valid image (png/jpeg)");
			}
		}

        // The following code relates to button on click functions ------------------------------------------------------------------------------------------------------------

        //Called when the remove background button is selected.
        private void Remove_BG_Btn_Click(object sender, EventArgs e)
		{
            Result_Text_Display.SendToBack();
            Result_Text_Display.Text = "";

			//Checks whether an image is actually present within the input_Img_Display picturebox.
			//If there is, call the removeBG with the image.
			//Else display an error to the user.
			if (Input_Img_Display.Image != null)
			{
				Bitmap bitmappedImage = new Bitmap(Input_Img_Display.Image);

                Current_Status_Label.Visible = true;
                Current_Status_Label.Text = "Loading...";
                Progress_Bar.Maximum = 5;
                Progress_Bar.Style = ProgressBarStyle.Blocks;
                Progress_Bar.Value = 0;

                Current_Status_Label.Text = "Loading image";
                BackgroundEdit.InputImage = bitmappedImage;
                Progress_Bar.Increment(1);

                Current_Status_Label.Text = "Loading Bar";
                BackgroundEdit.loadingBar = Progress_Bar;
                Progress_Bar.Increment(1);

                Current_Status_Label.Text = "Getting all Pixels";
                BackgroundEdit.GetAllPixels();
                Progress_Bar.Increment(1);

                Current_Status_Label.Text = "Increase HSB Values";
                BackgroundEdit.HSBPixels();
                Progress_Bar.Increment(1);

                Current_Status_Label.Text = "Setting new Pixels";
                BackgroundEdit.SetAllPixels();
                Progress_Bar.Increment(1);
            }
			else
			{
				//Display error to user.
				Image_Error_Display.BringToFront();
				Image_Error_Display.Text = "Please enter an image first";
                MessageBox.Show("Please enter an image first");
			}

			//Gets the final image (with the removed backkground) and sets the Result_Img
			//picture box image equal to this value.
			Result_Img_Display.Image = (Image)(BackgroundEdit.finalImage);
			Result_Img_Display.SizeMode = PictureBoxSizeMode.Zoom;
            Current_Status_Label.Text = "Done!";
            Select_Download_Type_CB.Text = "Single Image";
		}

        //Called when the split letters button is selected.
        private void Split_Letters_Btn_Click(object sender, EventArgs e)
        {
            Result_Text_Display.SendToBack();
            Result_Text_Display.Text = "";

            Progress_Bar.Maximum = 5;
            Progress_Bar.Style = ProgressBarStyle.Blocks;
            Progress_Bar.Value = 0;

            SplitImageToLetters tempLetters = split();
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

                //Create an array of the linked list to display the split.
                Bitmap[] letterArray = new Bitmap[letters.Count];

                for (int i = 0; i < letterArray.Length; i++)
                {
                    letterArray[i] = letters.First();
                    letters.RemoveFirst();
                }

                for (int i = 0; i < letterArray.Length; i++)
                {
                    letters.AddLast(letterArray[i]);
                }

                //Draw the new letters into the result picturebox.
                int start = 0;
                using (Graphics g = Graphics.FromImage(allLetters))
                {
                    for (int i = 0; i < length; i++)
                    {
                        g.DrawImage((Image)(drawletters.First.Value), start, 0);
                        start += drawletters.First.Value.Width;
                        drawletters.RemoveFirst();
                    }
                }

                for (int i = 0; i < letterArray.Length; i++)
                {
                    letters.AddLast(letterArray[i]);
                }

                Result_Img_Display.Image = allLetters;
                Result_Img_Display.SizeMode = PictureBoxSizeMode.Zoom;
                Current_Status_Label.Text = "Done!";
                Select_Download_Type_CB.Text = "Multiple Images";
			}
			else
			{

			}
        }

        //Called when the Convert to text button is selected.
        //Firstly, if the background has not already been removed, it will remove the background.
        //If it has already been called then it will set the Input_Img_Display to that image.
        private void Convert_To_Text_Btn_Click(object sender, EventArgs e)
		{
            split();
            Result_Text_Display.BringToFront();
		}

        //Called when the download button is selected.
        private void Download_Btn_Click(object sender, EventArgs e)
        {
            //Fetch the currently selected option from the combo box.
            string selectedDownloadOption = Select_Download_Type_CB.Text;

            switch (selectedDownloadOption)
            {
                case "Single Image":
                    //Firsly, check whether there is an image to download present.
                    if (Result_Img_Display.Image != null)
                    {
                        //Open up a folder viewer so the user can choose where to save their file.
                        FolderBrowserDialog getFolderLocation = new FolderBrowserDialog();

                        if (getFolderLocation.ShowDialog() == DialogResult.OK)
                        {
			    //Save the path of the selected download location.
                            string folderLocation = getFolderLocation.SelectedPath;
                            
			    //Create a new instance of the enter_File_Name_Page and halt the main page until this page has closed.
                            enter_File_Name_Page efnp = new enter_File_Name_Page(filename_Holder);
                            efnp.ShowDialog();
			    
			    //Get the users desired filename from the filename_Holder object that was assigned to by the enter_File_Name_Page.
                            string filename = filename_Holder.Text;
			    
			    //Attempt to save the file to the users pc.
                            try
                            {
                                Result_Img_Display.Image.Save(folderLocation + $@"\{filename}.png");
                                MessageBox.Show("Download Successful");
                                Result_Text_Display.SendToBack();
                            }
                            catch  //Could be thrown for a few reasons. An example being if a drive does not have enough space to store the file.
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
		    //The letters linked list at this point should contain all the bitmaps produced after the split_Letters_Btn_Click function was run.
		    //The linked list would be null if this function was not run prior to the download button being clicked.
                    if (letters.First != null)
                    {
		    	//Aquires the the folder location and the desired file name using the same method as the single image download option.
                        FolderBrowserDialog getFolderLocation = new FolderBrowserDialog();

                        if (getFolderLocation.ShowDialog() == DialogResult.OK)
                        {
                            string folderLocation = getFolderLocation.SelectedPath;
                            int num = 1;

                            enter_File_Name_Page efnp = new enter_File_Name_Page(filename_Holder);
                            efnp.ShowDialog();

                            string filename = filename_Holder.Text;
			    //Attempt to save the files to the users pc.
                            try
                            {
                                foreach (Bitmap letter in letters)
                                {
			            //As there will be multiple files, they cannot all have the same name.
				    //So an incrementing number is placed after the name of each saved image.
                                    letter.Save(folderLocation + $@"\{filename}{num}.png");
                                    Result_Img_Display.Image = null;
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
	            //Checks if there text is present in the Result_Text_Display text box.
		    //Text should be present in this text box after either the convert to text function was run.
		    //Or a text file has just been loaded from the database.
                    if (Result_Text_Display.Text != null)
					{
			//Aquires the the folder location and the desired file name using the same method as the single image download option.
                        FolderBrowserDialog getFolderLocation = new FolderBrowserDialog();

                        if (getFolderLocation.ShowDialog() == DialogResult.OK)
						{
                            string folderLocation = getFolderLocation.SelectedPath;

                            enter_File_Name_Page efnp = new enter_File_Name_Page(filename_Holder);
                            efnp.ShowDialog();

                            string filename = filename_Holder.Text;
			    //A new streamwriter object is created and used to save the users file to a specified location.
                            using (StreamWriter sw = new StreamWriter(folderLocation + $@"\{filename}.txt"))
							{
                                sw.WriteLine(Result_Text_Display.Text);
							}
                            MessageBox.Show("Download Successful");
                        }
                    }
                    else
                    {
                        MessageBox.Show("There must be text present to download");
                    }


                    break;
                default:
                    MessageBox.Show("Please select a download option");
                    break;
            }
        }

        //Called when the user tries to save a file to the database.
        private void Save_To_DB_Btn_Click(object sender, EventArgs e)
        {
            //Firstly, we need to check if the user is trying to save a text file, or an image file.
            //We do this by checking if the ML_Text_Display text box has any text present.

            if (Result_Text_Display.Text != "")
            {
                //We now know that the user is trying to save a text file.
                //And can therefore open up the database same menu.
                DB_Save_Page dbs = new DB_Save_Page(Result_Text_Display.Text, User_ID);
                dbs.Show();
            }
            else if (Result_Img_Display.Image != null)
			{
                //If the user is not saving text, we check is they're trying to save an image.
                DB_Save_Page dbs = new DB_Save_Page(Result_Img_Display.Image, User_ID);
                dbs.Show();
			}
			else
			{
                MessageBox.Show("There is nothing to save");
			}
        }

        //Called when the user tries to load a file from the database by pressing the Load button.
        private void Load_From_DB_Btn_Click(object sender, EventArgs e)
        {
            DB_Load_Page loadPage = new DB_Load_Page(User_ID, Result_Img_Display, Result_Text_Display, Select_Download_Type_CB);
            loadPage.Show();
        }

        // The following code contains functions that will be made use of multiple times by various button interactions. --------------------------------------------

        private SplitImageToLetters split()
        {
            //An image with a removed background is not present
            if (Input_Img_Display.Image != null)
            {
                Bitmap bitmappedImage = new Bitmap(Input_Img_Display.Image);

                Current_Status_Label.Visible = true;

                Current_Status_Label.Text = "Loading image";
                BackgroundEdit.InputImage = bitmappedImage;
                Progress_Bar.Increment(1);

                Current_Status_Label.Text = "Loading Bar";
                BackgroundEdit.loadingBar = Progress_Bar;
                Progress_Bar.Increment(1);

                Current_Status_Label.Text = "Getting all Pixels";
                BackgroundEdit.GetAllPixels();
                Progress_Bar.Increment(1);

                Current_Status_Label.Text = "Increase HSB Values";
                BackgroundEdit.HSBPixels();
                Progress_Bar.Increment(1);

                Current_Status_Label.Text = "Setting new Pixels";
                BackgroundEdit.SetAllPixels();
                Progress_Bar.Increment(1);


                //Splits image into chracters.
                Bitmap image = new Bitmap(BackgroundEdit.finalImage);
                SplitImageToLetters newLetter = new SplitImageToLetters(image);
                return newLetter;
            }
            else
            {
                //Display error to user.
                //Occurs when no image is present in either the result or input picture box.
                Image_Error_Display.BringToFront();
                Image_Error_Display.Text = "Please enter an image first";
                MessageBox.Show("Please enter an image first");
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
	}
}
