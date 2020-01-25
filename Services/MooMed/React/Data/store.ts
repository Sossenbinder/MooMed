import * as redux from "redux";

import { accountReducer } from "./reducers/accountReducer";
import { popUpNotificationReducer } from "./reducers/popUpNotificationReducer";

const reducers = redux.combineReducers({
    accountReducer: accountReducer,
    popUpNotificationReducer: popUpNotificationReducer,
});

export const store = redux.createStore(reducers);