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

		//Used after changes have been made to the bitmap. Replaces all old pixel colour values with the new ones.
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

				if (pixelBrightness > 0.1 && pixelSaturation != 0)
				{
					pixelBrightness *= 2;
					if (pixelBrightness > 1)
					{
						pixelBrightness = 1;
					}
				}
				else 
				{
					pixelBrightness = 0;
				}

				//Re-define the pixel as a RBG colour.
				pixels[i] = HSB2RGB(pixelHue, pixelSaturation, pixelBrightness);
				 
			}
		}

		//Convert HSB to RBG.
		public static Color HSB2RGB(double h, double sl, double l)
		{
			double v;
			double r, g, b;

			r = l;   // default to gray
			g = l;
			b = l;
			v = (l <= 0.5) ? (l * (1.0 + sl)) : (l + sl - l * sl);

			if (v > 0)
			{
				double m;
				double sv;
				int sextant;
				double fract, vsf, mid1, mid2;

				m = l + l - v;
				sv = (v - m) / v;
				h *= 6.0;
				sextant = (int)h;
				fract = h - sextant;
				vsf = v * sv * fract;
				mid1 = m + vsf;
				mid2 = v - vsf;
				switch (sextant)

				{
					case 0:
						r = v;
						g = mid1;
						b = m;
						break;
					case 1:
						r = mid2;
						g = v;
						b = m;
						break;
					case 2:
						r = m;
						g = v;
						b = mid1;
						break;
					case 3:
						r = m;
						g = mid2;
						b = v;
						break;
					case 4:
						r = mid1;
						g = m;
						b = v;
						break;
					case 5:
						r = v;
						g = m;
						b = mid2;
						break;
				}
			}
			
			return Color.FromArgb(255, (int)Convert.ToByte(r * 255.0f), (int)Convert.ToByte(r * 255.0f), (int)Convert.ToByte(b * 255.0f));

		} 
	}
}
