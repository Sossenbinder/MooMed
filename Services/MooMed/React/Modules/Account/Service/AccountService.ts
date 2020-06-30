// Functionality
import { Account } from "modules/Account/types";
import { IAccountService } from "Definitions/Service";
import ModuleService from "modules/common/Service/ModuleService";
import * as accountCommunication from "modules/Account/Communication/AccountCommunication";

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
}