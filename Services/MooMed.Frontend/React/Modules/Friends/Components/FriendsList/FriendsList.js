"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const react_redux_1 = require("react-redux");
const Flex_1 = require("Common/Components/Flex");
const FriendsListEntry_1 = require("./FriendsListEntry");
require("./Styles/FriendsList.less");
exports.FriendsList = ({ friends }) => {
    const friendsRendered = React.useMemo(() => {
        return friends.map(friend => {
            return (React.createElement(Flex_1.default, { direction: "Column", key: friend.id, style: { width: "100%" } },
                React.createElement(FriendsListEntry_1.default, { friend: friend })));
        });
    }, [friends]);
    return (React.createElement(Flex_1.default, { className: "FriendsList", direction: "Column" }, friendsRendered));
};
const mapStateToProps = (store) => {
    return {
        friends: store.friendsReducer.data,
    };
};
exports.default = react_redux_1.connect(mapStateToProps)(exports.FriendsList);
//# sourceMappingURL=FriendsList.js.map