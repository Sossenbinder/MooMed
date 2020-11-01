// Framework
import * as redux from "redux";

// Functionality
import { reducer as accountReducer } from "./reducers/accountReducer";
import { popUpNotificationReducer, IPopUpNotificationReducerState } from "./reducers/popUpNotificationReducer";
import { reducer as friendsReducer } from "modules/Friends/Reducer/FriendsReducer";
import { reducer as chatRoomsReducer } from "modules/Chat/Reducer/ChatRoomsReducer";
import { reducer as exchangeTradedsReducer} from "modules/Stocks/Reducer/ExchangeTradedsReducer";
import { reducer as savingConfigurationReducer } from "modules/Saving/Reducer/SavingConfigurationReducer";

// Types
import { ReducerState, MultiReducerState } from "modules/common/reducer/types";

import { Account } from "modules/Account/types";
import { Friend } from "modules/Friends/types";
import { ChatRoom } from "modules/Chat/types";
import { ExchangeTradedItem } from "modules/Stocks/types";
import { SavingInfo } from "modules/Saving/types"; 

export type ReduxStore = redux.Store & {
	accountReducer: MultiReducerState<Account>;
	popUpNotificationReducer: IPopUpNotificationReducerState;
	friendsReducer: MultiReducerState<Friend>;
	chatRoomsReducer: MultiReducerState<ChatRoom>;
	exchangeTradedsReducer: MultiReducerState<ExchangeTradedItem>;
	savingConfigurationReducer: ReducerState<SavingInfo>;
}

export const store: ReduxStore = redux.createStore(
    redux.combineReducers({
        accountReducer: accountReducer.reducer,
		popUpNotificationReducer: popUpNotificationReducer,
		friendsReducer: friendsReducer.reducer,
		chatRoomsReducer: chatRoomsReducer.reducer,
		exchangeTradedsReducer: exchangeTradedsReducer.reducer,
		savingConfigurationReducer: savingConfigurationReducer.reducer,
	}),
);

export default store;