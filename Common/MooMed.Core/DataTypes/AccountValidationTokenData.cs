using System;

namespace MooMed.Core.DataTypes
{
    public class AccountValidationTokenData
    {
        public int AccountId { get; set; }

        public Guid ValidationGuid { get; set; }
    }
}
