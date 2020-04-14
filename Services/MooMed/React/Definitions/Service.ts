import { Account } from "modules/Account/types";
import { SearchResult } from "modules/Search/types";

export interface IAccountService {
    getAccount(accountId: number): Promise<Account>;
    getOwnAccount(): Promise<Account>;
}

export interface IFriendsService {
    addFriend(friendId: number): Promise<void>;
}

export interface ISearchService {
    search(query: string): Promise<SearchResult>;
}

export interface INotificationService {

}

export type ServiceContext = {
    AccountService: IAccountService;
    FriendsService: IFriendsService;
	SearchService: ISearchService;
	NotificationService: INotificationService;
}