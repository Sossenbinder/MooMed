import * as React from "react";
import * as ReactDOM from "react-dom";
import { Provider } from "react-redux";
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

const Main: React.FC = () => (
    <div className="mainContentContainer">
        <NavBar />
        <PopUpMessageHolder />
        <div className="contentContainer">
            <div className="bodyContent">
                <Route 
                    path="/about" 
                    render={() => <AboutDialog />} />
                <Route 
                    path="/editProfile" 
                    render={() => <ProfileFull />} />
            </div>
            <div className="friendList">
                
            </div>
        </div>
        <ChatWidget />
    </div>
);

export default function renderMainView() {
    ReactDOM.render(
        <Provider store={store}>
            <BrowserRouter>
				<GlobalClickCapturer>
					<Main />
				</GlobalClickCapturer>
            </BrowserRouter>
        </Provider>,
        document.getElementById("content")
    );
};