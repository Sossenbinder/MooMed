"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
class LoadingButton extends React.Component {
    constructor(props) {
        super(props);
        this._onClick = () => {
        };
        this.state = {
            isLoading: true,
        };
    }
    render() {
        return (React.createElement("div", { className: "loadingButton" },
            this.state.isLoading &&
                React.createElement("div", null,
                    React.createElement("div", { className: "loadingButtonBubble" }),
                    React.createElement("div", { className: "loadingButtonBubble" }),
                    React.createElement("div", { className: "loadingButtonBubble" })),
            "Test1234"));
    }
}
exports.default = LoadingButton;
//# sourceMappingURL=LoadingButton.js.map