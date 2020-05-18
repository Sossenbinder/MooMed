// Framework
import * as React from "react";

// Components
import Flex from "Common/Components/Flex";

// Functionality
import { AccountOnlineState } from "enums/moomedEnums";

import "modules/Friends/Components/FriendsList/Styles/FriendListImage.less";

type Props = {
	profilePicturePath: string;
	onlineState: AccountOnlineState;
	size?: number;
};

const onlineStateClassMap = new Map<AccountOnlineState, string>([
	[AccountOnlineState.Online, "Online"],
	[AccountOnlineState.Offline, "Offline"],
]);

export const FriendListImage: React.FC<Props> = ({ profilePicturePath, onlineState, size = 32 }) => {
	return (
		<Flex 
			className={"FriendListImage"}
			style={{width: size, height: size}}>
			<Flex className={`OnlineStateMarker ${onlineStateClassMap.get(onlineState)}`}/>
			<img 
				className={"FriendProfilePicture"}
				src={profilePicturePath} 
				alt={"Profile picture"} />
		</Flex>
	);
}

export default FriendListImage;