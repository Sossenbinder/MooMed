"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const react_router_dom_1 = require("react-router-dom");
const ErrorAttachedTextInput_1 = require("views/Components/General/Form/ErrorAttached/ErrorAttachedTextInput");
const CheckBoxToggle_1 = require("views/Components/General/Form/CheckBoxToggle");
const requestUrls_1 = require("helper/requestUrls");
const PostRequest_1 = require("helper/requests/PostRequest");
const PopUpNotificationDefinitions_1 = require("definitions/PopUpNotificationDefinitions");
const popUpMessageHelper_1 = require("helper/popUpMessageHelper");
const enums_1 = require("helper/enums");
const LoadingButton_1 = require("views/components/general/form/LoadingButton");
class LoginDialog extends React.Component {
    constructor(props) {
        super(props);
        this.EmailField = React.createRef();
        this.PasswordField = React.createRef();
        this.RememberMeCheckBox = React.createRef();
        this._handleChange = (event) => {
            let newStateProperty = {};
            newStateProperty[event.target.id] = event.target.value;
            this.setState(newStateProperty);
        };
        this._hasFormErrors = () => {
            const isEmailError = this.EmailField.current._isError();
            const isPasswordError = this.PasswordField.current._isError();
            return isEmailError || isPasswordError;
        };
        this._handleSubmit = (event) => __awaiter(this, void 0, void 0, function* () {
            event.preventDefault();
            if (!this._hasFormErrors()) {
                const loginModel = {
                    Email: this.EmailField.current._getPayload(),
                    Password: this.PasswordField.current._getPayload(),
                    RememberMe: this.RememberMeCheckBox.current._getPayload()
                };
                const request = new PostRequest_1.default(requestUrls_1.default.logOn.login);
                const response = yield request.send(loginModel);
                if (response.success) {
                    location.href = "/";
                }
                else {
                    let responseMsg;
                    switch (response.payload.data.loginResult.LoginValidationResult) {
                        case enums_1.LoginValidationResult.EmailNullOrEmpty:
                            responseMsg = "Given email empty or invalid";
                            break;
                        case enums_1.LoginValidationResult.EmailNotValidated:
                            responseMsg = "Email was not validated yet. Please check your email folder.";
                            break;
                        case enums_1.LoginValidationResult.PasswordNullOrEmpty:
                            responseMsg = "Given password empty or invalid";
                            break;
                        default:
                            responseMsg = "Unspecified error";
                            break;
                    }
                    popUpMessageHelper_1.createPopUpMessage(responseMsg, PopUpNotificationDefinitions_1.PopUpMessageLevel.Error, undefined, 5000);
                }
            }
        });
    }
    render() {
        return (React.createElement("div", null,
            React.createElement("form", { onSubmit: this._handleSubmit, className: "signInDialog", id: "loginForm", role: "form" },
                React.createElement(ErrorAttachedTextInput_1.default, { ref: this.EmailField, name: "Email", payload: "", errorMessage: "Please provide a valid email address", errorFunc: (currentVal) => {
                        const isEmpty = currentVal === "";
                        const isInValidEmail = currentVal.search(/^\S+@\S+$/) === -1;
                        return isEmpty || isInValidEmail;
                    } }),
                React.createElement(ErrorAttachedTextInput_1.default, { ref: this.PasswordField, name: "Password", inputType: "password", payload: "", errorMessage: "Please provide a valid password", errorFunc: (currentVal) => currentVal === "" }),
                React.createElement(CheckBoxToggle_1.CheckBoxToggle, { ref: this.RememberMeCheckBox, name: "Stay logged in?", initialToggle: false }),
                React.createElement("div", { className: "form-group" },
                    React.createElement("div", { className: "col-md-offset-2 col-md-10" },
                        React.createElement("input", { type: "submit", value: "Log in", className: "btn btn-default" }))),
                React.createElement(LoadingButton_1.default, { action: () => { } })),
            React.createElement("hr", null),
            React.createElement("div", { className: "align-middle" },
                React.createElement(react_router_dom_1.Link, { to: "/forgotPassword" }, "Forgot password?"))));
    }
}
exports.default = LoginDialog;
//# sourceMappingURL=LoginDialog.js.map