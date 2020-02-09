import "bootstrap";
import * as React from "react";

import SignIn from "views/Components/Account/SignIn";
import PopUpMessageHolder from "views/Components/Main/PopUpMessage/PopUpMessageHolder";
import LanguagePicker from "views/Components/General/LanguagePicker";

import "Views/Pages/LogOn/Styles/LogOnMain.less";

export default class LogonPage extends React.Component {

    render() {
        return (
            <div className="logOnContentContainer">
                <PopUpMessageHolder />
                <LanguagePicker />
                <div className="mooMedLogoContainer">
                    <div className="mooMedLogo">
                        MooMed
                    </div>
                </div>
                <div className="signUpLoginContainer">
                    <SignIn />
                </div>
            </div>
        );
    }
}