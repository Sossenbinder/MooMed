// Types
import { Currency } from "enums/moomedEnums"
import { VoidPostRequest } from "helper/requests/PostRequest";
import GetRequest from "helper/requests/GetRequest";
import { CashFlowItem, Network } from "modules/Saving/types";

const savingRequests = {
	SetCurrency: "/Saving/SetCurrency",
	SetCashFlowItems: "/Saving/SetCashFlowItems",
	GetSavingInfo: "/Saving/GetSavingInfo",
}

export const setCurrency = (currency: Currency) => {

	const request = new VoidPostRequest<Network.SetCurrency.Request>(savingRequests.SetCurrency);

	return request.post({
		Currency: currency,
	});
}

export const setCashFlowItems = (cashFlowItems: Array<CashFlowItem>) => {
	const request = new VoidPostRequest<Network.SetCashFlowItems.Request>(savingRequests.SetCashFlowItems);

	return request.post({
		cashFlowItems: cashFlowItems,
	});
}

export const getSavingInfo = () => {

	const request = new GetRequest<Network.GetSavingInfo.Response>(savingRequests.GetSavingInfo);

	return request.get();
}