// Functionality
import { Network } from "modules/Account/types";
import PostRequest from "helper/requests/PostRequest";

const accountValidationRequests = {
	validateRegistration: "/AccountValidation/ValidateRegistration",
};

export async function validateRegistration(accountId: number, token: string) {
	const request = new PostRequest<Network.ValidateRegistration.Request, Network.ValidateRegistration.Response>(accountValidationRequests.validateRegistration);

	return await request.post({
		accountId,
		token,
	});    
}