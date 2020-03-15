import { Action } from "redux";
import { PopUpNotification } from "definitions/PopUpNotificationDefinitions";

export interface IPopUpNotificationReducerState {
    popUpNotifications: Array<PopUpNotification>;
}

const initialState: IPopUpNotificationReducerState = {
    popUpNotifications: []
};

const POPUPNOTIFICATION_ADD = "POPUPNOTIFICATION_ADD";
const POPUPNOTIFICATION_DELETE = "POPUPNOTIFICATION_DELETE";

export interface AddPopUpNotification extends Action{
    type: typeof POPUPNOTIFICATION_ADD;
    popUpNotification: PopUpNotification;
}

export interface DeletePopUpNotification extends Action{
    type: typeof POPUPNOTIFICATION_DELETE;
    popUpNotification: PopUpNotification;
}

export type PopUpNotificationAction = AddPopUpNotification | DeletePopUpNotification;

export function addPopUpNotification(popUpNotification: PopUpNotification): AddPopUpNotification {
    return {
        type: POPUPNOTIFICATION_ADD,
        popUpNotification: popUpNotification
    };;
}

export function deletePopUpNotification(popUpNotification: PopUpNotification): DeletePopUpNotification {
    return {
        type: POPUPNOTIFICATION_DELETE,
        popUpNotification: popUpNotification
    };
}

export function popUpNotificationReducer(state = initialState, action: PopUpNotificationAction): IPopUpNotificationReducerState {
    switch (action.type) {
        case POPUPNOTIFICATION_ADD:
            return {
                popUpNotifications: [...state.popUpNotifications, action.popUpNotification]
            };
        case POPUPNOTIFICATION_DELETE:
            return {
                popUpNotifications: state.popUpNotifications.filter(notification => notification.message !== action.popUpNotification.message)
            };
        default:
        return initialState;
    }
}