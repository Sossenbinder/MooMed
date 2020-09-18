// Functionality
import { Friend } from "modules/Friends/types";
import GetRequest from "helper/requests/GetRequest";
import PostRequest from "helper/requests/PostRequest";

export async function addFriend(friendId: number) {
    const request = new PostRequest<{ accountId: number}, void>("/Friends/AddFriend");

    return await request.post({ accountId: friendId});    
}

export async function getFriends() {
    const request = new PostRequest<void, Array<Friend>>("/Friends/GetFriends");

    return await request.post();    
}