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

namespace MailSender
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        public StringBuilder email = new StringBuilder();
        public StringBuilder password = new StringBuilder();
        StringBuilder query = new StringBuilder();
        SqlConnection connection = null;
        string connectionString = "Server=.;Database=MailSender;User Id=Cuni;Password=123456;";
        SqlCommand command = null;
        SqlDataReader reader = null;
        FormMain formMain = new FormMain();

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            try
            {
                email.Append(textBoxEmail.Text);
                password.Append(textBoxPassword.Text);
                query.Append($"Select * From Table_Users Where email = '{email}' And password = '{password}'");

                if (SqlGetUser(query.ToString()))
                {
                    MessageBox.Show("Hoşgeldiniz: " + email, "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Hide();
                    formMain.email = email;
                    formMain.password = password;
                    formMain.ShowDialog();
                    email.Clear();
                    password.Clear();
                    query.Clear();
                    Close();
                }
                else
                    MessageBox.Show("Hatalı Giriş!\nBilgilerinizi Kontrol Ediniz", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                email.Clear();
                password.Clear();
                query.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ex.message: " + ex.Message + " stacktrace: " + ex.StackTrace, "Login Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool SqlGetUser(string query)
        {
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                command = new SqlCommand(query, connection);
                reader = command.ExecuteReader();

                if (reader.Read())
                    return true;
                else
                {
                    reader.Close();
                    command.Dispose();
                    connection.Close();
                    connection.Dispose();
                    return false;
                }

            }
            catch (Exception ex)
            {
                reader.Close();
                command.Dispose();
                connection.Close();
                connection.Dispose();
                MessageBox.Show("ex.message: " + ex.Message + " stacktrace: " + ex.StackTrace, "SQL Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
