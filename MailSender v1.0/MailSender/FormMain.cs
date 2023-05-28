using System;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;


namespace MailSender
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        public StringBuilder email = new StringBuilder();
        public StringBuilder password = new StringBuilder();

        private void buttonSendMail_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxFrom.Text != string.Empty && textBoxTo.Text != string.Empty && textBoxSubject.Text != string.Empty && textBoxBody.Text != string.Empty &&
                    textBoxFrom.Text != "(Kimden)" && textBoxTo.Text != "(Kime)" && textBoxSubject.Text != "(Konu)" && textBoxBody.Text != "(Mesaj)")
                {
                    MailMessage msg = new MailMessage();
                    SmtpClient smtpClient = new SmtpClient();

                    smtpClient.Credentials = new NetworkCredential(email.ToString(), password.ToString());
                    smtpClient.Port = 587;
                    smtpClient.Host = "smtp-mail.outlook.com";
                    smtpClient.EnableSsl = true;

                    msg.To.Add(textBoxTo.Text);
                    msg.From = new MailAddress(email.ToString());
                    msg.Subject = textBoxSubject.Text;
                    msg.Body = textBoxBody.Text;
                    smtpClient.Send(msg);
                    MessageBox.Show("Mail Gönderildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (SmtpException se)
            {
                MessageBox.Show("se.message: " + se.Message + " stacktrace: " + se.StackTrace, "Mail Gönderme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ex.message: " + ex.Message + " stacktrace: " + ex.StackTrace, "Mail Gönderme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox_Enter(object sender, EventArgs e)
        {
            try
            {
                TextBox textBox = (TextBox)sender;
                switch (textBox.Text)
                {
                    case "(Kimden)":
                        textBox.Text = string.Empty;
                        break;
                    case "(Kime)":
                        textBox.Text = string.Empty;
                        break;
                    case "(Konu)":
                        textBox.Text = string.Empty;
                        break;
                    case "(Mesaj)":
                        textBox.Text = string.Empty;
                        break;
                }               
                textBox.ForeColor = Color.Black;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ex.message: " + ex.Message + " stacktrace: ", "Placeholder Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            try
            {
                textBoxFrom.Text = email.ToString();
                textBoxFrom.ForeColor = Color.Black;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ex.message: " + ex.Message + " stacktrace: " + ex.StackTrace, "Load Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
