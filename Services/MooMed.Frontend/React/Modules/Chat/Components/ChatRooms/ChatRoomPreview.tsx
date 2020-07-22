// Framework
import * as React from "react";

// Components
import Flex from "common/Components/Flex";
import FriendListImage from "modules/Friends/Components/FriendsList/FriendListImage";

// Functionality
import { Friend } from "modules/Friends/types";

import "./Styles/ChatRoomPreview.less";

type Props = {
	friend: Friend;
	onClick: React.Dispatch<number>;
	lastMessage?: string;
}

export const ChatRoomPreview: React.FC<Props> = ({ friend, onClick, lastMessage }) => {
	return (
		<Flex
			onClick={() => onClick(friend.id)}
			className={"ChatRoomPreview"}
			direction={"Row"}>
			<FriendListImage 
				onlineState={friend.onlineState}
				profilePicturePath={friend.profilePicturePath} />
			<Flex direction={"Column"}>
				<span className={"Name"}>{ friend.userName }</span>
			</Flex>
		</Flex>
	);
}

export default ChatRoomPreview;