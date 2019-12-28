"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const react_router_dom_1 = require("react-router-dom");
class SearchBarPreviewUserEntry extends React.Component {
    render() {
        return (React.createElement(react_router_dom_1.Link, { to: "/" }, this.props.account.UserName));
    }
}
exports.SearchBarPreviewUserEntry = SearchBarPreviewUserEntry;
//# sourceMappingURL=SearchBarPreviewEntry.js.map