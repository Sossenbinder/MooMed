using JetBrains.Annotations;

namespace MooMed.Common.Definitions.Models.User
{
    public class LoginModel
    {
        [NotNull]
        public string Email { get; set; }

        [NotNull]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
