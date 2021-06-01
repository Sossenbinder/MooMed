// Framework
import * as React from "react";
import * as ReactDOM from "react-dom";
import { Provider } from "react-redux";
import { store } from "data/store";
import { BrowserRouter } from "react-router-dom";

// Components
import NavBar from "views/Components/Main/NavBar";
import PopUpMessageHolder from "views/Components/Main/PopUpMessage/PopUpMessageHolder";
import Flex from "common/components/Flex";
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
				<Main />
			</BrowserRouter>
		</Provider>,
		document.getElementById("content")
	);
};