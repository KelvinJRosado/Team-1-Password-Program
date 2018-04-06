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
		PasswordRequirements passwordRequirements;

		public ResetPasswordForm()
		{
			InitializeComponent();
			instantiations++;
		}

		private void buttonResetPass_Click(object sender, EventArgs e)
		{

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
