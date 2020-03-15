// Functionality
import { Friend } from "modules/Friends/types";
import GetRequest from "helper/requests/GetRequest";
import PostRequest from "helper/requests/PostRequest";

export async function addFriend(friendId: number) {
    const request = new PostRequest<{ accountId: number}, void>("/Friends/AddFriend");

    return await request.send({ accountId: friendId});    
}

export async function getFriends() {
    const request = new GetRequest<Friend>("/Friends/GetFriends");

    return await request.send();    
}