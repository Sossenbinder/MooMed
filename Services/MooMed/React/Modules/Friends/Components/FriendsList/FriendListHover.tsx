// Framework
import * as React from "react";

// Components
import Flex from "Common/Components/Flex";
import FriendListImage from "./FriendListImage";

// Functionality
import { Friend } from "modules/friends/types";

import "./Styles/FriendListHover.less";

type Props = {
	friend: Friend;
}

export const FriendListHover: React.FC<Props> = ({ friend }) => {

	return (
		<Flex className={"FriendListHover"}>
			<FriendListImage 
				onlineState={friend.onlineState}
				profilePicturePath={friend.profilePicturePath} 
				size={64} />
			{ friend.userName }
		</Flex>
	);
}

export default FriendListHover;