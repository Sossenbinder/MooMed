"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const popUpNotificationReducer_1 = require("data/reducers/popUpNotificationReducer");
const store_1 = require("data/store");
function createPopUpMessage(message, messageLevel, heading, timeToLive = 0) {
    const popUpNotification = {
        Id: Math.random() * 1000000,
        Heading: heading,
        Message: message,
        MessageLevel: messageLevel,
        TimeToLive: timeToLive
    };
    store_1.store.dispatch(popUpNotificationReducer_1.addPopUpNotification(popUpNotification));
}
exports.createPopUpMessage = createPopUpMessage;
;
//# sourceMappingURL=popUpMessageHelper.js.map