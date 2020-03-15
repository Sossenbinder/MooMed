import { Account } from "modules/Account/types";
import { SearchResult } from "modules/Search/types";
import { Friend } from "modules/Friends/types";

export interface IAccountService {
    getAccount(accountId: number): Promise<Account>;
    getOwnAccount(): Promise<Account>;
}

export interface IFriendsService {
    addFriend(friendId: number): Promise<void>;
    getFriends(): Promise<Array<Friend>>;
}

export interface ISearchService {
    search(query: string): Promise<SearchResult>;
}

export type ServiceContext = {
    AccountService: IAccountService;
    FriendsService: IFriendsService;
    SearchService: ISearchService;
}