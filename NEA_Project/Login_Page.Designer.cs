
namespace NEA_Project
{
    partial class login_page
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
			this.login_Btn = new System.Windows.Forms.Button();
			this.user_Name_Entry = new System.Windows.Forms.TextBox();
			this.password_Entry = new System.Windows.Forms.TextBox();
			this.username_input = new System.Windows.Forms.Label();
			this.password_input = new System.Windows.Forms.Label();
			this.createAccount_Btn = new System.Windows.Forms.Button();
			this.small_background = new System.Windows.Forms.PictureBox();
			this.Title_Label = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.small_background)).BeginInit();
			this.SuspendLayout();
			// 
			// login_Btn
			// 
			this.login_Btn.Location = new System.Drawing.Point(286, 289);
			this.login_Btn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.login_Btn.Name = "login_Btn";
			this.login_Btn.Size = new System.Drawing.Size(76, 23);
			this.login_Btn.TabIndex = 0;
			this.login_Btn.Text = "Log In";
			this.login_Btn.UseVisualStyleBackColor = true;
			this.login_Btn.Click += new System.EventHandler(this.Login_Btn_Click);
			// 
			// user_Name_Entry
			// 
			this.user_Name_Entry.Location = new System.Drawing.Point(353, 203);
			this.user_Name_Entry.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.user_Name_Entry.Name = "user_Name_Entry";
			this.user_Name_Entry.Size = new System.Drawing.Size(118, 20);
			this.user_Name_Entry.TabIndex = 1;
			this.user_Name_Entry.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// password_Entry
			// 
			this.password_Entry.Location = new System.Drawing.Point(353, 244);
			this.password_Entry.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.password_Entry.Name = "password_Entry";
			this.password_Entry.PasswordChar = '*';
			this.password_Entry.Size = new System.Drawing.Size(118, 20);
			this.password_Entry.TabIndex = 2;
			// 
			// username_input
			// 
			this.username_input.AutoSize = true;
			this.username_input.Location = new System.Drawing.Point(284, 206);
			this.username_input.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.username_input.Name = "username_input";
			this.username_input.Size = new System.Drawing.Size(58, 13);
			this.username_input.TabIndex = 3;
			this.username_input.Text = "Username:";
			this.username_input.Click += new System.EventHandler(this.label1_Click);
			// 
			// password_input
			// 
			this.password_input.AutoSize = true;
			this.password_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.password_input.Location = new System.Drawing.Point(284, 244);
			this.password_input.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.password_input.Name = "password_input";
			this.password_input.Size = new System.Drawing.Size(56, 13);
			this.password_input.TabIndex = 4;
			this.password_input.Text = "Password:";
			this.password_input.Click += new System.EventHandler(this.password_input_Click);
			// 
			// createAccount_Btn
			// 
			this.createAccount_Btn.Location = new System.Drawing.Point(379, 289);
			this.createAccount_Btn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.createAccount_Btn.Name = "createAccount_Btn";
			this.createAccount_Btn.Size = new System.Drawing.Size(92, 23);
			this.createAccount_Btn.TabIndex = 5;
			this.createAccount_Btn.Text = "Create Account";
			this.createAccount_Btn.UseVisualStyleBackColor = true;
			this.createAccount_Btn.Click += new System.EventHandler(this.createAccount_Btn_Click);
			// 
			// small_background
			// 
			this.small_background.BackColor = System.Drawing.SystemColors.ButtonHighlight;
			this.small_background.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.small_background.Location = new System.Drawing.Point(260, 178);
			this.small_background.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.small_background.Name = "small_background";
			this.small_background.Size = new System.Drawing.Size(255, 158);
			this.small_background.TabIndex = 7;
			this.small_background.TabStop = false;
			// 
			// Title_Label
			// 
			this.Title_Label.AutoSize = true;
			this.Title_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F);
			this.Title_Label.Location = new System.Drawing.Point(284, 78);
			this.Title_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.Title_Label.Name = "Title_Label";
			this.Title_Label.Size = new System.Drawing.Size(241, 46);
			this.Title_Label.TabIndex = 8;
			this.Title_Label.Text = "NEA Project";
			// 
			// login_page
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.Title_Label);
			this.Controls.Add(this.createAccount_Btn);
			this.Controls.Add(this.password_input);
			this.Controls.Add(this.username_input);
			this.Controls.Add(this.password_Entry);
			this.Controls.Add(this.user_Name_Entry);
			this.Controls.Add(this.login_Btn);
			this.Controls.Add(this.small_background);
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.Name = "login_page";
			this.Text = "Login Page";
			this.Load += new System.EventHandler(this.login_page_Load);
			((System.ComponentModel.ISupportInitialize)(this.small_background)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button login_Btn;
        private System.Windows.Forms.TextBox user_Name_Entry;
        private System.Windows.Forms.TextBox password_Entry;
        private System.Windows.Forms.Label username_input;
        private System.Windows.Forms.Label password_input;
        private System.Windows.Forms.Button createAccount_Btn;
        private System.Windows.Forms.PictureBox small_background;
        private System.Windows.Forms.Label Title_Label;
    }
}

