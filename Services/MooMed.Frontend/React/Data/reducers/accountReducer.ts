import { Account } from "modules/Account/types";
import { createReducer } from "modules/common/reducer/CrudReducer";

export const reducer = createReducer<Account>({
	actionIdentifier: "ACCOUNT",
	key: "id"
});

export default reducer;