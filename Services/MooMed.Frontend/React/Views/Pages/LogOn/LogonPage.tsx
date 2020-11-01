// Framework
import * as ReactDOM from "react-dom";
import * as React from "react";
import { Router  } from "react-router-dom";
import { Provider } from "react-redux";
import { store } from "data/store";
import { createBrowserHistory } from 'history';

// Components
import SignIn from "modules/Logon/Components/SignIn";
import Flex from "common/components/Flex";
import CommonLandingPage from "Views/Pages/Common/CommonLandingPage";

const LogonPage: React.FC = () => (
	<CommonLandingPage>
		<Flex
			direction={"Row"}
			mainAlign={"End"}>
			<Flex className={"signUpLoginContainer"}>
				<SignIn />
			</Flex>
		</Flex>
	</CommonLandingPage>
);

export default function renderLogonView() {
	ReactDOM.render(
		<Provider store={store}>
			<Router history={createBrowserHistory()}>
				<LogonPage />
			</Router >
		</Provider>,
		document.getElementById("landingContainer"));
};