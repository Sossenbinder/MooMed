// Functionality
import ModuleService from "modules/common/Service/ModuleService";
import * as logonCommunication from "modules/Logon/Communication/LogonCommunication";
import { createPopUpMessage } from "helper/popUpMessageHelper";
import getTranslationForErrorCode from "Translations/helpers/IdentityErrorLookupHelper";

// Types
import { ILogonService } from "Definitions/Service";
import { PopUpMessageLevel } from "definitions/PopUpNotificationDefinitions";
import { IdentityErrorCode } from "enums/moomedEnums";

export default class LogonService extends ModuleService implements ILogonService {

	public constructor()
	{
		super();
	}

	public async start() {		

	}

	public async login(email: string, password: string, rememberMe: boolean) {
		
		const response = await logonCommunication.login(email, password, rememberMe);
		
		this.handleIdentityResponse(response.success, response.payload?.IdentityErrorCode);
	}
	
	public async register(email: string, userName: string, password: string, confirmPassword: string) {

		const response = await logonCommunication.register(email, userName, password, confirmPassword);
		
		this.handleIdentityResponse(response.success, response.payload?.IdentityErrorCode);
	}

	public async logOff() {
		const response = await logonCommunication.register(email, userName, password, confirmPassword);
	}

	private handleIdentityResponse(success: boolean, errorCode?: IdentityErrorCode) {
		if (success) {
			location.reload();
		} else {
			const errorMessage = getTranslationForErrorCode(errorCode);

			createPopUpMessage(errorMessage, PopUpMessageLevel.Error);
		}
	}
}