import * as $ from "jquery";
import * as React from "react";
import * as ReactDOM from "react-dom";
import { BrowserRouter } from "react-router-dom";
import { Provider } from "react-redux";
import { store } from "data/store";

import OtherMain from "views/Pages/Other/OtherMain";

$(() => {
    ReactDOM.render(
        <Provider store={store}>
            <BrowserRouter>
                <OtherMain />
            </BrowserRouter>
        </Provider>,
        document.getElementById("otherPageHolder"));
});