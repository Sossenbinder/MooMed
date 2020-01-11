"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const react_router_dom_1 = require("react-router-dom");
class SearchBarPreviewUserEntry extends React.Component {
    render() {
        return (React.createElement("div", { className: "searchBarPreviewEntryContainer" },
            React.createElement("img", { src: this.props.account.profilePicturePath, alt: "Profile picture", className: "searchBarPreviewEntryProfilePicture" }),
            React.createElement(react_router_dom_1.Link, { to: `/editProfile/${this.props.account.id}`, className: "searchBarPreviewEntryProfileLink" }, this.props.account.userName)));
    }
}
exports.default = SearchBarPreviewUserEntry;
//# sourceMappingURL=SearchBarPreviewEntry.js.map