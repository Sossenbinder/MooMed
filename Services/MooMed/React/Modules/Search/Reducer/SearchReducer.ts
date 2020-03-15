// Framework
import { Action } from "redux";

// Functionality
import { Account } from "modules/Account/types";

type SearchReducerState = {
    accounts: Array<Account>;
}
const SEARCHREDUCER_UPDATE = "SEARCHREDUCER_UPDATE";

type UpdateSearch = Action & {
    type: typeof SEARCHREDUCER_UPDATE;
    payload: string;
}

export type SearchReducerAction = UpdateSearch;

export function update(profilePicturePath: string): UpdateAccountPicture {
    return {
        type: SEARCHREDUCER_UPDATE,
        payload: profilePicturePath
    };
}

const initialState: IAccountReducerState = {
    account: undefined
};

export function searchReducer(state = initialState, action: AccountAction): IAccountReducerState {
    switch (action.type) {
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