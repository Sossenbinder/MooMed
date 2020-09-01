using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Configuration;
using MooMed.Core.Code.Helper.Email.Interface;

namespace MooMed.Core.Code.Helper.Email
{
	public class EmailManager : IEmailManager
	{
		[NotNull]
		private readonly string _smtpClientUserName;

		[NotNull]
		private readonly string _smtpClientPassword;

		public EmailManager([NotNull] IConfigProvider configProvider)
		{
			_smtpClientUserName = configProvider.ReadValueOrFail<string>("MooMed_Email_Name");
			_smtpClientPassword = configProvider.ReadDecryptedValueOrFail<string>("MooMed_Email_Password");
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
				UseDefaultCredentials = true,
				Credentials = new NetworkCredential(_smtpClientUserName, _smtpClientPassword),
			};

			return smtpClient;
		}

		public async Task Send(string address, string subject, string messageContent)
			=> await Send(new List<string>() { address }, subject, messageContent);

		public async Task Send([NotNull] IEnumerable<string> recipients, string subject, [NotNull] string messageContent)
		{
			var message = new MailMessage();

			foreach (var recipient in recipients)
			{
				message.To.Add(recipient);
			}

			message.Subject = subject;
			message.Body = messageContent;
			message.From = new MailAddress(_smtpClientUserName);

			using (var smtpClient = GetSmtpClient())
			{
				await smtpClient.SendMailAsync(message);
			}
		}
	}
}