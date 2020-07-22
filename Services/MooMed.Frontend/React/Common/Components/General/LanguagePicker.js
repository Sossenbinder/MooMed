"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const moment = require("moment");
const Flex_1 = require("Common/Components/Flex");
const cookieHelper = require("helper/cookieHelper");
require("./Styles/LanguagePicker.less");
exports.LanguagePicker = () => {
    const [currentLang, setCurrentLang] = React.useState("en");
    const pickLang = React.useCallback((lang, shouldReload = true) => {
        cookieHelper.setCookie({
            name: "lang",
            value: lang,
            expirationDate: moment().add(9999, 'days').format("ddd, DD MMM YYYY HH:mm:ss")
        });
        if (shouldReload) {
            location.reload();
        }
    }, []);
    React.useEffect(() => {
        const existingCookie = cookieHelper.getCookie("lang");
        if (existingCookie) {
            setCurrentLang(existingCookie.value);
        }
    }, []);
    return (React.createElement(Flex_1.default, { className: "languagePickerContainer" },
        React.createElement(Flex_1.default, { className: currentLang === "en" ? "languagePickerEn highlight" : "languagePickerEn", onClick: () => pickLang("en") }),
        React.createElement(Flex_1.default, { className: currentLang === "de" ? "languagePickerDe highlight" : "languagePickerDe", onClick: () => pickLang("de") })));
};
exports.default = exports.LanguagePicker;
//# sourceMappingURL=LanguagePicker.js.map