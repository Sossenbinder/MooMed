import { Account } from "modules/Account/types";
import { Action } from "redux";

export interface IAccountReducerState {
    account: Account;
}

const ACCOUNT_ADD = "ACCOUNTREDUCER_ADD";
const ACCOUNT_UPDATEACCOUNTPICTURE = "ACCOUNTREDUCER_UPDATEACCOUNTPICTURE";

export interface AddAccount extends Action {
    type: typeof ACCOUNT_ADD;
    payload: Account;
}

export interface UpdateAccountPicture extends Action{
    type: typeof ACCOUNT_UPDATEACCOUNTPICTURE;
    payload: string;
}

export type AccountAction = AddAccount | UpdateAccountPicture

export function addAccount(newAccount: Account): AddAccount {
    return {
        type: ACCOUNT_ADD,
        payload: newAccount
    };
}

export function updateAccountPicture(profilePicturePath: string): UpdateAccountPicture {
    return {
        type: ACCOUNT_UPDATEACCOUNTPICTURE,
        payload: profilePicturePath
    };
}

const initialState: IAccountReducerState = {
    account: undefined
};

export function accountReducer(state = initialState, action: AccountAction): IAccountReducerState {
    switch (action.type) {
        case ACCOUNT_ADD:
            return {
                ...state,
                account: action.payload
            };
        case ACCOUNT_UPDATEACCOUNTPICTURE:
            return {
                ...state,
                account: {
                    ...state.account,
                    profilePicturePath: action.payload
                }
            };
        default:
            return initialState;
    }
}