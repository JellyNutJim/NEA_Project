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
	class process
	{
		public static void RemoveBG(Bitmap input_Image)
		{
			BackgroundEdit.InputImage = input_Image;
			BackgroundEdit.GetAllPixels();
			BackgroundEdit.HSBPixels();
			BackgroundEdit.SetAllPixels();

			
		}


	}
}
