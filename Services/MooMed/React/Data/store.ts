// Framework
import * as redux from "redux";
import logger from "redux-logger";

// Functionality
import { ReducerState } from "modules/common/Reducer/CrudReducer";

import { accountReducer, IAccountReducerState } from "./reducers/accountReducer";
import { popUpNotificationReducer, IPopUpNotificationReducerState } from "./reducers/popUpNotificationReducer";

import { reducer as friendsReducer } from "modules/Friends/Reducer/FriendsReducer";
import { Friend } from "modules/Friends/types";

export type ReduxStore = {
	accountReducer: IAccountReducerState;
	popUpNotificationReducer: IPopUpNotificationReducerState;
	friendsReducer: ReducerState<Friend>;
}

export const store: ReduxStore = redux.createStore(
    redux.combineReducers({
        accountReducer: accountReducer,
		popUpNotificationReducer: popUpNotificationReducer,
		friendsReducer: friendsReducer.reducer,
	}),
	redux.applyMiddleware(logger),
);

export default store;