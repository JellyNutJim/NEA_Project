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
	class ConvertToText
	{
		//Call process to break down the iamge into letters
		//Convert letters
		//Add them to sentance class/subclasses

		//Breaks down the now brightened and greyscale image into individual sentances, then words, than characters.
		public static void breakDownSentance(Bitmap input_image)
		{
			//If pixel is black, get joined pixels.
			//create grid arround letter by getting the highest, lowest, most right, and most left pixel.
			//Create new img or bitmap from new grid.
			
			//Additionally measure distance between characters to calculate where words end/start.
			//Get normal distance between characters, when a distance longer than expected is found, it is most likely a space.

			Color[] pixels = BackgroundEdit.GetAllPixels(input_image);

			for (int y = 0; y < input_image.Height; y++)
			{
				for (int x = 0; x < input_image.Width; x++, i++)
				{
					if (CheckBlack(pixels[x, y]))
					{
						//Check all 8 possible pixels arround it, then check those.
						//Build up a grid of the letter.

						for ()

					}
				}
			}


			foreach (Color pixel in pixels)
			{
				//If the pixel is black
				if (CheckBlack(pixel))
				{
					
				}
			}
			
		}

		public static void CheckBlack(Color pixel)
		{
			if (pixel.GetBrightness < 0.1 )
			{
				return true;
			}
			return false;
		}
	}
}
