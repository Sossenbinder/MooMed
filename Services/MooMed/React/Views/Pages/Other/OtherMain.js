"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const react_router_dom_1 = require("react-router-dom");
const react_router_1 = require("react-router");
const PopUpMessageHolder_1 = require("views/Components/Main/PopUpMessage/PopUpMessageHolder");
const LanguagePicker_1 = require("views/Components/General/LanguagePicker");
const AccountValidationDialog_1 = require("views/Pages/Other/AccountValidationDialog");
const AccountValidationSuccess_1 = require("views/Pages/Other/AccountValidationSuccess");
const AccountValidationFailure_1 = require("views/Pages/Other/AccountValidationFailure");
require("Views/Page/Other/OtherPage.less");
class OtherMain extends React.Component {
    render() {
        let redirectRoute = window["reactRoute"];
        if (redirectRoute !== undefined) {
            window["reactRoute"] = undefined;
            return React.createElement(react_router_dom_1.Redirect, { to: redirectRoute });
        }
        return (React.createElement("div", { className: "otherContentContainer" },
            React.createElement(PopUpMessageHolder_1.default, null),
            React.createElement(LanguagePicker_1.default, null),
            React.createElement("div", { className: "mooMedLogoContainer" },
                React.createElement("div", { className: "mooMedLogo" }, "MooMed")),
            React.createElement("div", null, "MooMed - Finance done right"),
            React.createElement(react_router_1.Route, { exact: true, path: "/AccountValidation", render: () => React.createElement(AccountValidationDialog_1.default, null) }),
            React.createElement(react_router_1.Route, { path: "/AccountValidation/Success", render: props => React.createElement(AccountValidationSuccess_1.default, null) }),
            React.createElement(react_router_1.Route, { path: "/AccountValidation/Failure", render: props => React.createElement(AccountValidationFailure_1.default, null) })));
    }
}
exports.default = OtherMain;
//# sourceMappingURL=OtherMain.js.map