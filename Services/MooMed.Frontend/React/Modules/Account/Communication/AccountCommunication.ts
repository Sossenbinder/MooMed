// Functionality
import GetRequest from "helper/requests/GetRequest";
import PostRequest from "helper/requests/PostRequest";

// Types
import { Account, Network, PersonalData, PasswordData } from "modules/Account/types";

const accountRequests = {
	getOwnAccount: "/Account/GetOwnAccount",
	getAccount: "/Account/GetAccount",
	updatePersonalData: "/Profile/UpdatePersonalData",
	updatePassword: "/Profile/UpdatePassword",
};

export const getOwnAccount = async () => {
	const request = new GetRequest<Account>(accountRequests.getOwnAccount);
	return await request.get();
}

export const getAccount = async (accountId: number) => {
	const request = new PostRequest<Network.GetAccount.Request, Account>(accountRequests.getAccount);

	return await request.post({
		accountId
	});
}

export const updatePersonalData = async (personalData: PersonalData) => {
	const request = new PostRequest<Network.UpdatePersonalData.Request, Network.UpdatePersonalData.Request>(accountRequests.updatePersonalData);

	return await request.post(personalData);
}

export const updatePassword = async (passwordData: PasswordData) => {
	const request = new PostRequest<Network.UpdatePassword.Request, Network.UpdatePassword.Response>(accountRequests.updatePassword);

	return await request.post(passwordData);
}