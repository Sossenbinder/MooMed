// Framework
import * as React from "react";
import { Route } from "react-router";

// Components
import Flex from "common/components/Flex";
import Grid from "common/components/Grid/Grid";
import Cell from "common/components/Grid/Cell";
import ImagePreLoad from "common/components/ImagePreLoad";
import Navigation from "./SubComponents/Navigation";
import PersonalData from "./PersonalData/PersonalData";
import Separator from "common/components/Separator";

// Type
import { Account } from "modules/Account/types";

import "./Styles/ProfileFull.less";

type Props = {
	account: Account;
}

export const OwnProfileFull: React.FC<Props> = ({ account }) => {
	return (
		<Grid
			className="ProfileFull"
			gridProperties={{
				gridTemplateColumns: "10% auto"
			}}>
			<Cell
				className="LeftSectionContainer">
				<Flex
					direction="Column"
					className="LeftSection">
					<Flex
						mainAlign="Center">
						<ImagePreLoad
							imagePath={account.profilePicturePath}
							containerClassName="Picture"
						/>
					</Flex>
					<Flex
						mainAlign="Center">
						<h2 className="name">
							{account.userName}
						</h2>
					</Flex>
					<Separator />
					<Navigation />
				</Flex>
			</Cell>
			<Cell>
				<Flex
					className="MainSection">
					<Route
						exact={true}
						path={"/profileDetails/data"}
						render={() => <PersonalData
							account={account} />}
					/>
				</Flex>
			</Cell>
		</Grid>
	);
}

export default OwnProfileFull;