"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const timers_1 = require("timers");
class ErrorAttachedCheckbox extends React.Component {
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
                payload: event.target.checked
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
            React.createElement("div", { className: "form-check" },
                React.createElement("input", { id: this.props.name, className: "form-check-input", type: "checkbox", name: this.props.name, checked: this.state.payload, onChange: this._handleChange }),
                React.createElement("label", { className: "form-check-label", htmlFor: this.props.name },
                    this.props.name,
                    "?"))));
    }
}
exports.default = ErrorAttachedCheckbox;
//# sourceMappingURL=ErrorAttachedCheckbox.js.map