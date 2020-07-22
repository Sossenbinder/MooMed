"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const ReactDOM = require("react-dom");
const React = require("react");
const react_router_dom_1 = require("react-router-dom");
const react_redux_1 = require("react-redux");
const store_1 = require("data/store");
const OtherMain_1 = require("views/Pages/Other/OtherMain");
window.onload = () => {
    ReactDOM.render(React.createElement(react_redux_1.Provider, { store: store_1.store },
        React.createElement(react_router_dom_1.BrowserRouter, null,
            React.createElement(OtherMain_1.default, null))), document.getElementById("otherPageHolder"));
};
//# sourceMappingURL=otherEntryPoint.js.map