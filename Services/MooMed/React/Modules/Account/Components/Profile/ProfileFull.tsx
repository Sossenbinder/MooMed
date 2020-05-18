// Framework
import * as React from "react";

// Components
import Flex from "Common/Components/Flex";
import FriendshipButton from "./SubComponents/FriendshipButton"

// Functionality
import { Account } from "modules/Account/types";

type Props = {
	account: Account;
	isSelf: boolean;
}

export const ProfileFull: React.FC<Props> = ({account, isSelf}) => {

	return (
		<Flex className={"profileFull"}>
			<Flex 
				direction={"Column"}
				className={"header"}>                    
				<img className={"picture"} src={account.profilePicturePath} alt={"Profile picture"} />
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