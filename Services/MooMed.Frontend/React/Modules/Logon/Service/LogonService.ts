// Functionality
import ModuleService from "modules/common/Service/ModuleService";
import * as logonCommunication from "modules/Logon/Communication/LogonCommunication";
import handleIdentityResponse from "modules/Common/Helper/IdentityResultHelper";

// Types
import { ILogonService } from "Definitions/Service";

export default class LogonService extends ModuleService implements ILogonService {

	public constructor()
	{
		super();
	}

	public async start() {		

	}

	public async login(email: string, password: string, rememberMe: boolean) {
		
		const response = await logonCommunication.login(email, password, rememberMe);
		
		handleIdentityResponse(response.success, response.payload?.identityErrorCode);
	}
	
	public async register(email: string, userName: string, password: string, confirmPassword: string) {

		const response = await logonCommunication.register(email, userName, password, confirmPassword);
		
		handleIdentityResponse(response.success, response.payload?.identityErrorCode);
	}

	public async logOff() {
		const response = await logonCommunication.logOff();
	}
}