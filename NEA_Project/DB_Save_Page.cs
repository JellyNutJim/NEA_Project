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
		private string fileType;
		private DBTool tool;
		private int User_ID;
		private string compressionString;

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
		}

		private void Save_File_Btn_Click(object sender, EventArgs e)
		{
			string requestedFileName = File_Name_Entry.Text;

			//If the file name contains no spaces, continue.
			if (checkFileNameFormat(requestedFileName))
			{
				//Call certain functions bassed on what file type the user has entered.
				switch (fileType)
				{
					case "txt":
						//Define the values that will be entered into the Saved_Files database table.
						//This includes the compressed text, as well as various pieces of data relating to that text.
						//One example being the compressionString -> this string is what will be used to decode the compressed text.
						string binary = compressText(Save_Text_Box.Text);
						string file_Name = requestedFileName;
						int originalSizeInBits = Save_Text_Box.Text.Length * 8;
						int compressedSizeInBits = binary.Length;
						DateTime dateOfCreation = DateTime.Now;

						//The add_New_File function returns a bool bassed on whether the query was completed successfully.
						if (tool.add_New_File(User_ID, file_Name, binary, "Text", compressionString, compressedSizeInBits, dateOfCreation))
						{
							MessageBox.Show($"File saved successfully.\nFile was compressed from {originalSizeInBits} bits to {compressedSizeInBits} bits");
						}
						else
						{
							MessageBox.Show("File could not be saved.");
						}
						Close();
						break;
					case "img":
						break;
					default:
						break;
				}

			}
			else
			{
				MessageBox.Show("Please do not include spaces in your file name.");
			}
		}

		//I will be using a huffman table/compresion algorithm to compress my text.
		private string compressText(string textToCompress)
		{
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

			//Sort the letters into order of frequency smallest to largest.
			//The following code involes using a bubble sort.
			LinkedList<letterData> sortedData = new LinkedList<letterData>();
			LinkedList<letterData> letterBinary = new LinkedList<letterData>();
			sortedData = bubbleSort(letterAndFreq);

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

			//Now we have created our huffman tree, we need to create binary codes for each of the letters.
			//Left is 0, right is 1.

			letterAndBinaryCode[] letAndBin = new letterAndBinaryCode[letterBinary.Count];
			int counter = 0;

			while (letterBinary.First != null)
			{
				letAndBin[counter] = new letterAndBinaryCode(letterBinary.First.Value.Character, letterBinary.First.Value.binaryCode);
				compressionString += $"'{letterBinary.First.Value.Character}'{letterBinary.First.Value.binaryCode}";
				letterBinary.RemoveFirst();
				counter++;
			}

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

		private void mergeSort(LinkedList<letterData> listToSort, int left, int right)
		{
			int mid;
			if (right > left)
			{
				mid = (right + left) / 2;
				mergeSort(listToSort, left, mid);
				mergeSort(listToSort, (mid + 1), right);

			}
		}

		//Checks if the requested file name contains any spaces.
		private bool checkFileNameFormat(string name)
		{
			foreach (char letter in name)
			{
				if (letter == ' ')
				{
					return false;
				}
			}
			return true;
		}
	}


}
