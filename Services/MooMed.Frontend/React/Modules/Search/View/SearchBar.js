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
const SearchBarPreview_1 = require("./SearchBarPreview");
const Flex_1 = require("Common/Components/Flex");
const useServices_1 = require("hooks/useServices");
require("modules/Search/Styles/SearchBar.less");
exports.SearchBar = () => {
    const [searchQuery, setSearchQuery] = React.useState("");
    const [isPreviewOpen, setIsPreviewOpen] = React.useState(false);
    const [search, setSearch] = React.useState({
        correspondingAccounts: []
    });
    const { SearchService } = useServices_1.default();
    let typingTimer;
    const handleChange = (event) => {
        const newSearchQuery = event.target.value;
        setSearchQuery(newSearchQuery);
        clearTimeout(typingTimer);
        typingTimer = window.setTimeout(() => __awaiter(void 0, void 0, void 0, function* () {
            const searchResult = yield SearchService.search(newSearchQuery);
            setSearch(searchResult);
            setIsPreviewOpen(true);
        }), 250);
    };
    const onSearch = React.useCallback(() => __awaiter(void 0, void 0, void 0, function* () {
    }), [searchQuery]);
    const hasContent = React.useMemo(() => { var _a; return ((_a = search === null || search === void 0 ? void 0 : search.correspondingAccounts) === null || _a === void 0 ? void 0 : _a.length) > 0; }, [search]);
    return (React.createElement(Flex_1.default, { direction: "Column" },
        React.createElement(Flex_1.default, { className: "SearchBar", direction: "Row" },
            React.createElement("input", { className: "Input", autoComplete: "off", onChange: handleChange, onClick: () => {
                    if (hasContent) {
                        setIsPreviewOpen(true);
                    }
                }, type: "text", placeholder: "Search...", value: searchQuery }),
            React.createElement("button", { className: "Button", onClick: onSearch }, "Search")),
        React.createElement("div", null,
            React.createElement(SearchBarPreview_1.default, { visibility: isPreviewOpen && hasContent, previewAccounts: search === null || search === void 0 ? void 0 : search.correspondingAccounts, onOpenStateChange: newState => setIsPreviewOpen(newState) }))));
};
exports.default = exports.SearchBar;
//# sourceMappingURL=SearchBar.js.map