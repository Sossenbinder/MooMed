"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const Flex_1 = require("Common/Components/Flex");
exports.AccountValidationFailure = () => {
    let accountValidationResponse;
    if (window["dataModel"]) {
        accountValidationResponse = window["dataModel"];
    }
    return (React.createElement(Flex_1.default, { className: "validationContainer" },
        React.createElement(Flex_1.default, { className: "validationContent" },
            React.createElement("h2", null, "Account validation"),
            React.createElement("p", null, "Sadly your account could not be validated due to the following error:"),
            React.createElement("p", null, "Blablabla"),
            React.createElement("div", { className: "validationBackToLoginBtnContainer" },
                React.createElement("a", { className: "btn btn-primary validationBackToLoginBtn", href: "/" }, "Back to login")))));
};
exports.default = exports.AccountValidationFailure;
//# sourceMappingURL=AccountValidationFailure.js.map