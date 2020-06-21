// Functionality
import { Account, Network } from "modules/Account/types";
import GetRequest from "helper/requests/GetRequest";
import PostRequest from "helper/requests/PostRequest";

const accountRequests = {
	getOwnAccount: "/Account/GetOwnAccount",
	getAccount: "/Account/GetAccount",
};

export async function getOwnAccount() {
	const request = new GetRequest<Account>(accountRequests.getOwnAccount);

	return await request.send();    
}

export async function getAccount(accountId: number) {
	const request = new PostRequest<Network.GetAccountRequest, Account>(accountRequests.getAccount);

	return await request.post({
		accountId
	});
}