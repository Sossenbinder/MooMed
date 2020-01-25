"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const ErrorAttachedTextInput_1 = require("views/Components/General/Form/ErrorAttached/ErrorAttachedTextInput");
class ForgotPassword extends React.Component {
    constructor(props) {
        super(props);
        this._handleRequestPasswordReset = () => {
            console.log(this.state.forgotPasswordEmail.Value);
        };
        this.state = {
            forgotPasswordEmail: {
                Value: "",
                IsValid: true,
            },
        };
    }
    // Use submit instead
    render() {
        return (React.createElement("div", null,
            React.createElement(ErrorAttachedTextInput_1.default, { name: "Email", payload: "", errorMessage: "Please provide a valid email address", onEnterPress: this._handleRequestPasswordReset, onChangeFunc: (newVal, isValid) => {
                    if (this.state.forgotPasswordEmail.Value !== newVal) {
                        this.setState({
                            forgotPasswordEmail: {
                                Value: newVal,
                                IsValid: isValid,
                            }
                        });
                    }
                }, errorFunc: (currentVal) => {
                    const isEmpty = currentVal === "";
                    const isInValidEmail = currentVal.search(/^\S+@\S+$/) === -1;
                    return isEmpty || isInValidEmail;
                } })));
    }
}
exports.default = ForgotPassword;
//# sourceMappingURL=ForgotPassword.js.map