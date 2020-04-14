export enum LoginResponseCode {
	None = 0,
	Success = 1,
	EmailNullOrEmpty = 2,
	PasswordNullOrEmpty = 3,
	EmailNotValidated = 4,
	AccountNotFound = 5,
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