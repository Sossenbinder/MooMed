using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions;
using MooMed.Core.Code.Helper.Email.Interface;
using MooMed.Core.Translations;
using MooMed.Core.Translations.Helper;
using MooMed.Core.Translations.Resources;
using MooMed.Module.Accounts.Helper.Interface;

namespace MooMed.Module.Accounts.Helper
{
    public class AccountEmailValidationHelper : IAccountValidationEmailHelper
    {
        [NotNull]
        private readonly IEmailManager _emailManager;

        public AccountEmailValidationHelper(
            [NotNull] IEmailManager emailManager)
        {
            _emailManager = emailManager;
        }

        public async Task SendAccountValidationEmail(Language lang, string recipientMail, int accountId, string accountValidationToken)
        {
            using (new TranslationScope(lang))
            {
                var emailBody = TranslationFormatter.FormatWithParams(Translation.AccountEmailValidationBody, "http://51.136.126.247", accountId.ToString(), accountValidationToken);
                await _emailManager.Send(recipientMail, Translation.AccountEmailValidationSubject, emailBody);
            }
        }
    }
}
