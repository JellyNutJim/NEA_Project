﻿using System;
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

			for (int y = 0; y < imageHeight; y++)
			{
				for (int x = 0; x < imageWidth; x++, i++)
				{
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

			for (int y = 0; y < imageHeight; y++)
			{
				for (int x = 0; x < imageWidth; x++, i++)
				{
					finalImage.SetPixel(x, y, pixels[i]);
				}
			}

			return pixels;
		}

		public static void HSBPixels()
		{
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
				if (pixelSaturation > 1)
				{
					pixelSaturation = 1;
				}

				pixelBrightness *= 2f;
				if (pixelBrightness > 1)

				{
					pixelBrightness = 1;
				}

				
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
