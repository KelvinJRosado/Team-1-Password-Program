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

		//Person
		String personName;

		public LoginForm()
		{
			InitializeComponent();

			//Initialize 
			passHash = new PasswordHash();
			connection = new DatabaseConnection();

			
			String p1 = "Password!LoS";
			String p2 = "Passwordo1c^";
			String p3 = "Password58Bo@";
			String p4 = "PasswordJb_s";
			String p5 = "Passwordn.6.";

			p1 = PasswordHash.CreateHash(p1);
			p2 = PasswordHash.CreateHash(p2);
			p3 = PasswordHash.CreateHash(p3);
			p4 = PasswordHash.CreateHash(p4);
			p5 = PasswordHash.CreateHash(p5);

			Console.WriteLine(p1);
			Console.WriteLine(p2);
			Console.WriteLine(p3);
			Console.WriteLine(p4);
			Console.WriteLine(p5);
			

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
						cForm = new WelcomeForm();
						cForm.Show();
					}
				}
				else
				{
					//makes sure there is only one errorform instantiation
					if (ErrorForm.instantiations == 0)
					{
						eForm = new ErrorForm();
						eForm.Show();
					}
				}
			}

			catch (Exception exp)
			{

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
			String mac = getMacFormatted();

			//Ensure fields are filled
			if (user == "" || pass == "" || mac == "")
				return false;

			return connection.isAuthenticated(user, pass, mac, out personName);

		}

	}

	

}
