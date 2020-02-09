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
const moomedEnums_1 = require("enums/moomedEnums");
exports.LoginDialog = () => {
    const [email, setEmail] = React.useState({ Value: "", IsValid: false });
    const [password, setPassword] = React.useState({ Value: "", IsValid: false });
    const [rememberMe, setRememberMe] = React.useState({ Value: false, IsValid: false });
    const onChangeUpdate = React.useCallback((newVal, currentVal, setFunc, isValid) => {
        if (currentVal.Value === newVal) {
            return;
        }
        const newStateVal = Object.assign({}, currentVal);
        if (typeof isValid !== "undefined") {
            newStateVal.IsValid = isValid;
        }
        newStateVal.Value = newVal;
        setFunc(newStateVal);
    }, []);
    const hasErrors = () => {
        return email.IsValid || password.IsValid;
    };
    const handleLogin = () => __awaiter(void 0, void 0, void 0, function* () {
        if (!hasErrors()) {
            const loginModel = {
                Email: email.Value,
                Password: password.Value,
                RememberMe: rememberMe.Value,
            };
            const request = new PostRequest_1.default(requestUrls_1.default.logOn.login);
            const response = yield request.send(loginModel);
            if (response.success) {
                location.href = "/";
            }
            else {
                const loginResultErrorCode = response.payload.loginResponseCode;
                let errorMessage = "";
                switch (loginResultErrorCode) {
                    case moomedEnums_1.LoginResponseCode.AccountNotFound:
                        errorMessage = Translation.AccountNotFound;
                        break;
                    case moomedEnums_1.LoginResponseCode.EmailNotValidated:
                        errorMessage = Translation.AccountValidationEmailNotValidatedYet;
                }
                popUpMessageHelper_1.createPopUpMessage(errorMessage, PopUpNotificationDefinitions_1.PopUpMessageLevel.Error, undefined, 5000);
            }
        }
    });
    return (React.createElement("div", null,
        React.createElement(ErrorAttachedTextInput_1.default, { name: "Email", payload: "", errorMessage: "Please provide a valid email address", onChangeFunc: (newVal, isValid) => onChangeUpdate(newVal, email, setEmail, isValid), errorFunc: (currentVal) => currentVal === "" || currentVal.search(/^\S+@\S+$/) === -1 }),
        React.createElement(ErrorAttachedTextInput_1.default, { name: "Password", inputType: "password", payload: "", onChangeFunc: (newVal, isValid) => onChangeUpdate(newVal, password, setPassword, isValid), errorMessage: "Please provide a valid password", errorFunc: (currentVal) => currentVal === "" }),
        React.createElement(CheckBoxToggle_1.CheckBoxToggle, { name: "Stay logged in?", initialToggle: false, onChange: (newVal) => onChangeUpdate(newVal, rememberMe, setRememberMe) }),
        React.createElement("div", { className: "form-group" },
            React.createElement(Button_1.default, { title: Translation.Login, customStyles: "col-md-offset-2 col-md-10", disabled: hasErrors(), handleClick: handleLogin })),
        React.createElement("hr", null),
        React.createElement("div", { className: "align-middle" },
            React.createElement(react_router_dom_1.Link, { to: "/forgotPassword" }, "Forgot password?"))));
};
exports.default = exports.LoginDialog;
//# sourceMappingURL=LoginDialog.js.map