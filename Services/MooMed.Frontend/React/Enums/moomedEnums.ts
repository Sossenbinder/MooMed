
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
	None = 0,
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
}
