// Framework
import * as React from "react";

// Components
import Flex from "views/Components/General/Flex";
import FriendListImage from "./FriendListImage";

// Functionality
import { Friend } from "modules/friends/types";

import "./Styles/FriendsListEntry.less";

type Props = {
	friend: Friend;
}

export const FriendsListEntry: React.FC<Props> = ({ friend }) => {

	return (
		<Flex className={"FriendProfilePictureEntry"}>
			<FriendListImage 
				onlineState={friend.onlineState}
				profilePicturePath={friend.profilePicturePath}/>
			<Flex
				direction={"Column"}
				className={"FriendProfileLabel"}
				mainAlign={"Center"}>
				{friend.userName}
			</Flex>
		</Flex>
	);
}

export default FriendsListEntry;