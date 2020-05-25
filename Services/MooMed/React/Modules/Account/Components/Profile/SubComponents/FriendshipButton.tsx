// Framework
import * as React from "react";
import { connect } from "react-redux";

// Components
import Flex from "Common/Components/Flex";

// Functionality
import useServices from "hooks/useServices";
import { ReduxStore } from "data/store";
import { Friend } from "modules/friends/types";
import { formatTranslation } from "helper/translationHelper";

type Props = {
	targetAccountId: number;

	friends: Array<Friend>;
}

export const FriendshipButton: React.FC<Props> = ({ targetAccountId, friends  }) => {

	const { FriendsService } = useServices();

	const friend = React.useMemo(() => {
		const possibleFriends = friends.filter(x => x.id === targetAccountId);

		if (possibleFriends.length === 0) {
			return undefined;
		} else {
			return possibleFriends[0];
		}		
	}, [targetAccountId, friends]);

	const isAlreadyFriend = React.useMemo(() => typeof friend !== "undefined", [targetAccountId, friends]);

	return (
		<Flex>
			<If condition={!isAlreadyFriend}>
				<button onClick={async () => await FriendsService.addFriend(targetAccountId)}>
					{Translation.AddAsFriend}
				</button>
			</If>
			<If condition={isAlreadyFriend}>
				{formatTranslation(Translation.AlreadyFriendsWith, friend.userName)}
			</If>
		</Flex>
	);
}

const mapStateToProps = (store: ReduxStore) => {
	return {
		friends: store.friendsReducer.data,
	};
}

export default connect(mapStateToProps)(FriendshipButton);