import { PopUpMessageLevel, PopUpNotification } from "definitions/PopUpNotificationDefinitions";
import { addPopUpNotification } from "data/reducers/popUpNotificationReducer";
import { store } from "data/store";

export function createPopUpMessage(message: string, messageLevel: PopUpMessageLevel, heading: string, timeToLive: number = 0) {
    const popUpNotification: PopUpNotification = {
        Id: Math.random() * 1000000,
        Heading: heading,
        Message: message,
        MessageLevel: messageLevel,
        TimeToLive: timeToLive
    };

    store.dispatch(addPopUpNotification(popUpNotification));
};