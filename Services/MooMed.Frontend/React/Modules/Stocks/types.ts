import { ExchangeTradedType } from "enums/moomedEnums";

export type ExchangeTradedUiFilters = {
	type?: ExchangeTradedType;
}

export type ExchangeTradedItem = {
	type: ExchangeTradedType;
	isin: string;
	productFamily: string;
	xetraSymbol: string;
	feePercentage?: number;
	ongoingCharges?: number;
	profitUse: string;
	replicationMethod: string;
	fundCurrency: string;
	tradingCurrency: string;
}

export namespace Network {
	export namespace GetExchangeTradeds {
		export type Request = {
			exchangeTradedType?: ExchangeTradedType;
		}

		export type Response = Array<ExchangeTradedItem>;
	}	
}