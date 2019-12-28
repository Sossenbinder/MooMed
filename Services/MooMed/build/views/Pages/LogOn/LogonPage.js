"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
require("bootstrap");
const React = require("react");
const SignIn_1 = require("views/Components/Account/SignIn");
const PopUpMessageHolder_1 = require("views/Components/Main/PopUpMessage/PopUpMessageHolder");
const LanguagePicker_1 = require("views/Components/General/LanguagePicker");
class LogonPage extends React.Component {
    render() {
        return (React.createElement("div", { className: "logOnContentContainer" },
            React.createElement(PopUpMessageHolder_1.default, null),
            React.createElement(LanguagePicker_1.default, null),
            React.createElement("div", { className: "mooMedLogoContainer" },
                React.createElement("div", { className: "mooMedLogo" }, "MooMed")),
            React.createElement("div", { className: "signUpLoginContainer" },
                React.createElement(SignIn_1.SignIn, null))));
    }
}
exports.default = LogonPage;
//# sourceMappingURL=LogonPage.js.map