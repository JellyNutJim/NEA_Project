
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
			this.Remove_BG_Label = new System.Windows.Forms.Label();
			this.Convert_To_Text_Label = new System.Windows.Forms.Label();
			this.Convert_To_Text_Btn = new System.Windows.Forms.Button();
			this.Image_Error_Display = new System.Windows.Forms.Label();
			this.Split_Letters_Label = new System.Windows.Forms.Label();
			this.Split_Letters_Btn = new System.Windows.Forms.Button();
			this.Download_Label = new System.Windows.Forms.Label();
			this.Select_Download_Type_CB = new System.Windows.Forms.ComboBox();
			this.Download_Btn = new System.Windows.Forms.Button();
			this.DB_Tools_Label = new System.Windows.Forms.Label();
			this.Save_To_DB_Btn = new System.Windows.Forms.Button();
			this.Load_From_DB_Btn = new System.Windows.Forms.Button();
			this.Loading_Bar = new System.Windows.Forms.ProgressBar();
			this.Progress_Bar_Label = new System.Windows.Forms.Label();
			this.Current_Status_Label = new System.Windows.Forms.Label();
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
			// Remove_BG_Label
			// 
			this.Remove_BG_Label.AutoSize = true;
			this.Remove_BG_Label.Location = new System.Drawing.Point(599, 62);
			this.Remove_BG_Label.Name = "Remove_BG_Label";
			this.Remove_BG_Label.Size = new System.Drawing.Size(108, 13);
			this.Remove_BG_Label.TabIndex = 5;
			this.Remove_BG_Label.Text = "Remove Background";
			this.Remove_BG_Label.Click += new System.EventHandler(this.label2_Click);
			// 
			// Convert_To_Text_Label
			// 
			this.Convert_To_Text_Label.AutoSize = true;
			this.Convert_To_Text_Label.Location = new System.Drawing.Point(599, 139);
			this.Convert_To_Text_Label.Name = "Convert_To_Text_Label";
			this.Convert_To_Text_Label.Size = new System.Drawing.Size(84, 13);
			this.Convert_To_Text_Label.TabIndex = 6;
			this.Convert_To_Text_Label.Text = "Convert To Text";
			this.Convert_To_Text_Label.Click += new System.EventHandler(this.label2_Click_1);
			// 
			// Convert_To_Text_Btn
			// 
			this.Convert_To_Text_Btn.Location = new System.Drawing.Point(713, 129);
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
			// Split_Letters_Label
			// 
			this.Split_Letters_Label.AutoSize = true;
			this.Split_Letters_Label.Location = new System.Drawing.Point(599, 99);
			this.Split_Letters_Label.Name = "Split_Letters_Label";
			this.Split_Letters_Label.Size = new System.Drawing.Size(62, 13);
			this.Split_Letters_Label.TabIndex = 9;
			this.Split_Letters_Label.Text = "Split Letters";
			// 
			// Split_Letters_Btn
			// 
			this.Split_Letters_Btn.Location = new System.Drawing.Point(713, 94);
			this.Split_Letters_Btn.Name = "Split_Letters_Btn";
			this.Split_Letters_Btn.Size = new System.Drawing.Size(75, 23);
			this.Split_Letters_Btn.TabIndex = 10;
			this.Split_Letters_Btn.Text = "Submit";
			this.Split_Letters_Btn.UseVisualStyleBackColor = true;
			this.Split_Letters_Btn.Click += new System.EventHandler(this.Split_Letters_Btn_Click);
			// 
			// Download_Label
			// 
			this.Download_Label.AutoSize = true;
			this.Download_Label.Location = new System.Drawing.Point(599, 176);
			this.Download_Label.Name = "Download_Label";
			this.Download_Label.Size = new System.Drawing.Size(70, 13);
			this.Download_Label.TabIndex = 11;
			this.Download_Label.Text = "Download As";
			// 
			// Select_Download_Type_CB
			// 
			this.Select_Download_Type_CB.FormattingEnabled = true;
			this.Select_Download_Type_CB.Items.AddRange(new object[] {
            "Single Image",
            "Multiple Images",
            "Text File"});
			this.Select_Download_Type_CB.Location = new System.Drawing.Point(680, 173);
			this.Select_Download_Type_CB.Name = "Select_Download_Type_CB";
			this.Select_Download_Type_CB.Size = new System.Drawing.Size(108, 21);
			this.Select_Download_Type_CB.TabIndex = 12;
			this.Select_Download_Type_CB.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			// 
			// Download_Btn
			// 
			this.Download_Btn.Location = new System.Drawing.Point(641, 212);
			this.Download_Btn.Name = "Download_Btn";
			this.Download_Btn.Size = new System.Drawing.Size(114, 23);
			this.Download_Btn.TabIndex = 13;
			this.Download_Btn.Text = "Download";
			this.Download_Btn.UseVisualStyleBackColor = true;
			this.Download_Btn.Click += new System.EventHandler(this.Download_Btn_Click);
			// 
			// DB_Tools_Label
			// 
			this.DB_Tools_Label.AutoSize = true;
			this.DB_Tools_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
			this.DB_Tools_Label.Location = new System.Drawing.Point(616, 266);
			this.DB_Tools_Label.Name = "DB_Tools_Label";
			this.DB_Tools_Label.Size = new System.Drawing.Size(163, 26);
			this.DB_Tools_Label.TabIndex = 14;
			this.DB_Tools_Label.Text = "Database Tools";
			// 
			// Save_To_DB_Btn
			// 
			this.Save_To_DB_Btn.Location = new System.Drawing.Point(608, 307);
			this.Save_To_DB_Btn.Name = "Save_To_DB_Btn";
			this.Save_To_DB_Btn.Size = new System.Drawing.Size(75, 23);
			this.Save_To_DB_Btn.TabIndex = 15;
			this.Save_To_DB_Btn.Text = "Save";
			this.Save_To_DB_Btn.UseVisualStyleBackColor = true;
			this.Save_To_DB_Btn.Click += new System.EventHandler(this.Save_To_DB_Btn_Click);
			// 
			// Load_From_DB_Btn
			// 
			this.Load_From_DB_Btn.Location = new System.Drawing.Point(704, 307);
			this.Load_From_DB_Btn.Name = "Load_From_DB_Btn";
			this.Load_From_DB_Btn.Size = new System.Drawing.Size(75, 23);
			this.Load_From_DB_Btn.TabIndex = 16;
			this.Load_From_DB_Btn.Text = "Load";
			this.Load_From_DB_Btn.UseVisualStyleBackColor = true;
			// 
			// Loading_Bar
			// 
			this.Loading_Bar.Location = new System.Drawing.Point(608, 403);
			this.Loading_Bar.Name = "Loading_Bar";
			this.Loading_Bar.Size = new System.Drawing.Size(171, 23);
			this.Loading_Bar.TabIndex = 17;
			this.Loading_Bar.Click += new System.EventHandler(this.progressBar1_Click);
			// 
			// Progress_Bar_Label
			// 
			this.Progress_Bar_Label.AutoSize = true;
			this.Progress_Bar_Label.Location = new System.Drawing.Point(605, 378);
			this.Progress_Bar_Label.Name = "Progress_Bar_Label";
			this.Progress_Bar_Label.Size = new System.Drawing.Size(51, 13);
			this.Progress_Bar_Label.TabIndex = 18;
			this.Progress_Bar_Label.Text = "Progress:";
			// 
			// Current_Status_Label
			// 
			this.Current_Status_Label.AutoSize = true;
			this.Current_Status_Label.Location = new System.Drawing.Point(662, 378);
			this.Current_Status_Label.Name = "Current_Status_Label";
			this.Current_Status_Label.Size = new System.Drawing.Size(62, 13);
			this.Current_Status_Label.TabIndex = 19;
			this.Current_Status_Label.Text = "placeholder";
			// 
			// Main_Page
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.Current_Status_Label);
			this.Controls.Add(this.Progress_Bar_Label);
			this.Controls.Add(this.Loading_Bar);
			this.Controls.Add(this.Load_From_DB_Btn);
			this.Controls.Add(this.Save_To_DB_Btn);
			this.Controls.Add(this.DB_Tools_Label);
			this.Controls.Add(this.Download_Btn);
			this.Controls.Add(this.Select_Download_Type_CB);
			this.Controls.Add(this.Download_Label);
			this.Controls.Add(this.Split_Letters_Btn);
			this.Controls.Add(this.Split_Letters_Label);
			this.Controls.Add(this.Image_Error_Display);
			this.Controls.Add(this.Convert_To_Text_Btn);
			this.Controls.Add(this.Convert_To_Text_Label);
			this.Controls.Add(this.Remove_BG_Label);
			this.Controls.Add(this.Main_Menu_Logo_Temp);
			this.Controls.Add(this.Remove_BG_Btn);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.Result_Img_Display);
			this.Controls.Add(this.Input_Img_Display);
			this.Name = "Main_Page";
			this.Text = "Main Page";
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
		private System.Windows.Forms.Label Remove_BG_Label;
		private System.Windows.Forms.Label Convert_To_Text_Label;
		private System.Windows.Forms.Button Convert_To_Text_Btn;
		private System.Windows.Forms.Label Image_Error_Display;
        private System.Windows.Forms.Label Split_Letters_Label;
        private System.Windows.Forms.Button Split_Letters_Btn;
        private System.Windows.Forms.Label Download_Label;
        private System.Windows.Forms.ComboBox Select_Download_Type_CB;
        private System.Windows.Forms.Button Download_Btn;
        private System.Windows.Forms.Label DB_Tools_Label;
        private System.Windows.Forms.Button Save_To_DB_Btn;
        private System.Windows.Forms.Button Load_From_DB_Btn;
        private System.Windows.Forms.ProgressBar Loading_Bar;
        private System.Windows.Forms.Label Progress_Bar_Label;
        private System.Windows.Forms.Label Current_Status_Label;
    }
}