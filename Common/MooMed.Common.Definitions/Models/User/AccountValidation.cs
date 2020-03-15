using System;
using MooMed.Common.Definitions.Interface;

namespace MooMed.Common.Definitions.Models.User
{
    public class AccountValidation : IModel
    {
        public int AccountId { get; set; }

        public Guid ValidationGuid { get; set; }
    }
}
