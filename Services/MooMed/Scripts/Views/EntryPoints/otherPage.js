"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const $ = require("jquery");
const React = require("react");
const ReactDOM = require("react-dom");
const react_router_dom_1 = require("react-router-dom");
const react_redux_1 = require("react-redux");
const store_1 = require("data/store");
const OtherMain_1 = require("views/Pages/Other/OtherMain");
$(() => {
    ReactDOM.render(React.createElement(react_redux_1.Provider, { store: store_1.store },
        React.createElement(react_router_dom_1.BrowserRouter, null,
            React.createElement(OtherMain_1.default, null))), document.getElementById("otherPageHolder"));
});
//# sourceMappingURL=otherPage.js.map