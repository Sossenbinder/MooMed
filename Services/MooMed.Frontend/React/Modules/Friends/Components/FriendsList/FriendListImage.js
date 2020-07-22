"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const Flex_1 = require("Common/Components/Flex");
const moomedEnums_1 = require("enums/moomedEnums");
require("modules/Friends/Components/FriendsList/Styles/FriendListImage.less");
const onlineStateClassMap = new Map([
    [moomedEnums_1.AccountOnlineState.Online, "Online"],
    [moomedEnums_1.AccountOnlineState.Offline, "Offline"],
]);
exports.FriendListImage = ({ profilePicturePath, onlineState, size = 32 }) => {
    return (React.createElement(Flex_1.default, { className: "FriendListImage", style: { width: size, height: size } },
        React.createElement(Flex_1.default, { className: `OnlineStateMarker ${onlineStateClassMap.get(onlineState)}` }),
        React.createElement("img", { className: "FriendProfilePicture", src: profilePicturePath, alt: "Profile picture" })));
};
exports.default = exports.FriendListImage;
//# sourceMappingURL=FriendListImage.js.map