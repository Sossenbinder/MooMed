import { createReducer } from "modules/common/reducer/CrudReducer";
import { Friend } from "modules/friends/types";

export const reducer = createReducer<Friend>({
	actionIdentifier: "FRIENDS",
	key: "id"
});

export default reducer;