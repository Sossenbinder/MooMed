"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const timers_1 = require("timers");
class ErrorAttachedTextInput extends React.Component {
    constructor(props) {
        super(props);
        this._getPayload = () => this.state.payload;
        this._isError = () => {
            if (this.state.touched) {
                if (this.props.errorFunc) {
                    return this.props.errorFunc(this.state.payload);
                }
            }
            return false;
        };
        this._handleChange = (event) => {
            this.setState({
                payload: event.target.value
            });
            clearTimeout(this._errorTimer);
            this._errorTimer = timers_1.setTimeout(() => {
                this.setState({
                    touched: true
                });
            }, 500);
        };
        this.state = {
            payload: props.payload,
            touched: false
        };
    }
    render() {
        return (React.createElement("div", { className: "form-group" },
            React.createElement("input", { className: this._isError() ? "form-control is-invalid" : "form-control", type: this.props.inputType != null ? this.props.inputType : "text", name: this.props.name, value: this.state.payload, onChange: this._handleChange, placeholder: this.props.name }),
            (this.props.errorMessage !== "") &&
                React.createElement("div", { className: "invalid-feedback" }, this.props.errorMessage)));
    }
}
exports.default = ErrorAttachedTextInput;
//# sourceMappingURL=ErrorAttachedTextInput.js.map