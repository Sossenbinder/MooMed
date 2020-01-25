"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const initialState = {
    popUpNotifications: []
};
const POPUPNOTIFICATION_ADD = "POPUPNOTIFICATION_ADD";
const POPUPNOTIFICATION_DELETE = "POPUPNOTIFICATION_DELETE";
function addPopUpNotification(popUpNotification) {
    return {
        type: POPUPNOTIFICATION_ADD,
        popUpNotification: popUpNotification
    };
    ;
}
exports.addPopUpNotification = addPopUpNotification;
function deletePopUpNotification(popUpNotification) {
    return {
        type: POPUPNOTIFICATION_DELETE,
        popUpNotification: popUpNotification
    };
}
exports.deletePopUpNotification = deletePopUpNotification;
function popUpNotificationReducer(state = initialState, action) {
    switch (action.type) {
        case POPUPNOTIFICATION_ADD:
            return {
                popUpNotifications: [...state.popUpNotifications, action.popUpNotification]
            };
        case POPUPNOTIFICATION_DELETE:
            return {
                popUpNotifications: state.popUpNotifications.filter(notification => notification.Id !== action.popUpNotification.Id)
            };
        default:
            return initialState;
    }
}
exports.popUpNotificationReducer = popUpNotificationReducer;
//# sourceMappingURL=popUpNotificationReducer.js.map