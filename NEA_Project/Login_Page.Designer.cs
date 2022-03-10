
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
            this.userNameEntry = new System.Windows.Forms.TextBox();
            this.passwordEntry = new System.Windows.Forms.TextBox();
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
            this.login_Btn.Location = new System.Drawing.Point(382, 356);
            this.login_Btn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.login_Btn.Name = "login_Btn";
            this.login_Btn.Size = new System.Drawing.Size(101, 28);
            this.login_Btn.TabIndex = 0;
            this.login_Btn.Text = "Log In";
            this.login_Btn.UseVisualStyleBackColor = true;
            this.login_Btn.Click += new System.EventHandler(this.Login_Btn_Click);
            // 
            // userNameEntry
            // 
            this.userNameEntry.Location = new System.Drawing.Point(471, 250);
            this.userNameEntry.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.userNameEntry.Name = "userNameEntry";
            this.userNameEntry.Size = new System.Drawing.Size(156, 22);
            this.userNameEntry.TabIndex = 1;
            this.userNameEntry.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // passwordEntry
            // 
            this.passwordEntry.Location = new System.Drawing.Point(471, 300);
            this.passwordEntry.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.passwordEntry.Name = "passwordEntry";
            this.passwordEntry.PasswordChar = '*';
            this.passwordEntry.Size = new System.Drawing.Size(156, 22);
            this.passwordEntry.TabIndex = 2;
            // 
            // username_input
            // 
            this.username_input.AutoSize = true;
            this.username_input.Location = new System.Drawing.Point(378, 253);
            this.username_input.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.username_input.Name = "username_input";
            this.username_input.Size = new System.Drawing.Size(77, 17);
            this.username_input.TabIndex = 3;
            this.username_input.Text = "Username:";
            this.username_input.Click += new System.EventHandler(this.label1_Click);
            // 
            // password_input
            // 
            this.password_input.AutoSize = true;
            this.password_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.password_input.Location = new System.Drawing.Point(378, 300);
            this.password_input.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.password_input.Name = "password_input";
            this.password_input.Size = new System.Drawing.Size(73, 17);
            this.password_input.TabIndex = 4;
            this.password_input.Text = "Password:";
            this.password_input.Click += new System.EventHandler(this.password_input_Click);
            // 
            // createAccount_Btn
            // 
            this.createAccount_Btn.Location = new System.Drawing.Point(505, 356);
            this.createAccount_Btn.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.createAccount_Btn.Name = "createAccount_Btn";
            this.createAccount_Btn.Size = new System.Drawing.Size(123, 28);
            this.createAccount_Btn.TabIndex = 5;
            this.createAccount_Btn.Text = "Create Account";
            this.createAccount_Btn.UseVisualStyleBackColor = true;
            this.createAccount_Btn.Click += new System.EventHandler(this.createAccount_Btn_Click);
            // 
            // small_background
            // 
            this.small_background.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.small_background.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.small_background.Location = new System.Drawing.Point(346, 219);
            this.small_background.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.small_background.Name = "small_background";
            this.small_background.Size = new System.Drawing.Size(339, 194);
            this.small_background.TabIndex = 7;
            this.small_background.TabStop = false;
            // 
            // Title_Label
            // 
            this.Title_Label.AutoSize = true;
            this.Title_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F);
            this.Title_Label.Location = new System.Drawing.Point(378, 96);
            this.Title_Label.Name = "Title_Label";
            this.Title_Label.Size = new System.Drawing.Size(296, 58);
            this.Title_Label.TabIndex = 8;
            this.Title_Label.Text = "NEA Project";
            // 
            // login_page
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.Title_Label);
            this.Controls.Add(this.createAccount_Btn);
            this.Controls.Add(this.password_input);
            this.Controls.Add(this.username_input);
            this.Controls.Add(this.passwordEntry);
            this.Controls.Add(this.userNameEntry);
            this.Controls.Add(this.login_Btn);
            this.Controls.Add(this.small_background);
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "login_page";
            this.Text = "Login Page";
            this.Load += new System.EventHandler(this.login_page_Load);
            ((System.ComponentModel.ISupportInitialize)(this.small_background)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button login_Btn;
        private System.Windows.Forms.TextBox userNameEntry;
        private System.Windows.Forms.TextBox passwordEntry;
        private System.Windows.Forms.Label username_input;
        private System.Windows.Forms.Label password_input;
        private System.Windows.Forms.Button createAccount_Btn;
        private System.Windows.Forms.PictureBox small_background;
        private System.Windows.Forms.Label Title_Label;
    }
}

