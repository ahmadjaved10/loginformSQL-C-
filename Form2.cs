using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace login_form
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // Ensure Form2 loads on application start if it’s the initial form
            this.Show();
        }

        // Hashing method for password security
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder hashedPassword = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    hashedPassword.Append(b.ToString("x2"));
                }
                return hashedPassword.ToString();
            }
        }

        // Sign-Up button click handler
        private void button1_Click(object sender, EventArgs e)
        {
            // Check for empty fields
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

            // Hash the password before saving it
            string hashedPassword = HashPassword(textBox4.Text);

            // Connection string (modify according to your setup)
            string connectionString = "Data Source=laptop-fn7q3gfd;Initial Catalog=facebookDB;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    MessageBox.Show("Connection opened successfully");  // Debugging message

                    // SQL command to insert user data with validation on email uniqueness
                    string query = "IF NOT EXISTS (SELECT 1 FROM Users WHERE Email = @Email) " +
                                   "BEGIN " +
                                   "INSERT INTO Users (FirstName, LastName, Email, Password) VALUES (@FirstName, @LastName, @Email, @Password) " +
                                   "END";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@FirstName", textBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@LastName", textBox2.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", textBox3.Text.Trim().ToLower()); // Ensure email is case-insensitive
                        cmd.Parameters.AddWithValue("@Password", hashedPassword);

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("User signed up successfully!");

                            // Redirect to Form1 after successful signup
                            Form1 form1 = new Form1();
                            form1.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Email is already registered.");
                        }
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

        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }

        // Placeholder for Sign-Up button click handler
        private void button2_Click(object sender, EventArgs e) { }

        // Text box change events (optional for validation or UI updates)
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void textBox3_TextChanged(object sender, EventArgs e) { }
        private void textBox4_TextChanged(object sender, EventArgs e) { }
    }
}
