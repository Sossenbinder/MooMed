"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const ajaxHelper_1 = require("helper/ajaxHelper");
const requestUrls_1 = require("helper/requestUrls");
require("Views/Pages/Other/Styles/AccountValidation.less");
exports.AccountValidationDialog = () => {
    const accountValidationMetaData = window["dataModel"];
    const onValidationClicked = React.useCallback((event) => {
        event.preventDefault();
        ajaxHelper_1.default({
            actionUrl: requestUrls_1.default.accountValidation.validateRegistration,
            data: accountValidationMetaData,
        });
    }, []);
    return (React.createElement("div", { className: "validationContainer" },
        React.createElement("div", { className: "validationContent" },
            React.createElement("h2", null, "Account validation"),
            "Do you want to validate your account?",
            React.createElement("input", { type: "button", className: "btn btn-primary", value: "Validate", onClick: onValidationClicked }),
            React.createElement("div", { className: "validationBackToLoginBtnContainer" },
                React.createElement("a", { className: "btn btn-primary validationBackToLoginBtn", href: "/" }, "Back to login")))));
};
exports.default = exports.AccountValidationDialog;
//# sourceMappingURL=AccountValidationDialog.js.map