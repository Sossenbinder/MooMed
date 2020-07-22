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
const moomedEnums_1 = require("enums/moomedEnums");
class NotificationService extends ModuleService_1.default {
    constructor(connectionProvider) {
        super();
        this.getNotificationTypeName = (notificationType) => moomedEnums_1.NotificationType[notificationType];
        this._hubConnection = connectionProvider.getSignalRConnection();
    }
    start() {
        return __awaiter(this, void 0, void 0, function* () { });
    }
    subscribe(notificationType, onNotify) {
        const notificationName = this.getNotificationTypeName(notificationType);
        this._hubConnection.on(notificationName, onNotify);
    }
    unsubscribe(notificationType) {
        this._hubConnection.off(this.getNotificationTypeName(notificationType));
    }
}
exports.default = NotificationService;
//# sourceMappingURL=NotificationService.js.map