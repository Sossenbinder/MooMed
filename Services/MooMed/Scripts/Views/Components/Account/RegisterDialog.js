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
const ErrorAttachedTextInput_1 = require("views/Components/General/Form/ErrorAttached/ErrorAttachedTextInput");
const PostRequest_1 = require("helper/requests/PostRequest");
const requestUrls_1 = require("helper/requestUrls");
const PopUpNotificationDefinitions_1 = require("definitions/PopUpNotificationDefinitions");
const popUpMessageHelper_1 = require("helper/popUpMessageHelper");
const Button_1 = require("views/components/general/form/Buttons/Button");
class RegisterDialog extends React.Component {
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
        this._hasErrors = () => !this.state.formElements.email.IsValid || !this.state.formElements.userName.IsValid ||
            !this.state.formElements.password.IsValid || !this.state.formElements.confirmPassword.IsValid;
        this._handleRegisterClick = () => __awaiter(this, void 0, void 0, function* () {
            if (!this._hasErrors()) {
                const registerModel = {
                    Email: this.state.formElements.email.Value,
                    UserName: this.state.formElements.userName.Value,
                    Password: this.state.formElements.password.Value,
                    ConfirmPassword: this.state.formElements.confirmPassword.Value,
                };
                const request = new PostRequest_1.default(requestUrls_1.default.logOn.login);
                const response = yield request.send(registerModel);
                if (response.success) {
                    location.reload();
                }
                else {
                    popUpMessageHelper_1.createPopUpMessage(response.payload.responseJson, PopUpNotificationDefinitions_1.PopUpMessageLevel.Error, "Registration failed", 5000);
                }
            }
        });
        this.state = {
            formElements: {
                email: {
                    IsValid: true,
                    Value: "",
                },
                userName: {
                    IsValid: true,
                    Value: "",
                },
                password: {
                    IsValid: true,
                    Value: "",
                },
                confirmPassword: {
                    IsValid: true,
                    Value: "",
                }
            },
        };
    }
    render() {
        return (React.createElement("div", null,
            React.createElement("div", { id: "RegisterForm" },
                React.createElement(ErrorAttachedTextInput_1.default, { name: "Username", payload: "", errorMessage: "Please provide a valid display name.", onChangeFunc: (newVal, isValid) => this._onChangeUpdate("userName", newVal, isValid), errorFunc: (currentVal) => currentVal === "" }),
                React.createElement(ErrorAttachedTextInput_1.default, { name: "Email", payload: "", errorMessage: "Please provide a valid email", onChangeFunc: (newVal, isValid) => this._onChangeUpdate("email", newVal, isValid), errorFunc: (currentVal) => {
                        const isEmpty = currentVal === "";
                        const isInValidEmail = currentVal.search(/^\S+@\S+$/) === -1;
                        return isEmpty || isInValidEmail;
                    } }),
                React.createElement(ErrorAttachedTextInput_1.default, { name: "Password", payload: "", inputType: "password", errorMessage: "Please provide a valid password", onChangeFunc: (newVal, isValid) => this._onChangeUpdate("password", newVal, isValid), errorFunc: (currentVal) => currentVal === "" }),
                React.createElement(ErrorAttachedTextInput_1.default, { name: "Confirm password", payload: "", inputType: "password", errorMessage: "Please make sure the passwords are the same", onChangeFunc: (newVal, isValid) => this._onChangeUpdate("confirmPassword", newVal, isValid), errorFunc: (currentVal) => {
                        const isEmpty = currentVal === "";
                        const areEqual = this.state.formElements.password.Value === currentVal;
                        return isEmpty && areEqual;
                    } }),
                React.createElement("div", { className: "form-group" },
                    React.createElement(Button_1.default, { title: Translation.Register, disabled: this._hasErrors(), customStyles: "col-md-offset-2 col-md-10", handleClick: this._handleRegisterClick })))));
    }
}
exports.default = RegisterDialog;
//# sourceMappingURL=RegisterDialog.js.map