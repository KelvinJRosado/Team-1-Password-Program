using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Team_1_Password_Program
{
	public partial class LoginForm : Form
	{

		//forms to be used in the program
        WelcomeForm cForm;
        ErrorForm eForm;

		//call the password hashing and database functions
		PasswordHash passHash;
		DatabaseConnection connection;

		//Check how many login attempts
		int failedAttempts = 0;

		//Person
		String personName;
		public static String account;
		public static int accountID;
		String mac;

		public LoginForm()
		{
			InitializeComponent();

			//Initialize 
			passHash = new PasswordHash();
			connection = new DatabaseConnection();
			mac = getMacFormatted();

		}

		
		private void buttonLogin_Click(object sender, EventArgs e)
		{
			
			try
			{
				//Verify credentials
				if (isLoginGood())
				{
					//makes sure there is only one completeform instantiation
					if (WelcomeForm.instantiations == 0)
					{
						//Close error form on success
						if(eForm != null) eForm.Close();

						this.Hide();
						cForm = new WelcomeForm(personName);
						cForm.Show();
					}
				}
				else
				{
					failedAttempts++;

					//Block user after 5 attempts by not getting MAC address
					if (failedAttempts >= 5) mac = "";

					if(eForm != null)
					{
						eForm.Close();
						eForm = null;

					}

					eForm = new ErrorForm();
					eForm.Show();

				}
			}

			catch (Exception exp)
			{
				Console.WriteLine(exp);
			}
			
	}


	private String getMacFormatted()
	{
		//Get raw MAC address
		String mac = NetworkInterface.GetAllNetworkInterfaces()
		.Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
		.Select(nic => nic.GetPhysicalAddress().ToString())
		.FirstOrDefault();

		//Put MAC address in proper format
		String macFormat = "";
		for (int i = 0; i < mac.Length; i++)
		{
			if (i % 2 == 0)
			{
				macFormat += mac[i];
			}
			else
			{
				macFormat += mac[i] + "-";
			}
		}

		macFormat = macFormat.Substring(0, macFormat.Length - 1);

		return macFormat;

		}

		private bool isLoginGood()
		{
			//Get sanitized input
			String user = DatabaseConnection.CleanString(LoginText.Text);
			String pass = DatabaseConnection.CleanString(PassText.Text);
			account = user;

			//Ensure fields are filled
			if (user == "" || pass == "" || mac == "")
				return false;

			return connection.isAuthenticated(user, pass, mac, out personName, out accountID);

		}

	}

	

}
