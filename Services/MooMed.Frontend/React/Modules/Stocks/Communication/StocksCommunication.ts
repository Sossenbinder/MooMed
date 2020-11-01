// Functionality
import GetRequest from "helper/requests/GetRequest";
import { Network } from "modules/Stocks/types";

const stockRequests = {
	GetExchangeTradeds: "/Stocks/GetExchangeTradeds",
}

export async function getExchangeTradeds() {
	
    const request = new GetRequest<Network.GetExchangeTradeds.Response>(stockRequests.GetExchangeTradeds);

    return await request.send();    
}