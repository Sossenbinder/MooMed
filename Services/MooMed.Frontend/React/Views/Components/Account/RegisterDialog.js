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
const ErrorAttachedTextInput_1 = require("common/components/General/Input/ErrorAttached/ErrorAttachedTextInput");
const Button_1 = require("common/components/general/Input/Buttons/Button");
const PostRequest_1 = require("helper/requests/PostRequest");
const requestUrls_1 = require("helper/requestUrls");
const PopUpNotificationDefinitions_1 = require("definitions/PopUpNotificationDefinitions");
const popUpMessageHelper_1 = require("helper/popUpMessageHelper");
const useTranslations_1 = require("hooks/useTranslations");
exports.RegisterDialog = () => {
    const [email, setEmail] = React.useState({ Value: "", IsValid: false });
    const [userName, setUserName] = React.useState({ Value: "", IsValid: false });
    const [password, setPassword] = React.useState({ Value: "", IsValid: false });
    const [confirmPassword, setConfirmPassword] = React.useState({ Value: "", IsValid: false });
    const Translation = useTranslations_1.default();
    const onChangeUpdate = (newVal, currentVal, setFunc, isValid) => {
        if (currentVal.Value === newVal) {
            return;
        }
        const newStateVal = Object.assign({}, currentVal);
        if (typeof isValid !== "undefined") {
            newStateVal.IsValid = isValid;
        }
        newStateVal.Value = newVal;
        setFunc(newStateVal);
    };
    const hasErrors = () => !(email.IsValid && userName.IsValid && password.IsValid && confirmPassword.IsValid);
    const handleRegisterClick = () => __awaiter(void 0, void 0, void 0, function* () {
        if (!hasErrors()) {
            const registerModel = {
                Email: email.Value,
                UserName: userName.Value,
                Password: password.Value,
                ConfirmPassword: confirmPassword.Value,
            };
            const request = new PostRequest_1.default(requestUrls_1.default.logOn.register);
            const response = yield request.post(registerModel);
            if (response.success) {
                location.reload();
            }
            else {
                popUpMessageHelper_1.createPopUpMessage(response.payload.responseJson, PopUpNotificationDefinitions_1.PopUpMessageLevel.Error, "Registration failed", 5000);
            }
        }
    });
    return (React.createElement("div", null,
        React.createElement("div", { id: "RegisterForm" },
            React.createElement(ErrorAttachedTextInput_1.default, { name: "Username", payload: "", errorMessage: "Please provide a valid display name.", onChangeFunc: (newVal, isValid) => onChangeUpdate(newVal, userName, setUserName, isValid), errorFunc: (currentVal) => currentVal === "" }),
            React.createElement(ErrorAttachedTextInput_1.default, { name: "Email", payload: "", errorMessage: "Please provide a valid email", onChangeFunc: (newVal, isValid) => onChangeUpdate(newVal, email, setEmail, isValid), errorFunc: (currentVal) => {
                    const isEmpty = currentVal === "";
                    const isInValidEmail = currentVal.search(/^\S+@\S+$/) === -1;
                    return isEmpty || isInValidEmail;
                } }),
            React.createElement(ErrorAttachedTextInput_1.default, { name: "Password", payload: "", inputType: "password", errorMessage: "Please provide a valid password", onChangeFunc: (newVal, isValid) => onChangeUpdate(newVal, password, setPassword, isValid), errorFunc: (currentVal) => currentVal === "" }),
            React.createElement(ErrorAttachedTextInput_1.default, { name: "Confirm password", payload: "", inputType: "password", errorMessage: "Please make sure the passwords are the same", onChangeFunc: (newVal, isValid) => onChangeUpdate(newVal, confirmPassword, setConfirmPassword, isValid), errorFunc: (currentVal) => {
                    const isEmpty = currentVal === "";
                    const areEqual = password.Value === currentVal;
                    return isEmpty && areEqual;
                } }),
            React.createElement("div", { className: "form-group" },
                React.createElement(Button_1.default, { title: Translation.Register, disabled: hasErrors(), customStyles: "col-md-offset-2 col-md-10", handleClick: handleRegisterClick })))));
};
exports.default = exports.RegisterDialog;
//# sourceMappingURL=RegisterDialog.js.map