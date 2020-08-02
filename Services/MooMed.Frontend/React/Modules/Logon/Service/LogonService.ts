// Functionality
import ModuleService from "modules/common/Service/ModuleService";
import * as logonCommunication from "modules/Logon/Communication/LogonCommunication";
import { createPopUpMessage } from "helper/popUpMessageHelper";
import getTranslationForErrorCode from "Translations/helpers/IdentityErrorLookupHelper";

// Types
import { ILogonService } from "Definitions/Service";
import { PopUpMessageLevel } from "definitions/PopUpNotificationDefinitions";

export default class LogonService extends ModuleService implements ILogonService {

	public constructor()
	{
		super();
	}

	public async start() {		

	}

	public async login(email: string, password: string, rememberMe: boolean) {
		
		const response = await logonCommunication.login(email, password, rememberMe);
		
		if (response.success) {
			location.href = "/";
		} else if (!response.success) {
			const loginResultErrorCode = response.payload.IdentityErrorCode;

			const errorMessage = getTranslationForErrorCode(loginResultErrorCode);

			createPopUpMessage(errorMessage, PopUpMessageLevel.Error);
		}
	}
	
	public async register(email: string, userName: string, password: string, confirmPassword: string) {
		const request = new PostRequest<IRegisterModel, any>(requestUrls.logOn.register);
		const response = await request.post(registerModel);

		debugger;
		
		if (response.success) {
			location.reload();
		} else {
			createPopUpMessage(response.payload.responseJson, PopUpMessageLevel.Error, "Registration failed", 5000);
		}
	}
}