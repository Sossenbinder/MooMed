// Framework
import * as React from "react";

// Components
import Flex from "Common/Components/Flex";
import FriendshipButton from "./SubComponents/FriendshipButton"
import ProfileFullPicture from "./SubComponents/ProfileFullPicture";

// Type
import { Account } from "modules/Account/types";

import "./Styles/ProfileFull.less";

type Props = {
	account: Account;
	isSelf: boolean;
}

export const ProfileFull: React.FC<Props> = ({account, isSelf}) => {

	return (
		<Flex className={"ProfileFull"}>
			<Flex 
				direction={"Column"}
				className={"header"}>
				<ProfileFullPicture 
					accountId={account.id}
					profilePicturePath={account.profilePicturePath}
				/>
				<h2 className={"name"}>
					{account.userName}
				</h2>
				<If condition={!isSelf}>
					<FriendshipButton 
						targetAccountId={account.id} />
				</If>
			</Flex>
			<Flex>
				
			</Flex>
		</Flex>
	);
}

export default ProfileFull;