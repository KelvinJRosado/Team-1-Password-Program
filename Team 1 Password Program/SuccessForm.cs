﻿using System;
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
	public partial class SuccessForm : Form
	{
		public static int instantiations = 0;

		public SuccessForm(ResetPasswordForm form)
		{
			InitializeComponent();
			instantiations++;
			form.Close();
		}
	}
}
