import * as React from "react";
import { AccountValidationResult } from "enums/moomedEnums";

interface IAccountValidationResponse {
    AccountValidationResult: AccountValidationResult;
}

interface IProps {

}

interface IState {

}

export default class AccountValidationFailure extends React.Component<IProps, IState> {

    _accountValidationResponse: IAccountValidationResponse;

    constructor(props: IProps) {
        super(props);

        if (window["dataModel"]) {
            this._accountValidationResponse = window["dataModel"];
        }
    }

    render() {
        return (
            <div className="validationContainer">
                <div className="validationContent">
                    <h2>Account validation</h2>
                    <p>Sadly your account could not be validated due to the following error:</p>

                    <p>Blablabla</p>

                    <div className="validationBackToLoginBtnContainer">
                        <a className="btn btn-primary validationBackToLoginBtn" href="/">Back to login</a>
                    </div>
                </div>
            </div>
        );
    }
}