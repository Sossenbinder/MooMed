namespace MooMed.Common.Definitions.Models.User.ErrorCodes
{
    public enum AccountValidationResult
    {
        None,
        Success,
        AlreadyValidated,
        ValidationGuidInvalid,
        TokenInvalid,
        AccountNotFound
    }
}
