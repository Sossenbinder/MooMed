// Functionality
import { addPopUpNotification } from "data/reducers/popUpNotificationReducer";
import { store } from "data/store";

// Types
import { PopUpMessageLevel, PopUpNotification } from "definitions/PopUpNotificationDefinitions";

export function createPopUpMessage(
	message: string, 
	messageLevel: PopUpMessageLevel, 
	heading?: string, 
	timeToLive?: number) {
    const popUpNotification: PopUpNotification = {
        heading,
        message,
        messageLevel,
        timeToLive
    };

    store.dispatch(addPopUpNotification(popUpNotification));
};

export default createPopUpMessage;