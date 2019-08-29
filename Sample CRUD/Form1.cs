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
    public partial class Form1 : Form
    {
        SqlConnection cn = new SqlConnection(@"Data Source=SERVER\SQLEXPRESS;Initial Catalog=CRUD_db;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean found = false;
                string name = "";
                cn.Open();
                cm = new SqlCommand("select * from tblUser where username like '" + txtUser.Text + "' and password like '" + txtPass.Text + "'", cn);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    found = true;
                    name = dr.GetValue(2).ToString();
                }
                else
                {
                    found = false;
                }
                dr.Close();
                cn.Close();
                if (found ==true)
                {
                    MessageBox.Show("Welcome " + name, "ACCESS GRANTED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form2 f2 = new Form2(this);
                    f2.getUser(name);
                    f2.LoadRecord();
                    txtPass.Clear();
                    txtUser.Clear();
                    f2.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Invalid username or password ", "ACCESS DENIED", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to exit this application?", "Exit App",MessageBoxButtons.YesNo , MessageBoxIcon.Information)== DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
