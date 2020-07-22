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
const GetRequest_1 = require("helper/requests/GetRequest");
const PostRequest_1 = require("helper/requests/PostRequest");
const accountRequests = {
    getOwnAccount: "/Account/GetOwnAccount",
    getAccount: "/Account/GetAccount",
};
function getOwnAccount() {
    return __awaiter(this, void 0, void 0, function* () {
        const request = new GetRequest_1.default(accountRequests.getOwnAccount);
        return yield request.send();
    });
}
exports.getOwnAccount = getOwnAccount;
function getAccount(accountId) {
    return __awaiter(this, void 0, void 0, function* () {
        const request = new PostRequest_1.default(accountRequests.getAccount);
        return yield request.post({
            accountId
        });
    });
}
exports.getAccount = getAccount;
//# sourceMappingURL=AccountCommunication.js.map