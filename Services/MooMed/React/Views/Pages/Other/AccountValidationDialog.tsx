import * as React from "react";

import ajaxPost from "helper/ajaxHelper";
import requestUrl from "helper/requestUrls";

import "Views/Pages/Other/Styles/AccountValidation.less";

interface IAccountValidationTokenData {
    AccountId: number;
    ValidationGuid: string;
}

interface IAccountValidationModel {
    AccountName: string;
    AccountValidationTokenData: IAccountValidationTokenData;
}

interface IProps {

}

interface IState {

}

export default class AccountValidationDialog extends React.Component<IProps, IState> {

    _accountValidationMetaData: IAccountValidationModel;

    constructor(props: IProps) {
        super(props);

        if (window["dataModel"]) {
            this._accountValidationMetaData = window["dataModel"];
        }
    }

    render() {
        return (
            <div className="validationContainer">
                <div className="validationContent">
                    <h2>Account validation</h2>
                    Do you want to validate your account?
                    
                    <input type="button" className="btn btn-primary" value="Validate" onClick={this._onValidationClicked}/>

                    <div className="validationBackToLoginBtnContainer">
                        <a className="btn btn-primary validationBackToLoginBtn" href="/">Back to login</a>
                    </div>
                </div>
            </div>
        );
    }

    _onValidationClicked = (event) => {
        event.preventDefault();

        ajaxPost({
            actionUrl: requestUrl.accountValidation.validateRegistration,
            data: this._accountValidationMetaData,
            onSuccess: () => {
                console.log("Success");
            },
            onError: () => {
                console.log("Failure");
            }
        });
    }
}