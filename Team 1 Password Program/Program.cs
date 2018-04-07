﻿using System;
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
			connection = new SqlConnection("Data Source=on-campus-navigation.caqb3uzoiuo3.us-east-1.rds.amazonaws.com,1433;"
				+ "user id=Android; password=password;"
				+ "database=Manufacturing");

		}

		/*
		//Executes a select query and returns a list filled with arrays of columns
		public List<String[]> SelectQuery(String query)
		{

			SqlCommand command = new SqlCommand(query, connection);

			try
			{
				//Open database connection
				connection.Open();
				List<String[]> result = new List<String[]>();

				//Execute the query and populate list with results
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					String[] columns = new String[reader.VisibleFieldCount];
					for (int i = 0; i < reader.VisibleFieldCount; i++)
					{
						columns[i] = reader[i].ToString();
					}
					result.Add(columns);
				}

				return result;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
				return null;
			}
		}

		public void InsertUpdateQuery(String query)
		{

			SqlCommand command = new SqlCommand(query, connection);

			try
			{
				connection.Open();
				command.ExecuteNonQuery();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
				return;
			}

		}

		//Prints the result of a query to the console; Debug Only
		public void PrintQueryResult(List<String[]> result)
		{
			foreach (String[] row in result)
			{

				String ss = "";
				for (int i = 0; i < row.Length; i++)
				{
					ss += row[i] + " |" + "\t";
				}
				ss = ss.Substring(0, ss.Length - 1);
				Console.WriteLine(ss);
			}
		}
		*/

		public static String CleanString(String input)
		{
			return String.Join("", input.Split('(', ')', ';', '\'', '"', '/', '\\', ' ', '\n', '\t', '\r',
			':', '{', '}', '[', ']', '`', '~', '=', '-'));
		}

		public bool isAuthenticated(String username, String pass, String mac, out String name)
		{

			//Generate query with parameters 
			String query = "SELECT Username, MAC_Hash, Active_Pass_Hash, First_Name, Last_Name FROM Employee "
				+ "WHERE Username = @USER";

			SqlCommand command = new SqlCommand(query, connection);
			command.Parameters.Add("@USER", SqlDbType.VarChar);
			command.Parameters["@USER"].Value = username;

			//Attempt to execute query
			try
			{
				String[] result = new String[5];//Returned columns
				connection.Open();

				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					for(int i = 0; i < result.Length; i++)
					{
						result[i] = reader[i].ToString();
					}
				}

				name = result[3] + " " + result[4];

				//Validate password
				if(!PasswordHash.ValidatePassword(pass, result[2]))
				{
					return false;
				}

				/*
				//Validate MAC Address
				if (!PasswordHash.ValidatePassword(mac, result[1]))
				{
					return false;
				}
				*/

				connection.Close();

				return true;
			}
			catch(Exception e)
			{
				Console.WriteLine(e.ToString());
				name = "";
				connection.Close();
				return false;
			}
		}
		
	}
	
}
