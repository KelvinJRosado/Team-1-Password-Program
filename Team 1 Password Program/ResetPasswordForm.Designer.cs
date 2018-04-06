namespace Team_1_Password_Program
{
	partial class ResetPasswordForm
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxCurrentPass = new System.Windows.Forms.TextBox();
			this.textBoxNewPass = new System.Windows.Forms.TextBox();
			this.textBoxVerifyNewPass = new System.Windows.Forms.TextBox();
			this.buttonResetPass = new System.Windows.Forms.Button();
			this.buttonPassRequirements = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(151, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "Current Password";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(12, 87);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(125, 20);
			this.label2.TabIndex = 1;
			this.label2.Text = "New Password";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(12, 168);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(201, 20);
			this.label3.TabIndex = 2;
			this.label3.Text = "Re-enter New Password";
			// 
			// textBoxCurrentPass
			// 
			this.textBoxCurrentPass.Location = new System.Drawing.Point(16, 33);
			this.textBoxCurrentPass.MaxLength = 50;
			this.textBoxCurrentPass.Name = "textBoxCurrentPass";
			this.textBoxCurrentPass.PasswordChar = '*';
			this.textBoxCurrentPass.Size = new System.Drawing.Size(359, 20);
			this.textBoxCurrentPass.TabIndex = 3;
			this.textBoxCurrentPass.UseSystemPasswordChar = true;
			// 
			// textBoxNewPass
			// 
			this.textBoxNewPass.Location = new System.Drawing.Point(16, 110);
			this.textBoxNewPass.MaxLength = 50;
			this.textBoxNewPass.Name = "textBoxNewPass";
			this.textBoxNewPass.PasswordChar = '*';
			this.textBoxNewPass.Size = new System.Drawing.Size(359, 20);
			this.textBoxNewPass.TabIndex = 4;
			this.textBoxNewPass.UseSystemPasswordChar = true;
			// 
			// textBoxVerifyNewPass
			// 
			this.textBoxVerifyNewPass.Location = new System.Drawing.Point(16, 191);
			this.textBoxVerifyNewPass.MaxLength = 50;
			this.textBoxVerifyNewPass.Name = "textBoxVerifyNewPass";
			this.textBoxVerifyNewPass.PasswordChar = '*';
			this.textBoxVerifyNewPass.Size = new System.Drawing.Size(359, 20);
			this.textBoxVerifyNewPass.TabIndex = 5;
			this.textBoxVerifyNewPass.UseSystemPasswordChar = true;
			// 
			// buttonResetPass
			// 
			this.buttonResetPass.Location = new System.Drawing.Point(216, 232);
			this.buttonResetPass.Name = "buttonResetPass";
			this.buttonResetPass.Size = new System.Drawing.Size(106, 35);
			this.buttonResetPass.TabIndex = 6;
			this.buttonResetPass.Text = "Reset Password";
			this.buttonResetPass.UseVisualStyleBackColor = true;
			this.buttonResetPass.Click += new System.EventHandler(this.buttonResetPass_Click);
			// 
			// buttonPassRequirements
			// 
			this.buttonPassRequirements.Location = new System.Drawing.Point(67, 232);
			this.buttonPassRequirements.Name = "buttonPassRequirements";
			this.buttonPassRequirements.Size = new System.Drawing.Size(106, 35);
			this.buttonPassRequirements.TabIndex = 7;
			this.buttonPassRequirements.Text = "Password Requirements";
			this.buttonPassRequirements.UseVisualStyleBackColor = true;
			this.buttonPassRequirements.Click += new System.EventHandler(this.buttonPassRequirements_Click);
			// 
			// ResetPasswordForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(393, 281);
			this.Controls.Add(this.buttonPassRequirements);
			this.Controls.Add(this.buttonResetPass);
			this.Controls.Add(this.textBoxVerifyNewPass);
			this.Controls.Add(this.textBoxNewPass);
			this.Controls.Add(this.textBoxCurrentPass);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "ResetPasswordForm";
			this.Text = "Reset Password";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxCurrentPass;
		private System.Windows.Forms.TextBox textBoxNewPass;
		private System.Windows.Forms.TextBox textBoxVerifyNewPass;
		private System.Windows.Forms.Button buttonResetPass;
		private System.Windows.Forms.Button buttonPassRequirements;
	}
}