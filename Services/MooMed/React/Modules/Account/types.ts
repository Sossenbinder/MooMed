export type Account = {
    userName: string;
    email: string;
    profilePicturePath: string;
    lastAccessedAt: Date;
    id: number;
}

export namespace Network {

    export type GetAccountRequest = {
        accountId: number;
    }
}