"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const ErrorAttachedTextInput_1 = require("common/components/General/Input/ErrorAttached/ErrorAttachedTextInput");
exports.ForgotPassword = () => {
    const [forgotPasswordEmail, setForgotPasswordEmail] = React.useState({ Value: "", IsValid: false });
    return (React.createElement("div", null,
        React.createElement(ErrorAttachedTextInput_1.default, { name: "Email", payload: "", errorMessage: "Please provide a valid email address", onEnterPress: () => { }, onChangeFunc: (newVal, isValid) => {
                if (forgotPasswordEmail.Value !== newVal) {
                    setForgotPasswordEmail({
                        Value: newVal,
                        IsValid: isValid,
                    });
                }
            }, errorFunc: (currentVal) => {
                const isEmpty = currentVal === "";
                const isInValidEmail = currentVal.search(/^\S+@\S+$/) === -1;
                return isEmpty || isInValidEmail;
            } })));
};
exports.default = exports.ForgotPassword;
//# sourceMappingURL=ForgotPassword.js.map