"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
class AccountValidationSuccess extends React.Component {
    render() {
        return (React.createElement("div", { className: "validationContainer" },
            React.createElement("div", { className: "validationContent" },
                React.createElement("h2", null, "Account validation"),
                React.createElement("p", null, "Great - your account was validated successfully. You can now login."),
                React.createElement("div", { className: "validationBackToLoginBtnContainer" },
                    React.createElement("a", { className: "btn btn-primary validationBackToLoginBtn", href: "/" }, "Back to login")))));
    }
}
exports.default = AccountValidationSuccess;
//# sourceMappingURL=AccountValidationSuccess.js.map