using MooMed.Core.DataTypes;

namespace MooMed.Web.Models
{
    public class AccountValidationModel
    {
        public string AccountName { get; set; }

        public AccountValidationTokenData AccountValidationTokenData { get; set; }
    }
}