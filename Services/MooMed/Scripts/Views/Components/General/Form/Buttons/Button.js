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
class Button extends React.Component {
    constructor(props) {
        super(props);
        this._onClick = (event) => __awaiter(this, void 0, void 0, function* () {
            event.preventDefault();
            this.setState({
                isLoading: true,
            });
            yield this.props.handleClick();
            this.setState({
                isLoading: false,
            });
        });
        this.state = {
            isLoading: false,
        };
    }
    render() {
        return (React.createElement("button", { className: "mooMedButton" + (this.props.customStyles !== undefined ? (" " + this.props.customStyles) : ""), onClick: this._onClick, disabled: this.props.disabled },
            React.createElement(Choose, null,
                React.createElement(When, { condition: this.state.isLoading },
                    React.createElement("div", { className: "loadingButtonBubbleContainer" },
                        React.createElement("div", { className: "loadingButtonBubble" }),
                        React.createElement("div", { className: "loadingButtonBubble" }),
                        React.createElement("div", { className: "loadingButtonBubble" }))),
                React.createElement(When, { condition: !this.state.isLoading },
                    React.createElement("div", null,
                        React.createElement("p", { className: "buttonParagraph" }, this.props.title))))));
    }
}
exports.default = Button;
Button.defaultProps = {
    handleClick: () => { },
    disabled: false
};
//# sourceMappingURL=Button.js.map