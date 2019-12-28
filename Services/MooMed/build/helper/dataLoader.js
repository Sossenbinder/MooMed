"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
Object.defineProperty(exports, "__esModule", { value: true });
const $ = require("jquery");
const accountReducer_1 = require("data/reducers/accountReducer");
const requestUrls_1 = require("./requestUrls");
const store_1 = require("data/store");
const Main_1 = require("views/Pages/MainPage/Main");
const GetRequest_1 = require("helper/requests/GetRequest");
$(() => __awaiter(this, void 0, void 0, function* () {
    const request = new GetRequest_1.default(requestUrls_1.default.account.getAccount);
    const response = yield request.send();
    if (response.success) {
        store_1.store.dispatch(accountReducer_1.addAccount(response.payload.data));
        Main_1.default();
    }
}));
//# sourceMappingURL=dataLoader.js.map