import { createReducer } from "modules/common/Reducer/CrudReducer";
import { Friend } from "modules/friends/types";

export const reducer = createReducer<Friend>({
	actionIdentifier: "FRIENDS",
	key: "id"
});

export default reducer;