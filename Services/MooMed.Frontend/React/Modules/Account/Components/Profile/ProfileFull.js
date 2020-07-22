"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const Flex_1 = require("Common/Components/Flex");
const Grid_1 = require("Common/Components/Grid/Grid");
const Cell_1 = require("Common/Components/Grid/Cell");
const FriendshipButton_1 = require("./SubComponents/FriendshipButton");
const ImagePreLoad_1 = require("common/Components/ImagePreLoad");
const Navigation_1 = require("./SubComponents/Navigation");
require("./Styles/ProfileFull.less");
exports.ProfileFull = ({ account, isSelf }) => {
    return (React.createElement(Grid_1.default, { className: "ProfileFull", gridProperties: {
            gridTemplateColumns: "100px 200px 300px"
        } },
        React.createElement(Cell_1.default, null,
            React.createElement(Flex_1.default, { direction: "Column", className: "LeftSection" },
                React.createElement(ImagePreLoad_1.default, { imagePath: account.profilePicturePath, containerClassName: "Picture" }),
                React.createElement("h2", { className: "name" }, account.userName),
                React.createElement(If, { condition: !isSelf },
                    React.createElement(FriendshipButton_1.default, { targetAccountId: account.id })),
                React.createElement(Navigation_1.default, null))),
        React.createElement(Cell_1.default, null,
            React.createElement(Flex_1.default, { className: "MainSection" }))));
};
exports.default = exports.ProfileFull;
//# sourceMappingURL=ProfileFull.js.map