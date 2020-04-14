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