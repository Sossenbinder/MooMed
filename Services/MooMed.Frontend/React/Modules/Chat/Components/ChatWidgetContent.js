"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const Flex_1 = require("Common/Components/Flex");
const ChatRoom_1 = require("./ChatRooms/ChatRoom");
const ChatRoomPreview_1 = require("./ChatRooms/ChatRoomPreview");
require("./Styles/ChatWidgetContent.less");
exports.ChatWidgetContent = ({ activeChatId, friends, setActiveChatPartnerId, chatRooms }) => {
    const chatRoomPreviews = React.useMemo(() => {
        return friends.map(friend => React.createElement(ChatRoomPreview_1.default, { friend: friend, onClick: setActiveChatPartnerId, key: friend.id }));
    }, [friends]);
    return (React.createElement(Flex_1.default, { className: "ChatWidgetContent", direction: "Column", space: "Between" },
        React.createElement(If, { condition: activeChatId !== 0 },
            React.createElement(ChatRoom_1.default, { friend: friends.find(x => x.id === activeChatId), setActiveChatPartnerId: setActiveChatPartnerId, chatRoom: chatRooms.find(x => x.roomId == activeChatId) })),
        React.createElement(If, { condition: activeChatId === 0 },
            React.createElement(Flex_1.default, { direction: "Column", className: "ChatRoomPreviewContainer" }, chatRoomPreviews))));
};
exports.default = exports.ChatWidgetContent;
//# sourceMappingURL=ChatWidgetContent.js.map