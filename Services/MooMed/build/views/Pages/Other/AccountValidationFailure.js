"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
class AccountValidationFailure extends React.Component {
    constructor(props) {
        super(props);
        if (window["dataModel"]) {
            this._accountValidationResponse = window["dataModel"];
        }
    }
    render() {
        return (React.createElement("div", { className: "validationContainer" },
            React.createElement("div", { className: "validationContent" },
                React.createElement("h2", null, "Account validation"),
                React.createElement("p", null, "Sadly your account could not be validated due to the following error:"),
                React.createElement("p", null, "Blablabla"),
                React.createElement("div", { className: "validationBackToLoginBtnContainer" },
                    React.createElement("a", { className: "btn btn-primary validationBackToLoginBtn", href: "/" }, "Back to login")))));
    }
}
exports.default = AccountValidationFailure;
//# sourceMappingURL=AccountValidationFailure.js.map