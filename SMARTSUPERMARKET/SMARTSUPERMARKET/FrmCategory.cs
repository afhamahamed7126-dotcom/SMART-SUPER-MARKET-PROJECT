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

namespace SMARTSUPERMARKET
{
    public partial class FrmCategory : Form
    {
        DBConnect dbcon = new DBConnect();

        int CatId;
        public FrmCategory()
        {
            InitializeComponent();
            BindCategory();
        }

        private void frmCategory_Load(object sender, EventArgs e)
        {
            btnUbdate.Visible = false;
            btnDELETE.Visible = false;
            lblCatID.Visible = false;
            BindCategory();
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            if (txtCatname.Text == String.Empty)
            {
                MessageBox.Show("PLEASE ENTER  Category NAME  ", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCatname.Focus();
                return;

            }
            else if (rtbCatDescription.Text == String.Empty)
            {
                MessageBox.Show("PLEASE ENTER Category Description ", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rtbCatDescription.Focus();
                return;
            }
            else
            {

                SqlCommand cmd = new SqlCommand("select CategoryName from Category  where CategoryName=@CategoryName", dbcon.GetCon());
                cmd.Parameters.AddWithValue("@CategoryName", txtCatname.Text);
                dbcon.OpenCon();
                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    MessageBox.Show("CategoryName already exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtClear();
                }
                else
                {
                    cmd = new SqlCommand("INSERT INTO Category  (CategoryName, CategoryDesc) VALUES (@name, @desc)", dbcon.GetCon());
                    cmd.Parameters.AddWithValue("@name", txtCatname.Text);
                    cmd.Parameters.AddWithValue("@desc", rtbCatDescription.Text);


                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Category Inserted", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtClear();
                        BindCategory();
                    }
                    dbcon.CloseCon();
                }

            }
        }

        private void txtClear()
        {
            txtCatname.Clear();
            rtbCatDescription.Clear();

        }
        private void BindCategory()
        {
            SqlCommand cmd = new SqlCommand("select CatId,CategoryName,CategoryDesc from Category ", dbcon.GetCon());
            dbcon.OpenCon();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            dbcon.CloseCon();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            btnUbdate.Visible = true;
            btnDELETE.Visible = true;
            lblCatID.Visible = true;
            btnAddCategory.Visible = false;

            CatId = int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            txtCatname.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            rtbCatDescription.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
        }





        private void btnUbdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblCatID.Text == String.Empty)
                {
                    MessageBox.Show("Please select CategoryID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCatname.Focus();
                    return;
                }
                if (txtCatname.Text == String.Empty)
                {
                    MessageBox.Show("Please Enter CategoryName", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCatname.Focus();
                    return;
                }
                else if (rtbCatDesc.Text == String.Empty)
                {
                    MessageBox.Show("Please Enter Category Description", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    rtbCatDesc.Focus();
                    return;
                }
                else
                {
                    dbcon.OpenCon();
                    string query = "Update Category  set CategoryName='" + txtCatname.Text.Trim() + "', CategoryDesc='" + rtbCatDescription.Text.Trim() + "' where CatID='" + CatId + "'";
                    SqlCommand cmd = new SqlCommand(query, dbcon.GetCon());
                    //cmd.Parameters.AddWithValue("@CatID", Convert.ToInt32(lblCatID.Text));
                    //cmd.Parameters.AddWithValue("@CategoryName", txtCatname.Text);
                    //cmd.Parameters.AddWithValue("@CategoryDesc", rtbCatDesc.Text);
                    // cmd.CommandType = CommandType.StoredProcedure;
                    int i = cmd.ExecuteNonQuery();
                    dbcon.CloseCon();
                    if (i > 0)
                    {
                        MessageBox.Show("Category updated Successfully...", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtClear();
                        BindCategory();
                        btnUbdate.Visible = false;
                        btnDELETE.Visible = false;
                        btnAddCategory.Visible = true;
                        lblCatID.Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("update failed...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtClear();
                    }

                    dbcon.CloseCon();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnDELETE_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblCatID.Text == String.Empty)
                {
                    MessageBox.Show("Please select CategoryID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (lblCatID.Text != String.Empty)
                {
                    if (DialogResult.Yes == MessageBox.Show("Do You Want to Delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                    {
                        
                        string query = "delete from Category  where CatID='" + CatId+"'";
                        SqlCommand cmd = new SqlCommand(query, dbcon.GetCon());
                        
                        dbcon.OpenCon();
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            MessageBox.Show("Category  Deleted  Successfully...", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtClear();
                            BindCategory();
                            btnUbdate.Visible = false;
                            btnDELETE.Visible = false;
                            btnAddCategory.Visible = true;
                            lblCatID.Visible = false;
                        }
                        else
                        {
                            MessageBox.Show("Delete failed...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtClear();
                        }
                        dbcon.CloseCon();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void FrmCategory_Load(object sender, EventArgs e)
        {
           
        }

       
      

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void rtbCatDescription_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
