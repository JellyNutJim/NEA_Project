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
		DBTool tool;

		public DB_Load_Page(int User_ID, PictureBox imageDisplay, RichTextBox textDisplay)
		{
			InitializeComponent();
			tool = new DBTool();
			this.User_ID = User_ID;
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
					string decompressedText = decompressText(fileAndCompressionString[0], fileAndCompressionString[1]);
					Console.WriteLine(decompressedText);
					break;

				case "image":
					break;

				default:
					break;
			}
			
		}

		private void decompressImage(string img, string compresionString)
		{

		}

		private string decompressText(string text, string compresionString)
		{
			string decompressedText = "";
			LinkedList<string[]> letter_Data = new LinkedList<string[]>();
			//Create huffman table.
			string[] temp = new string[2];

			for (int i = 0; i < compresionString.Length; i++)
			{ 
				if (compresionString[i] == '\'')
				{
					temp[0] = Convert.ToString(compresionString[i + 1]);
					string binarySequence = "";
					i += 2;

					do
					{
						binarySequence += compresionString[i];
						if (i >= compresionString.Length - 1)
						{
							break;
						}
						i++;
					}
					while (compresionString[i] != '\'');
					//string[] temp = { character, binarySequence };
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

			Console.WriteLine(compresionString);
			Console.WriteLine(letterAndSequence[0][0]);
			Console.WriteLine(letterAndSequence[0][1]);
			Console.WriteLine(letterAndSequence[1][0]);
			Console.WriteLine(letterAndSequence[1][1]);
			Console.WriteLine(letterAndSequence[2][0]);
			Console.WriteLine(letterAndSequence[2][1]);

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
