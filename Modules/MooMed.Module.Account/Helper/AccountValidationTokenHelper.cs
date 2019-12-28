using System;
using System.Text;
using JetBrains.Annotations;
using MooMed.Core.DataTypes;
using MooMed.Module.Accounts.Helper.Interface;

namespace MooMed.Module.Accounts.Helper
{
    internal class AccountValidationTokenHelper : IAccountValidationTokenHelper
    {
        [NotNull]
        public string Serialize(AccountValidationTokenData validationTokenData)
        {
            var combinedParams = $"{validationTokenData.AccountId}|{validationTokenData.ValidationGuid.ToString()}";

            var serializedData = Convert.ToBase64String(Encoding.UTF8.GetBytes(combinedParams));

            return serializedData;
        }

        [NotNull]
        public AccountValidationTokenData Deserialize([NotNull] string tokenRaw)
        {
            var dataFromToken = new AccountValidationTokenData();

            var deserializedToken = Encoding.UTF8.GetString(Convert.FromBase64String(tokenRaw));

            var deserializedParamSplit = deserializedToken.Split('|');

            dataFromToken.AccountId = int.Parse(deserializedParamSplit[0]);
            dataFromToken.ValidationGuid = new Guid(deserializedParamSplit[1]);

            return dataFromToken;
        }
    }
}
