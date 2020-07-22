// Functionality
import { IStocksService } from "Definitions/Service";
import ModuleService from "modules/common/Service/ModuleService";
import * as stocksCommunication from "modules/Stocks/Communication/StocksCommunication";
import { reducer as exchangeTradedsReducer } from "modules/Stocks/Reducer/ExchangeTradedsReducer";

export default class StocksService extends ModuleService implements IStocksService {
	
	constructor() {
		super();
	}

	public async start() {
		
		const stocksResponse = await stocksCommunication.getExchangeTradeds();

		if (stocksResponse.success)	{
			this.dispatch(exchangeTradedsReducer.add(stocksResponse.payload));
		}
	}
}