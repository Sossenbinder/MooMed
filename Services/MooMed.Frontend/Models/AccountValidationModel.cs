using MooMed.Core.DataTypes;

namespace MooMed.Frontend.Models
{
    public class AccountValidationModel
    {
        public string AccountName { get; set; }

        public AccountValidationTokenData AccountValidationTokenData { get; set; }
    }
}