namespace Team_1_Password_Program
{
	partial class WelcomeForm
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
			this.labelWelcome = new System.Windows.Forms.Label();
			this.btResetPassword = new System.Windows.Forms.Button();
			this.btLogoff = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// labelWelcome
			// 
			this.labelWelcome.AutoSize = true;
			this.labelWelcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelWelcome.Location = new System.Drawing.Point(45, 46);
			this.labelWelcome.Name = "labelWelcome";
			this.labelWelcome.Size = new System.Drawing.Size(104, 24);
			this.labelWelcome.TabIndex = 0;
			this.labelWelcome.Text = "Welcome!";
			// 
			// btResetPassword
			// 
			this.btResetPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btResetPassword.Location = new System.Drawing.Point(96, 136);
			this.btResetPassword.Name = "btResetPassword";
			this.btResetPassword.Size = new System.Drawing.Size(142, 38);
			this.btResetPassword.TabIndex = 1;
			this.btResetPassword.Text = "Reset Password";
			this.btResetPassword.UseVisualStyleBackColor = true;
			this.btResetPassword.Click += new System.EventHandler(this.btResetPassword_Click);
			// 
			// btLogoff
			// 
			this.btLogoff.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btLogoff.Location = new System.Drawing.Point(96, 196);
			this.btLogoff.Name = "btLogoff";
			this.btLogoff.Size = new System.Drawing.Size(142, 38);
			this.btLogoff.TabIndex = 2;
			this.btLogoff.Text = "Log out";
			this.btLogoff.UseVisualStyleBackColor = true;
			this.btLogoff.Click += new System.EventHandler(this.btLogoff_Click);
			// 
			// WelcomeForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(342, 279);
			this.Controls.Add(this.btLogoff);
			this.Controls.Add(this.btResetPassword);
			this.Controls.Add(this.labelWelcome);
			this.Name = "WelcomeForm";
			this.Text = "Welcome";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelWelcome;
		private System.Windows.Forms.Button btResetPassword;
		private System.Windows.Forms.Button btLogoff;
	}
}