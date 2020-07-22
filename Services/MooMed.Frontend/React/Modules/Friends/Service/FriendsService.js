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
const friendsCommunication = require("modules/Friends/Communication/FriendsCommunication");
const FriendsReducer_1 = require("modules/Friends/Reducer/FriendsReducer");
const serviceRegistry_1 = require("helper/serviceRegistry");
const moomedEnums_1 = require("enums/moomedEnums");
class FriendsService extends ModuleService_1.default {
    constructor() {
        super();
        this.onOnlineStateUpdated = (notification) => {
            const notificationData = notification.data;
            const respectiveFriend = this.getStore().friendsReducer.data.find(x => x.id === notificationData.accountId);
            respectiveFriend.onlineState = notificationData.accountOnlineState;
            this.dispatch(FriendsReducer_1.reducer.update(respectiveFriend));
        };
        const notificationService = serviceRegistry_1.services.NotificationService;
        notificationService.subscribe(moomedEnums_1.NotificationType.FriendOnlineStateChange, this.onOnlineStateUpdated);
    }
    start() {
        return __awaiter(this, void 0, void 0, function* () {
            const friendsResponse = yield friendsCommunication.getFriends();
            if (friendsResponse.success) {
                this.dispatch(FriendsReducer_1.reducer.add(friendsResponse.payload));
            }
        });
    }
    addFriend(friendId) {
        return __awaiter(this, void 0, void 0, function* () {
            yield friendsCommunication.addFriend(friendId);
        });
    }
}
exports.default = FriendsService;
//# sourceMappingURL=FriendsService.js.map