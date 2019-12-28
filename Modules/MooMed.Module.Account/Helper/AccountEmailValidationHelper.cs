using System.Threading.Tasks;
using JetBrains.Annotations;
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
        private readonly IEmailManager m_emailManager;

        public AccountEmailValidationHelper(
            [NotNull] IEmailManager emailManager)
        {
            m_emailManager = emailManager;
        }

        public async Task SendAccountValidationEmail(Language lang, string recipient, string accountValidationToken)
        {
            using (new TranslationScope(lang))
            {
                var emailBody = TranslationFormatter.FormatWithParams(Translation.AccountEmailValidationBody, accountValidationToken);
                await m_emailManager.Send(recipient, Translation.AccountEmailValidationSubject, emailBody);
            }
        }
    }
}
