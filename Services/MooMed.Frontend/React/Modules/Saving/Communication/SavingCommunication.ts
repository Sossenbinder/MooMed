// Types
import { Currency } from "enums/moomedEnums"
import { VoidPostRequest } from "helper/requests/PostRequest";
import GetRequest from "helper/requests/GetRequest";
import { BasicSavingInfo, Network } from "modules/Saving/types";

const savingRequests = {
	SetCurrency: "/Saving/SetCurrency",
	SaveBasicSettings: "/Saving/SaveBasicSettings",
	GetSavingInfo: "/Saving/GetSavingInfo",
}

export const setCurrency = (currency: Currency) => {

	const request = new VoidPostRequest<Network.SetCurrency.Request>(savingRequests.SetCurrency);

	return request.post({
		Currency: currency,
	});
}

export const saveBasicSettings = (basicSavingInfo: BasicSavingInfo) => {
	const request = new VoidPostRequest<Network.SaveBasicSettings.Request>(savingRequests.SaveBasicSettings);

	return request.post(basicSavingInfo);
}

export const getSavingInfo = () => {

	const request = new GetRequest<Network.GetSavingInfo.Response>(savingRequests.GetSavingInfo);

	return request.get();
}