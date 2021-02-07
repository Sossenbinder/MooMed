// Functionality
import { AccountOnlineState } from "Enums/moomedEnums";
import { Account } from "modules/account/types";

export type Friend = Account & {
	onlineState: AccountOnlineState;
}

export type OnlineStateNotification = {
	accountId: number;
	accountOnlineState: AccountOnlineState;
}

export type AccountChangeNotification = {
	account: Account;
}