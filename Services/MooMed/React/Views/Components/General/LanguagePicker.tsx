import * as React from "react";
import * as cookieHelper from "helper/cookieHelper";
import * as moment from "moment";

import "./Styles/LanguagePicker.less";

interface IProps {

}

interface IState {
    currentLang: string;
}

export default class LanguagePicker extends React.Component<IProps, IState> {

    constructor(props: IProps) {
        super(props);

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
        return (
            <div className="languagePickerContainer">
                <div className={this.state.currentLang === "en" ? "languagePickerEn highlight" : "languagePickerEn"} onClick={() => this._pickLanguage("en")}>
                </div>
                <div className={this.state.currentLang === "de" ? "languagePickerDe highlight" : "languagePickerDe"} onClick={() => this._pickLanguage("de")}>
                </div>
            </div>
        );
    }

    _pickLanguage = (lang: any, shouldReload = true) => {
        cookieHelper.setCookie({
            name: "lang",
            value: lang,
            expirationDate: moment().add(31, 'days').format("ddd, DD MMM YYYY HH:mm:ss")
        });

        if (shouldReload) {
            location.reload();
        }
    }
}