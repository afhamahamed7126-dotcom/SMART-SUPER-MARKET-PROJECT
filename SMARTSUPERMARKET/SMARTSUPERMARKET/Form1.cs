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
    public partial class Form1 : Form
    {
        DBConnect dbcon = new DBConnect();
       public static String loginname, logintype;
        public Form1()
        {
            InitializeComponent();
        }

        private void combRole_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbRole.SelectedIndex = 0;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if(cmbRole.SelectedIndex>0)
                {
                    if (txtUsername.Text == String.Empty)
                    {
                        MessageBox.Show("PLEASE ENTER  VALID USERNAME  ", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtUsername.Focus();
                        return;

                    }
                    if (txtpass.Text==String.Empty)
                    {
                        MessageBox.Show("PLEASE ENTER VALID PASSWOARD  ", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtpass.Focus();
                        return;
                    }
                    if (cmbRole.SelectedIndex > 0 && txtUsername.Text != String.Empty && txtpass.Text != String.Empty)
                    {
                        //login code 
                        if(cmbRole.Text=="Admin")
                        {

                            SqlCommand cmd = new SqlCommand("select top 1 AdminID,Password,FullName from tblAdmin where AdminID=@AdminID and Password=@Password", dbcon.GetCon());
                            cmd.Parameters.AddWithValue("@AdminID", txtUsername.Text);
                            cmd.Parameters.AddWithValue("@Password", txtpass.Text);
                            dbcon.OpenCon();
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                MessageBox.Show("LOGIN SUCCESS WELCOME TO HOME PAGE   ", "SUCCES", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                loginname = txtUsername.Text;
                                logintype = cmbRole.Text;
                                clrValue();
                                this.Hide();
                                Form3 fm = new Form3();
                                fm.Show();
                            }
                            else
                            {
                                MessageBox.Show(" INVALID LOGIN PLEASE CHECK USER NAME PASSWOARD   ", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            
                        }
                        else if (cmbRole.Text == "Seller")
                        {
                            SqlCommand Cmd = new SqlCommand("Select top 1 SellerName,Sellerpass from tblseller Where SellerName=@SellerName and Sellerpass= @Sellerpass ", dbcon.GetCon());
                            object p1 = Cmd.Parameters.AddWithValue("@SellerName", txtUsername.Text);
                            object p = Cmd.Parameters.AddWithValue("@Sellerpass", txtpass.Text);
                            dbcon.OpenCon();
                            SqlDataAdapter da = new SqlDataAdapter(Cmd);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                MessageBox.Show("LOGIN SUCCESS WELCOME TO HOME PAGE   ", "SUCCES", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                loginname = txtUsername.Text;
                                logintype = cmbRole.Text;
                                clrValue();
                                this.Hide();
                                Form3 fm = new Form3();
                                fm.Show();
                            }
                            else
                            {
                                MessageBox.Show(" INVALID LOGIN PLEASE CHECK USER NAME PASSWOARD   ", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("PLEASE ENTER USERNAME OR PASSWORD  ", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        clrValue();

                    }
                }
                else
                {
                    MessageBox.Show("PLEASE SELECT ANY ROLE " ,"ERROR", MessageBoxButtons.OK,MessageBoxIcon.Error);
                    cmbRole.SelectedIndex = 0;
                    txtUsername.Clear();
                    txtpass.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void clrValue()
        {
            cmbRole.SelectedIndex = 0;
            txtUsername.Clear();
            txtpass.Clear();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
