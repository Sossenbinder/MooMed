// Framework
import * as React from "react";
import { connect } from "react-redux";

// Components
import Flex from "Common/Components/Flex";
import FriendsListEntry from "./FriendsListEntry";

// Functionality
import { ReduxStore } from "data/store";
import { Friend } from "modules/friends/types";

import "./Styles/FriendsList.less";

type Props = {
	friends: Array<Friend>
}

export const FriendsList: React.FC<Props> = ({ friends }) => {

	const friendsRendered = React.useMemo(() => {
		return friends.map(friend => {
			return (
				<Flex 
					direction={"Column"}
					key={friend.id}
					style={{width: "100%"}}>
					<FriendsListEntry
						friend={friend}/>
				</Flex>
			);
		});
	}, [friends]);

	return (
		<Flex 
			className={"FriendsList"}
			direction={"Column"}>
			{friendsRendered}
		</Flex>
	);
}

const mapStateToProps = (store: ReduxStore): Props => {
	return {
		friends: store.friendsReducer.data,
	};
}

export default connect(mapStateToProps)(FriendsList);