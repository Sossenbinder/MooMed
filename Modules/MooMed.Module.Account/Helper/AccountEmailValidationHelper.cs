using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions;
using MooMed.Common.Definitions.Configuration;
using MooMed.Core.Code.Helper.Email.Interface;
using MooMed.Core.Translations;
using MooMed.Core.Translations.Helper;
using MooMed.Core.Translations.Resources;
using MooMed.Module.Accounts.Helper.Interface;

namespace MooMed.Module.Accounts.Helper
{
	public class AccountEmailValidationHelper : IAccountValidationEmailHelper
	{
		private readonly IEmailManager _emailManager;

		private readonly string _urlPrefix;

		public AccountEmailValidationHelper(
			IEmailManager emailManager,
			IUrlHelper urlHelper,
			IConfigProvider configProvider)
		{
			_emailManager = emailManager;
			_urlPrefix = urlHelper.GetDeploymentUrl(configProvider["ASPNETCORE_ENVIRONMENT"]);
		}

		public async Task SendAccountValidationEmail(Language lang, string recipientMail, int accountId, string accountValidationToken)
		{
			using (new TranslationScope(lang))
			{
				var emailBody = TranslationFormatter.FormatWithParams(Translation.AccountEmailValidationBody, _urlPrefix, accountId.ToString(), accountValidationToken);
				await _emailManager.Send(recipientMail, Translation.AccountEmailValidationSubject, emailBody);
			}
		}
	}
}