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
	public partial class WelcomeForm : Form
	{
		public static int instantiations = 0;
		ResetPasswordForm resetForm;

		public WelcomeForm(String name)
		{
			InitializeComponent();
			labelWelcome.Text = "Welcome " + name + "!";
			instantiations++;
		}

		private void btLogoff_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void WelcomeFom_FormClosed(object sender, FormClosedEventArgs e)
		{
			instantiations--;
		}

		private void btResetPassword_Click(object sender, EventArgs e)
		{
			
			//makes sure there is only one passform instantiation
			if (ResetPasswordForm.instantiations == 0)
			{
				resetForm = new ResetPasswordForm();
				resetForm.Show();
			}
			
		}
	}
}
