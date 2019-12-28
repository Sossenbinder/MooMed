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
const ErrorAttachedTextInput_1 = require("views/Components/General/Form/ErrorAttached/ErrorAttachedTextInput");
const PostRequest_1 = require("helper/requests/PostRequest");
const requestUrls_1 = require("helper/requestUrls");
const PopUpNotificationDefinitions_1 = require("definitions/PopUpNotificationDefinitions");
const popUpMessageHelper_1 = require("helper/popUpMessageHelper");
class RegisterDialog extends React.Component {
    constructor(props) {
        super(props);
        this.EmailField = React.createRef();
        this.UserNameField = React.createRef();
        this.PasswordField = React.createRef();
        this.ConfirmPasswordField = React.createRef();
        this._hasFormErrors = () => {
            const isEmailError = this.EmailField.current._isError();
            const isDisplayNameError = this.UserNameField.current._isError();
            const isPasswordError = this.PasswordField.current._isError();
            return isEmailError || isDisplayNameError || isPasswordError;
        };
        this._handleSubmit = (event) => __awaiter(this, void 0, void 0, function* () {
            event.preventDefault();
            if (!this._hasFormErrors()) {
                const registerModel = {
                    Email: this.EmailField.current._getPayload(),
                    UserName: this.UserNameField.current._getPayload(),
                    Password: this.PasswordField.current._getPayload(),
                    ConfirmPassword: this.ConfirmPasswordField.current._getPayload()
                };
                const request = new PostRequest_1.default(requestUrls_1.default.logOn.login);
                const response = yield request.send(registerModel);
                if (response.success) {
                    location.reload();
                }
                else {
                    popUpMessageHelper_1.createPopUpMessage(response.payload.responseJson, PopUpNotificationDefinitions_1.PopUpMessageLevel.Error, "Registration failed", 5000);
                }
                if (this.props.handleSubmit) {
                    this.props.handleSubmit(registerModel);
                }
            }
        });
    }
    render() {
        return (React.createElement("div", null,
            React.createElement("form", { onSubmit: this._handleSubmit, id: "RegisterForm", role: "form" },
                React.createElement(ErrorAttachedTextInput_1.default, { ref: this.UserNameField, name: "Username", payload: "", errorMessage: "Please provide a valid displayname", errorFunc: (currentVal) => currentVal === "" }),
                React.createElement(ErrorAttachedTextInput_1.default, { ref: this.EmailField, name: "Email", payload: "", errorMessage: "Please provide a valid email", errorFunc: (currentVal) => {
                        const isEmpty = currentVal === "";
                        const isInValidEmail = currentVal.search(/^\S+@\S+$/) === -1;
                        return isEmpty || isInValidEmail;
                    } }),
                React.createElement(ErrorAttachedTextInput_1.default, { ref: this.PasswordField, name: "Password", payload: "", inputType: "password", errorMessage: "Please provide a valid password", errorFunc: (currentVal) => currentVal === "" }),
                React.createElement(ErrorAttachedTextInput_1.default, { ref: this.ConfirmPasswordField, name: "Confirm password", payload: "", inputType: "password", errorMessage: "Please make sure the passwords are the same", errorFunc: (currentVal) => {
                        const isEmpty = currentVal === "";
                        const areEqual = this.PasswordField.current._getPayload() === currentVal;
                        return isEmpty && areEqual;
                    } }),
                React.createElement("div", { className: "form-group" },
                    React.createElement("div", { className: "col-md-offset-2 col-md-10" },
                        React.createElement("input", { type: "submit", value: "Register", className: "btn btn-default" }))))));
    }
}
exports.default = RegisterDialog;
//# sourceMappingURL=RegisterDialog.js.map