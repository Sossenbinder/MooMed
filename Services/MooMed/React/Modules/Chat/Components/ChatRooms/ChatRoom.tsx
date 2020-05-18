// Framework
import * as React from "react";

// Components
import Flex from "common/Components/Flex";
import FriendListImage from "modules/Friends/Components/FriendsList/FriendListImage";
import ChatRoomInput from "./ChatRoomInput";

// Functionality
import { Friend } from "modules/Friends/types";
import { ChatRoom as Room} from "modules/Chat/types";

import "./Styles/ChatRoom.less";

type Props = {
	friend: Friend;
	setActiveChatPartnerId: React.Dispatch<number>;

	chatRoom: Room;
}

export const ChatRoom: React.FC<Props> = ({ friend, setActiveChatPartnerId, chatRoom }) => {

	const messages = React.useMemo(() => {
		return chatRoom.messages?.map(x => {
			return x.content;
		});
	}, [chatRoom.messages]);

	return (
		<Flex
			className={"ChatRoomContainer"}
			direction={"Column"}>
			<Flex 
				className={"Header"}
				direction={"Row"}
				mainAlign={"Start"}>
				<Flex 
					className={"BackButton"}
					onClick={() => setActiveChatPartnerId(0)}>
					<img src={"Resources/Icons/BackButton.png"} />
				</Flex>
				<Flex
					className={"Receiver"}
					direction={"Row"}>
					<FriendListImage
						onlineState={friend.onlineState}
						profilePicturePath={friend.profilePicturePath}
						size={24} />
					<span className={"Name"}>
						{friend.userName}
					</span>
				</Flex>
			</Flex>
			<Flex
				className={"Messages"}
				direction={"Column"}>
				{messages}
			</Flex>
			<Flex
				mainAlign={"End"}
				direction={"Row"}
				className={"Input"}>
				<ChatRoomInput 
					receiverId={friend.id}/>
			</Flex>
		</Flex>
	);
}

export default ChatRoom;