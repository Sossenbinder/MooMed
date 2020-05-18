// Framework
import * as React from "react";

// Components
import Flex from "Common/Components/Flex";
import FriendListImage from "./FriendListImage";
import FriendListHover from "./FriendListHover";

// Functionality
import { Friend } from "modules/friends/types";
import { useServices } from "hooks/useServices";

import "./Styles/FriendsListEntry.less";

type Props = {
	friend: Friend;
}

export const FriendsListEntry: React.FC<Props> = ({ friend }) => {

	const { ChatService } = useServices();

	return (
		<Flex 
			className={"FriendListEntry"}>
			<FriendListHover 
				friend={friend}/>
			<Flex onClick={() => ChatService.openChat(friend.id)}>
				<FriendListImage 
					onlineState={friend.onlineState}
					profilePicturePath={friend.profilePicturePath}/>
				<Flex
					direction={"Column"}
					className={"FriendProfileLabel"}
					mainAlign={"Center"}>
					{ friend.userName }
				</Flex>
			</Flex>
		</Flex>
	);
}

export default FriendsListEntry;