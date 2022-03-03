﻿using System;
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
		private int x;
		private int y;
		DBTool tool;

		public DB_Load_Page(int user_ID, PictureBox imageDisplay, RichTextBox textDisplay)
		{
			InitializeComponent();

			tool = new DBTool();
			this.User_ID = user_ID;
			this.ImageDisplay = imageDisplay;
			this.TextDisplay = textDisplay;
		}

		private void DB_Load_Page_Load(object sender, EventArgs e)
		{
			//Gets general data about the file, not including the file or the compression string.
			LinkedList<Saved_File_Data> data = tool.get_All_Files(User_ID);

			while (data.First != null)
			{
				Saved_File_Data temp = data.First.Value;
				ListViewItem test = new ListViewItem(temp.file_Name, 0); 
				test.SubItems.Add(temp.file_Type);
				test.SubItems.Add(Convert.ToString(temp.compressed_File_Size));
				test.SubItems.Add(Convert.ToString(temp.date_Of_Creation));
				File_Display_View.Items.Add(test);

				data.RemoveFirst();
			}
		}

		private void Load_File_Btn_Click(object sender, EventArgs e)
		{
			string selectedName = File_Display_View.SelectedItems[0].SubItems[0].Text;
			string selectedFileType = File_Display_View.SelectedItems[0].SubItems[1].Text;

			//Get file binary and compression string.
			string[] fileAndCompressionString = tool.get_Saved_File(User_ID, selectedName);

			switch (selectedFileType)
			{
				case "text":
					try
					{
						string decompressedText = decompressText(fileAndCompressionString[0], fileAndCompressionString[1]);
						TextDisplay.Text = decompressedText;
						TextDisplay.BringToFront();
						Close();
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex);
						MessageBox.Show("File could not be loaded");
					}
					break;

				case "image":
					//try
					//{
						Bitmap decompressedImage = decompressImage(fileAndCompressionString[0], fileAndCompressionString[1]);
						ImageDisplay.Image = decompressedImage;
						ImageDisplay.BringToFront();
						Close();
					//} 
					//catch (Exception exc)
					//{
						//Console.WriteLine(exc);
						//MessageBox.Show("File could not be loaded");
					//}

					break;

				default:
					break;
			}
		}

		private Bitmap decompressImage(string img, string compresionString)
		{
			//Get the important value from the compression string.
			//The length of the width/height binary strings.
			//And the length of the colour binary strings.
			string whLength = "";
			string cbLength = "";
			bool wh = true;

			for (int i = 0; i < compresionString.Length; i++)
			{
				if (compresionString[i] == '_')
				{
					wh = false;
					continue;
				}

				if (wh)
				{
					whLength += compresionString[i];
				}
				else
				{
					cbLength += compresionString[i];
				}
			}

			Console.WriteLine("whlength: " + whLength + " cbWidth: " + cbLength);

			//Get width and height of the bitmap and create a new bitmap of that size.
			string width = "";
			string height = "";

			int whLength_Int = Convert.ToInt32(whLength);

			for (int i = 0; i < whLength_Int; i++)
			{
				width += img[i];
			}

			for (int i = whLength_Int; i < whLength_Int * 2; i++)
			{
				height += img[i];
			}

			Console.WriteLine("Width: " + width + " height: " + height);
			Console.WriteLine("Width: " + binaryToDenary(width) + " height: " + binaryToDenary(height));

			Bitmap mapToDraw = new Bitmap(binaryToDenary(width), binaryToDenary(height));


			//Get the length of a colour group, and then use this to decoded the binary sequence.

			int binary_Length = binaryToDenary(cbLength);
			int counter = 1;
			int newline = 0;
			int colour = 0;
			y = 0;
			x = 0;

			string amountOfPixels = "";

			for (int i = whLength_Int * 2; i < img.Length; i++)
			{
				counter++;
				if (counter <= binary_Length - 2)
				{
					amountOfPixels += img[i];
				}
				else if (counter <= binary_Length - 1)
				{
					colour = img[i];
				}
				else
				{
					newline = img[i];
					counter = 1;
					addPixels(mapToDraw, binaryToDenary(amountOfPixels), colour, newline);

					amountOfPixels = "";
					counter = 1;
				}
			}

			Console.WriteLine("yes4");

			return mapToDraw;
		}

		private string decompressText(string text, string compressionString)
		{
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

		private void addPixels(Bitmap bm, int amountToAdd, int colourInt, int newline)
		{
			Color colour;
			if (colourInt == 0)
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

			if (newline == 1)
			{
				y++;
			}

		}

		private int binaryToDenary(string binary)
		{
			int denary = 0;
			double num = 0;

			for (int i = 0; i < binary.Length; i++)
			{
				if (binary[i] == '1')
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
