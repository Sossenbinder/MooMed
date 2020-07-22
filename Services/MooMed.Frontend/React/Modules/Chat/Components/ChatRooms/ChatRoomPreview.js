"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const Flex_1 = require("common/Components/Flex");
const FriendListImage_1 = require("modules/Friends/Components/FriendsList/FriendListImage");
require("./Styles/ChatRoomPreview.less");
exports.ChatRoomPreview = ({ friend, onClick, lastMessage }) => {
    return (React.createElement(Flex_1.default, { onClick: () => onClick(friend.id), className: "ChatRoomPreview", direction: "Row" },
        React.createElement(FriendListImage_1.default, { onlineState: friend.onlineState, profilePicturePath: friend.profilePicturePath }),
        React.createElement(Flex_1.default, { direction: "Column" },
            React.createElement("span", { className: "Name" }, friend.userName))));
};
exports.default = exports.ChatRoomPreview;
//# sourceMappingURL=ChatRoomPreview.js.map