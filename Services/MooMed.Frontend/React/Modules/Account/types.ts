import { IdentityErrorCode } from "enums/moomedEnums";

export type Account = {
	userName: string;
	email: string;
	profilePicturePath: string;
	lastAccessedAt: Date;
	id: number;
}

export type AccountValidationResult = {
	success: boolean;
	identityErrorCode?: IdentityErrorCode;
}

export type PersonalData = {
	userName: string;
	email: string;
}

export type PasswordData = {
	oldPassword: string;
	newPassword: string;
}

export namespace Network {

	export namespace GetAccount {
		export type Request = {
			accountId: number;
		}
	}

	export namespace ValidateRegistration {
		export type Request = {
			accountId: number;
			token: string;
		}

		export type Response = {
			identityErrorCode: IdentityErrorCode;
		}
	}

	export namespace UpdatePersonalData {
		export type Request = PersonalData;

		export type Response = {			
			identityErrorCode: IdentityErrorCode;
		}
	}

	export namespace UpdatePassword {
		export type Request = PasswordData;

		export type Response = {			
			identityErrorCode: IdentityErrorCode;
		}
	}
}