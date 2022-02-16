
namespace NEA_Project
{
	partial class Main_Page
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Input_Img_Display = new System.Windows.Forms.PictureBox();
			this.Result_Img_Display = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.Remove_BG_Btn = new System.Windows.Forms.Button();
			this.Main_Menu_Logo_Temp = new System.Windows.Forms.Label();
			this.Remove_BG_Text = new System.Windows.Forms.Label();
			this.Convert_To_Text_Label = new System.Windows.Forms.Label();
			this.Convert_To_Text_Btn = new System.Windows.Forms.Button();
			this.Image_Error_Display = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.Input_Img_Display)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Result_Img_Display)).BeginInit();
			this.SuspendLayout();
			// 
			// Input_Img_Display
			// 
			this.Input_Img_Display.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Input_Img_Display.Location = new System.Drawing.Point(12, 12);
			this.Input_Img_Display.Name = "Input_Img_Display";
			this.Input_Img_Display.Size = new System.Drawing.Size(262, 426);
			this.Input_Img_Display.TabIndex = 0;
			this.Input_Img_Display.TabStop = false;
			this.Input_Img_Display.Click += new System.EventHandler(this.pictureBox1_Click);
			this.Input_Img_Display.DragDrop += new System.Windows.Forms.DragEventHandler(this.Input_Img_Display_DragDrop);
			this.Input_Img_Display.DragEnter += new System.Windows.Forms.DragEventHandler(this.Input_Img_Display_DragEnter);
			// 
			// Result_Img_Display
			// 
			this.Result_Img_Display.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Result_Img_Display.Location = new System.Drawing.Point(312, 12);
			this.Result_Img_Display.Name = "Result_Img_Display";
			this.Result_Img_Display.Size = new System.Drawing.Size(262, 426);
			this.Result_Img_Display.TabIndex = 1;
			this.Result_Img_Display.TabStop = false;
			this.Result_Img_Display.Click += new System.EventHandler(this.Result_Img_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(618, 41);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(0, 13);
			this.label1.TabIndex = 2;
			this.label1.Click += new System.EventHandler(this.label1_Click);
			// 
			// Remove_BG_Btn
			// 
			this.Remove_BG_Btn.Location = new System.Drawing.Point(713, 57);
			this.Remove_BG_Btn.Name = "Remove_BG_Btn";
			this.Remove_BG_Btn.Size = new System.Drawing.Size(75, 23);
			this.Remove_BG_Btn.TabIndex = 3;
			this.Remove_BG_Btn.Text = "Submit";
			this.Remove_BG_Btn.UseVisualStyleBackColor = true;
			this.Remove_BG_Btn.Click += new System.EventHandler(this.Remove_BG_Btn_Click);
			// 
			// Main_Menu_Logo_Temp
			// 
			this.Main_Menu_Logo_Temp.AutoSize = true;
			this.Main_Menu_Logo_Temp.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
			this.Main_Menu_Logo_Temp.Location = new System.Drawing.Point(636, 12);
			this.Main_Menu_Logo_Temp.Name = "Main_Menu_Logo_Temp";
			this.Main_Menu_Logo_Temp.Size = new System.Drawing.Size(119, 26);
			this.Main_Menu_Logo_Temp.TabIndex = 4;
			this.Main_Menu_Logo_Temp.Text = "Main Menu";
			// 
			// Remove_BG_Text
			// 
			this.Remove_BG_Text.AutoSize = true;
			this.Remove_BG_Text.Location = new System.Drawing.Point(599, 62);
			this.Remove_BG_Text.Name = "Remove_BG_Text";
			this.Remove_BG_Text.Size = new System.Drawing.Size(108, 13);
			this.Remove_BG_Text.TabIndex = 5;
			this.Remove_BG_Text.Text = "Remove Background";
			this.Remove_BG_Text.Click += new System.EventHandler(this.label2_Click);
			// 
			// Convert_To_Text_Label
			// 
			this.Convert_To_Text_Label.AutoSize = true;
			this.Convert_To_Text_Label.Location = new System.Drawing.Point(599, 106);
			this.Convert_To_Text_Label.Name = "Convert_To_Text_Label";
			this.Convert_To_Text_Label.Size = new System.Drawing.Size(84, 13);
			this.Convert_To_Text_Label.TabIndex = 6;
			this.Convert_To_Text_Label.Text = "Convert To Text";
			this.Convert_To_Text_Label.Click += new System.EventHandler(this.label2_Click_1);
			// 
			// Convert_To_Text_Btn
			// 
			this.Convert_To_Text_Btn.Location = new System.Drawing.Point(713, 101);
			this.Convert_To_Text_Btn.Name = "Convert_To_Text_Btn";
			this.Convert_To_Text_Btn.Size = new System.Drawing.Size(75, 23);
			this.Convert_To_Text_Btn.TabIndex = 7;
			this.Convert_To_Text_Btn.Text = "Submit";
			this.Convert_To_Text_Btn.UseVisualStyleBackColor = true;
			this.Convert_To_Text_Btn.Click += new System.EventHandler(this.Convert_To_Text_Btn_Click);
			// 
			// Image_Error_Display
			// 
			this.Image_Error_Display.AutoSize = true;
			this.Image_Error_Display.Location = new System.Drawing.Point(26, 413);
			this.Image_Error_Display.Name = "Image_Error_Display";
			this.Image_Error_Display.Size = new System.Drawing.Size(45, 13);
			this.Image_Error_Display.TabIndex = 8;
			this.Image_Error_Display.Text = "No error";
			// 
			// Main_Page
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.Image_Error_Display);
			this.Controls.Add(this.Convert_To_Text_Btn);
			this.Controls.Add(this.Convert_To_Text_Label);
			this.Controls.Add(this.Remove_BG_Text);
			this.Controls.Add(this.Main_Menu_Logo_Temp);
			this.Controls.Add(this.Remove_BG_Btn);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.Result_Img_Display);
			this.Controls.Add(this.Input_Img_Display);
			this.Name = "Main_Page";
			this.Text = "Main_Page";
			this.Load += new System.EventHandler(this.Main_Page_Load);
			((System.ComponentModel.ISupportInitialize)(this.Input_Img_Display)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Result_Img_Display)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox Input_Img_Display;
		private System.Windows.Forms.PictureBox Result_Img_Display;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button Remove_BG_Btn;
		private System.Windows.Forms.Label Main_Menu_Logo_Temp;
		private System.Windows.Forms.Label Remove_BG_Text;
		private System.Windows.Forms.Label Convert_To_Text_Label;
		private System.Windows.Forms.Button Convert_To_Text_Btn;
		private System.Windows.Forms.Label Image_Error_Display;
	}
}