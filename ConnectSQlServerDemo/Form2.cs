using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;


namespace ConnectSQlServerDemo
{

    public partial class Form2 : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader dr;
        public Form2()
        {
            InitializeComponent();
            conn=new SqlConnection(ConfigurationManager.ConnectionStrings["defaultConn"].ConnectionString);
        }

        public void ClearFieldProduct()
        {
            txtPid.Clear();
            txtPName.Clear();
            txtPprice.Clear();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "insert into productinfo values(@name,@price)";
                cmd = new SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("@name", txtPName.Text);
                cmd.Parameters.AddWithValue("@price", Convert.ToDouble(txtPprice.Text));

                conn.Open();
                int result = cmd.ExecuteNonQuery();

                if (result >= 1)
                {
                    MessageBox.Show("Product Added Successfull");
                    ClearFieldProduct();
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

        private void btnSearchProduct_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "select pname,pprice from productinfo where pid=@id";
                cmd=new SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("@id",Convert.ToInt32(txtPid.Text));

                conn.Open( );
                 dr=cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        txtPName.Text = dr["pname"].ToString();
                        txtPprice.Text = dr["pprice"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Product not found");
                    ClearFieldProduct();
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

        private void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "update productinfo set pname=@name,pprice=@price where pid=@id";
                cmd = new SqlCommand(str, conn);
                
                cmd.Parameters.AddWithValue("@name",txtPName.Text);
                cmd.Parameters.AddWithValue("@price",Convert.ToDouble(txtPprice.Text));
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtPid.Text));

                conn.Open();
                int result = cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("Product Update succesfully");
                    ClearFieldProduct();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "delete from productinfo where pid=@id";
                cmd = new SqlCommand(str, conn);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtPid.Text));

                conn.Open();
                int result = cmd.ExecuteNonQuery();
                if (result >= 1) 
                {
                    MessageBox.Show("Product delete successfully");
                    ClearFieldProduct();
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

        private void btnViewAllProduct_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "select * from productinfo";
                cmd = new SqlCommand(str, conn);

                conn.Open();
                dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView1.DataSource = dt;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close( );
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFieldProduct();
        }
    }
}
