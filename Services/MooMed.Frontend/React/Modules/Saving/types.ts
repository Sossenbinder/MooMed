import { Currency } from "enums/moomedEnums"

export type FreeFormSavingInfo = {
	name: string;
	amount: number;
}

export type SavingInfo = {
	income: number;
	rent: number;
	transport: number;
	freeFormSavingInfo: FreeFormSavingInfo;
	currency: Currency;
}

export namespace Network {
	export namespace SetCurrency {
		export type Request = {
			Currency: Currency;
		}
	}
}