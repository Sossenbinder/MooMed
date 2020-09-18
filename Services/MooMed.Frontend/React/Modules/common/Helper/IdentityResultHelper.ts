// Functionality
import getTranslationForErrorCode from "Translations/helpers/IdentityErrorLookupHelper";
import { createPopUpMessage } from "helper/popUpMessageHelper";

// Types
import { IdentityErrorCode } from "enums/moomedEnums";
import { PopUpMessageLevel } from "definitions/PopUpNotificationDefinitions";

export const handleIdentityResponse = (success: boolean, errorCode?: IdentityErrorCode) => {
	if (success) {
		location.reload();
	} else {
		const errorMessage = getTranslationForErrorCode(errorCode);

		createPopUpMessage(errorMessage, PopUpMessageLevel.Error);
	}
}

export default handleIdentityResponse;