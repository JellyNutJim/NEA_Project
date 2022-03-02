
namespace NEA_Project
{
	partial class DB_Load_Page
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
			this.File_Display_View = new System.Windows.Forms.ListView();
			this.File_Name_Column = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.File_Type_Column = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.File_Size_Column = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.DOC_Column = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Load_File_Btn = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// Title_Label
			// 
			this.Title_Label.AutoSize = true;
			this.Title_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
			this.Title_Label.Location = new System.Drawing.Point(128, 9);
			this.Title_Label.Name = "Title_Label";
			this.Title_Label.Size = new System.Drawing.Size(101, 26);
			this.Title_Label.TabIndex = 1;
			this.Title_Label.Text = "Load File";
			// 
			// File_Display_View
			// 
			this.File_Display_View.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.File_Name_Column,
            this.File_Type_Column,
            this.File_Size_Column,
            this.DOC_Column});
			this.File_Display_View.HideSelection = false;
			this.File_Display_View.Location = new System.Drawing.Point(12, 59);
			this.File_Display_View.MultiSelect = false;
			this.File_Display_View.Name = "File_Display_View";
			this.File_Display_View.Size = new System.Drawing.Size(331, 306);
			this.File_Display_View.TabIndex = 2;
			this.File_Display_View.UseCompatibleStateImageBehavior = false;
			this.File_Display_View.View = System.Windows.Forms.View.Details;
			// 
			// File_Name_Column
			// 
			this.File_Name_Column.Text = "Name:";
			this.File_Name_Column.Width = 70;
			// 
			// File_Type_Column
			// 
			this.File_Type_Column.Text = "File Type:";
			this.File_Type_Column.Width = 70;
			// 
			// File_Size_Column
			// 
			this.File_Size_Column.Text = "File Size:";
			this.File_Size_Column.Width = 70;
			// 
			// DOC_Column
			// 
			this.DOC_Column.Text = "Created On:";
			this.DOC_Column.Width = 115;
			// 
			// Load_File_Btn
			// 
			this.Load_File_Btn.Location = new System.Drawing.Point(136, 396);
			this.Load_File_Btn.Name = "Load_File_Btn";
			this.Load_File_Btn.Size = new System.Drawing.Size(93, 33);
			this.Load_File_Btn.TabIndex = 3;
			this.Load_File_Btn.Text = "Load";
			this.Load_File_Btn.UseVisualStyleBackColor = true;
			this.Load_File_Btn.Click += new System.EventHandler(this.Load_File_Btn_Click);
			// 
			// DB_Load_Page
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(355, 458);
			this.Controls.Add(this.Load_File_Btn);
			this.Controls.Add(this.File_Display_View);
			this.Controls.Add(this.Title_Label);
			this.Name = "DB_Load_Page";
			this.Text = "Load File";
			this.Load += new System.EventHandler(this.DB_Load_Page_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label Title_Label;
		private System.Windows.Forms.ListView File_Display_View;
		private System.Windows.Forms.Button Load_File_Btn;
		private System.Windows.Forms.ColumnHeader File_Name_Column;
		private System.Windows.Forms.ColumnHeader File_Type_Column;
		private System.Windows.Forms.ColumnHeader File_Size_Column;
		private System.Windows.Forms.ColumnHeader DOC_Column;
	}
}