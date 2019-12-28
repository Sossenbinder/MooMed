import * as $ from "jquery";
import { addAccount } from "data/reducers/accountReducer";

import requestUrls from "./requestUrls";

import { store } from "data/store";

import renderMainView from "views/Pages/MainPage/Main";
import GetRequest from "helper/requests/GetRequest";

$(async () => {

	const request = new GetRequest<any>(requestUrls.account.getAccount);
	const response = await request.send();

	if (response.success) {
		store.dispatch(addAccount(response.payload.data));
		renderMainView();
	}
});