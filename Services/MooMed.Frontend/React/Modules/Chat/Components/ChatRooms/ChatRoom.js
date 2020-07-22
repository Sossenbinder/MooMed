"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const Flex_1 = require("common/Components/Flex");
const Icon_1 = require("common/Components/Icon");
const FriendListImage_1 = require("modules/Friends/Components/FriendsList/FriendListImage");
const ChatRoomInput_1 = require("./ChatRoomInput");
const ChatMessage_1 = require("./ChatMessage");
require("./Styles/ChatRoom.less");
exports.ChatRoom = ({ friend, setActiveChatPartnerId, chatRoom }) => {
    const messages = React.useMemo(() => {
        if (typeof chatRoom.messages !== "undefined") {
            const sortedMessages = chatRoom.messages.sort((x, y) => x.timestamp.getTime() - y.timestamp.getTime());
            const timestampMap = new Map();
            sortedMessages.forEach(msg => {
                const dateString = msg.timestamp.toDateString();
                if (!timestampMap.has(dateString)) {
                    timestampMap.set(dateString, []);
                }
                timestampMap.get(dateString).push(msg);
            });
            const ref = React.createRef();
            const elements = new Array();
            let index = 0;
            timestampMap.forEach((messages, date) => {
                elements.push({
                    ref: undefined,
                    node: (React.createElement(Flex_1.default, { className: "DateHeader", mainAlign: "Center", key: date }, date)),
                    message: undefined,
                });
                messages.forEach((msg, i) => {
                    elements.push({
                        ref: undefined,
                        node: (React.createElement(ChatMessage_1.default, { message: msg.message, sentByMe: msg.senderId != friend.id, timestamp: msg.timestamp, key: `${msg.senderId}_${index}` })),
                        message: null,
                    });
                    index++;
                });
            });
            return elements;
        }
        return undefined;
    }, [chatRoom.messages]);
    const bottomScrollDivRef = React.useRef();
    const onScroll = React.useCallback(() => {
    }, []);
    React.useEffect(() => {
        var _a;
        (_a = bottomScrollDivRef.current) === null || _a === void 0 ? void 0 : _a.scrollIntoView();
    }, []);
    React.useEffect(() => {
        var _a;
        (_a = bottomScrollDivRef.current) === null || _a === void 0 ? void 0 : _a.scrollIntoView({ behavior: "smooth" });
    }, [chatRoom.messages]);
    return (React.createElement(Flex_1.default, { className: "ChatRoomContainer", direction: "Column" },
        React.createElement(Flex_1.default, { className: "Header", direction: "Row", mainAlign: "Start" },
            React.createElement(Icon_1.default, { className: "BackButton", iconName: "BackButton", size: 24, onClick: () => setActiveChatPartnerId(0) }),
            React.createElement(Flex_1.default, { className: "Receiver", direction: "Row" },
                React.createElement(FriendListImage_1.default, { onlineState: friend.onlineState, profilePicturePath: friend.profilePicturePath, size: 24 }),
                React.createElement("span", { className: "Name" }, friend.userName))),
        React.createElement(Flex_1.default, { className: "Messages", direction: "Column" }, messages === null || messages === void 0 ? void 0 :
            messages.map(msg => msg.node),
            React.createElement("div", { ref: bottomScrollDivRef })),
        React.createElement(Flex_1.default, { mainAlign: "End", direction: "Row", className: "Input" },
            React.createElement(ChatRoomInput_1.default, { receiverId: friend.id }))));
};
exports.default = exports.ChatRoom;
//# sourceMappingURL=ChatRoom.js.map