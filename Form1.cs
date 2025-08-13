using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace login_form
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }

        private void textBox2_TextChanged(object sender, EventArgs e) { }

        private void loginButton_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=laptop-fn7q3gfd;Initial Catalog=facebookDB;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    MessageBox.Show("Connection Open");

                    string query = "INSERT INTO FacebookUsers (Username, Password) VALUES (@Username, @Password)";
                    using (SqlCommand cm = new SqlCommand(query, conn))
                    {
                        string username = textBoxUsername.Text;  // Access textBoxUsername
                        string password = textBoxPassword.Text;  // Access textBoxPassword

                        cm.Parameters.AddWithValue("@Username", username);
                        cm.Parameters.AddWithValue("@Password", password);

                        cm.ExecuteNonQuery();
                        MessageBox.Show("User added successfully");
                    }
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("SQL Error: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e) { }
    }
}
