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
		//The color of this pixel.
		//The actual colour is not super important, it is mainly used to allow the program to check whether a pixel on a bitmap
		//has already been checked or not.
		public Color px;

		//Represents the pixels surrounding this pixel.
		//
		public letterPixels l, r, u, d;
		
		//The position of the pixel relative to the bitmap its from.
		public int x;
		public int y;
		
		//The constructor is used to define all the values in this specific object.
		public letterPixels(Color input_Colour, int x, int y)
		{
			this.px = input_Colour;
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
			int endX;
			int startY;
			int startX;

			//Variable used to check when a new line of text has been found.
			int checkY = 0;

			//Search along x, then y
			for (int y = 0; y < input_Image_Height; y++)
			{

				for (int x = 0; x < input_Image_Width; x++)
				{
                    //Gets the color of a pixel at postion (x, y);
                    //Console.WriteLine(y + " " + input_Image_Height);
                    //Console.WriteLine(x + " " + input_Image_Width);
 					Color pixel = input_Image.GetPixel(x, y);

					//If the pixel is black, then a new letter has been detected.
					if (CheckBlack(pixel) == true)
					{
						//Check all 8 possible pixels arround it, then check those.
						//Build up a grid of the letter.
						int pixelsInLetter = 0;

						//Define the start and end x,y values of the box that the letter exists within.
						startX = x;
						endX = x;
						startY = y;
						endY = y;

						//Console.WriteLine("X: " + x);
						//Console.WriteLine("Y: " + y);

						//Define the first letterPixels object, and set its x and y to the first black pixels location.
						//This object will be redfined over time, so we save this first letterPixel to  to variable initialOrigin.
						//Therefore we can store the original coordinates of the pixel for later use.
						letterPixels px = new letterPixels(pixel, x, y);
						letterPixels initialOrigin = px;

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

							//If the current letterPixels x coordinate is behind the current startX value.
							//Set startX to the current X.
							if (pxs.x < startX)
							{
								startX = pxs.x;
							}

							//Does the same, but For the Y corrdinate.
							if (pxs.y < startY)
							{
								startY = pxs.y;
							}

							//Checks the left pixel of the current object.
							if (CheckBlack(input_Image.GetPixel(pxs.x - 1, pxs.y)))
							{
								pixelsInLetter++;
								//Console.WriteLine("l");

								///Defines the object to the left of pxs.
								pxs.l = new letterPixels(Color.FromArgb(0, 0, 0), pxs.x - 1, pxs.y);

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

								if (pxs.x >= endX)
								{
									endX = pxs.x + 1;
								}

								pxs.r = new letterPixels(Color.FromArgb(0, 0, 0), pxs.x + 1, pxs.y);
								letterPixels tempLP = pxs.r; 
								pxToCheck.AddLast(tempLP);
								input_Image.SetPixel(pxs.x + 1, pxs.y, Color.FromArgb(255, 255, 255));
							}

                            //Check Up
                            if (CheckBlack(input_Image.GetPixel(pxs.x, pxs.y - 1)))
							{
								pixelsInLetter += 1;
								//Console.WriteLine("u");

								pxs.u = new letterPixels(Color.FromArgb(0, 0, 0), pxs.x, pxs.y - 1);
								letterPixels tempLP = pxs.u;
								pxToCheck.AddLast(tempLP);
								input_Image.SetPixel(pxs.x, pxs.y - 1, Color.FromArgb(255, 255, 255));
							}

                            //Check Down
                            if (CheckBlack(input_Image.GetPixel(pxs.x, pxs.y + 1)))
							{
								pixelsInLetter += 1;
								//Console.WriteLine("d");

								if (pxs.y >= endY)
								{
									endY = pxs.y + 1;
								}

								pxs.d = new letterPixels(Color.FromArgb(0, 0, 0), pxs.x, pxs.y + 1);
								letterPixels tempLP = pxs.d;
								pxToCheck.AddLast(tempLP);
								input_Image.SetPixel(pxs.x, pxs.y + 1, Color.FromArgb(255, 255, 255));
                            }

							//Potential error reducing code.
                            /*else
                            {
                                for (int i = 1; i < 6; i++)
                                {
                                    if (CheckBlack(input_Image.GetPixel(pxs.x, pxs.y + 1 + i)))
                                    {
                                        pxs.d = new letterPixels(Color.FromArgb(0, 0, 0), pxs.x, pxs.y + 1 + i);
                                        letterPixels tempLP = pxs.d;
                                        pxToCheck.AddLast(tempLP);
                                        input_Image.SetPixel(pxs.x, pxs.y + 1, Color.FromArgb(255, 255, 255));
                                    }
                                }
                            }*/

							pxToCheck.RemoveFirst();
						}

                        //Remove small defects.
						if (pixelsInLetter < 5)
						{
							break;
						}

                        ////////////////----------------------------------------------------- add correct order detecting

						//Reconstruct the letter using the newly created objects.

						//Use start and end x/y values to create a new bitmap that exactly fits the given letter.
						//Also define newx/y values which determine the posistion of the first pixel to be drawn.
						int letterWidth = endX - startX;
						int newX = x - startX;

						int letterHeight = endY - startY;
						int newY = y - startY;

						//Creates thep bitmap for the new letter.
						//We add one to both letterWidth and letterHeight as the  
						Bitmap newLetter = new Bitmap(letterWidth + 1, letterHeight + 1);

						//Set the postion of the first pixel to be drawn.
						initialOrigin.x = newX;
						initialOrigin.y = newY;

						//Testing writelines.
						//Console.WriteLine("StartX: " + startX +  " EndX: " + endX);
						//Console.WriteLine("Start Y: " + startY + " endY: " + endY);
						//Console.WriteLine("X: " + x + " Y: " + y);
						//Console.WriteLine("newX: " + newX + " newY: " + newY);
						//Console.WriteLine("LetterH: " + letterHeight + " letterW: " + letterWidth);

						//Define colours.
						Color black = Color.FromArgb(255, 0, 0, 0);
						Color empty = Color.FromArgb(0, 0, 0, 0);

						//Draw the first pixel onto the new bitmap.
						newLetter.SetPixel(newX, newY, initialOrigin.px);

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

						//If y has been iterated
						if (y > checkY)
						{
							y += Convert.ToInt32(letterHeight * 0.6);
							checkY = y;

                            if (y > input_Image_Height)
                            {
                                y = input_Image_Height - 1;
                            }

						}

						letters.AddLast(newLetter);
					} 
				}
			}
		}

		//Returns true if a pixel is black (has a brightness of less than 0.2)
		//Normally this would not necessarily return true for only black colors, but as there are only black and white colors present on the new page this is not a problem.
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
