
namespace NEA_Project
{
	partial class verifyPassword
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
			this.password_Input = new System.Windows.Forms.TextBox();
			this.Submit_Btn = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// password_Input
			// 
			this.password_Input.Location = new System.Drawing.Point(48, 57);
			this.password_Input.Name = "password_Input";
			this.password_Input.PasswordChar = '*';
			this.password_Input.Size = new System.Drawing.Size(100, 20);
			this.password_Input.TabIndex = 0;
			// 
			// Submit_Btn
			// 
			this.Submit_Btn.Location = new System.Drawing.Point(193, 57);
			this.Submit_Btn.Name = "Submit_Btn";
			this.Submit_Btn.Size = new System.Drawing.Size(75, 23);
			this.Submit_Btn.TabIndex = 1;
			this.Submit_Btn.Text = "Submit";
			this.Submit_Btn.UseVisualStyleBackColor = true;
			this.Submit_Btn.Click += new System.EventHandler(this.Submit_Btn_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(45, 18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(221, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Please re-enter your password for verification:";
			this.label1.Click += new System.EventHandler(this.label1_Click);
			// 
			// verifyPassword
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(352, 121);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.Submit_Btn);
			this.Controls.Add(this.password_Input);
			this.Name = "verifyPassword";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox password_Input;
		private System.Windows.Forms.Button Submit_Btn;
		private System.Windows.Forms.Label label1;
	}
}