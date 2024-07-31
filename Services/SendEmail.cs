
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using SendEmailService.NewFolder;

namespace SendEmailService.Services
{
    public class SendEmail : ISendEmail
    {
        private readonly SendEmailSettingsModel _sendEmailSettingsModel;
        public SendEmail(IOptions<SendEmailSettingsModel> sendEmailSettingsModel)
        {
            _sendEmailSettingsModel = sendEmailSettingsModel.Value;
        }


        public async Task Send(string emailTo, string subject, string body, List<IFormFile> attachments)
        {
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_sendEmailSettingsModel.Email),
                Subject = subject,
            };

            email.To.Add(MailboxAddress.Parse(emailTo));
            var builder = new BodyBuilder();

            if (attachments != null)
            {

                byte[] dataBytes;
                foreach(var file in attachments)
                {
                    if(file.Length> 0)
                    {
                        using var memoryStream = new MemoryStream();
                        file.CopyTo(memoryStream);
                        dataBytes= memoryStream.ToArray();
                        builder.Attachments.Add( file.FileName, dataBytes ,ContentType.Parse(file.ContentType));
                        memoryStream.Close();

                    }
                }
            }

            builder.HtmlBody = body;
            email.Body = builder.ToMessageBody();

            email.From.Add(new MailboxAddress(_sendEmailSettingsModel.DispalyName, _sendEmailSettingsModel.Email));

            using var smtp = new SmtpClient();
            smtp.Connect(_sendEmailSettingsModel.Host, _sendEmailSettingsModel.Port , MailKit.Security.SecureSocketOptions.StartTls);

            smtp.Authenticate(_sendEmailSettingsModel.Email, _sendEmailSettingsModel.Password);

            await smtp.SendAsync(email);

            smtp.Disconnect(true);


        }
    }
}
