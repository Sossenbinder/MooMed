// Framework
import * as React from "react";
import { connect } from "react-redux";

// Components
import Flex from "common/components/Flex";
import FriendsListEntry from "./FriendsListEntry";
import LoadingBubbles from "common/components/LoadingBubbles";

// Functionality
import { ReduxStore } from "data/store";
import { Friend } from "modules/friends/types";

import "./Styles/FriendsList.less";

type Props = {
	friends: Array<Friend>
}

export const FriendsList: React.FC<Props> = ({ friends }) => {

	const friendsRendered = React.useMemo(() => friends?.map(friend => (
		<Flex
			direction={"Column"}
			key={friend.id}
			style={{ width: "100%" }}>
			<FriendsListEntry
				friend={friend} />
		</Flex>
	)), [friends]);

	return (
		<Flex
			className={"FriendsList"}
			direction={"Column"}>
			<If condition={friends.length === 0}>
				<LoadingBubbles />
			</If>
			<If condition={friends.length > 0}>
				{friendsRendered}
			</If>
		</Flex>
	);
}

const mapStateToProps = (store: ReduxStore): Props => {
	return {
		friends: store.friendsReducer.data,
	};
}

export default connect(mapStateToProps)(FriendsList);