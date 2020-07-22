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
const PostRequest_1 = require("helper/requests/PostRequest");
function addFriend(friendId) {
    return __awaiter(this, void 0, void 0, function* () {
        const request = new PostRequest_1.default("/Friends/AddFriend");
        return yield request.post({ accountId: friendId });
    });
}
exports.addFriend = addFriend;
function getFriends() {
    return __awaiter(this, void 0, void 0, function* () {
        const request = new PostRequest_1.default("/Friends/GetFriends");
        return yield request.post();
    });
}
exports.getFriends = getFriends;
//# sourceMappingURL=FriendsCommunication.js.map