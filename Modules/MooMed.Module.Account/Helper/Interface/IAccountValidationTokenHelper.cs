using JetBrains.Annotations;
using MooMed.Core.DataTypes;

namespace MooMed.Module.Accounts.Helper.Interface
{
    public interface IAccountValidationTokenHelper
    {
        string Serialize([NotNull] AccountValidationTokenData validationTokenData);

        AccountValidationTokenData Deserialize(string tokenRaw);
    }
}
