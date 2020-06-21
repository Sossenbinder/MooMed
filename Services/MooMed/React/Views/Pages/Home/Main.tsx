// Framework
import * as React from "react";
import * as ReactDOM from "react-dom";
import { Provider, connect } from "react-redux";
import { store } from "data/store";
import { BrowserRouter } from "react-router-dom";

// Components
import NavBar from "views/Components/Main/NavBar";
import PopUpMessageHolder from "views/Components/Main/PopUpMessage/PopUpMessageHolder";
import GlobalClickCapturer from "views/Components/Helper/GlobalClickCapturer";
import Flex from "Common/Components/Flex";
import MainContent from "./MainContent";
import ChatWidget from "modules/Chat/Components/ChatWidget";

import "./Styles/Home.less";

const Main: React.FC = () => (
	<Flex
		direction={"Column"}
		className={"pageContainer"}>
		<NavBar />
		<PopUpMessageHolder />
		<MainContent />
		<ChatWidget />
	</Flex>
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