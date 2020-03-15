// Functionality
import { Account } from "modules/Account/types";
import { IAccountService } from "Definitions/Service";
import * as accountCommunication from "modules/Account/Communication/AccountCommunication";

export default class AccountService implements IAccountService {
    
    public async getOwnAccount(): Promise<Account> {
        const response = await accountCommunication.getOwnAccount();
        return response.payload;
    }
}