using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions;
using MooMed.Core.Translations;

namespace MooMed.Module.Accounts.Helper.Interface
{
    public interface IAccountValidationEmailHelper
    {
        Task SendAccountValidationEmail(Language lang, [NotNull] string recipient, [NotNull] string accountValidationToken);
    }
}
