using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Team_1_Password_Program
{
	public partial class ResetPasswordForm : Form
	{
		public static int instantiations = 0;
		ErrorForm eForm;
		PasswordRequirements passwordRequirements;
		SuccessForm sForm;
		DatabaseConnection connection;

		public ResetPasswordForm()
		{
			InitializeComponent();
			instantiations++;
			connection = new DatabaseConnection();
		}

		private void buttonResetPass_Click(object sender, EventArgs e)
		{
			//Clean input
			String oldPass = DatabaseConnection.CleanString(textBoxCurrentPass.Text);
			String newPass = DatabaseConnection.CleanString(textBoxNewPass.Text);
			String verifyPass = DatabaseConnection.CleanString(textBoxVerifyNewPass.Text);

			String username = LoginForm.account;
			int EID = LoginForm.accountID;

			if(connection.isPasswordChanged(oldPass, newPass, verifyPass, EID))
			{
				//On success
				if (SuccessForm.instantiations == 0)
				{
					sForm = new SuccessForm();
					//Close others
					if (eForm != null) eForm.Close();
					if (passwordRequirements != null) passwordRequirements.Close();
					sForm.Show();
				}
			}
			else
			{
				if(ErrorForm.instantiations == 0)
				{
					eForm = new ErrorForm();
					eForm.Show();
				}
			}


		}

		private void buttonPassRequirements_Click(object sender, EventArgs e)
		{
			if (PasswordRequirements.instantiations == 0)
			{
				passwordRequirements = new PasswordRequirements();
				passwordRequirements.Show();
			}
		}

		private void PasswordResetForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			instantiations--;
		}
	}
}
