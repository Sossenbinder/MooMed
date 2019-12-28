"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const cookieHelper = require("helper/cookieHelper");
const moment = require("moment");
class LanguagePicker extends React.Component {
    constructor(props) {
        super(props);
        this._pickLanguage = (lang, shouldReload = true) => {
            cookieHelper.setCookie({
                name: "lang",
                value: lang,
                expirationDate: moment().add(31, 'days').format("ddd, DD MMM YYYY HH:mm:ss")
            });
            if (shouldReload) {
                location.reload();
            }
        };
        this.state = {
            currentLang: ""
        };
    }
    componentDidMount() {
        const existingCookie = cookieHelper.getCookie("lang");
        if (!existingCookie) {
            this._pickLanguage("en", false);
            this.setState({
                currentLang: "en"
            });
        }
        this.setState({
            currentLang: existingCookie.value
        });
    }
    render() {
        return (React.createElement("div", { className: "languagePickerContainer" },
            React.createElement("div", { className: this.state.currentLang === "en" ? "languagePickerEn highlight" : "languagePickerEn", onClick: () => this._pickLanguage("en") }),
            React.createElement("div", { className: this.state.currentLang === "de" ? "languagePickerDe highlight" : "languagePickerDe", onClick: () => this._pickLanguage("de") })));
    }
}
exports.default = LanguagePicker;
//# sourceMappingURL=LanguagePicker.js.map