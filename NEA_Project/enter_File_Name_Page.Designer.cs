
namespace NEA_Project
{
	partial class enter_File_Name_Page
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
			this.filename_Entry_TextBox = new System.Windows.Forms.TextBox();
			this.file_Name_Enter_Label = new System.Windows.Forms.Label();
			this.name_Entry_Btn = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// filename_Entry_TextBox
			// 
			this.filename_Entry_TextBox.Location = new System.Drawing.Point(12, 37);
			this.filename_Entry_TextBox.Name = "filename_Entry_TextBox";
			this.filename_Entry_TextBox.Size = new System.Drawing.Size(157, 20);
			this.filename_Entry_TextBox.TabIndex = 0;
			// 
			// file_Name_Enter_Label
			// 
			this.file_Name_Enter_Label.AutoSize = true;
			this.file_Name_Enter_Label.Location = new System.Drawing.Point(12, 9);
			this.file_Name_Enter_Label.Name = "file_Name_Enter_Label";
			this.file_Name_Enter_Label.Size = new System.Drawing.Size(157, 13);
			this.file_Name_Enter_Label.TabIndex = 1;
			this.file_Name_Enter_Label.Text = "Please enter a desired filename:";
			// 
			// name_Entry_Btn
			// 
			this.name_Entry_Btn.Location = new System.Drawing.Point(190, 12);
			this.name_Entry_Btn.Name = "name_Entry_Btn";
			this.name_Entry_Btn.Size = new System.Drawing.Size(75, 48);
			this.name_Entry_Btn.TabIndex = 2;
			this.name_Entry_Btn.Text = "Submit";
			this.name_Entry_Btn.UseVisualStyleBackColor = true;
			this.name_Entry_Btn.Click += new System.EventHandler(this.name_Entry_Btn_Click);
			// 
			// enter_File_Name_Page
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(276, 78);
			this.Controls.Add(this.name_Entry_Btn);
			this.Controls.Add(this.file_Name_Enter_Label);
			this.Controls.Add(this.filename_Entry_TextBox);
			this.Name = "enter_File_Name_Page";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.TopMost = true;
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox filename_Entry_TextBox;
		private System.Windows.Forms.Label file_Name_Enter_Label;
		private System.Windows.Forms.Button name_Entry_Btn;
	}
}