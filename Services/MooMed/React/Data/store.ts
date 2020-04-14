// Framework
import * as redux from "redux";

// Functionality
import { ReducerState } from "modules/common/Reducer/CrudReducer";

import { reducer as accountReducer } from "./reducers/accountReducer";
import { Account } from "modules/Account/types";
import { popUpNotificationReducer, IPopUpNotificationReducerState } from "./reducers/popUpNotificationReducer";

import { reducer as friendsReducer } from "modules/Friends/Reducer/FriendsReducer";
import { Friend } from "modules/Friends/types";

export type ReduxStore = {
	accountReducer: ReducerState<Account>;
	popUpNotificationReducer: IPopUpNotificationReducerState;
	friendsReducer: ReducerState<Friend>;
}

export const store: ReduxStore = redux.createStore(
    redux.combineReducers({
        accountReducer: accountReducer.reducer,
		popUpNotificationReducer: popUpNotificationReducer,
		friendsReducer: friendsReducer.reducer,
	}),
);

export default store;