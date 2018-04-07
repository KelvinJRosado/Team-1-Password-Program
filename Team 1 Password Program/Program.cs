using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Data;

namespace Team_1_Password_Program

{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new LoginForm());
		}
	}

	public class PasswordHash
	{
		//Constants may be changed without breaking existing hashes.
		public const int SALT_BYTE_SIZE = 24;
		public const int HASH_BYTE_SIZE = 24;
		public const int PBKDF2_ITERATIONS = 1000;

		public const int ITERATION_INDEX = 0;
		public const int SALT_INDEX = 1;
		public const int PBKDF2_INDEX = 2;

		// Creates a salted PBKDF2 hash of the password.
		// <param name="password">The password to hash.</param>
		// <returns>The hash of the password.</returns>
		public static string CreateHash(string password)
		{
			// Generate a random salt
			RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider();
			byte[] salt = new byte[SALT_BYTE_SIZE];
			csprng.GetBytes(salt);

			// Hash the password and encode the parameters
			byte[] hash = PBKDF2(password, salt, PBKDF2_ITERATIONS, HASH_BYTE_SIZE);
			return PBKDF2_ITERATIONS + ":" +
				Convert.ToBase64String(salt) + ":" +
				Convert.ToBase64String(hash);
		}

		// Validates a password given a hash of the correct one.
		// <param name="password">The password to check.</param>
		// <param name="correctHash">A hash of the correct password.</param>
		// <returns>True if the password is correct. False otherwise.</returns>
		public static bool ValidatePassword(string password, string correctHash)
		{
			// Extract the parameters from the hash
			char[] delimiter = { ':' };
			string[] split = correctHash.Split(delimiter);
			int iterations = Int32.Parse(split[ITERATION_INDEX]);
			byte[] salt = Convert.FromBase64String(split[SALT_INDEX]);
			byte[] hash = Convert.FromBase64String(split[PBKDF2_INDEX]);

			byte[] testHash = PBKDF2(password, salt, iterations, hash.Length);
			return SlowEquals(hash, testHash);
		}

		/* Compares two byte arrays in length-constant time. This comparison
        method is used so that password hashes cannot be extracted from
        on-line systems using a timing attack and then attacked off-line. */
		// <param name="a">The first byte array.</param>
		// <param name="b">The second byte array.</param>
		// <returns>True if both byte arrays are equal. False otherwise.</returns>
		private static bool SlowEquals(byte[] a, byte[] b)
		{
			uint diff = (uint)a.Length ^ (uint)b.Length;
			for (int i = 0; i < a.Length && i < b.Length; i++)
				diff |= (uint)(a[i] ^ b[i]);
			return diff == 0;
		}

		// Computes the PBKDF2-SHA1 hash of a password.
		// <param name="password">The password to hash.</param>
		// <param name="salt">The salt.</param>
		// <param name="iterations">The PBKDF2 iteration count.</param>
		// <param name="outputBytes">The length of the hash to generate, in bytes.</param>
		// <returns>A hash of the password.</returns>
		private static byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
		{
			Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt);
			pbkdf2.IterationCount = iterations;
			return pbkdf2.GetBytes(outputBytes);
		}
	}

	class DatabaseConnection
	{
		private SqlConnection connection;

		public DatabaseConnection()
		{
			//Connection String
			connection = new SqlConnection("Data Source=on-campus-navigation.caqb3uzoiuo3.us-east-1.rds.amazonaws.com,1433;"
				+ "user id=Android; password=password;"
				+ "database=Manufacturing");

		}

		public static String CleanString(String input)
		{
			return String.Join("", input.Split('(', ')', ';', '\'', '"', '/', '\\', ' ', '\n', '\t', '\r',
			':', '{', '}', '[', ']', '`', '~', '=', '-'));
		}

		//Attempt to authenticate User; Return result
		public bool isAuthenticated(String username, String pass, String mac, out String name, out int ID)
		{

			//Generate query with parameters 
			String query = "SELECT Username, MAC_Hash, Active_Pass_Hash, First_Name, Last_Name, Employee_ID FROM Employee "
				+ "WHERE Username = @USER";

			SqlCommand command = new SqlCommand(query, connection);
			command.Parameters.Add("@USER", SqlDbType.VarChar);
			command.Parameters["@USER"].Value = username;

			//Attempt to execute query
			try
			{
				String[] result = new String[6];//Returned columns
				connection.Open();

				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					for(int i = 0; i < result.Length; i++)
					{
						result[i] = reader[i].ToString();
					}
				}

				connection.Close();

				name = result[3] + " " + result[4];
				ID = -1;

				if (result[5] == null) return false;
				ID = Int32.Parse(result[5]);
				

				//Validate password
				if(!PasswordHash.ValidatePassword(pass, result[2]))
				{
					Console.WriteLine("Incorrect Password");
					return false;
				}

				//Validate MAC Address
				if (!PasswordHash.ValidatePassword(mac, result[1]))
				{
					Console.WriteLine("Incorrect MAC Address");
					return false;
				}
				

				return true;
			}
			catch(Exception e)
			{
				Console.WriteLine(e.ToString());
				name = "";
				ID = 0;
				connection.Close();
				return false;
			}
		}

		public bool isPasswordChanged(String oldPass, String newPass, String newPass2, int id)
		{
			int passID;

			//Validate new password
			if (newPass != newPass2)
			{
				Console.WriteLine("New passwords do not match");
				return false;
			}

			if(newPass.Length > 49 || newPass.Length < 10)
			{
				Console.WriteLine("Password not within length constraints");
				return false;
			}

			if(newPass.IndexOfAny("'\"\\/(){}[]~`-=;:\r\t\n".ToCharArray()) != -1)
			{
				Console.WriteLine("Password cannot contain illegal characters");
				return false;
			}

			if (oldPass == newPass)
			{
				Console.WriteLine("Password cannot be identical");
				return false;
			}

			if(oldPass == newPass.Reverse().ToString())
			{
				Console.WriteLine("Password cannot be reverse of old password");
				return false;
			}

			if (are3CharsSame(oldPass, newPass))
			{
				Console.WriteLine("Password must change at least 3 characters");
				return false;
			}

			//Compare new password to old hashes
			String query = "SELECT Pass_Hash FROM Password_Log WHERE Employee_ID = @ID;";
			SqlCommand command = new SqlCommand(query, connection);
			command.Parameters.Add("@ID", SqlDbType.Int);
			command.Parameters["@ID"].Value = id;
			List<String> oldHashes = new List<String>();

			try
			{
				connection.Open();

				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					String result = reader[0].ToString();
					oldHashes.Add(result);
				}

				connection.Close();

			}
			catch(Exception e)
			{
				connection.Close();
				Console.WriteLine(e.ToString());
				return false;
			}

			//Validate new password against old hashes
			foreach(String hash in oldHashes)
			{
				bool test = PasswordHash.ValidatePassword(newPass, hash);
				if (test)
				{
					Console.WriteLine("Password cannot match a past hash");
					return false;
				}
			}

			//Hash passwords if tests passed
			String oldHash = PasswordHash.CreateHash(oldPass);
			String newHash = PasswordHash.CreateHash(newPass);
			passID = oldHashes.Count() + 1;

			//Insert old pass into database
			query = "INSERT INTO Password_Log (Employee_ID, Password_ID, Pass_Hash) VALUES (@EID, @PID, @Pass)";
			command = new SqlCommand(query, connection);
			//Give values to parameters
			command.Parameters.Add("@EID", SqlDbType.Int);
			command.Parameters.Add("@PID", SqlDbType.Int);
			command.Parameters.Add("@Pass", SqlDbType.VarChar);
			command.Parameters["@EID"].Value = id;
			command.Parameters["@PID"].Value = passID;
			command.Parameters["@Pass"].Value = oldHash;

			try
			{
				connection.Open();
				command.ExecuteNonQuery();
				connection.Close();
			}
			catch(Exception e)
			{
				Console.WriteLine(e.ToString());
				connection.Close();
				return false;
			}

			//Update pass in database
			query = "UPDATE Employee SET Active_Pass_Hash = @Pass WHERE Employee_ID = @EID;";
			command = new SqlCommand(query, connection);
			//Give values to parameters
			command.Parameters.Add("@EID", SqlDbType.Int);
			command.Parameters.Add("@Pass", SqlDbType.VarChar);
			command.Parameters["@EID"].Value = id;
			command.Parameters["@Pass"].Value = oldHash;

			try
			{
				connection.Open();
				command.ExecuteNonQuery();
				connection.Close();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
				connection.Close();
				return false;
			}


			return true;
		}

		private bool are3CharsSame(String old, String now)
		{
			int count = 0;

			if(old.Length <= now.Length)
			{
				for (int i = 0; i < old.Length; i++)
				{
					if (old[i].Equals(now[i])) count++;//Increment if same character
				}
			}
			else
			{
				for(int i = 0; i < now.Length; i++)
				{
					if (old[i].Equals(now[i])) count++; //Increment
				}
			}

			return (count >= 3);
			

		}

		
	}
	
}
