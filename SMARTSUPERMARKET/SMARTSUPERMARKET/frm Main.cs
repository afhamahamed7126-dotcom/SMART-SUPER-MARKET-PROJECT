using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMARTSUPERMARKET
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            if(Form1.loginname!=null)
            {
                toolStripStatusLabel2.Text = Form1.loginname;
            }
            if(Form1.logintype!=null && Form1.logintype == "Seller")
            {
                masterToolStripMenuItem.Enabled = false;
                masterToolStripMenuItem.ForeColor = Color.Red;
                productToolStripMenuItem.Enabled = false;
                productToolStripMenuItem.ForeColor = Color.Red;
                addUserToolStripMenuItem.Enabled = false;
                addUserToolStripMenuItem.ForeColor = Color.Red;
            }
        }

        private void addCategotryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCategory frm= new FrmCategory();
            frm.ShowDialog();
        }

        private void cToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCategory frm = new FrmCategory();
            frm.ShowDialog();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 abt = new AboutBox1();
            abt.Show();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
          
           DialogResult dialog = MessageBox.Show("DO you relly want to close ?", "close", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
            if (dialog == DialogResult.No)
            {
                e.Cancel = true; 
            }
            else
            {
                Application.Exit();
            }
                   
                
                    
        }

        private void sellerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddNewSeller fseller = new frmAddNewSeller();
            fseller.ShowDialog();
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ADMIN aDMIN = new ADMIN();
            aDMIN.ShowDialog();

        }

        private void addProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddProduct ap = new AddProduct();
            ap.ShowDialog();

        }

        private void sellerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Selling sel = new Selling();
            sel.ShowDialog();
        }
    }
}
