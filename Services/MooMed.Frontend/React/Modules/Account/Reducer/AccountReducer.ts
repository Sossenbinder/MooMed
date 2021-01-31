import { Account } from "modules/Account/types";
import { createSingleReducer } from "modules/common/reducer/CrudReducer";

export const reducer = createSingleReducer<Account>({
	actionIdentifier: "ACCOUNT",
});

export default reducer;