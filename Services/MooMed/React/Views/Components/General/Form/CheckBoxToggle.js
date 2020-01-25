"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
class CheckBoxToggle extends React.Component {
    constructor(props) {
        super(props);
        this._onSwitchClick = () => {
            this.setState({
                payload: !this.state.payload
            });
            this.props.onChange(this.state.payload);
        };
        this._getPayload = () => this.state.payload;
        this.state = {
            payload: this.props.initialToggle,
            touched: false
        };
    }
    render() {
        return (React.createElement("div", { className: "form-group" },
            React.createElement("div", { className: "toggleCheckBoxContainer" },
                React.createElement("label", { className: "toggleCheckBoxSwitchContainer" },
                    React.createElement("input", { type: "checkbox", className: "toggleCheckBoxSwitch", onClick: this._onSwitchClick }),
                    React.createElement("span", { className: "toggleCheckBoxSwitchSlider" })),
                React.createElement("p", { className: "toggleCheckBoxLabel" }, this.props.name))));
    }
}
exports.CheckBoxToggle = CheckBoxToggle;
//# sourceMappingURL=CheckBoxToggle.js.map