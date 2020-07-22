"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const Flex_1 = require("Common/Components/Flex");
exports.AccountValidationSuccess = () => {
    return (React.createElement(Flex_1.default, { className: "validationContainer" },
        React.createElement(Flex_1.default, { className: "validationContent" },
            React.createElement("h2", null, "Account validation"),
            React.createElement("p", null, "Great - your account was validated successfully. You can now login."),
            React.createElement(Flex_1.default, { className: "validationBackToLoginBtnContainer" },
                React.createElement("a", { className: "btn btn-primary validationBackToLoginBtn", href: "/" }, "Back to login")))));
};
exports.default = exports.AccountValidationSuccess;
//# sourceMappingURL=AccountValidationSuccess.js.map