// Types
import { IdentityErrorCode } from "enums/moomedEnums";

export const getTranslationForErrorCode = (errorCode: IdentityErrorCode) => {

	const translationName = `IdentityError_${IdentityErrorCode[errorCode].toString()}`;

	return Translation[translationName] as string;
}

export default getTranslationForErrorCode;