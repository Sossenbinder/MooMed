// Framework
import * as React from "react";
import { connect } from "react-redux";
import { Route } from "react-router";

// Components
import AboutDialog from "views/Pages/AboutDialog";
import Profile from "modules/Account/Components/Profile/Profile";
import ChatWidget from "views/Components/Friends/ChatWidget";
import FriendsList from "modules/Friends/Components/FriendsList/FriendsList";
import Flex from "Views/Components/General/Flex";

import "./Styles/Home.less";

const MainContent: React.FC = () => (
	<Flex 
		className={"contentContainerHolder"}
		direction={"Row"}>
		<Flex 
			direction={"Column"}
			className={"contentContainer"}>
			<Flex className={"bodyContent"}>
				<Route 
					path={"/about" }
					render={() => <AboutDialog />} />
				<Route 
					path={"/profileDetails"}
					render={routeProps => {
							const url = routeProps.location.pathname;
							const accountId = parseInt(url.substring(url.lastIndexOf('/') + 1));
							return <Profile
								accountId={accountId} />;
						}
					} />
			</Flex>
		</Flex>
		<Flex mainAlign={"End"}>
			<FriendsList />
		</Flex>
		<ChatWidget />
	</Flex>
);

export default MainContent;