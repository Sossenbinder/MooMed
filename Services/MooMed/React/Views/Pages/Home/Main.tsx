// Framework
import * as React from "react";
import * as ReactDOM from "react-dom";
import { Provider, connect } from "react-redux";
import { store } from "data/store";
import { BrowserRouter } from "react-router-dom";
import { Route } from "react-router";

// Components
import NavBar from "views/Components/Main/NavBar";
import AboutDialog from "views/Pages/AboutDialog";
import Profile from "modules/Account/Components/Profile/Profile";
import ChatWidget from "views/Components/Friends/ChatWidget";
import PopUpMessageHolder from "views/Components/Main/PopUpMessage/PopUpMessageHolder";
import GlobalClickCapturer from "views/Components/Helper/GlobalClickCapturer";
import Flex from "Views/Components/General/Flex";

// Functionality
import { Account } from "modules/Account/types";

import "./Styles/Home.less";

type Props = {
    account: Account;
}

const Main: React.FC<Props> = ({ account }) => (
    <Flex
        direction={"Column"}
        space={"Around"}
        className={"mainContentContainer"}>
        <NavBar />
        <PopUpMessageHolder />
        <Flex 
            direction={"Row"}
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
            <Flex className={"friendList"}>
                
            </Flex>
        </Flex>
        <ChatWidget />
    </Flex>
);

const mapStateToProps = state => {
	return {
		account: state.accountReducer.account
	};
}

const MainConnected = connect(mapStateToProps)(Main);

export default function renderMainView() {
    ReactDOM.render(
        <Provider store={store}>
            <BrowserRouter>
				<GlobalClickCapturer>
					<MainConnected />
				</GlobalClickCapturer>
            </BrowserRouter>
        </Provider>,
        document.getElementById("content")
    );
};