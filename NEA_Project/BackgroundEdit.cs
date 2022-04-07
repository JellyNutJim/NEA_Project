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
	class BackgroundEdit
	{
		public static Bitmap inputImage;
		public static Bitmap finalImage;
		public static ProgressBar loadingBar;
		public static int imageHeight;
		public static int imageWidth;
		public static int imageArea;
		public static Color[] pixels;

		//Whenever a new object is created, the image must be defined.
		BackgroundEdit(Bitmap userImage)
		{
			inputImage = userImage;
			imageHeight = inputImage.Height;
			imageWidth = inputImage.Width;
			imageArea = imageHeight * imageWidth;
		}

		//Upon the user image being defined, all nessercary values are calculated.
		public static Bitmap InputImage
		{
			get { return inputImage; }
			set
			{ 
				inputImage = value;
				imageHeight = inputImage.Height;
				imageWidth = inputImage.Width;
				imageArea = imageHeight * imageWidth;
			}
		}

		//Returns every pixel within a bitmap, stores values in an array called pixels then returns it.
		public static Color[] GetAllPixels()
		{
			pixels = new Color[imageArea];
			int i = 0;
			
			//The image is searched row by row, the y represent the current row, and the x representing
			//the current position on a row.
			//The x and y values can also be used to access specific pixels within the image.
			for (int y = 0; y < imageHeight; y++)
			{
				for (int x = 0; x < imageWidth; x++, i++)
				{
					//Each pixel is added to the pixel array. (A pixel is just a colour)
					pixels[i] = inputImage.GetPixel(x, y);
				}
			}

			return pixels;

		}

		//Used after changes have been made to the bitmap.
		//Replaces all old pixel colour values with the new ones.
		public static Color[] SetAllPixels()
		{
			finalImage = new Bitmap(imageWidth, imageHeight);

			int i = 0;
			
			//Functions in a very similar way to the GetAllPixels function, expect this time the 
			//x and y values are used to set a colour to a new bitmap.
			for (int y = 0; y < imageHeight; y++)
			{
				for (int x = 0; x < imageWidth; x++, i++)
				{
					finalImage.SetPixel(x, y, pixels[i]);
				}
			}

			return pixels;
		}
		
		//The goal of this funciton is to convert any pixels in the pixels array into one of two
		//possible colours, black or white. This does not include grey scale, only black
		//and white colours should be returned.
		public static void HSBPixels()
		{	
			//imageArea will also be the amount of colours present in the pixels array.
			//As we need to loop through every value in the array, we can just use the image area.
			for (int i = 0; i < imageArea; i++)
			{
				float pixelHue = pixels[i].GetHue();

				//Convert hue values to be between 0 and 1.
				//If this code is present, their will be colour in the resulting bitmap.
				//If it is removed, then a greyscale is created.

				pixelHue /= 360;

				float pixelSaturation = pixels[i].GetSaturation();
				float pixelBrightness = pixels[i].GetBrightness();

				//Console.WriteLine("hue1: " + pixelHue);
				//Console.WriteLine("sat: " + pixelSaturation);
				//Console.WriteLine("bri: " + pixelBrightness);

				//Increase saturation and brightness
				pixelSaturation *= 2;
				
				//Max value of saturation is 1. So if its greater than this value, it should be set back to 1.
				if (pixelSaturation > 1)
				{
					pixelSaturation = 1;
				}
				
				//Double the brightness of every pixel.
				pixelBrightness *= 2f;
				
				//Max value of brightness is 1. So if its greater than this value, it should be set back to 1.
				if (pixelBrightness > 1)
				{
					pixelBrightness = 1;
				}

				//If the brightness is below 0.9, set the pixel to be black.
				//Else the pixel will be white.
				if (pixelBrightness < 0.9)
				{
					pixelBrightness = 0;
				}

				//Re-define the pixel as a RBG colour.
				if (pixelBrightness == 0)
				{
					pixels[i] = Color.FromArgb(0, 0, 0);
				}
				else
				{
					pixels[i] = Color.FromArgb(255, 255, 255);
				}

			}
		}
	}
}
