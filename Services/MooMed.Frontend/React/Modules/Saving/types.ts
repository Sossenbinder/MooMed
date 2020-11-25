import { Currency } from "enums/moomedEnums"

export type FreeFormSavingInfo = {
	name: string;
	amount: number;
}

export type BasicSavingInfo = {
	income: number;
	rent: number;
	groceries: number;
}

export type SavingInfo = {
	currency: Currency;
	basicSavingInfo: BasicSavingInfo;
	freeFormSavingInfo: Array<FreeFormSavingInfo>;
}

export namespace Network {
	export namespace SetCurrency {
		export type Request = {
			Currency: Currency;
		}
	}

	export namespace SaveBasicSettings {
		export type Request = BasicSavingInfo;
	}

	export namespace GetSavingInfo {
		export type Response = SavingInfo;
	}
}