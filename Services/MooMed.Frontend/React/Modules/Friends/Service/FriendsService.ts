// Functionality
import { IFriendsService } from "Definitions/Service";
import ModuleService from "modules/common/Service/ModuleService";
import * as friendsCommunication from "modules/Friends/Communication/FriendsCommunication";
import { reducer as friendsReducer } from "modules/Friends/Reducer/FriendsReducer";
import { services } from "helper/serviceRegistry";
import { NotificationType } from "enums/moomedEnums";
import { OnlineStateNotification, AccountChangeNotification } from "../types";
import { SignalRNotification } from "data/notifications";

export default class FriendsService extends ModuleService implements IFriendsService {

	constructor() {

		super();

		const notificationService = services.NotificationService;

		notificationService.subscribe<OnlineStateNotification>(
			NotificationType.FriendOnlineStateChange,
			this.onOnlineStateUpdated);

		notificationService.subscribe<AccountChangeNotification>(
			NotificationType.AccountChange,
			this.onAccountChange);
	}

	public async start() {
		friendsCommunication.getFriends().then(friendsResponse => {
			if (friendsResponse.success && friendsResponse.payload) {
				this.dispatch(friendsReducer.add(friendsResponse.payload));
			}
		});
	}

	public async addFriend(friendId: number): Promise<void> {
		await friendsCommunication.addFriend(friendId);
	}

	private onOnlineStateUpdated = (notification: SignalRNotification<OnlineStateNotification>) => {

		const notificationData = notification.data;

		const respectiveFriend = this.getStore().friendsReducer.data.find(x => x.id === notificationData.accountId);

		respectiveFriend.onlineState = notificationData.accountOnlineState;

		this.dispatch(friendsReducer.update(respectiveFriend));
	}

	private onAccountChange = (notification: SignalRNotification<AccountChangeNotification>) => {

		const notificationData = notification.data;

		const respectiveFriend = this.getStore().friendsReducer.data.find(x => x.id === notificationData.account.id);

		const newFriend = {
			...notificationData.account,
			...respectiveFriend,
		};

		this.dispatch(friendsReducer.update(newFriend));
	}
}