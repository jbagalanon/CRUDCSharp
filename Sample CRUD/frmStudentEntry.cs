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
namespace Sample_CRUD
{
    public partial class frmStudentEntry : Form
    {
        SqlConnection cn = new SqlConnection(@"Data Source=SERVER\SQLEXPRESS;Initial Catalog=CRUD_db;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        frmLogin fr1;
        string _studentno, _lnamem, _fname, _mi, _tracks;
        public frmStudentEntry(frmLogin f1)
        {
            InitializeComponent();
            fr1 = f1;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        public void getUser(string puser)
        {
            lblUser.Text = puser;
        }

        private void cboTrack_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int i = dataGridView1.CurrentRow.Index;
            _studentno = dataGridView1[0, i].Value.ToString();
            _lnamem = dataGridView1[1, i].Value.ToString();
            _fname = dataGridView1[2, i].Value.ToString();
            _mi = dataGridView1[3, i].Value.ToString();
            _tracks = dataGridView1[4, i].Value.ToString();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            txtFname.Text = _fname;
            txtLname.Text = _lnamem;
            txtMI.Text = _mi;
            txtSNo.Text = _studentno;
            cboTrack.Text = _tracks;
            button2.Enabled = true;
            button3.Enabled = true;
            button1.Enabled = false;
            txtSNo.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to update this record?", "Exit App", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("update tblstudent set lname = '" + txtLname.Text + "', fname = '" + txtFname.Text + "', mi = '" + txtMI.Text + "', track = '" + cboTrack.Text + "' where studentno like '" + txtSNo.Text + "'", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Record has been successfully updated.", "RECORD UPDATED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                    LoadRecord();
                }
            }
            catch(Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to delete this record?", "Exit App", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("delete from tblstudent where studentno like '" + txtSNo.Text + "'", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Record has been successfully deleted.", "RECORD DELETED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                    LoadRecord();
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void LoadRecord()
        {
            dataGridView1.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("select * from tblStudent", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr.GetValue(0).ToString(), dr.GetValue(1).ToString(), dr.GetValue(2).ToString(), dr.GetValue(3).ToString(), dr.GetValue(4).ToString());
            }
            dr.Close();
            cn.Close();
            lblCount.Text = dataGridView1.RowCount.ToString();
             }
        private void cboTrack_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
               
                cn.Open();
                cm = new SqlCommand("insert into tblstudent (studentno, lname, fname, mi, track) values('" + txtSNo.Text + "','" + txtLname.Text + "','" + txtFname.Text + "','" + txtMI.Text + "','" + cboTrack.Text + "')", cn);
                cm.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Record has been successfully saved.", "RECORD SAVED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
                LoadRecord();
                      
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

           
        }
        private void Clear()
        {
            txtLname.Clear();
            txtFname.Clear();
            txtMI.Clear();
            txtSNo.Clear();
            cboTrack.Text = String.Empty;
            button2.Enabled = false;
            button3.Enabled = false;
            button1.Enabled = true;
            txtSNo.Enabled = true;
        }
    }
}
