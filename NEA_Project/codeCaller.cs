using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace NEA_Project
{
	class codeCaller
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
	}
}
