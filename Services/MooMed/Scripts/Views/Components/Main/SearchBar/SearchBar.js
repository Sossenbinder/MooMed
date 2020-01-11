"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const timers_1 = require("timers");
const SearchBarPreview_1 = require("./SearchBarPreview");
const ajaxHelper_1 = require("helper/ajaxHelper");
const requestUrls_1 = require("helper/requestUrls");
const GlobalClickCapturer_1 = require("views/Components/Helper/GlobalClickCapturer");
const CoordinateHelper_1 = require("helper/Coordinate/CoordinateHelper");
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
                                correspondingAccounts: response.data.searchResult.correspondingUsers
                            },
                            isClosed: false
                        });
                    }
                });
            }, 250);
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
                        },
                        isClosed: false
                    });
                }
            });
            event.preventDefault();
        };
        this._handleGlobalClicks = (event) => __awaiter(this, void 0, void 0, function* () {
            if (!this.state.isClosed && !CoordinateHelper_1.IsInRect(this._searchBarPreviewCoordinates, event.x, event.y)) {
                this.setState({
                    isClosed: true,
                });
            }
        });
        this._closePreview = () => {
            this.setState({
                isClosed: true,
            });
        };
        this.state = {
            searchQuery: "",
            searchResult: {
                correspondingAccounts: [],
            },
            isClosed: true,
        };
    }
    componentDidMount() {
        this._searchBarPreviewCoordinates = document.getElementById('searchBarPreview').getBoundingClientRect();
        GlobalClickCapturer_1.ClickEvent.Register(this._handleGlobalClicks);
    }
    render() {
        return (React.createElement("div", null,
            React.createElement("div", { className: "autoCompleteSearchBar" },
                React.createElement("form", { onSubmit: this._onSearch },
                    React.createElement("input", { className: "form-control searchBarInput", id: "searchBar", autoComplete: "off", onChange: this._handleChange, onClick: () => this.setState({
                            isClosed: false
                        }), type: "text", placeholder: "Search...", value: this.state.searchQuery }),
                    React.createElement("input", { className: "btn btn-primary", type: "submit", id: "searchBarSubmit", name: "searchBarSubmit", value: "Search" }))),
            React.createElement(SearchBarPreview_1.default, { visibility: !this.state.isClosed && this.state.searchResult.correspondingAccounts.length > 0, searchResult: this.state.searchResult, onClosePreview: this._closePreview })));
    }
}
exports.default = SearchBar;
//# sourceMappingURL=SearchBar.js.map