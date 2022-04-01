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
}
