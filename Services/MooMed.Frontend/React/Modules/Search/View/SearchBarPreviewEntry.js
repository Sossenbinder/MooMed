"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const react_router_dom_1 = require("react-router-dom");
const Flex_1 = require("Common/Components/Flex");
require("modules/Search/Styles/SearchBarPreviewEntry.less");
exports.SearchBarPreviewUserEntry = ({ account, onClick }) => (React.createElement(Flex_1.default, { className: "entryContainer" },
    React.createElement("img", { src: account.profilePicturePath, alt: "Profile picture", className: "entryProfilePicture" }),
    React.createElement(react_router_dom_1.Link, { to: `/profileDetails/${account.id}`, onClick: onClick, className: "entryProfileLink" }, account.userName)));
exports.default = exports.SearchBarPreviewUserEntry;
//# sourceMappingURL=SearchBarPreviewEntry.js.map