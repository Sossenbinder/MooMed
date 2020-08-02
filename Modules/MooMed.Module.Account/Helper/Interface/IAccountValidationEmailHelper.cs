using System.Threading.Tasks;
using JetBrains.Annotations;
using MooMed.Common.Definitions;

namespace MooMed.Module.Accounts.Helper.Interface
{
    public interface IAccountValidationEmailHelper
    {
        Task SendAccountValidationEmail(Language lang, [NotNull] string recipient, int accountId, [NotNull] string accountValidationToken);
    }
}
