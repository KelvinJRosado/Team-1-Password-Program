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
	public partial class PasswordRequirements : Form
	{

		public static int instantiations = 0;

		public PasswordRequirements()
		{
			InitializeComponent();
			instantiations++;
		}

		private void PasswordRequirements_FormClosed(object sender, FormClosedEventArgs e)
		{
			instantiations--;
		}

	}
}
