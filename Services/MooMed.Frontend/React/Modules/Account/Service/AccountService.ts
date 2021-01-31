// Functionality
import { IAccountService } from "Definitions/Service";
import ModuleService from "modules/common/Service/ModuleService";
import * as accountCommunication from "modules/Account/Communication/AccountCommunication";
import { reducer as accountReducer } from "modules/account/Reducer/AccountReducer";

// Types
import { Account, PersonalData, PasswordData } from "modules/Account/types";

export default class AccountService extends ModuleService implements IAccountService {

	public constructor() {
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

	public async updateProfilePicture(file: File): Promise<void> {

	}

	public async updatePersonalData(personalData: PersonalData): Promise<boolean> {
		const response = await accountCommunication.updatePersonalData(personalData);

		if (response.success) {
			const currentAccount = { ...this.getStore().accountReducer.data };

			Object.keys(personalData).forEach(x => {
				if (personalData[x]) {
					currentAccount[x] = personalData[x];
				}
			});

			this.dispatch(accountReducer.update(currentAccount));
		}

		return response.success;
	}

	public async updatePassword(passwordData: PasswordData): Promise<boolean> {
		const response = await accountCommunication.updatePassword(passwordData);
		return response.success;
	}
}