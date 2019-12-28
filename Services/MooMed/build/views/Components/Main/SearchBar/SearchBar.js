"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const timers_1 = require("timers");
const SearchBarPreview_1 = require("./SearchBarPreview");
const ajaxHelper_1 = require("helper/ajaxHelper");
const requestUrls_1 = require("helper/requestUrls");
class SearchBar extends React.Component {
    constructor(props) {
        super(props);
        this._handleChange = (event) => {
            this.setState({ searchQuery: event.target.value });
            clearTimeout(this._typingTimer);
            this._typingTimer = timers_1.setTimeout(() => {
                ajaxHelper_1.default({
                    actionUrl: requestUrls_1.default.search.searchForQuery,
                    data: {
                        query: this.state.searchQuery
                    },
                    onSuccess: (response) => {
                        this.setState({
                            searchResult: {
                                correspondingAccounts: response.data.searchResult.CorrespondingUsers
                            }
                        });
                    }
                });
            }, 200);
        };
        this._onSearch = (event) => {
            ajaxHelper_1.default({
                actionUrl: requestUrls_1.default.search.searchForQuery,
                data: {
                    query: this.state.searchQuery
                },
                onSuccess: (response) => {
                    this.setState({
                        searchResult: {
                            correspondingAccounts: response.data
                        }
                    });
                }
            });
            event.preventDefault();
        };
        this.state = {
            searchQuery: "",
            searchResult: {
                correspondingAccounts: []
            }
        };
    }
    render() {
        return (React.createElement("div", null,
            React.createElement("div", { className: "autoCompleteSearchBar" },
                React.createElement("form", { onSubmit: this._onSearch },
                    React.createElement("input", { className: "form-control searchBarInput", id: "searchBar", autoComplete: "off", onChange: this._handleChange, type: "text", placeholder: "Search...", value: this.state.searchQuery }),
                    React.createElement("input", { className: "btn btn-primary", type: "submit", id: "searchBarSubmit", name: "searchBarSubmit", value: "Search" }))),
            React.createElement(SearchBarPreview_1.default, { visibility: this.state.searchResult.correspondingAccounts.length > 0, searchResult: this.state.searchResult })));
    }
}
exports.default = SearchBar;
//# sourceMappingURL=SearchBar.js.map