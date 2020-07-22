"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const redux = require("redux");
const accountReducer_1 = require("./reducers/accountReducer");
const popUpNotificationReducer_1 = require("./reducers/popUpNotificationReducer");
const FriendsReducer_1 = require("modules/Friends/Reducer/FriendsReducer");
const ChatRoomsReducer_1 = require("modules/Chat/Reducer/ChatRoomsReducer");
const ExchangeTradedsReducer_1 = require("modules/Stocks/Reducer/ExchangeTradedsReducer");
exports.store = redux.createStore(redux.combineReducers({
    accountReducer: accountReducer_1.reducer.reducer,
    popUpNotificationReducer: popUpNotificationReducer_1.popUpNotificationReducer,
    friendsReducer: FriendsReducer_1.reducer.reducer,
    chatRoomsReducer: ChatRoomsReducer_1.reducer.reducer,
    exchangeTradedsReducer: ExchangeTradedsReducer_1.reducer.reducer,
}));
exports.default = exports.store;
//# sourceMappingURL=store.js.map