import * as React from "react";
import * as ReactDOM from "react-dom";
import { Provider, connect } from "react-redux";
import { store } from "data/store";
import { BrowserRouter } from "react-router-dom";
import { Route } from "react-router";

import NavBar from "views/Components/Main/NavBar";

//Route components
import AboutDialog from "views/Pages/AboutDialog";
import ProfileFull from "views/Pages/Profile/ProfileFull";

import ChatWidget from "views/Components/Friends/ChatWidget";
import PopUpMessageHolder from "views/Components/Main/PopUpMessage/PopUpMessageHolder";

import GlobalClickCapturer from "views/Components/Helper/GlobalClickCapturer";

import "./Styles/Home.less";

type Props = {
    account: Account;
}

const Main: React.FC<Props> = ({ account }) => (
    <div className="mainContentContainer">
        <NavBar />
        <PopUpMessageHolder />
        <div className="contentContainer">
            <div className="bodyContent">
                <Route 
                    path="/about" 
                    render={() => <AboutDialog />} />
                <Route 
                    path="/profile" 
                    render={routeProps => <ProfileFull profileAccount={account}/>} />
            </div>
            <div className="friendList">
                
            </div>
        </div>
        <ChatWidget />
    </div>
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