// Functionality
import { VoidPostRequest } from "helper/Requests/PostRequest";
import { Network } from "modules/Portfolio/types";

const portFolioRequests = {
	AddToPortfolio: "/Portfolio/AddToPortfolio",
}

export async function addToPortfolio(isin: string, amount: number) {
	
    const request = new VoidPostRequest<Network.AddToPortfolio.Request>(portFolioRequests.AddToPortfolio);

    return await request.send({
		isin,
		amount,
	});    
}