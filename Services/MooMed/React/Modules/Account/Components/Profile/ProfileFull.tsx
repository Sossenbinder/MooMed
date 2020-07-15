// Framework
import * as React from "react";

// Components
import Flex from "Common/Components/Flex";
import Grid from "Common/Components/Grid/Grid";
import Cell from "Common/Components/Grid/Cell";
import FriendshipButton from "./SubComponents/FriendshipButton"
import ImagePreLoad from "common/Components/ImagePreLoad";
import Navigation from "./SubComponents/Navigation";

// Type
import { Account } from "modules/Account/types";

import "./Styles/ProfileFull.less";

type Props = {
	account: Account;
	isSelf: boolean;
}

export const ProfileFull: React.FC<Props> = ({account, isSelf}) => {

	return (
		<Grid
			className="ProfileFull"
			gridProperties={{
				gridTemplateColumns: "100px 200px 300px"
			}}>
			<Cell>
				<Flex 
					direction="Column"
					className="LeftSection">		
					<ImagePreLoad
						imagePath={account.profilePicturePath}
						containerClassName="Picture"
					/>
					<h2 className="name">
						{account.userName}
					</h2>
					<If condition={!isSelf}>
						<FriendshipButton 
							targetAccountId={account.id} />
					</If>
					<Navigation />
				</Flex>
			</Cell>
			<Cell>
				<Flex
					className="MainSection">
					
				</Flex>
			</Cell>
		</Grid>
	);
}

export default ProfileFull;