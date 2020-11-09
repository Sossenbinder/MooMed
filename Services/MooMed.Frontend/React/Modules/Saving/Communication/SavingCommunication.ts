// Types
import { Currency } from "enums/moomedEnums"
import { VoidPostRequest } from "helper/requests/PostRequest";
import { Network } from "modules/Saving/types";

const savingRequests = {
	SetCurrency: "/Saving/SetCurrency",
}

export const setCurrency = (currency: Currency) => {

	const request = new VoidPostRequest<Network.SetCurrency.Request>(savingRequests.SetCurrency);

	return request.post({
		Currency: currency,
	});
}