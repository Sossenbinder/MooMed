// Framework
import * as redux from "redux";

// Functionality
import { ReducerState } from "modules/common/Reducer/CrudReducer";

import { reducer as accountReducer } from "./reducers/accountReducer";
import { Account } from "modules/Account/types";
import { popUpNotificationReducer, IPopUpNotificationReducerState } from "./reducers/popUpNotificationReducer";

import { reducer as friendsReducer } from "modules/Friends/Reducer/FriendsReducer";
import { Friend } from "modules/Friends/types";

import { reducer as chatRoomsReducer } from "modules/Chat/Reducer/ChatRoomsReducer";
import { ChatRoom } from "modules/Chat/types";

export type ReduxStore = redux.Store & {
	accountReducer: ReducerState<Account>;
	popUpNotificationReducer: IPopUpNotificationReducerState;
	friendsReducer: ReducerState<Friend>;
	chatRoomsReducer: ReducerState<ChatRoom>;
}

export const store: ReduxStore = redux.createStore(
    redux.combineReducers({
        accountReducer: accountReducer.reducer,
		popUpNotificationReducer: popUpNotificationReducer,
		friendsReducer: friendsReducer.reducer,
		chatRoomsReducer: chatRoomsReducer.reducer,
	}),
);

export default store;