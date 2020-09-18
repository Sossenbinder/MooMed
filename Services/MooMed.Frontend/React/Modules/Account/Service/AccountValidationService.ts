// Functionality
import { IAccountValidationService } from "Definitions/Service"
import ModuleService from "modules/common/Service/ModuleService"
import * as accountValidationCommunication from "modules/Account/Communication/AccountValidationCommunication";

// Types
import { AccountValidationResult } from "modules/Account/types";

export default class AccountValidationService extends ModuleService implements IAccountValidationService {

	public constructor() {
		super()
	}

	public async start(): Promise<void> {

	}

	public async validateRegistration(accountId: number, token: string){
		const response = await accountValidationCommunication.validateRegistration(accountId, token);

		return {
			success: response.success,
			identityErrorCode: response.payload.identityErrorCode,
		} as AccountValidationResult;
	}
	
}