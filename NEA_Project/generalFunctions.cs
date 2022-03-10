using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Drawing.Text;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace NEA_Project
{
	class generalFunctions
	{
		//Takes the given image and increase the saturation and brightness values.
		//This will make it easier to distingush between characters on the page.
		//Therefore making it easier to seperate characters and sentances from the page before interpreting them.
		public static void RemoveBG(Bitmap input_Image, ProgressBar loadingBar, Label status_Label)
		{
            status_Label.Text = "Loading image";
			BackgroundEdit.InputImage = input_Image;
            loadingBar.Increment(1);

            status_Label.Text = "Loading Bar";
            BackgroundEdit.loadingBar = loadingBar;
            loadingBar.Increment(1);

            status_Label.Text = "Getting all Pixels";
            BackgroundEdit.GetAllPixels();
            loadingBar.Increment(1);

            status_Label.Text = "Increase HSB Values";
            BackgroundEdit.HSBPixels();
            loadingBar.Increment(1);

            status_Label.Text = "Setting new Pixels";
            BackgroundEdit.SetAllPixels();
            loadingBar.Increment(1);
        }

        //Checks whether a requested filename meets the basic critera.
        public static bool checkFileNameValid(string filename)
        {
            string[] bannedFileNames = { "CON", "PRN", "AUX", "NUL", "LST", "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9", "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9" };
            string bannedCharacters = @"[\/:*?'<>|]".Replace("'", "\"");

            //Checks to see if the filename is within a valid length.
            if (filename.Length > 20)
			{
                MessageBox.Show("Please enter a filename under 20 characters");
                return false;
			}

            //Checks to see if any invalid filename characters have been used.
            if ((Regex.Match(filename, bannedCharacters).Success))
            {
                MessageBox.Show("Make sure your filename does not include any of the following characters:\n" + @"'\', '/', ':', ':', '*', '?', ', '<', '>', '|'");
                return false;
            }

            //Checks to see if the filename ends with a space or period.
            if (filename.Last() == ' ' || filename.Last() == '.')
			{
                MessageBox.Show("Please do not end your filename with a space or period.");
                return false;
			}

            //Checks to see if the filename is equal to a windows reserved name.
            foreach (string bannedName in bannedFileNames)
            {
                if (filename.ToUpper() == bannedName)
                {
                    MessageBox.Show("This file name is reserved for specific windows commands\nPlease choose a different name.");
                    return false;
                }
            }

            return true;
        }
    }


}
