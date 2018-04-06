namespace Team_1_Password_Program
{
	partial class LoginForm
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
			this.labelUsername = new System.Windows.Forms.Label();
			this.labelPassword = new System.Windows.Forms.Label();
			this.LoginText = new System.Windows.Forms.TextBox();
			this.PassText = new System.Windows.Forms.TextBox();
			this.buttonLogin = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// labelUsername
			// 
			this.labelUsername.AutoSize = true;
			this.labelUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelUsername.Location = new System.Drawing.Point(24, 19);
			this.labelUsername.Name = "labelUsername";
			this.labelUsername.Size = new System.Drawing.Size(110, 25);
			this.labelUsername.TabIndex = 0;
			this.labelUsername.Text = "Username";
			// 
			// labelPassword
			// 
			this.labelPassword.AutoSize = true;
			this.labelPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelPassword.Location = new System.Drawing.Point(24, 88);
			this.labelPassword.Name = "labelPassword";
			this.labelPassword.Size = new System.Drawing.Size(106, 25);
			this.labelPassword.TabIndex = 1;
			this.labelPassword.Text = "Password";
			// 
			// LoginText
			// 
			this.LoginText.Location = new System.Drawing.Point(29, 47);
			this.LoginText.MaxLength = 50;
			this.LoginText.Name = "LoginText";
			this.LoginText.Size = new System.Drawing.Size(297, 20);
			this.LoginText.TabIndex = 2;
			// 
			// PassText
			// 
			this.PassText.Location = new System.Drawing.Point(29, 116);
			this.PassText.MaxLength = 50;
			this.PassText.Name = "PassText";
			this.PassText.PasswordChar = '*';
			this.PassText.Size = new System.Drawing.Size(297, 20);
			this.PassText.TabIndex = 3;
			this.PassText.UseSystemPasswordChar = true;
			// 
			// buttonLogin
			// 
			this.buttonLogin.Location = new System.Drawing.Point(124, 164);
			this.buttonLogin.Name = "buttonLogin";
			this.buttonLogin.Size = new System.Drawing.Size(94, 30);
			this.buttonLogin.TabIndex = 4;
			this.buttonLogin.Text = "Log in";
			this.buttonLogin.UseVisualStyleBackColor = true;
			this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
			// 
			// LoginForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(360, 210);
			this.Controls.Add(this.buttonLogin);
			this.Controls.Add(this.PassText);
			this.Controls.Add(this.LoginText);
			this.Controls.Add(this.labelPassword);
			this.Controls.Add(this.labelUsername);
			this.Name = "LoginForm";
			this.Text = "Employee Login";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelUsername;
		private System.Windows.Forms.Label labelPassword;
		private System.Windows.Forms.TextBox LoginText;
		private System.Windows.Forms.TextBox PassText;
		private System.Windows.Forms.Button buttonLogin;
	}
}

