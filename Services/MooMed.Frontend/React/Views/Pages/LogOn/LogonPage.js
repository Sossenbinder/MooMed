"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const SignIn_1 = require("views/Components/Account/SignIn");
const PopUpMessageHolder_1 = require("views/Components/Main/PopUpMessage/PopUpMessageHolder");
const LanguagePicker_1 = require("common/components/General/LanguagePicker");
const Flex_1 = require("Common/Components/Flex");
require("Views/Pages/LogOn/Styles/LogOnMain.less");
class LogonPage extends React.Component {
    render() {
        return (React.createElement(Flex_1.default, { space: "Between", direction: "Column", className: "logOnContentContainer" },
            React.createElement(PopUpMessageHolder_1.default, null),
            React.createElement(Flex_1.default, { className: "mooMedLogoContainer" },
                React.createElement(Flex_1.default, { className: "mooMedLogo" }, "MooMed")),
            React.createElement(Flex_1.default, { direction: "Row", mainAlign: "End" },
                React.createElement(Flex_1.default, { className: "signUpLoginContainer" },
                    React.createElement(SignIn_1.default, null))),
            React.createElement(Flex_1.default, { direction: "Row", mainAlign: "End", className: "languagePicker" },
                React.createElement(LanguagePicker_1.default, null))));
    }
}
exports.default = LogonPage;
//# sourceMappingURL=LogonPage.js.map