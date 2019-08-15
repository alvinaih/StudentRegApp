using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentRegister.DataClasses

{

	public class MainClass
	{
		//getter and setter(retrieve data and set data from sql)

		public string StudentNo { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string ContactNo { get; set; }

		public string EmailAddress { get; set; }

		public string IDNumber { get; set; }

		public string Course { get; set; }

		public string DateEnrol { get; set; }

		static string myConString = ConfigurationManager.ConnectionStrings["studentDB"].ConnectionString;

		public DataTable Select()
		{
			//connect to the database
			SqlConnection sqlCon = new SqlConnection(myConString);
			DataTable dt = new DataTable();

			try
			{
				//write the sql query
				string sql = "SELECT * FROM tblStudents";

				//creating a cmd using 'sql' and 'sqlCon'
				SqlCommand cmd = new SqlCommand(sql, sqlCon);

				//creating a sql adapter using 'cmd'
				SqlDataAdapter sda = new SqlDataAdapter(cmd);

				sqlCon.Open();
				sda.Fill(dt);



			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error Loading data", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			finally
			{
				sqlCon.Close();
			}
			return dt;
		}

		public bool Insert(MainClass mc)
		{
			//setting default return type and setting its value to false 
			bool isSuccess = false;

			//connect to the Database 
			SqlConnection sqlCon = new SqlConnection(myConString);
			try
			{
				//create a sql query to Insert Data 
				string sql = "INSERT into tblStudents(StudentNo, FirstName, LastName, ContactNo, EmailAddress, IDNumber, Course, DateEnrol) VALUES(@StudentNo, @FirstName, @LastName, @ContactNo, @EmailAddress, @IDNumber, @Course, @DateEnrol)";

				//Creating a sql command using sql and sqlCon
				SqlCommand cmd = new SqlCommand(sql, sqlCon);

				//create parameters to add data 
				cmd.Parameters.AddWithValue("@StudentNo", mc.StudentNo);
				cmd.Parameters.AddWithValue("@FirstName", mc.FirstName);
				cmd.Parameters.AddWithValue("@LastName", mc.LastName);
				cmd.Parameters.AddWithValue("@ContactNo", mc.ContactNo);
				cmd.Parameters.AddWithValue("@EmailAddress", mc.EmailAddress);
				cmd.Parameters.AddWithValue("@IDNumber", mc.IDNumber);
				cmd.Parameters.AddWithValue("@Course", mc.Course);
				cmd.Parameters.AddWithValue("@DateEnrol", mc.DateEnrol);

				//open connection 
				sqlCon.Open();

				//if the query runs successfully then the value of the rows will be greater than zero else its value will be 0 

				int rows = cmd.ExecuteNonQuery();

				if (rows > 0)
				{
					isSuccess = true;
				}
				else
				{
					isSuccess = false;
				}
			}

			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			finally
			{
				sqlCon.Close();
			}
			return isSuccess;	


		}

		public bool Update(MainClass mc)
		{
			//setting default return type and setting its value to false 
			bool isSuccess = false;

			//connect to the Database 
			SqlConnection sqlCon = new SqlConnection(myConString);
			try
			{
				//create a sql query to update Data 
				string sql = "UPDATE tblStudents SET FirstName=@FirstName, LastName=@LastName, ContactNo=@ContactNo, EmailAddress=@EmailAddress, IDNumber=@IDNumber, Course=@Course, DateEnrol=@DateEnrol WHERE StudentNo=@StudentNo";

				//Creating a sql command using sql and sqlCon
				SqlCommand cmd = new SqlCommand(sql, sqlCon);

				//create parameters to add data 
				cmd.Parameters.AddWithValue("@StudentNo", mc.StudentNo);
				cmd.Parameters.AddWithValue("@FirstName", mc.FirstName);
				cmd.Parameters.AddWithValue("@LastName", mc.LastName);
				cmd.Parameters.AddWithValue("@ContactNo", mc.ContactNo);
				cmd.Parameters.AddWithValue("@EmailAddress", mc.EmailAddress);
				cmd.Parameters.AddWithValue("@IDNumber", mc.IDNumber);
				cmd.Parameters.AddWithValue("@Course", mc.Course);
				cmd.Parameters.AddWithValue("@DateEnrol", mc.DateEnrol);

				//open connection 
				sqlCon.Open();

				//if the query runs successfully then the value of the rows will be greater than zero else its value will be 0 

				int rows = cmd.ExecuteNonQuery();

				if (rows > 0)
				{
					isSuccess = true;
				}
				else
				{
					isSuccess = false;
				}
			}

			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			finally
			{
				sqlCon.Close();
			}
			return isSuccess;


		}

		public bool Delete(MainClass mc)
		{
			//setting default return type and setting its value to false 
			bool isSuccess = false;

			//connect to the Database 
			SqlConnection sqlCon = new SqlConnection(myConString);
			try
			{
				//create a sql query to remove Data 
				string sql = "DELETE FROM tblStudents WHERE StudentNo=@StudentNo";

				//Creating a sql command using sql and sqlCon
				SqlCommand cmd = new SqlCommand(sql, sqlCon);

				//create parameters to remove data 
				cmd.Parameters.AddWithValue("@StudentNo", mc.StudentNo);

				//open connection 
				sqlCon.Open();

				//if the query runs successfully then the value of the rows will be greater than zero else its value will be 0 

				int rows = cmd.ExecuteNonQuery();

				if (rows > 0)
				{
					isSuccess = true;
				}
				else
				{
					isSuccess = false;
				}
			}

			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			finally
			{
				sqlCon.Close();
			}
			return isSuccess;


		}

	}
}
