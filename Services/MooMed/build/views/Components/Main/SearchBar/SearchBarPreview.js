"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const SearchBarPreviewEntry_1 = require("./SearchBarPreviewEntry");
class SearchBarPreview extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            userEntries: []
        };
    }
    render() {
        let userEntries = [];
        this.props.searchResult.correspondingAccounts.forEach(acc => {
            userEntries.push(React.createElement(SearchBarPreviewEntry_1.SearchBarPreviewUserEntry, { account: acc, key: acc.Id }));
        });
        return (React.createElement("div", { style: { visibility: this.props.visibility ? 'visible' : 'hidden' }, className: "searchBarPreviewContainer" },
            React.createElement("div", { className: "searchBarPreviewContent" },
                React.createElement("p", null,
                    React.createElement("strong", null, "Users:")),
                React.createElement("div", { className: "userEntries" }, userEntries),
                React.createElement("div", { className: "searchBarPreviewCloseBtn" }))));
    }
}
exports.default = SearchBarPreview;
//# sourceMappingURL=SearchBarPreview.js.map