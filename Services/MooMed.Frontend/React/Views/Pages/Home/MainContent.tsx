// Framework
import * as React from "react";
import { Route } from "react-router";

// Components
import AboutDialog from "views/Pages/AboutDialog";
import Profile from "modules/Account/Components/Profile/Profile";
import FriendsList from "modules/Friends/Components/FriendsList/FriendsList";
import StocksDialog from "modules/Stocks/Components/StocksDialog";
import Flex from "Common/Components/Flex";
import PostsContainer from "modules/Posts/Components/PostsContainer";

import "./Styles/MainContent.less";

const MainContent: React.FC = () => {
	return (
		<Flex
			className={"ContentContainerHolder"}
			direction={"Row"}>
			<Flex
				direction={"Column"}
				className={"ContentContainer"}>
				<Flex
					className={"BodyContent"}>
					<Route
						path={"/profileDetails"}
						render={routeProps => {
							const url = routeProps.location.pathname;
							const accountId = parseInt(url.substring(url.lastIndexOf('/') + 1));
							return <Profile
								accountId={accountId} />;
						}} />
					<Route
						path={"/about"}
						render={() => <AboutDialog />} />
					<Route
						path={"/Investing"}
						render={() => <StocksDialog />} />
					<PostsContainer />
				</Flex>
			</Flex>
			<FriendsList />
		</Flex>
	);
}

export default MainContent;