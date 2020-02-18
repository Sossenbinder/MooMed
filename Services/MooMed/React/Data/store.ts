import * as redux from "redux";

import { accountReducer } from "./reducers/accountReducer";
import { popUpNotificationReducer } from "./reducers/popUpNotificationReducer";

export const store = redux.createStore(
    redux.combineReducers({
        accountReducer: accountReducer,
        popUpNotificationReducer: popUpNotificationReducer,
    })
);

export default store;