import { Currency, CashFlowItemType, CashFlow } from "enums/moomedEnums"

export type CashFlowItem = {
	name: string;
	identifier?: string;
	cashFlowItemType: CashFlowItemType;
	amount: number;
	flowType: CashFlow;
}

export type BasicSavingInfo = {
	income: CashFlowItem;
	rent: CashFlowItem;
	groceries: CashFlowItem;
}

export type SavingInfo = {
	currency: Currency;
	basicSavingInfo: BasicSavingInfo;
	freeFormSavingInfo: Array<CashFlowItem>;
}

export namespace Network {
	export namespace SetCurrency {
		export type Request = {
			Currency: Currency;
		}
	}
	
	export namespace SetCashFlowItems {
		export type Request = {
			cashFlowItems: Array<CashFlowItem>;
		}
	}

	export namespace GetSavingInfo {
		export type Response = SavingInfo;
	}
}