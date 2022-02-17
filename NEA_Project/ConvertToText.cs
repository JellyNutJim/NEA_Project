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

		//If pixel is black, get joined pixels.
		//create grid arround letter by getting the highest, lowest, most right, and most left pixel.
		//Create new img or bitmap from new grid.
		//Additionally measure distance between characters to calculate where words end/start.
		//Get normal distance between characters, when a distance longer than expected is found, it is most likely a space.
	}

	//Each object represents a pixel that makes up a letter.
	class letterPixels
	{
		//The color of this pixel. --------------------------------------------- should probably delete?????
		public Color px;
		public string origin;

		//Represents the pixels surrounding this pixel.
		public letterPixels l, r, u, d;
		
		//The position of the pixel.
		public int x;
		public int y;

		public letterPixels(Color input_Colour, string origin, int x, int y)
		{
			this.px = input_Colour;
			this.origin = origin;
			this.x = x;
			this.y = y;
		}
	}

	//Breaks down a bitmap into several letter components.
	class createLetter
	{
		public Bitmap input_Image;
		public LinkedList<Bitmap> letters;
		int input_Image_Height;
		int input_Image_Width;

		//Constructor, gets input bitmap and defines the height and width variables.
		//Addtionally a linked list that holds bitmaps is defined.
		//This linked list will contain the individual letter bitmaps.

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
			//Start and end Y coordinates of the letter.
			//Can be used to calculate height of the letter.
			int endY;
			int startY;

			//Search along x, then y
			for (int y = 0; y < input_Image_Height; y++)
			{
				for (int x = 0; x < input_Image_Width; x++)
				{
					//Gets the color of a pixel at postion (x, y);
					Color pixel = input_Image.GetPixel(x, y);

					//If the pixel is black, then a new letter has been detected.
					if (CheckBlack(pixel) == true)
					{
						//Check all 8 possible pixels arround it, then check those.
						//Build up a grid of the letter.
						int pixelsInLetter = 0;
						int letterLength = 0;
						endY = 1;

						//Console.WriteLine("X: " + x);
						//Console.WriteLine("Y: " + y);

						//Define the first letterPixels object, and set its x and y to the first black pixels location.
						//This object will be redfined over time, so we save this first letterPixel to  to variable initialOrigin.
						//Therefore we can store the original coordinates of the pixel for later use.
						letterPixels px = new letterPixels(pixel, "o", x, y);
						letterPixels initialOrigin = px;
						startY = initialOrigin.x;

						//After checking and creating a new obeject to represent a pixel I set the pixel colour to white.
						//Doing this esentially tells the program that this pixel has already been checked, and should not be re-checked.
						//Therefore stopping any looping of pixels that my occur. 
						//Setting pixels to white will be used througout the pixel detecting loop.
						input_Image.SetPixel(x, y, Color.FromArgb(255, 255, 255));

						//This linked list contains letterPixel objects that need to be checked.
						LinkedList<letterPixels> pxToCheck = new LinkedList<letterPixels>();
						pxToCheck.AddLast(px);

						//While the linked list is not empty, the loop will repeat.
						while (pxToCheck.First != null)
						{
							//Gets the first object from the linked list.
							letterPixels pxs = pxToCheck.First.Value;

							//Checks the left pixel of the current object.
							if (CheckBlack(input_Image.GetPixel(pxs.x - 1, pxs.y)))
							{
								pixelsInLetter++;
								//Console.WriteLine("l");
								
								//If a new lowest y is detected, save the value to lowestY.
								if (pxs.y > lowestY)
								{
									lowestY = pxs.y;
									letterLength++;
								}
								///Defines the object to the left of pxs.
								pxs.l = new letterPixels(Color.FromArgb(0, 0, 0), "r", pxs.x - 1, pxs.y);

								//Add this new object to the linked list of objects to check.
								letterPixels tempLP = pxs.l;
								pxToCheck.AddLast(tempLP);

								//Set checked pixel to white.
								input_Image.SetPixel(pxs.x - 1, pxs.y, Color.FromArgb(255, 255, 255));
							}

							//The following check if statements function the same as check left.
							//They are just checking different coordinates.

							//Check Right
							if (CheckBlack(input_Image.GetPixel(pxs.x + 1, pxs.y)))
							{
								pixelsInLetter += 1;
								//Console.WriteLine("r");

								if (pxs.y > lowestY)
								{
									lowestY = pxs.y;
									letterLength++;
								}

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

								if (pxs.y > lowestY)
								{
									lowestY = pxs.y;
									letterLength++;
								}

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
						letterLength *= 2; //Set to endY - Start Y ----------------------------------------------------------------------------

						//Create a new bitmap for the letter.
						Bitmap newLetter = new Bitmap(letterLength * 2, letterLength * 2);
						int startX = letterLength / 2;
						int startY = letterLength / 2;

						//Set the postion of the first pixel to be drawn.
						initialOrigin.x = startX;
						initialOrigin.y = startY;

						//Define colours.
						Color black = Color.FromArgb(255, 0, 0, 0);
						Color empty = Color.FromArgb(0, 0, 0, 0);

						//Draw the first pixel onto the new bitmap.
						newLetter.SetPixel(startX, startY, initialOrigin.px);

						//Create new linked list that will run through all existing letterPixel objects for this letter.
						LinkedList<letterPixels> nextPx = new LinkedList<letterPixels>();
						nextPx.AddLast(initialOrigin);

						while (nextPx.First != null)
						{
							letterPixels write = nextPx.First.Value; //Change variable name ---------------------------------------------------
							//Console.WriteLine("X: " + write.x + " Y: " + write.y);

							//Check the object left of write. 
							//If the object exists, then check if the colour is empty or not.
							//Empty represent not black.
							if (write.l != null)
							{
								if (write.l.px != empty)
								{
									//Set a new pixel to the left of write.
									newLetter.SetPixel(write.x - 1, write.y, black);
									//Redefine the position of the object left of write.
									write.l.px = empty;
									write.l.x = write.x - 1;
									write.l.y = write.y;
									//Add this object to the linked list.
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
					}
				}
			}
		}

		//Returns true if a pixel is black (has a brightness of less than 0.2)
		//Normally this would not necerseraly return true only on black colors, but there are only black and white colors present on the new page,------------- spell check
		public bool CheckBlack(Color pixel)
		{
			if (pixel.GetBrightness() <= 0.2)
			{
				return true;
			}
			return false;
		}
	}
}
