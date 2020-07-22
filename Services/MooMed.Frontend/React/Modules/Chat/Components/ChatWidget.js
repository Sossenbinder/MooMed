"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const react_redux_1 = require("react-redux");
const classnames_1 = require("classnames");
const Flex_1 = require("Common/Components/Flex");
const ChatWidgetTopBar_1 = require("./ChatWidgetTopBar");
const ChatWidgetContent_1 = require("./ChatWidgetContent");
const useServices_1 = require("hooks/useServices");
require("./Styles/ChatWidget.less");
exports.ChatWidget = ({ friends, chatRooms }) => {
    const { ChatService } = useServices_1.default();
    const [maximized, setMaximized] = React.useState(false);
    const [initiallyRendered, setInitiallyRendered] = React.useState(false);
    const [activeChatPartnerId, setActiveChatPartnerId] = React.useState(0);
    React.useEffect(() => {
        ChatService.registerForActiveChatChange(partnerId => {
            if (!maximized) {
                setMaximized(true);
            }
            setActiveChatPartnerId(partnerId);
        });
    }, []);
    const classes = classnames_1.default({
        "ChatWidget": true,
        "maximized": maximized,
        "minimized": !maximized && initiallyRendered,
    });
    return (React.createElement(Flex_1.default, { className: classes, direction: "Column" },
        React.createElement(ChatWidgetTopBar_1.default, { onClick: () => {
                setMaximized(!maximized);
                setInitiallyRendered(true);
            } }),
        React.createElement(If, { condition: maximized },
            React.createElement(ChatWidgetContent_1.default, { activeChatId: activeChatPartnerId, friends: friends, setActiveChatPartnerId: setActiveChatPartnerId, chatRooms: chatRooms }))));
};
const mapStateToProps = (state) => ({
    friends: state.friendsReducer.data,
    chatRooms: state.chatRoomsReducer.data,
});
exports.default = react_redux_1.connect(mapStateToProps)(exports.ChatWidget);
//# sourceMappingURL=ChatWidget.js.map