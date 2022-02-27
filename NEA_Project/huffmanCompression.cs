using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Drawing.Text;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace NEA_Project
{
	class letterData
	{
		private char character;
		private int frequency;
		private string[] data = new string[2];

		public letterData leftNode;
		public letterData rightNode;

		//Represents a binary bit (0 or 1).
		public string binaryCode = "";
		public bool branch;

		public letterData(char character, int frequency)
		{
			this.character = character;
			this.frequency = frequency;
			this.branch = false;

			data[0] = Convert.ToString(character);
			data[1] = Convert.ToString(frequency);
		}

		public letterData(int cumulativeFrequency)
		{
			this.frequency = cumulativeFrequency;
			branch = true;
		}

		public void addBit(string bit)
		{
			this.binaryCode = bit + binaryCode;

			if (leftNode != null)
			{
				leftNode.addBit(bit);
			}

			if (rightNode != null)
			{
				rightNode.addBit(bit);
			}

		}

		public int Frequency
		{
			get { return frequency;  }
			set
			{ 
				frequency = value;
				data[1] = Convert.ToString(frequency);
			}
		}

		public char Character
		{
			get { return character;  }
		}

	}

	class letterAndBinaryCode
	{
		public char character;
		public string binary;

		public letterAndBinaryCode(char character, string binary)
		{
			this.character = character;
			this.binary = binary;
		}
	}
}
