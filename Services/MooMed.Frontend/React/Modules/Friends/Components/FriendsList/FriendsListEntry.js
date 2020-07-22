"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const Flex_1 = require("Common/Components/Flex");
const FriendListImage_1 = require("./FriendListImage");
const FriendListHover_1 = require("./FriendListHover");
const useServices_1 = require("hooks/useServices");
require("./Styles/FriendsListEntry.less");
exports.FriendsListEntry = ({ friend }) => {
    const { ChatService } = useServices_1.useServices();
    return (React.createElement(Flex_1.default, { className: "FriendListEntry" },
        React.createElement(FriendListHover_1.default, { friend: friend }),
        React.createElement(Flex_1.default, { onClick: () => ChatService.openChat(friend.id) },
            React.createElement(FriendListImage_1.default, { onlineState: friend.onlineState, profilePicturePath: friend.profilePicturePath }),
            React.createElement(Flex_1.default, { direction: "Column", className: "FriendProfileLabel", mainAlign: "Center" }, friend.userName))));
};
exports.default = exports.FriendsListEntry;
//# sourceMappingURL=FriendsListEntry.js.map