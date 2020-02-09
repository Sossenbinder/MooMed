import * as React from "react";
import { Redirect } from "react-router-dom";
import { Route } from "react-router";

import PopUpMessageHolder from "views/Components/Main/PopUpMessage/PopUpMessageHolder";
import LanguagePicker from "views/Components/General/LanguagePicker";
import AccountValidationDialog from "views/Pages/Other/AccountValidationDialog";
import AccountValidationSuccess from "views/Pages/Other/AccountValidationSuccess";
import AccountValidationFailure from "views/Pages/Other/AccountValidationFailure";

import "Views/Page/Other/OtherPage.less";

interface IProps {

}

interface IState {

}

export default class OtherMain extends React.Component<IProps, IState> {
    render() {
        let redirectRoute = window["reactRoute"];

        if (redirectRoute !== undefined) {
            window["reactRoute"] = undefined;
            return <Redirect to={redirectRoute}/>;
        }

        return (
            <div className="otherContentContainer">
                <PopUpMessageHolder />
                <LanguagePicker />
                <div className="mooMedLogoContainer">
                    <div className="mooMedLogo">
                        MooMed
                    </div>
                </div>
                <div>
                    MooMed - Finance done right
                </div>
                <Route exact path="/AccountValidation" render={() => <AccountValidationDialog />} />
                <Route path="/AccountValidation/Success" render={props => <AccountValidationSuccess />} />
                <Route path="/AccountValidation/Failure" render={props => <AccountValidationFailure />} />
            </div>
        );
    }
}