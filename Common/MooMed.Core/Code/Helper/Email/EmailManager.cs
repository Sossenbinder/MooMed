using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Core.Code.Configuration.Interface;
using MooMed.Core.Code.Helper.Email.Interface;

namespace MooMed.Core.Code.Helper.Email
{
    public class EmailManager : IEmailManager
    {
        [NotNull]
        private readonly string m_smtpClientUserName;

        [NotNull]
        private readonly string m_smtpClientPassword;

        public EmailManager([NotNull] IConfigSettingsProvider configSettingsProvider)
        {
            m_smtpClientUserName = configSettingsProvider.ReadValueOrFail<string>("MooMed_Email_Name");
            m_smtpClientPassword = configSettingsProvider.ReadDecryptedValueOrFail<string>("MooMed_Email_Password");
        }

        [NotNull]
        private SmtpClient GetSmtpClient()
        {
	        var smtpClient = new SmtpClient
	        {
		        Host = "smtp.gmail.com",
                Port = 587,
		        EnableSsl = true,
		        DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
		        Credentials = new NetworkCredential(m_smtpClientUserName, m_smtpClientPassword),
	        };

	        return smtpClient;
        }

        public async Task Send(string address, string subject, string messageContent)
            => await Send(new List<string>(){address}, subject, messageContent);

        public async Task Send([NotNull] IEnumerable<string> recipients, string subject, [NotNull] string messageContent)
        {
            var message = new MailMessage();

            foreach (var recipient in recipients)
            {
                message.To.Add(recipient);
            }

            message.Subject = subject;
            message.Body = messageContent;
            message.From = new MailAddress(m_smtpClientUserName);

            using (var smtpClient = GetSmtpClient())
            {
                await smtpClient.SendMailAsync(message);
            }
        }
    }
}
