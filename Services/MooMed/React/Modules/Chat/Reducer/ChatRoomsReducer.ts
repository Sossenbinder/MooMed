import { createReducer } from "modules/common/Reducer/CrudReducer";
import { ChatRoom } from "modules/Chat/types";

export const reducer = createReducer<ChatRoom>({
	actionIdentifier: "CHATROOM",
	key: "partnerId"
});

export default reducer;