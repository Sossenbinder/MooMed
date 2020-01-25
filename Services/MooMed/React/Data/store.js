"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const redux = require("redux");
const accountReducer_1 = require("./reducers/accountReducer");
const popUpNotificationReducer_1 = require("./reducers/popUpNotificationReducer");
const reducers = redux.combineReducers({
    accountReducer: accountReducer_1.accountReducer,
    popUpNotificationReducer: popUpNotificationReducer_1.popUpNotificationReducer,
});
exports.store = redux.createStore(reducers);
//# sourceMappingURL=store.js.map