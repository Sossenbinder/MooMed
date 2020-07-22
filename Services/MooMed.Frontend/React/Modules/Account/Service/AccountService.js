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
const ModuleService_1 = require("modules/common/Service/ModuleService");
const accountCommunication = require("modules/Account/Communication/AccountCommunication");
class AccountService extends ModuleService_1.default {
    constructor() {
        super();
    }
    start() {
        return __awaiter(this, void 0, void 0, function* () {
        });
    }
    getAccount(accountId) {
        return __awaiter(this, void 0, void 0, function* () {
            const response = yield accountCommunication.getAccount(accountId);
            return response.payload;
        });
    }
    getOwnAccount() {
        return __awaiter(this, void 0, void 0, function* () {
            const response = yield accountCommunication.getOwnAccount();
            return response.payload;
        });
    }
}
exports.default = AccountService;
//# sourceMappingURL=AccountService.js.map