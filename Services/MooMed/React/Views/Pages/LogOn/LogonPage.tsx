// Framework
import * as React from "react";

// Components
import SignIn from "views/Components/Account/SignIn";
import PopUpMessageHolder from "views/Components/Main/PopUpMessage/PopUpMessageHolder";
import LanguagePicker from "views/Components/General/LanguagePicker";
import Flex from "views/Components/General/Flex";

import "Views/Pages/LogOn/Styles/LogOnMain.less";

export default class LogonPage extends React.Component {

    render() {
        return (
            <Flex 
                direction="Column"
                className="logOnContentContainer">
                <PopUpMessageHolder />
                <Flex className="mooMedLogoContainer">
                    <Flex className="mooMedLogo">
                        MooMed
                    </Flex>
                </Flex>
                <Flex className="signUpLoginContainer">
                    <SignIn />
                </Flex>
                <Flex 
                    crossAlign="End">
                    <LanguagePicker />
                </Flex>
            </Flex>
        );
    }
}