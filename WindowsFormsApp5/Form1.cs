using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-EBA91V4\\SQLEXPRESS;Initial Catalog=Course;Integrated Security=True");

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                int studid = int.Parse(textBox1.Text);
                string studname = textBox2.Text, coursename = comboBox1.Text, duration = comboBox2.Text, installment = textBox4.Text, onetime = textBox5.Text;
                con.Open();
                SqlCommand cmd = new SqlCommand("InsertCourse_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                
                cmd.Parameters.AddWithValue("@studid", studid);
                cmd.Parameters.AddWithValue("@studname", studname);
                cmd.Parameters.AddWithValue("@coursename", coursename);
                cmd.Parameters.AddWithValue("@duration", duration);
                cmd.Parameters.AddWithValue("@installment", installment);
                cmd.Parameters.AddWithValue("@onetime", onetime);
                cmd.ExecuteNonQuery();

                con.Close();

                MessageBox.Show("Successfully Inserted");
                GetCourseList();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                int studid = int.Parse(textBox1.Text);
                string studname = textBox2.Text, coursename = comboBox1.Text, duration = comboBox2.Text, installment = textBox4.Text, onetime = textBox5.Text;
                con.Open();
                SqlCommand cmd = new SqlCommand("UpdateCourse_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;

               
                cmd.Parameters.AddWithValue("@studid", studid);
                cmd.Parameters.AddWithValue("@studname", studname);
                cmd.Parameters.AddWithValue("@coursename", coursename);
                cmd.Parameters.AddWithValue("@duration", duration);
                cmd.Parameters.AddWithValue("@installment", installment);
                cmd.Parameters.AddWithValue("@onetime", onetime);
                cmd.ExecuteNonQuery();

                con.Close();

                MessageBox.Show("Successfully Updated");
                GetCourseList();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete?", "Delete Document", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int studid = int.Parse(textBox1.Text);

                con.Open();

                SqlCommand cmd = new SqlCommand("DeleteCourse_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                
                cmd.Parameters.AddWithValue("@studid", studid);

                cmd.ExecuteNonQuery();

                con.Close();

                MessageBox.Show("Successfully Deleted");
                GetCourseList();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (ValidateStudId())
            {
                int studid = int.Parse(textBox1.Text);
                SqlCommand cmd = new SqlCommand("DisplayCourse_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;

              
                cmd.Parameters.AddWithValue("@studid", studid);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sqlDataAdapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private bool ValidateInput()
        {
           
            if (!int.TryParse(textBox1.Text, out int studid) || studid <= 0)
            {
                MessageBox.Show("Invalid StudID. Please enter a valid positive integer.");
                return false;
            }

          
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("StudName cannot be empty.");
                return false;
            }

          
            if (string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                MessageBox.Show("Coursename cannot be empty.");
                return false;
            }

            
            if (string.IsNullOrWhiteSpace(comboBox2.Text))
            {
                MessageBox.Show("Duration cannot be empty.");
                return false;
            }

            
            if (!double.TryParse(textBox4.Text, out double installment) || installment < 0)
            {
                MessageBox.Show("Invalid Installment. Please enter a valid non-negative number.");
                return false;
            }

         
            if (!double.TryParse(textBox5.Text, out double onetime) || onetime < 0)
            {
                MessageBox.Show("Invalid Onetime. Please enter a valid non-negative number.");
                return false;
            }

       
            return true;
        }

        private bool ValidateStudId()
        {
           
            if (!int.TryParse(textBox1.Text, out int studid) || studid <= 0)
            {
                MessageBox.Show("Invalid StudID. Please enter a valid positive integer.");
                return false;
            }

          
            return true;
        }

        void GetCourseList()
        {
            SqlCommand cmd = new SqlCommand("exec ListCourse_SP", con);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetCourseList();
        }
    }
}
