"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const react_router_dom_1 = require("react-router-dom");
const Flex_1 = require("Common/Components/Flex");
const Icon_1 = require("Common/Components/Icon");
const FriendListImage_1 = require("./FriendListImage");
require("./Styles/FriendListHover.less");
exports.FriendListHover = ({ friend }) => {
    const history = react_router_dom_1.useHistory();
    return (React.createElement(Flex_1.default, { direction: "Column", className: "FriendListHover" },
        React.createElement(Flex_1.default, { direction: "Row", className: "TopSection" },
            React.createElement(FriendListImage_1.default, { onlineState: friend.onlineState, profilePicturePath: friend.profilePicturePath, size: 64 }),
            React.createElement(Flex_1.default, { className: "ProfileLink", direction: "Row", mainAlign: "End", crossAlign: "Start" },
                React.createElement(Icon_1.default, { iconName: "GotoLink", size: 32, onClick: () => {
                        history.push(`/profileDetails/${friend.id}`);
                    } }))),
        React.createElement(Flex_1.default, { direction: "Row" }),
        React.createElement(Flex_1.default, { direction: "Row", className: "Name" }, friend.userName)));
};
exports.default = exports.FriendListHover;
//# sourceMappingURL=FriendListHover.js.map