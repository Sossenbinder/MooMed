"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
require("Views/Components/Friends/Styles/ChatWidget.less");
class ChatWidget extends React.Component {
    constructor(props) {
        super(props);
        this._minimizeChat = () => {
            this.setState({
                minimized: !this.state.minimized
            });
        };
        this.state = {
            minimized: true
        };
    }
    render() {
        return (React.createElement("div", { className: "chatWidget" },
            React.createElement("div", { className: "chatWidgetBar", onClick: this._minimizeChat }, "Chat"),
            !this.state.minimized &&
                React.createElement("div", { className: "chatWidgetContent" },
                    React.createElement("p", null, "Maximized"))));
    }
}
exports.default = ChatWidget;
//# sourceMappingURL=ChatWidget.js.map