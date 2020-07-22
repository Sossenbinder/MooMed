// Framework
import * as React from "react";
import { useHistory  } from "react-router-dom";
import classNames from "classnames";

// Components
import Flex from "Common/Components/Flex";
import Icon from "Common/Components/Icon";
import FriendListImage from "./FriendListImage";

// Functionality
import { Friend } from "modules/friends/types";

import "./Styles/FriendListHover.less";

type Props = {
	friend: Friend;
}

export const FriendListHover: React.FC<Props> = ({ friend }) => {

	const history = useHistory();

	return (
		<Flex
			direction={"Column"}
			className={"FriendListHover"}>
			<Flex
				direction={"Row"}
				className={"TopSection"}>
				<FriendListImage
					onlineState={friend.onlineState}
					profilePicturePath={friend.profilePicturePath}
					size={64} />
				<Flex
					className={"ProfileLink"}
					direction={"Row"}
					mainAlign={"End"}
					crossAlign={"Start"}>
					<Icon
						iconName={"GotoLink"}
						size={32} 
						onClick={() => {
							history.push(`/profileDetails/${friend.id}`);
						}} />
				</Flex>
			</Flex>
			<Flex
				direction={"Row"}>
			</Flex>
			<Flex
				direction={"Row"}
				className={"Name"}>
				{friend.userName}
			</Flex>
		</Flex>
	);
}

export default FriendListHover;