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
		/*public static void breakDownSentance(Bitmap input_image)
		{
			//If pixel is black, get joined pixels.
			//create grid arround letter by getting the highest, lowest, most right, and most left pixel.
			//Create new img or bitmap from new grid.

			//Additionally measure distance between characters to calculate where words end/start.
			//Get normal distance between characters, when a distance longer than expected is found, it is most likely a space.

			//Color[] pixels = BackgroundEdit.GetAllPixels(input_image);

			for (int y = 0; y < input_image.Height; y++)
			{
				for (int x = 0; x < input_image.Width; x++, i++)
				{
					if (CheckBlack(pixels[x, y]))
					{
						//Check all 8 possible pixels arround it, then check those.
						//Build up a grid of the letter.



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
			if (pixel.GetBrightness < 0.1)
			{
				return true;
			}
			return false;
		}*/
	}

	class createLetter
	{
		public Bitmap input_Image;
		public LinkedList<Bitmap> letters;
		int input_Image_Height;
		int input_Image_Width;




		public createLetter(Bitmap user_Image)
		{
			input_Image = user_Image;
			input_Image_Height = input_Image.Height;
			input_Image_Width = input_Image.Width;
			letters = new LinkedList<Bitmap>();
			getLetter();

		}

		public void getLetter()
		{

			//Search along x, then y
			for (int y = 0; y < input_Image_Height; y++)
			{
				for (int x = 0; x < input_Image_Width; x++)
				{
					Color pixel = input_Image.GetPixel(x, y);

					if (CheckBlack(pixel) == true)
					{
						//Check all 8 possible pixels arround it, then check those.
						//Build up a grid of the letter.
						int pixelsInLetter = 0;
						int lowestY = 1;
						int letterLength = 0;

						//Console.WriteLine("X: " + x);
						//Console.WriteLine("Y: " + y);

						letterPixels px = new letterPixels(pixel, "o", x, y);
						letterPixels initialOrigin = px;
						input_Image.SetPixel(x, y, Color.FromArgb(255, 255, 255)); //Set pixel to white

						LinkedList<letterPixels> pxToCheck = new LinkedList<letterPixels>();
						pxToCheck.AddLast(px);

						while (pxToCheck.First != null)
						{
							letterPixels pxs = pxToCheck.First.Value;
							//Console.WriteLine("X: " + pxs.x + " Y: " + pxs.y);

							//Check Left
							if (CheckBlack(input_Image.GetPixel(pxs.x - 1, pxs.y)))
							{
								pixelsInLetter += 1;
								//Console.WriteLine("l");
								pxs.l = new letterPixels(Color.FromArgb(0, 0, 0), "r", pxs.x - 1, pxs.y);
								letterPixels tempLP = pxs.l;
								pxToCheck.AddLast(tempLP);
								input_Image.SetPixel(pxs.x - 1, pxs.y, Color.FromArgb(255, 255, 255));
							}

							//Check Right
							if (CheckBlack(input_Image.GetPixel(pxs.x + 1, pxs.y)))
							{
								pixelsInLetter += 1;
								//Console.WriteLine("r");
								pxs.r = new letterPixels(Color.FromArgb(0, 0, 0), "l", pxs.x + 1, pxs.y);
								letterPixels tempLP = pxs.r; 
								pxToCheck.AddLast(tempLP);
								input_Image.SetPixel(pxs.x + 1, pxs.y, Color.FromArgb(255, 255, 255));
							}

							//Check Up
							if (CheckBlack(input_Image.GetPixel(pxs.x, pxs.y - 1)))
							{
								pixelsInLetter += 1;
								//Console.WriteLine("u");
								pxs.u = new letterPixels(Color.FromArgb(0, 0, 0), "d", pxs.x, pxs.y - 1);
								letterPixels tempLP = pxs.u;
								pxToCheck.AddLast(tempLP);
								input_Image.SetPixel(pxs.x, pxs.y - 1, Color.FromArgb(255, 255, 255));
							}

							//Check Down
							if (CheckBlack(input_Image.GetPixel(pxs.x, pxs.y + 1)))
							{
								pixelsInLetter += 1;
								//Console.WriteLine("d");

								if (pxs.y > lowestY)
								{
									lowestY = pxs.y;
									letterLength++;
								}

								pxs.d = new letterPixels(Color.FromArgb(0, 0, 0), "u", pxs.x, pxs.y + 1);
								letterPixels tempLP = pxs.d;
								pxToCheck.AddLast(tempLP);
								input_Image.SetPixel(pxs.x, pxs.y + 1, Color.FromArgb(255, 255, 255));
							}

							pxToCheck.RemoveFirst();
						}

						//Reconstruct letter from objects
						letterLength *= 2;

						Bitmap newLetter = new Bitmap(letterLength * 2, letterLength * 2);
						int startX = letterLength / 2;
						int startY = letterLength / 2;
						initialOrigin.x = startX;
						initialOrigin.y = startY;

						Color black = Color.FromArgb(255, 0, 0, 0);
						Color empty = Color.FromArgb(0, 0, 0, 0);

						newLetter.SetPixel(startX, startY, initialOrigin.px);

						LinkedList<letterPixels> nextPx = new LinkedList<letterPixels>();

						nextPx.AddLast(initialOrigin);

						while (nextPx.First != null)
						{
							letterPixels write = nextPx.First.Value;
							//Console.WriteLine("X: " + write.x + " Y: " + write.y);

							//Check Left

							if (write.l != null)
							{
								if (write.l.px != empty)
								{
									newLetter.SetPixel(write.x - 1, write.y, black);
									write.l.px = empty;
									write.l.x = write.x - 1;
									write.l.y = write.y;
									nextPx.AddLast(write.l);
								}
							}

							//Check Right
							if (write.r != null)
							{
								if (write.r.px != empty)
								{
									newLetter.SetPixel(write.x + 1, write.y, black);
									write.r.px = empty;
									write.r.x = write.x + 1;
									write.r.y = write.y;
									nextPx.AddLast(write.r);
								}
							}

							//Check Up
							if (write.u != null)
							{
								if (write.u.px != empty)
								{
									newLetter.SetPixel(write.x, write.y - 1, black);
									write.u.px = empty;
									write.u.x = write.x;
									write.u.y = write.y - 1;
									nextPx.AddLast(write.u);
								}
							}

							//Check Down
							if (write.d != null)
							{
								if (write.d.px != empty)
								{
									newLetter.SetPixel(write.x, write.y + 1, black);
									write.d.px = empty;
									write.d.x = write.x;
									write.d.y = write.y + 1;
									nextPx.AddLast(write.d);
								}
							}

							nextPx.RemoveFirst();
						}

						letters.AddLast(newLetter);
						
						//Create new letter
					}
					
				}
			}
		}


		public bool CheckBlack(Color pixel)
		{
			if (pixel.GetBrightness() <= 0.2)
			{
				return true;
			}
			return false;
		}

	}

	class letterPixels
	{
		public Color px;
		public string origin;
		public letterPixels l, r, u, d;
		public int x;
		public int y;
		public letterPixels(Color input_Colour, string origin, int x, int y)
		{
			this.px = input_Colour;
			this.origin = origin;
			this.x = x;
			this.y = y;

			//Set previous

			/*switch (fromDirection)
			{
				default:
					break;
			}*/

		}
	}



}
