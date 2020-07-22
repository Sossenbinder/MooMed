"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const Flex_1 = require("Common/Components/Flex");
require("./Styles/ChatMessage.less");
exports.ChatMessage = ({ sentByMe, timestamp, message }) => {
    const timestampString = React.useMemo(() => {
        const hours = timestamp.getHours();
        const minutes = timestamp.getMinutes();
        return `${hours < 10 ? `0${hours}` : hours}:${minutes < 10 ? `0${minutes}` : minutes}`;
    }, [timestamp]);
    return (React.createElement(Flex_1.default, { className: "ChatMessageFlex", direction: "Row", mainAlign: sentByMe ? "Start" : "End" },
        React.createElement(Flex_1.default, { direction: sentByMe ? "Row" : "RowReverse" },
            React.createElement(Flex_1.default, { className: `ChatMessageContainer ${sentByMe ? "Mine" : "Theirs"}`, mainAlign: "End" },
                React.createElement("span", { className: "ChatMessage" }, message)),
            React.createElement(Flex_1.default, { mainAlign: "Start" },
                React.createElement("span", { className: "Timestamp" }, timestampString)))));
};
exports.default = exports.ChatMessage;
//# sourceMappingURL=ChatMessage.js.map