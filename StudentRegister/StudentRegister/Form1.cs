using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StudentRegister.DataClasses;

namespace StudentRegister
{
    public partial class Form1 : Form
    {
		MainClass mc = new MainClass();

         public Form1()
        {
            InitializeComponent();
        }

		DataTable dt = new DataTable("tblStudents");

       

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["studentDB"].ConnectionString))
                {
                    if (sqlCon.State == ConnectionState.Closed)
                    {
                        sqlCon.Open();
                        using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tblStudents", sqlCon))
                        {
                            //Fill the database
                            sda.Fill(dt);
                            dgvStudentList.DataSource = dt;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

		private void txtSearchBar_TextChanged(object sender, EventArgs e)
		{
			
		}

		private void txtSearchBar_KeyPress(object sender, KeyPressEventArgs e)
		{
			
			
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			txtStudentNo.Text = "";
			txtFirstName.Text = "";
			txtLastName.Text = "";
			txtContactNo.Text = "";
			txtEmailAddress.Text = "";
			txtIDNo.Text = "";
			txtCourseName.Text = "";
			txtDateEnrol.Text = "";

		//another option e.g txtStudentNo.Clear();
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			const string message = "Do you want to Exit?";
			const string caption = "Exit";
			var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo);
			if (result == DialogResult.Yes)
			{
				Application.Exit();
			}
		}

		private void dgvStudentList_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}

		private void dgvStudentList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			int rowIndex = e.RowIndex;
			txtStudentNo.Text = dgvStudentList.Rows[rowIndex].Cells[0].Value.ToString();
			txtFirstName.Text = dgvStudentList.Rows[rowIndex].Cells[1].Value.ToString();
			txtLastName.Text = dgvStudentList.Rows[rowIndex].Cells[2].Value.ToString();
			txtContactNo.Text = dgvStudentList.Rows[rowIndex].Cells[3].Value.ToString();
			txtEmailAddress.Text = dgvStudentList.Rows[rowIndex].Cells[4].Value.ToString();
			txtIDNo.Text = dgvStudentList.Rows[rowIndex].Cells[5].Value.ToString();
			txtCourseName.Text = dgvStudentList.Rows[rowIndex].Cells[6].Value.ToString();
			txtDateEnrol.Text = dgvStudentList.Rows[rowIndex].Cells[7].Value.ToString();
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			DataTable dt = mc.Select();
			DataView dv = dt.DefaultView;
			//filter the DatagridView using the txtBox --(txtSearchBar)
			dv.RowFilter = String.Format("StudentNo like '%{0}%' OR Course like '%{0}%' OR LastName like '%{0}%' OR FirstName like '%{0}%' ", txtSearchBar.Text);
			dgvStudentList.DataSource = dv.ToTable();

		}

		private void btnUpdate_Click(object sender, EventArgs e)
		{
			mc.StudentNo = txtStudentNo.Text;
			mc.FirstName = txtFirstName.Text;
			mc.LastName = txtLastName.Text;
			mc.EmailAddress = txtEmailAddress.Text;
			mc.ContactNo = txtContactNo.Text;
			mc.IDNumber = txtIDNo.Text;
			mc.Course = txtCourseName.Text;
			mc.DateEnrol = txtDateEnrol.Text;

			bool Success = mc.Update(mc);

			if(Success)
			{
				MessageBox.Show("New student successfully added");
				Clear();
			}
			else
			{
				MessageBox.Show("failed to insert new student");

			}
			DataTable bt = mc.Select();
			dgvStudentList.DataSource = bt;
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			//get the value from the input fields 
			mc.StudentNo = txtStudentNo.Text;
			mc.FirstName = txtFirstName.Text;
			mc.LastName = txtLastName.Text;
			mc.EmailAddress = txtEmailAddress.Text;
			mc.ContactNo = txtContactNo.Text;
			mc.IDNumber = txtIDNo.Text;
			mc.Course = txtCourseName.Text;
			mc.DateEnrol = txtDateEnrol.Text;


			bool success = mc.Insert(mc);
			if (success)
			{
				MessageBox.Show("New student successfully inserted");
			}
			else
			{
				MessageBox.Show("Failed to insert student");
			}

			//load the data on the DataGridView--basically so that you can see new record
			DataTable dt = mc.Select();
			dgvStudentList.DataSource = dt; 

		}


		public void Clear()
		{
			txtStudentNo.Clear();
			txtFirstName.Clear();
			txtLastName.Clear();
			txtContactNo.Clear();
			txtEmailAddress.Clear();
			txtIDNo.Clear();
			txtCourseName.Clear();
			txtDateEnrol.Clear();

			bool success = mc.Insert(mc);
			if (success)
			{
				MessageBox.Show("New Student Successfully Inserted");
			}
			else
			{
				MessageBox.Show("Failed to insert Student Successfully");
			}
			//load data to the DataGridView
			DataTable dt = mc.Select();
			dgvStudentList.DataSource = dt;
		}

		public void btnDelete_Click(object sender, EventArgs e)
		{

			mc.StudentNo = txtStudentNo.Text;

			bool success = mc.Delete(mc);

			if (success)
			{
				MessageBox.Show("You have successfully Deleted a student");
			}
			else
			{
				MessageBox.Show("You were not able to delete the student.");
			}

			//Load the data on the DataGridView
			DataTable dt = mc.Select();
			dgvStudentList.DataSource = dt;
		}
		
	}
}
