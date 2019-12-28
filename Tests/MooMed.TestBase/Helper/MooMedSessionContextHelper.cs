using System;
using JetBrains.Annotations;
using MooMed.Common.Definitions.Models.Session;
using MooMed.Common.Definitions.Models.User;

namespace MooMed.TestBase.Helper
{
    public class MooMedSessionContextHelper
    {
        [NotNull]
        public static SessionContext CreateFakeSessionContext()
        {
            return new SessionContext
            {
                Account = new Account()
                {
                    Id = 123123123,
                    Email = "blablabla@test.de",
                    UserName = "Tester1234",
                    EmailValidated = true,
                    LastAccessedAt = DateTime.Now
                }
            };
        }
    }
}
