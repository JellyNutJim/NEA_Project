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
	public partial class DB_Load_Page : Form
	{
		private int User_ID;
		private PictureBox ImageDisplay;
		private RichTextBox TextDisplay;
		private ComboBox comboReturn;
		private int x;
		private int y;
		DBTool tool;

		//The constructor.
		//The User_ID is used so that the user is only shown their own files.
		//The imageDisplay and textDisplay are both form elements that can be used to display the users chosen file.
		//As we do not know what type of file the user is going to try load, we need both form elements just in case.
		public DB_Load_Page(int user_ID, PictureBox imageDisplay, RichTextBox textDisplay, ComboBox comboReturn)
		{
			InitializeComponent();

			//Define a new database tool that will be used to fetch all files associated with one user. 
			tool = new DBTool();
			this.User_ID = user_ID;
			this.ImageDisplay = imageDisplay;
			this.TextDisplay = textDisplay;
			this.comboReturn = comboReturn;
		}

		//Called after the load page has loaded.
		private void DB_Load_Page_Load(object sender, EventArgs e)
		{
			//Gets general data about the file, not including the file or the compression string as this would just be wasted processing.
			LinkedList<Saved_File_Data> data = tool.get_All_Files(User_ID);
			
			//While the data linkedlist is not empty,
			while (data.First != null)
			{
				//Gets the first object in the data linkedlist.
				Saved_File_Data temp = data.First.Value;

				//Creates a new object of listItemView.
				//This object can be inserted into a List_Viewer form object as a new section of the table.
				ListViewItem test = new ListViewItem(temp.file_Name, 0); 

				//I then add subitems that cannot be selected by the user, but will show information such as file type with the file.
				test.SubItems.Add(temp.file_Type);
				test.SubItems.Add(Convert.ToString(temp.compressed_File_Size));
				test.SubItems.Add(Convert.ToString(temp.date_Of_Creation));

				//Add this new item to the List_Viewer form element.
				File_Display_View.Items.Add(test);

				//Remove the first item in the list, essentially allowing us to increment throught it.
				data.RemoveFirst();
			}
		}

		//Called when the load file button is clicked.
		private void Load_File_Btn_Click(object sender, EventArgs e)
		{
			Console.WriteLine("Load_File_Btn_Click has been called");

			//Gets the currently selected file based on what the user has currently highlighted on the File_Display_View.
			//We get the selected name to know what file we should search for when querying the database.
			//And we get the file type so we know what decompression algorithm to run, and how to return it to the main page.
			string selectedName = File_Display_View.SelectedItems[0].SubItems[0].Text;
			string selectedFileType = File_Display_View.SelectedItems[0].SubItems[1].Text;

			//Get the file binary and compression string.
			string[] fileAndCompressionString = tool.get_Saved_File(User_ID, selectedName);

			switch (selectedFileType)
			{
				case "text":
					try
					{
						//Decompress the string using the compression string.
						string decompressedText = decompressText(fileAndCompressionString[0], fileAndCompressionString[1]);

						//Assign the text_Diplay form element on the main page to the now decompressed string.
						TextDisplay.Text = decompressedText;
						TextDisplay.BringToFront();
						comboReturn.Text = "Text File";

						//Close the load page display.
						Close();
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex);
						MessageBox.Show("File could not be loaded");
					}
					break;

				case "image":
					try
					{
						Bitmap decompressedImage = decompressImage(fileAndCompressionString[0], fileAndCompressionString[1]);
						ImageDisplay.Image = decompressedImage;
						ImageDisplay.BringToFront();
						ImageDisplay.SizeMode = PictureBoxSizeMode.Zoom;
						comboReturn.Text = "Single image";
						Close();
					} 
					catch (Exception exc)
					{
						Console.WriteLine(exc);
						MessageBox.Show("File could not be loaded");
					}

					break;

				default:
					break;
			}
		}

		private Bitmap decompressImage(string img, string compresionString)
		{
			Console.WriteLine(img);

			//Get the important value from the compression string.
			//The length of the width/height binary strings.
			//And the length of the colour binary strings.
			string imgWidth = "";
			string imgHeight = "";
			string sequenceLength = "";

			//Represents what part of the compression string is currently being accesed.
			int compressionStringComponent = 1;

			//Get the following values from the compression string:
			// The width of the image
			// The height of the image
			// The max length of each pixel binary sequence
			for (int i = 0; i < compresionString.Length; i++)
			{
				//When a '_' is found, we know the end of a number has been reached.
				if (compresionString[i] == '_')
				{
					compressionStringComponent += 1;
				}
				else
				{
					switch (compressionStringComponent)
					{
						case 1:
							imgWidth += compresionString[i];
							break;
						case 2:
							imgHeight += compresionString[i];
							break;
						case 3:
							sequenceLength += compresionString[i];
							break;
					}
				}
			}

			//Display split compression string.
			//Console.WriteLine("width: " + imgWidth + " height: " + imgHeight + " length: " + sequenceLength);

			//Define imgHeight, imgWidth and sequenceLength as integers.
			//Get the length of a colour group, and then use this to decoded the binary sequence.
			string amountOfPixels = "";
			int width = Convert.ToInt32(imgWidth);
			int height = Convert.ToInt32(imgHeight);
			int binary_Length = Convert.ToInt32(sequenceLength);
			int sequencePosition = 0;
			char newline;
			char colour;
			y = 0;
			x = 0;

			Console.WriteLine(binary_Length + " ------------------------------");

			//Create a new bitmap of the correct size.
			Bitmap mapToDraw = new Bitmap(width, height);


			for (int i = 0; i < img.Length / binary_Length; i++)
			{
				//Represents a small length of binary that includes the length of a repeated colour, the colour and whether its a new line.
				string sequence = "";
				amountOfPixels = "";
				for (int p = 0; p < binary_Length; p++)
				{
					sequence += img[sequencePosition];
					sequencePosition++;
				}

				//Define the sequences colour and whether its the end of a line.
				newline = sequence[binary_Length - 1];
				colour = sequence[binary_Length - 2];

				//Define the amount of pixels to be drawn.
				for (int a = 0; a < binary_Length - 2; a++)
				{
					amountOfPixels += sequence[a];
				}

				//Console.WriteLine("num of px: " + binaryToDenary(amountOfPixels));

				Console.WriteLine("Len: " + binaryToDenary(amountOfPixels) + " Colour: " + colour + " NewLine: " + newline);

				mapToDraw = addPixels(mapToDraw, binaryToDenary(amountOfPixels), colour, newline);
			}

			return mapToDraw;
			
		}

		private string decompressText(string text, string compressionString)
		{
			Console.WriteLine("Text: " + text);
			Console.WriteLine("CS: " + compressionString);
			string decompressedText = "";
			LinkedList<string[]> letter_Data = new LinkedList<string[]>();
			//Create huffman table.
			string character;
			string tempSequence;
			int len = compressionString.Length;

			for (int i = 0; i < len; i++)
			{ 
				if (compressionString[i] == '\'')
				{
					character = Convert.ToString(compressionString[i + 1]);
					tempSequence = "";
					i += 2;

					while (i < len && compressionString[i] != '\'')
					{
						tempSequence += compressionString[i];
						i++;
					}
					i--;

					string[] temp = { character, tempSequence };
					letter_Data.AddFirst(temp);
				}
			}

			string[][] letterAndSequence = new string[letter_Data.Count][];
			int counter = 0;

			while (letter_Data.First != null)
			{
				letterAndSequence[counter] = letter_Data.First.Value;
				letter_Data.RemoveFirst();
				counter++;
			}

			string  tempBinary = "";

			//Replace with huffman tree.
			foreach (char binary in text)
			{
				tempBinary += binary;
				for (int i = 0; i < letterAndSequence.Length; i++)
				{
					if (tempBinary == letterAndSequence[i][1])
					{
						decompressedText += letterAndSequence[i][0];
						tempBinary = "";
					}
				}
			}

			return decompressedText;
		}

		private Bitmap addPixels(Bitmap bm, int amountToAdd, char colourChar, char newline)
		{
			Color colour;
			if (colourChar == '1')
			{
				colour = Color.FromArgb(255, 0, 0, 0);
			}
			else
			{
				colour = Color.FromArgb(255, 255, 255, 255);
			}


			for (int i = 0; i < amountToAdd; i++)
			{
				bm.SetPixel(x, y, colour);
				x++;
			}

			if (newline == '1')
			{
				y++;
				x = 0;
			}

			return bm;
		}

		//Converts a binary sequence into a denary integer.
		private int binaryToDenary(string binary)
		{
			string reversedBinary = "";
			for (int i = 0; i < binary.Length; i++)
			{
				reversedBinary += binary[binary.Length - 1 - i];
			}

			int denary = 0;
			double num = 0;

			for (int i = 0; i < reversedBinary.Length; i++)
			{
				if (reversedBinary[i] == '1')
				{
					num += Math.Pow(2, i);
				}
			}

			//It's more efficient to use a double and during the for statement, and convert to int afterwards than it would be to use an int,
			//and therefore convert to an int each time it loops
			denary = Convert.ToInt32(num);

			return denary;
		}

	}

	class Saved_File_Data
	{
		public string file_Name;
		public string file_Type;
		public int compressed_File_Size;
		public DateTime date_Of_Creation;

		public Saved_File_Data(string File_Name, string File_Type, int Compressed_File_Size, DateTime Date_Of_Creation)
		{
			file_Name = File_Name;
			file_Type = File_Type;
			compressed_File_Size = Compressed_File_Size;
			date_Of_Creation = Date_Of_Creation;
		}
	}

}
