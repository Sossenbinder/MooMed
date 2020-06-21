// Framework
import * as React from "react";

// Components
import SignIn from "views/Components/Account/SignIn";
import PopUpMessageHolder from "views/Components/Main/PopUpMessage/PopUpMessageHolder";
import LanguagePicker from "common/components/General/LanguagePicker";
import Flex from "Common/Components/Flex";

import "Views/Pages/LogOn/Styles/LogOnMain.less";

export default class LogonPage extends React.Component {

    render() {
        return (
            <Flex 
                space={"Between"}
                direction={"Column"}
                className={"logOnContentContainer"}>
                <PopUpMessageHolder />
                <Flex className={"mooMedLogoContainer"}>
                    <Flex className={"mooMedLogo"}>
                        MooMed
                    </Flex>
                </Flex>
                <Flex
                    direction={"Row"}
                    mainAlign={"End"}>
                    <Flex className={"signUpLoginContainer"}>
                        <SignIn />
                    </Flex>
                </Flex>
                <Flex
                    direction={"Row"}
                    mainAlign={"End"}
                    className={"languagePicker"}>
                    <LanguagePicker />
                </Flex>
            </Flex>
        );
    }
}