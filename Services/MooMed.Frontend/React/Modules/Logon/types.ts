import { IdentityErrorCode } from "enums/moomedEnums";

export namespace Network {
	export namespace Login {
		export type Request = {
			Email: string;
			Password: string;
			RememberMe: boolean;
		}

		export type Response = {
			identityErrorCode: IdentityErrorCode;
		}
	}

	export namespace Register {
		export type Request = {
			Email: string;
			UserName: string;
			Password: string;
			ConfirmPassword: string;
		}

		export type Response = {
			identityErrorCode: IdentityErrorCode;
		}
	}
}