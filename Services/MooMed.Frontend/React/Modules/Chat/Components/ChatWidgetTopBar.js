"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const Flex_1 = require("Common/Components/Flex");
const useTranslations_1 = require("hooks/useTranslations");
require("./Styles/ChatWidgetTopBar.less");
exports.ChatWidgetTopBar = ({ onClick }) => {
    const Translation = useTranslations_1.useTranslations();
    return (React.createElement(Flex_1.default, { className: "ChatWidgetTopBar", onClick: onClick, direction: "Column", mainAlign: "Center" },
        React.createElement("span", { className: "Heading" }, Translation.Chat)));
};
exports.default = exports.ChatWidgetTopBar;
//# sourceMappingURL=ChatWidgetTopBar.js.map