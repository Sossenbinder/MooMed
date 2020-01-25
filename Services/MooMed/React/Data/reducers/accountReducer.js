"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const ACCOUNT_ADD = "ACCOUNTREDUCER_ADD";
const ACCOUNT_UPDATEACCOUNTPICTURE = "ACCOUNTREDUCER_UPDATEACCOUNTPICTURE";
function addAccount(newAccount) {
    return {
        type: ACCOUNT_ADD,
        payload: newAccount
    };
}
exports.addAccount = addAccount;
function updateAccountPicture(profilePicturePath) {
    return {
        type: ACCOUNT_UPDATEACCOUNTPICTURE,
        payload: profilePicturePath
    };
}
exports.updateAccountPicture = updateAccountPicture;
const initialState = {
    account: undefined
};
function accountReducer(state = initialState, action) {
    switch (action.type) {
        case ACCOUNT_ADD:
            return Object.assign(Object.assign({}, state), { account: action.payload });
        case ACCOUNT_UPDATEACCOUNTPICTURE:
            return Object.assign(Object.assign({}, state), { account: Object.assign(Object.assign({}, state.account), { profilePicturePath: action.payload }) });
        default:
            return initialState;
    }
}
exports.accountReducer = accountReducer;
//# sourceMappingURL=accountReducer.js.map