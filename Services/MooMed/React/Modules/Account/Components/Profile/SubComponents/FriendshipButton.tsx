// Framework
import * as React from "react";
import { connect } from "react-redux";

// Components
import Flex from "Common/Components/Flex";

// Functionality
import useServices from "hooks/useServices";
import { ReduxStore } from "data/store";
import { Friend } from "modules/friends/types";

type Props = {
	targetAccountId: number;

	friends: Array<Friend>;
}

export const FriendshipButton: React.FC<Props> = ({ targetAccountId, friends  }) => {

	const { FriendsService } = useServices();

	const isAlreadyFriend = React.useMemo(() => friends.some(x => x.id === targetAccountId), [targetAccountId, friends]);

	return (
		<Flex>
			<If condition={!isAlreadyFriend}>
				<button onClick={async () => await FriendsService.addFriend(targetAccountId)}>
					{Translation.AddAsFriend}
				</button>
			</If>
			<If condition={isAlreadyFriend}>
				{Translation.AlreadyFriendsWith}
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