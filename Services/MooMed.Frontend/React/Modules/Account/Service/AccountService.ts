// Functionality
import { IAccountService } from "Definitions/Service";
import ModuleService from "modules/common/Service/ModuleService";
import * as accountCommunication from "modules/Account/Communication/AccountCommunication";

// Types
import { Account, PersonalData, PasswordData } from "modules/Account/types";

export default class AccountService extends ModuleService implements IAccountService {

	public constructor()
	{
		super();
	}

	public async start() {
		
	}

    public async getAccount(accountId: number): Promise<Account> {
        const response = await accountCommunication.getAccount(accountId);
        return response.payload;
    }
    
    public async getOwnAccount(): Promise<Account> {
        const response = await accountCommunication.getOwnAccount();
        return response.payload;
	}
	
	public async updatePersonalData(personalData: PersonalData): Promise<void> {
		const response = await accountCommunication.updatePersonalData(personalData);

		debugger;

		if (response.success)
		{
			
		}
	}
	
	public async updatePassword(passwordData: PasswordData): Promise<void> {
		const response = await accountCommunication.updatePassword(passwordData);

		debugger;
	}
}