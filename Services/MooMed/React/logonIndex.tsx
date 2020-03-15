// Framework
import * as ReactDOM from "react-dom";
import * as React from "react";
import { Router  } from "react-router-dom";
import { Provider } from "react-redux";
import { store } from "data/store";
import { createBrowserHistory } from 'history';

// Components
import LogonPage from "views/Pages/LogOn/LogonPage";

window.onload = () => {
    ReactDOM.render(
        <Provider store={store}>
			<Router history={createBrowserHistory()}>
                <LogonPage />
			</Router >
        </Provider>,
        document.getElementById("logonPageHolder"));
};