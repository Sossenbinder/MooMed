"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const react_router_1 = require("react-router");
const AboutDialog_1 = require("views/Pages/AboutDialog");
const Profile_1 = require("modules/Account/Components/Profile/Profile");
const FriendsList_1 = require("modules/Friends/Components/FriendsList/FriendsList");
const StocksDialog_1 = require("modules/Stocks/Components/StocksDialog");
const Flex_1 = require("Common/Components/Flex");
require("./Styles/MainContent.less");
const MainContent = () => {
    return (React.createElement(Flex_1.default, { className: "ContentContainerHolder", direction: "Row" },
        React.createElement(Flex_1.default, { direction: "Column", className: "ContentContainer" },
            React.createElement(Flex_1.default, { className: "BodyContent" },
                React.createElement(react_router_1.Route, { path: "/about", render: () => React.createElement(AboutDialog_1.default, null) }),
                React.createElement(react_router_1.Route, { path: "/Stocks", render: () => React.createElement(StocksDialog_1.default, null) }),
                React.createElement(react_router_1.Route, { path: "/profileDetails", render: routeProps => {
                        const url = routeProps.location.pathname;
                        const accountId = parseInt(url.substring(url.lastIndexOf('/') + 1));
                        return React.createElement(Profile_1.default, { accountId: accountId });
                    } }))),
        React.createElement(FriendsList_1.default, null)));
};
exports.default = MainContent;
//# sourceMappingURL=MainContent.js.map