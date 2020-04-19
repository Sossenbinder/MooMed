// Functionality
import { AccountOnlineState } from "Enums/moomedEnums";

export type Friend = {
	id: number;
	userName: string;
	email: string;
	lastAccessedAt: Date;
	profilePicturePath: string;
	onlineState: AccountOnlineState;
}

export type OnlineStateNotification = {
	accountId: number;
	accountOnlineState: AccountOnlineState;
}