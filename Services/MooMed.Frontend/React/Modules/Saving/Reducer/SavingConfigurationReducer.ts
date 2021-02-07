// Functionality
import { createSingleReducer } from "modules/common/reducer/CrudReducer";

// Types
import { SavingInfo } from "modules/saving/types";

export const reducer = createSingleReducer<SavingInfo>({
	actionIdentifier: "SAVING",
	additionalActions: [

	]
});

export default reducer;