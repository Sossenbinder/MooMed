// Framework
import * as React from "react";
import * as ReactDOM from "react-dom";
import { Route, Router } from "react-router-dom";
import { Provider } from "react-redux";
import { store } from "data/store";
import { createBrowserHistory } from 'history';

// Components
import CommonLandingPage from "Views/Pages/Common/CommonLandingPage";
import AccountValidationDialogRoute from "modules/Account/Components/AccountValidation/AccountValidationDialogRoute";

export const OtherPage: React.FC = () => (
	<CommonLandingPage>
		<Route path="/AccountValidation" render={() => <AccountValidationDialogRoute />} />
	</CommonLandingPage>
);

export default function renderOtherPage() {
	ReactDOM.render(
		<Provider store={store}>
			<Router history={createBrowserHistory()}>
				<OtherPage />
			</Router >
		</Provider>,
		document.getElementById("landingContainer"));
};