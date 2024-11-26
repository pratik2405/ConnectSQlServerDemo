using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace ConnectSQlServerDemo
{
    public partial class Form1 : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader dr;
        
        public Form1()
        {
            InitializeComponent();
            conn=new SqlConnection(ConfigurationManager.ConnectionStrings["defaultConn"].ConnectionString);
        }

        public void ClearFormField()
        {
            txtId.Clear();
            txtName.Clear();
            txtEmail.Clear();
            txtSal.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            { 
                string qry="insert into employee values(@name,@email,@sal)";
                cmd=new SqlCommand(qry,conn);
                cmd.Parameters.AddWithValue("@name",txtName.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@sal",Convert.ToInt32(txtSal.Text));

                conn.Open();

                int result = cmd.ExecuteNonQuery();
                if(result >= 1)
                {
                    MessageBox.Show("Employee Added  Successfully");
                    ClearFormField();
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "update employee set ename=@name,email=@email,esal=@sal where eid=@id";
                cmd = new SqlCommand(qry, conn);
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@sal", Convert.ToDouble(txtSal.Text));
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtId.Text));

                conn.Open();

                int result = cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("Employee Update Successfully");
                    ClearFormField() ;
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select ename,email,esal from employee where eid=@id";
                cmd = new SqlCommand(qry, conn);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtId.Text));

                conn.Open();
                dr =cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        txtName.Text = dr["ename"].ToString();
                        txtEmail.Text = dr["email"].ToString();
                        txtSal.Text = dr["esal"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Record not found !");
                    ClearFormField();
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "delete from employee where eid=@id";
                cmd = new SqlCommand(qry, conn);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtId.Text));

                conn.Open();

                int result = cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("Employee Deleted Successfully");
                    ClearFormField();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from employee";
                cmd = new SqlCommand(qry, conn);
                conn.Open();
                dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr); // convert dr object in to row & col format
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally 
            {
                conn.Close();
            }
        }
    }
}
