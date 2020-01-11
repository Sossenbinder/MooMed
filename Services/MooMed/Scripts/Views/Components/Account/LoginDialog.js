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
const react_router_dom_1 = require("react-router-dom");
const ErrorAttachedTextInput_1 = require("views/Components/General/Form/ErrorAttached/ErrorAttachedTextInput");
const CheckBoxToggle_1 = require("views/Components/General/Form/CheckBoxToggle");
const requestUrls_1 = require("helper/requestUrls");
const PostRequest_1 = require("helper/requests/PostRequest");
const PopUpNotificationDefinitions_1 = require("definitions/PopUpNotificationDefinitions");
const popUpMessageHelper_1 = require("helper/popUpMessageHelper");
const Button_1 = require("views/components/general/form/Buttons/Button");
class LoginDialog extends React.Component {
    constructor(props) {
        super(props);
        this._onChangeUpdate = (identifier, newState, isValid) => {
            const currentState = this.state.formElements[identifier];
            if (newState !== currentState) {
                const respectiveFormElement = this.state.formElements[identifier];
                respectiveFormElement.Value = newState;
                if (typeof isValid !== "undefined") {
                    respectiveFormElement.IsValid = isValid;
                }
                const formElementsNew = this.state.formElements;
                formElementsNew[identifier] = respectiveFormElement;
                this.setState({
                    formElements: formElementsNew,
                });
            }
        };
        this._hasErrors = () => {
            return this.state.formElements.email.IsValid || this.state.formElements.password.IsValid;
        };
        this._handleChange = (event) => {
            let newStateProperty = {};
            newStateProperty[event.target.id] = event.target.value;
            this.setState(newStateProperty);
        };
        this._handleLogin = () => __awaiter(this, void 0, void 0, function* () {
            if (!this._hasErrors()) {
                const loginModel = {
                    Email: this.state.formElements.email.Value,
                    Password: this.state.formElements.password.Value,
                    RememberMe: this.state.formElements.rememberMe.Value,
                };
                const request = new PostRequest_1.default(requestUrls_1.default.logOn.login);
                const response = yield request.send(loginModel);
                if (response.success) {
                    location.href = "/";
                }
                else {
                    popUpMessageHelper_1.createPopUpMessage(response.errorMessage, PopUpNotificationDefinitions_1.PopUpMessageLevel.Error, undefined, 5000);
                }
            }
        });
        this.state = {
            formElements: {
                email: {
                    IsValid: true,
                    Value: "",
                },
                password: {
                    IsValid: true,
                    Value: "",
                },
                rememberMe: {
                    IsValid: true,
                    Value: false,
                },
            },
            isLoading: false,
        };
    }
    render() {
        return (React.createElement("div", null,
            React.createElement("form", { className: "signInDialog", id: "loginForm", onSubmit: this._handleLogin },
                React.createElement(ErrorAttachedTextInput_1.default, { name: "Email", payload: "", errorMessage: "Please provide a valid email address", onChangeFunc: (newVal, isValid) => this._onChangeUpdate("email", newVal, isValid), errorFunc: (currentVal) => currentVal === "" || currentVal.search(/^\S+@\S+$/) === -1 }),
                React.createElement(ErrorAttachedTextInput_1.default, { name: "Password", inputType: "password", payload: "", onChangeFunc: (newVal, isValid) => this._onChangeUpdate("password", newVal, isValid), errorMessage: "Please provide a valid password", errorFunc: (currentVal) => currentVal === "" }),
                React.createElement(CheckBoxToggle_1.CheckBoxToggle, { name: "Stay logged in?", initialToggle: false, onChange: (newVal) => this._onChangeUpdate("rememberMe", newVal) }),
                React.createElement("div", { className: "form-group" },
                    React.createElement(Button_1.default, { title: Translation.Login, customStyles: "col-md-offset-2 col-md-10", disabled: this._hasErrors(), handleClick: this._handleLogin }))),
            React.createElement("hr", null),
            React.createElement("div", { className: "align-middle" },
                React.createElement(react_router_dom_1.Link, { to: "/forgotPassword" }, "Forgot password?"))));
    }
}
exports.default = LoginDialog;
//# sourceMappingURL=LoginDialog.js.map