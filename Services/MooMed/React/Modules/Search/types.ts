import { Account } from "modules/Account/types";

export type SearchResult = {
    correspondingAccounts: Array<Account>;
}

export namespace Network {

    export type SearchRequest = {
        query: string;
    }
    
    export type SearchResult = {
        correspondingUsers: Array<Account>;
    }
}