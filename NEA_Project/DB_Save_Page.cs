using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NEA_Project
{
	public partial class DB_Save_Page : Form
	{
		//If string is parsed as an argument then the 
		private DBTool tool;
		private string fileType;
		private string compressionString;
		private int User_ID;

		//If this constructor is called, we know the user is trying to save a text file.
		//The parameters are the text that we want to save, as well as the User_ID of the current user.
		//The ID is a parameter so that when the file is saved to the database (after compression) we know what user to save the file to.
		public DB_Save_Page(string textToSave, int User_ID)
		{
			InitializeComponent();

			this.User_ID = User_ID;
			Save_Text_Box.Text = textToSave;
			fileType = "txt";
		}

		//This constructor is called when the user is trying to save an image file.
		public DB_Save_Page(Image imageToSave, int User_ID)
		{
			InitializeComponent();

			this.User_ID = User_ID;
			Save_Text_Box.SendToBack();
			Save_Image_Box.Image = imageToSave;
			Save_Image_Box.SizeMode = PictureBoxSizeMode.Zoom;
			fileType = "img";
		}

		private void DB_Save_Page_Load(object sender, EventArgs e)
		{
			//On load we define new database tool and ask the user to name their file.
			File_Name_Entry.Text = "Please enter a file name";
			tool = new DBTool();
			Console.WriteLine($"Filetype is {fileType}");
		}

		private void Save_File_Btn_Click(object sender, EventArgs e)
		{
			string requestedFileName = File_Name_Entry.Text;

			//If the file name matches a certain set of requirements.
			if (checkFileNameFormat(requestedFileName))
			{
				//Call certain functions bassed on what file type the user has entered.
				string FileInBinary;
				int originalSizeInBits;
				int compressedFileSizeInBits;
				DateTime dateOfCreation = DateTime.Now;

				switch (fileType)
				{
					case "txt":
						//Define the values that will be entered into the Saved_Files database table.
						//This includes the compressed text, as well as various pieces of data relating to that text.
						//One example being the compressionString -> this string is what will be used to decode the compressed text.
						Console.WriteLine("Calling compressText");
						FileInBinary = compressText(Save_Text_Box.Text);
						originalSizeInBits = Save_Text_Box.Text.Length * 8;
						compressedFileSizeInBits = FileInBinary.Length;

						//The add_New_File function returns a bool bassed on whether the query was completed successfully.
						if (tool.add_New_File(User_ID, requestedFileName, "text", FileInBinary, compressionString, compressedFileSizeInBits, dateOfCreation))
						{
							MessageBox.Show($"File saved successfully.\nFile was compressed from {originalSizeInBits} bits to {compressedFileSizeInBits} bits\n(Not including metadata)");
						}
						else
						{
							MessageBox.Show("File could not be saved.");
						}

						Close();
						break;

					case "img":
						//Compress image into binary
						Console.WriteLine("Calling compressImage");
						FileInBinary = compressImage(Save_Image_Box.Image);
						originalSizeInBits = Save_Image_Box.Size.Width * Save_Image_Box.Image.Width * 8 * 3;
						compressedFileSizeInBits = FileInBinary.Length;

						//The compression string in this case, represents the length of one group of colours.

						if (tool.add_New_File(User_ID, requestedFileName, "image", FileInBinary, compressionString, compressedFileSizeInBits, dateOfCreation))
						{
							MessageBox.Show($"File saved successfully.\nFile was compressed from {originalSizeInBits} bits to {compressedFileSizeInBits} bits\n(Not including metadata)");
						}
						else
						{
							MessageBox.Show("File could not be saved.");
						}

						Close();
						break;

					default:
						break;
				}

			}
		}

		// ----------------------------------------------------------------------------------------------------------------------------------------- Image Compression

		private string compressImage(Image imgToCompress)
		{
			//When compressing this image there are a few things to take into account.
			// 1: It will be made up of only black and white pixels.
			// 2: It will contain writing.
			// 3: It must be stored as a binary string.

			//Taking all these factors into account I have decided to use a binary form of run length encoding.
			//I beleive this to be the best option as im only dealing with two colours which can be easiliy represented with either a 1 or a 0.
			//As image width can vary significantly, therefore, the maximum amount of repeating characters can also vary.
			//It wouldn't make sense to use the same static upper bound for every image as this could either be too small, or too large.
			//Too small and we would loose data on compression, too big and lots of space is wasted.
			//Therefore the system will detect the largest binary sequence (a sequence representing an amount of repeating pixels) and have that be the standard max.
			
			//Additionally two numbers representing the height and width will be placed at the start of the binary string.

			//This is an example of what a image with a 4k width in my binary compression form would look like:
			// Bits 1 - 14: Represent the number of a certain colour.
			// Bit 15:      Represents the colour of said pixel (1 = white, 0 = black)
			// Bit 16:      Represents a whether this pixel group is the last on a line. (0 = no, 1 = yes)

			//This system will also make it incrediby easy to decompress.

			//Setup the loading bar.
			Loading_Bar.Maximum = 4;
			Loading_Bar.Style = ProgressBarStyle.Blocks;
			Loading_Bar.Value = 0;

			Bitmap bitmapToCompress = new Bitmap(imgToCompress);
			LinkedList<String> tempBinaryHolder = new LinkedList<string>();
			string bitmapAsCompressedBinary = "", colour = "", newline = "0";
			int amountOfRepeats = 0;

			//The loading bar is not incremented during the while loop.
			//This is because it significantly reduces the speed of the program.
			Loading_Bar.Increment(1);

			//Checks each pixel along the x and y coordinates.
			//Line by line.
			for (int y = 0; y < bitmapToCompress.Height; y++)
			{
				for (int x = 0; x < bitmapToCompress.Width; x++)
				{
					newline = "0";
					amountOfRepeats = 0;

					//Check for white groups. --> I check for white first as the majority of pixels will be white.
					if (!isBlack(bitmapToCompress.GetPixel(x, y)))
					{
						colour = "0";
						amountOfRepeats++;

						//Continue until a pixel is black.
						do
						{
							amountOfRepeats++;
							x++;

							//Check if we have reached the end of a line but not the end of the file.
							if (x == bitmapToCompress.Width)
							{
								if (y == bitmapToCompress.Height - 1)
								{
									amountOfRepeats--;
									break;
								}
								amountOfRepeats--;
								newline = "1";
								break;
							}
						}
						while (!isBlack(bitmapToCompress.GetPixel(x, y)));
					}
					else
					{
						colour = "1";
						amountOfRepeats++;
						do
						{
							amountOfRepeats++;
							x++;

							//Check if we have reached the end of a line but not the end of the file.
							if (x == bitmapToCompress.Width)
							{
								if (y == bitmapToCompress.Height - 1)
								{
									amountOfRepeats--;
									break;
								}
								amountOfRepeats--;
								newline = "1";
								break;
							}
						}
						while (isBlack(bitmapToCompress.GetPixel(x, y)));
					}

					tempBinaryHolder.AddLast($"{convertToBinary(amountOfRepeats)}{colour}{newline}");
					Console.WriteLine(amountOfRepeats);
				}
			}

			Loading_Bar.Increment(1);

			//Add the width and height values to start of the compressed binary string.
			//This is less time efficent then just storing these values in the compression string, but it is more space efficient.
			int width = bitmapToCompress.Width;
			int height = bitmapToCompress.Height;

			compressionString += width + "_" + height;

			//Find the longest binary string, and add 0s to all the other strings so they are the same standard length.
			int maxLength = 0;

			//Defines maxlength to be the length of the sequence with the highest length.
			foreach (string sequence in tempBinaryHolder)
			{
				if (sequence.Length > maxLength)
				{
					maxLength = sequence.Length;
				}
			}

			//Increases the length of the other binary sequences to be equal to the longest sequence.
			//Then we add all of those binary sequence into the main string bitmapAsCompressedBinary.

			string tempSequence;
			
			while (tempBinaryHolder.First != null)
			{
				tempSequence = tempBinaryHolder.First.Value;

				while (tempSequence.Length < maxLength)
				{
					tempSequence = "0" + tempSequence;
				}
				bitmapAsCompressedBinary += tempSequence;

				tempBinaryHolder.RemoveFirst();
			}

			Loading_Bar.Increment(1);
			compressionString += "_" + Convert.ToString(maxLength);

			Console.WriteLine(bitmapAsCompressedBinary);

			return bitmapAsCompressedBinary;
		}

		//Converts an integer (Denary) into a binary sequence.
		private string convertToBinary(int num)
		{
			string binary = "";

			while (num > 0)
			{
				binary = Convert.ToString(num % 2) + binary;
				num /= 2;
			}

			return binary;
		}


		//MAKE INTO A STATIC FUNCTION IN A SEPERATE CLASS AS BG REMOVE ALSO USES THIS FUNCTION
		private bool isBlack(Color pixel)
		{
			if (pixel.GetBrightness() <= 0.2)
			{
				return true;
			}
			return false;
		}

		// ----------------------------------------------------------------------------------------------------------------------------------------- Text Compression

		//I will be using a huffman table/compresion algorithm to compress my text.
		private string compressText(string textToCompress)
		{
			//Setup the loading bar.
			Loading_Bar.Maximum = 5;
			Loading_Bar.Style = ProgressBarStyle.Blocks;
			Loading_Bar.Value = 0;


			//Create a frequency "table".
			//The table is actually a linked list that contains letterData object types.
			//Letter data contains values that can contain the character and its frequency.
			LinkedList<letterData> letterAndFreq = new LinkedList<letterData>();

			//Populate the linked list with every unique character and its frequency in textToCompress.
			foreach (char letter in textToCompress)
			{
				//The letter exists function returns a bool. If it returns false, then a new letter is added.
				//If the letter already exists then find that letter and increase the frequency by one.
				if (!letterExists(letterAndFreq, letter))
				{
					//The constructor takes the letter and the start frequency.
					letterData temp = new letterData(letter, 1);
					letterAndFreq.AddLast(temp);
				}
			}

			Loading_Bar.Increment(1);

			//Sort the letters into order of frequency smallest to largest.
			//The following code involes using a bubble sort.
			LinkedList<letterData> sortedData = new LinkedList<letterData>();
			LinkedList<letterData> letterBinary = new LinkedList<letterData>();
			sortedData = bubbleSort(letterAndFreq);
			Loading_Bar.Increment(1);

			int len = sortedData.Count();

			//Now a huffman tree can be created.
			
			while (len > 1)
			{
				//Console.WriteLine(len);
				//Get the first node from sorted data.
				letterData firstNode = sortedData.First();
				sortedData.RemoveFirst();

				letterData secondNode = sortedData.First();
				sortedData.RemoveFirst();

				//Hold letter values in seperate linked list.
				//This is done so we can fetch the binary codes to represent the letters.
				if (firstNode.branch == false)
				{
					letterBinary.AddLast(firstNode);
				}

				if (secondNode.branch == false)
				{
					letterBinary.AddLast(secondNode);
				}

				int cumulativeFrequency = firstNode.Frequency + secondNode.Frequency;

				letterData freqNode = new letterData(cumulativeFrequency);
				freqNode.leftNode = firstNode;
				freqNode.rightNode = secondNode;

				freqNode.leftNode.addBit("0");
				freqNode.rightNode.addBit("1");

				//Place node back into linked list.
				bool placed = false;

				LinkedList<letterData> tempData = new LinkedList<letterData>();

				while (sortedData.First != null)
				{
					letterData temp = sortedData.First();
					tempData.AddLast(temp);
					if (sortedData.First.Value.Frequency >= freqNode.Frequency && placed == false)
					{
						placed = true;
						tempData.AddLast(freqNode);
					}
					sortedData.RemoveFirst();
				}

				if (!placed)
				{
					tempData.AddLast(freqNode);
				}


				//Place the temp data back into sortedData.
				foreach (letterData l in tempData)
				{
					sortedData.AddLast(l);
				}

				//Testing writelines.
				/*foreach (letterData l in sortedData)
				{
					Console.WriteLine("char: " + l.Character + " freq: " + l.Frequency + " len: " + sortedData.Count());
				}*/

				//Get the new length of sortedData.
				len = sortedData.Count();
			}

			Loading_Bar.Increment(1);

			//Now we have created our huffman tree, we need to create binary codes for each of the letters.
			//Left is 0, right is 1.

			letterAndBinaryCode[] letAndBin = new letterAndBinaryCode[letterBinary.Count];
			int counter = 0;

			while (letterBinary.First != null)
			{
				letAndBin[counter] = new letterAndBinaryCode(letterBinary.First.Value.Character, letterBinary.First.Value.binaryCode);
				compressionString += $"'{letterBinary.First.Value.Character}{letterBinary.First.Value.binaryCode}";
				letterBinary.RemoveFirst();
				counter++;
			}

			Loading_Bar.Increment(1);

			//We now have a table of out letters and their new binary substitutes.
			string textInBinary = "";
			
			foreach (char character in textToCompress)
			{
				for (int i = 0; i < letAndBin.Length; i++)
				{
					if (character == letAndBin[i].character)
					{
						textInBinary += letAndBin[i].binary;
						break;
					}
				}
			}

			Loading_Bar.Increment(1);
			return textInBinary;
		}

		//Checks if a letter is already present within the table.
		private bool letterExists(LinkedList<letterData> letters, char character)
		{
			foreach (letterData letterData in letters)
			{
				if (character == letterData.Character)
				{
					letterData.Frequency += 1;
					return true;
				}
			}
			return false;
		}

		//Takes in a linked list of letterData and returns an array of characters in ascending order of frequency.
		private LinkedList<letterData> bubbleSort(LinkedList<letterData> listToSort)
		{
			LinkedList<letterData> sortedLL = new LinkedList<letterData>();
			int length = listToSort.Count();
			letterData[] orderedLetters = new letterData[length];

			//Convert listToSort into an array that will be used for sorting.
			int counter = 0;

			foreach (letterData l in listToSort)
			{
				orderedLetters[counter] = l;
				counter++;
			}

			//Replace sort type??
			for (int i = 0; i < length - 1; i++)
			{
				for (int p = 0; p < length - i - 1; p++)
				{
					if (orderedLetters[p].Frequency > orderedLetters[p + 1].Frequency)
					{
						letterData temp = orderedLetters[p];
						orderedLetters[p] = orderedLetters[p + 1];
						orderedLetters[p + 1] = temp;
					}
				}
			}

			foreach (letterData l in orderedLetters)
			{
				sortedLL.AddLast(l);
			}

			return sortedLL;
		}


		//Checks if the requested file name contains any spaces and whether the name is already used.
		private bool checkFileNameFormat(string name)
		{
			if (!generalFunctions.checkFileNameValid(name))
			{
				return false;
			}

			//Checks the names off all files associated with this user and them compares the requested name to these exising names.
			//If the requested name matches a name aleady present, then return false, as a unique name is required to distinguish files.
			foreach (string file_Name in tool.check_Table_For_Values($"SELECT File_Name FROM Saved_Files WHERE User_ID = {User_ID}"))
			{
				Console.WriteLine(file_Name);

				if (name == file_Name)
				{
					MessageBox.Show("This file name already exists.\nPlease choose a different name.");
					return false;
				}
			}

			return true;
		}
	}
}
