// Functionality
import { Friend } from "modules/Friends/types";
import { IFriendsService } from "Definitions/Service";
import * as friendsCommunication from "modules/Friends/Communication/FriendsCommunication";

export default class FriendsService implements IFriendsService {

    public async addFriend(friendId: number): Promise<void> {
        await friendsCommunication.addFriend(friendId);
    }
    
    public async getFriends(): Promise<Array<Friend>> {
        const response = await friendsCommunication.getFriends();
        return response.payload;
    }
}