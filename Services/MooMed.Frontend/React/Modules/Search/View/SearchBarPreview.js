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
const SearchBarPreviewEntry_1 = require("./SearchBarPreviewEntry");
const Flex_1 = require("Common/Components/Flex");
const GlobalClickCapturer_1 = require("views/Components/Helper/GlobalClickCapturer");
const CoordinateHelper_1 = require("helper/Coordinate/CoordinateHelper");
require("modules/Search/Styles/SearchBarPreview.less");
exports.SearchBarPreview = ({ previewAccounts, visibility, onOpenStateChange }) => {
    const searchBarPreviewRef = React.useRef();
    const userEntries = React.useMemo(() => previewAccounts === null || previewAccounts === void 0 ? void 0 : previewAccounts.map(account => React.createElement(SearchBarPreviewEntry_1.default, { account: account, key: account.id, onClick: () => onOpenStateChange(false) })), [previewAccounts]);
    const handleGlobalClicks = React.useCallback((event) => __awaiter(void 0, void 0, void 0, function* () {
        if (visibility && !CoordinateHelper_1.IsInRect(searchBarPreviewRef.current.getBoundingClientRect(), event.x, event.y)) {
            onOpenStateChange(false);
        }
    }), [visibility]);
    React.useEffect(() => {
        const listenerId = GlobalClickCapturer_1.ClickEvent.Register(handleGlobalClicks);
        return () => GlobalClickCapturer_1.ClickEvent.Unregister(listenerId);
    }, [visibility]);
    return (React.createElement("div", { ref: searchBarPreviewRef },
        React.createElement(Flex_1.default, { style: { visibility: visibility ? 'visible' : 'hidden' }, className: "Container" },
            React.createElement(Flex_1.default, { className: "PreviewContent" },
                React.createElement("p", null,
                    React.createElement("strong", null, "Users:")),
                React.createElement(Flex_1.default, { className: "UserEntries" }, userEntries),
                React.createElement(Flex_1.default, { className: "CloseButton", onClick: () => onOpenStateChange(false) })))));
};
exports.default = exports.SearchBarPreview;
//# sourceMappingURL=SearchBarPreview.js.map