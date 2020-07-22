"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
Object.defineProperty(exports, "__esModule", { value: true });
const accountReducer_1 = require("data/reducers/accountReducer");
const store_1 = require("data/store");
const Main_1 = require("views/Pages/Home/Main");
const useServices_1 = require("hooks/useServices");
exports.init = () => __awaiter(void 0, void 0, void 0, function* () {
    const { AccountService } = useServices_1.default();
    const account = yield AccountService.getOwnAccount();
    if (account) {
        store_1.store.dispatch(accountReducer_1.reducer.add(account));
        Main_1.default();
    }
});
//# sourceMappingURL=dataLoader.js.map