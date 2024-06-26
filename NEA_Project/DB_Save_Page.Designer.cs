﻿
namespace NEA_Project
{
	partial class DB_Save_Page
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
            this.Title_Label = new System.Windows.Forms.Label();
            this.Save_Image_Box = new System.Windows.Forms.PictureBox();
            this.Save_File_Btn = new System.Windows.Forms.Button();
            this.File_Name_Entry = new System.Windows.Forms.TextBox();
            this.Loading_Bar = new System.Windows.Forms.ProgressBar();
            this.Save_Text_Box = new System.Windows.Forms.RichTextBox();
            this.Progress_Label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Save_Image_Box)).BeginInit();
            this.SuspendLayout();
            // 
            // Title_Label
            // 
            this.Title_Label.AutoSize = true;
            this.Title_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.Title_Label.Location = new System.Drawing.Point(146, 9);
            this.Title_Label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Title_Label.Name = "Title_Label";
            this.Title_Label.Size = new System.Drawing.Size(127, 31);
            this.Title_Label.TabIndex = 0;
            this.Title_Label.Text = "Save File";
            // 
            // Save_Image_Box
            // 
            this.Save_Image_Box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Save_Image_Box.Location = new System.Drawing.Point(16, 58);
            this.Save_Image_Box.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Save_Image_Box.Name = "Save_Image_Box";
            this.Save_Image_Box.Size = new System.Drawing.Size(387, 361);
            this.Save_Image_Box.TabIndex = 1;
            this.Save_Image_Box.TabStop = false;
            // 
            // Save_File_Btn
            // 
            this.Save_File_Btn.Location = new System.Drawing.Point(280, 438);
            this.Save_File_Btn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Save_File_Btn.Name = "Save_File_Btn";
            this.Save_File_Btn.Size = new System.Drawing.Size(124, 41);
            this.Save_File_Btn.TabIndex = 2;
            this.Save_File_Btn.Text = "Save";
            this.Save_File_Btn.UseVisualStyleBackColor = true;
            this.Save_File_Btn.Click += new System.EventHandler(this.Save_File_Btn_Click);
            // 
            // File_Name_Entry
            // 
            this.File_Name_Entry.Location = new System.Drawing.Point(16, 438);
            this.File_Name_Entry.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.File_Name_Entry.Name = "File_Name_Entry";
            this.File_Name_Entry.Size = new System.Drawing.Size(192, 22);
            this.File_Name_Entry.TabIndex = 3;
            // 
            // Loading_Bar
            // 
            this.Loading_Bar.Location = new System.Drawing.Point(16, 503);
            this.Loading_Bar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Loading_Bar.Name = "Loading_Bar";
            this.Loading_Bar.Size = new System.Drawing.Size(193, 28);
            this.Loading_Bar.TabIndex = 4;
            // 
            // Save_Text_Box
            // 
            this.Save_Text_Box.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Save_Text_Box.Location = new System.Drawing.Point(33, 71);
            this.Save_Text_Box.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Save_Text_Box.Name = "Save_Text_Box";
            this.Save_Text_Box.Size = new System.Drawing.Size(351, 334);
            this.Save_Text_Box.TabIndex = 5;
            this.Save_Text_Box.Text = "";
            // 
            // Progress_Label
            // 
            this.Progress_Label.AutoSize = true;
            this.Progress_Label.Location = new System.Drawing.Point(16, 480);
            this.Progress_Label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Progress_Label.Name = "Progress_Label";
            this.Progress_Label.Size = new System.Drawing.Size(69, 17);
            this.Progress_Label.TabIndex = 6;
            this.Progress_Label.Text = "Progress:";
            // 
            // DB_Save_Page
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 564);
            this.Controls.Add(this.Progress_Label);
            this.Controls.Add(this.Save_Text_Box);
            this.Controls.Add(this.Loading_Bar);
            this.Controls.Add(this.File_Name_Entry);
            this.Controls.Add(this.Save_File_Btn);
            this.Controls.Add(this.Save_Image_Box);
            this.Controls.Add(this.Title_Label);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "DB_Save_Page";
            this.Text = "Save File";
            this.Load += new System.EventHandler(this.DB_Save_Page_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Save_Image_Box)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label Title_Label;
		private System.Windows.Forms.PictureBox Save_Image_Box;
		private System.Windows.Forms.Button Save_File_Btn;
		private System.Windows.Forms.TextBox File_Name_Entry;
		private System.Windows.Forms.ProgressBar Loading_Bar;
		private System.Windows.Forms.RichTextBox Save_Text_Box;
		private System.Windows.Forms.Label Progress_Label;
	}
}