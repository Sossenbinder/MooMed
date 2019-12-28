import * as $ from "jquery";
import * as React from "react";
import * as ReactDOM from "react-dom";
import { Router  } from "react-router-dom";
import { Provider } from "react-redux";
import { store } from "data/store";
import { createBrowserHistory } from 'history';

import LogonPage from "views/Pages/LogOn/LogonPage";

$(() => {
    ReactDOM.render(
        <Provider store={store}>
			<Router history={createBrowserHistory()}>
                <LogonPage />
			</Router >
        </Provider>,
        document.getElementById("logonPageHolder"));
});