
export enum LoginResponseCode {
	None = 0,
	Success = 1,
	EmailNullOrEmpty = 2,
	PasswordNullOrEmpty = 3,
	EmailNotValidated = 4,
	AccountNotFound = 5,
	PasswordWrong = 6,
	UnknownFailure = 7,
}

export enum AccountValidationResult {
	None = 0,
	Success = 1,
	AlreadyValidated = 2,
	ValidationGuidInvalid = 3,
	TokenInvalid = 4,
	AccountNotFound = 5,
}

export enum AccountOnlineState {
	Offline = 0,
	Online = 1,
}

export enum NotificationType {
	None = 0,
	FriendOnlineStateChange = 1,
	NewChatMessage = 2,
}

export enum ExchangeTradedType {
	Etf = 0,
	Etc = 1,
	Etn = 2,
	ActiveEtf = 3,
}

export enum IdentityErrorCode {
	Success = 0,
	DefaultError = 1,
	ConcurrencyFailure = 2,
	PasswordMismatch = 3,
	InvalidToken = 4,
	LoginAlreadyAssociated = 5,
	InvalidUserName = 6,
	InvalidEmail = 7,
	EmailMissing = 8,
	DuplicateUserName = 9,
	DuplicateEmail = 10,
	InvalidRoleName = 11,
	DuplicateRoleName = 12,
	UserAlreadyHasPassword = 13,
	UserLockoutNotEnabled = 14,
	UserAlreadyInRole = 15,
	UserNameNullOrEmpty = 16,
	UserNotInRole = 17,
	PasswordTooShort = 18,
	PasswordRequiresNonAlphanumeric = 19,
	PasswordRequiresDigit = 20,
	PasswordRequiresLower = 21,
	PasswordRequiresUpper = 22,
	PasswordMissing = 23,
	EmailNotConfirmed = 24,
	EmailAlreadyConfirmed = 25,
}

export enum Currency {
	Euro = 0,
	Dollar = 1,
}

export enum CashFlow {
	Income = 0,
	Outcome = 1,
}

export enum CashFlowItemType {
	Unspecified = 0,
	Income = 1,
	Rent = 2,
	Groceries = 3,
}
