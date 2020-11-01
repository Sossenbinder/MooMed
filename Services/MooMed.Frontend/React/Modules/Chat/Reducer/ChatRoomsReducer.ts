import { createReducer } from "modules/common/reducer/CrudReducer";
import { ChatRoom } from "modules/Chat/types";

export const reducer = createReducer<ChatRoom>({
	actionIdentifier: "CHATROOM",
	key: "roomId"
});

export default reducer;