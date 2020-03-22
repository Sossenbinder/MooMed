// Functionality
import { Friend } from "modules/Friends/types";
import { IFriendsService } from "Definitions/Service";
import ModuleService from "modules/common/Service/ModuleService";
import * as friendsCommunication from "modules/Friends/Communication/FriendsCommunication";
import { reducer as friendsReducer } from "modules/Friends/Reducer/FriendsReducer";

export default class FriendsService extends ModuleService implements IFriendsService {

	public async start() {
		const friendsResponse = await friendsCommunication.getFriends();
		
		if (friendsResponse.success){
			this.dispatch(friendsReducer.add(friendsResponse.payload));
		}
	}

	public async addFriend(friendId: number): Promise<void> {
		await friendsCommunication.addFriend(friendId);
	}
}