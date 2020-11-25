// Functionality
import { VoidPostRequest } from "helper/requests/PostRequest";
import { Network } from "modules/Portfolio/types";

const portFolioRequests = {
	AddToPortfolio: "/Portfolio/AddToPortfolio",
}

export async function addToPortfolio(isin: string, amount: number) {
	
    const request = new VoidPostRequest<Network.AddToPortfolio.Request>(portFolioRequests.AddToPortfolio);

    return await request.post({
		isin,
		amount,
	});    
}