// Functionality
import { IPortfolioService } from "Definitions/Service";
import ModuleService from "modules/common/Service/ModuleService";
import * as portfolioCommunication from "modules/Portfolio/Communication/PortfolioCommunication";

export default class PortfolioService extends ModuleService implements IPortfolioService {
	
	constructor() {
		super();
	}

	public async start() {
		
	}
	
 	public async addToPortfolio(isin: string, amount: number) {
		 await portfolioCommunication.addToPortfolio(isin, amount);
	}
}