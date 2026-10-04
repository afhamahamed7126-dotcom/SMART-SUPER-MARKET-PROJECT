using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SMARTSUPERMARKET
{
    public partial class frmAddNewSeller : Form
    {
        DBConnect dbcon = new DBConnect();
     

        public frmAddNewSeller()
        {
            InitializeComponent();
            BindSeller();
        }

        private void frmAddNewSeller_Load(object sender, EventArgs e)
        {
            lblseller.Visible = false;
            btnUbdate.Visible = false;
            btnDELETE.Visible = false;
            btnAdd.Visible = true;
            BindSeller();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtSellerName.Text == String.Empty)
            {
                MessageBox.Show("PLEASE ENTER SELLER NAME", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSellerName.Focus();
                return;
            }
            else if (txtpasword.Text == String.Empty)
            {
                MessageBox.Show("Please Enter password", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtpasword.Focus();
                return;
            }
            else
            {
                SqlCommand cmd = new SqlCommand("SELECT SellerName FROM tblSeller WHERE SellerName=@SellerName", dbcon.GetCon());
                cmd.Parameters.AddWithValue("@SellerName", txtSellerName.Text);
                dbcon.OpenCon();
                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    MessageBox.Show("Seller Name already exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtClear();
                }
                else
                {
                    cmd = new SqlCommand("spSellerInsert", dbcon.GetCon());
                    cmd.Parameters.AddWithValue("@SellerName", txtSellerName.Text);
                    cmd.Parameters.AddWithValue("@SellerAge", Convert.ToInt32(txtAge.Text));
                    cmd.Parameters.AddWithValue("@SellerPhone", txtphone.Text);
                    cmd.Parameters.AddWithValue("@SellerPass", txtpasword.Text);
                    cmd.CommandType = CommandType.StoredProcedure;
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Seller Inserted Successfully...", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtClear();
                        BindSeller();
                    }
                }
                dbcon.CloseCon();
            }
        }

            private void txtClear()
            {
                txtSellerName.Clear();
                txtAge.Clear();
                txtpasword.Clear();
                txtphone.Clear();
            }


            private void btnUbdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSellerName.Text == String.Empty)
                {
                    MessageBox.Show("Please select seller ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (txtSellerName.Text == String.Empty)
                {
                    MessageBox.Show("Please Enter Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSellerName.Focus();
                    return;
                }
                else if (txtpasword.Text == String.Empty)
                {
                    MessageBox.Show("Please Enter Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtpasword.Focus();
                    return;
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("SELECT SellerName FROM tblSeller WHERE SellerName=@SellerName", dbcon.GetCon());
                    cmd.Parameters.AddWithValue("@SellerName", txtSellerName.Text);
                    dbcon.OpenCon();
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        MessageBox.Show("Seller Name already exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtClear();
                    }
                    else
                    {
                        cmd = new SqlCommand("spSellerUpdate", dbcon.GetCon());
                        cmd.Parameters.AddWithValue("@SellerID", Convert.ToInt32(txtSellerName.Text));
                        cmd.Parameters.AddWithValue("@SellerName", txtSellerName.Text);
                        cmd.Parameters.AddWithValue("@SellerAge", Convert.ToInt32(txtAge.Text));
                        cmd.Parameters.AddWithValue("@SellerPhone", txtphone.Text);
                        cmd.Parameters.AddWithValue("@SellerPass", txtpasword.Text);
                        cmd.CommandType = CommandType.StoredProcedure;
                        int i = cmd.ExecuteNonQuery();
                        dbcon.CloseCon();
                        if (i > 0)
                        {
                            MessageBox.Show("Seller updated successfully...", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtClear();
                            BindSeller();
                            btnUbdate.Visible = false;
                            btnDELETE.Visible = false;
                            btnAdd.Visible = true;
                            txtSellerName.Visible = false;
                        }
                        else
                        {
                            MessageBox.Show("Update failed...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtClear();
                        }
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
                if (txtSellerName.Text == String.Empty)
                {
                    MessageBox.Show("Please select SellerID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (txtSellerName.Text != String.Empty)
                {
                    if (DialogResult.Yes == MessageBox.Show("Do You Want to Delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                    {
                        SqlCommand cmd = new SqlCommand("spSellerDelete", dbcon.GetCon());
                        cmd.Parameters.AddWithValue("@SellerID", Convert.ToInt32(txtSellerName.Text));
                        cmd.CommandType = CommandType.StoredProcedure;
                        dbcon.OpenCon();
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            MessageBox.Show("Seller Deleted Successfully...", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtClear();
                            BindSeller();
                            btnUbdate.Visible = false;
                            btnDELETE.Visible = false;
                            btnAdd.Visible = true;
                            txtSellerName.Visible = false;
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
        private void BindSeller()
        {
            SqlCommand cmd = new SqlCommand("SELECT SellerID, SellerName, SellerAge, SellerPhone FROM tblSeller", dbcon.GetCon());
            dbcon.OpenCon();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dbcon.CloseCon();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void dataGridView1_Click(object sender, EventArgs e)
        {
            btnUbdate.Visible = true;
            btnDELETE.Visible = true;
            txtSellerName.Visible = true;
            btnAdd.Visible = false;
            txtSellerName.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            txtSellerName.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();

            txtAge.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            txtphone.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            txtpasword.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
        }
    }

        
    }
