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
	//The objects of this class will be used to create a huffman tree, each object acting as a node on that tree.
	//Each object will either contain a letter and its frequency, or the cumulative frequency of the attached nodes.
	class letterData
	{
		private char character;
		private int frequency;
		private string[] data = new string[2];
		
		//The tree will be made out of letterData nodes, each node can have two nodes attached to itself.
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
		
		//If a cumulative frequency is set, then we know that this node is a branch,
		//and will therefore not contain a letter.
		public letterData(int cumulativeFrequency)
		{
			this.frequency = cumulativeFrequency;
			branch = true;
		}
		
		//When a bit is added to a node, all nodes that are attached to it should also receive that new bit.
		//This function will recursively until all nodes below the current node have be altered. 
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
	
	//Used to create a table containing characters and their equivilent binary code.
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
