import * as React from "react";

import ajaxPost from "helper/ajaxHelper";
// Framework
import requestUrl from "helper/requestUrls";

// Components
import Flex from "Views/Components/General/Flex";

// Functionality


import "Views/Pages/Other/Styles/AccountValidation.less";

interface IAccountValidationTokenData {
    AccountId: number;
    ValidationGuid: string;
}

interface IAccountValidationModel {
    AccountName: string;
    AccountValidationTokenData: IAccountValidationTokenData;
}

export const AccountValidationDialog: React.FC = () => {

    const accountValidationMetaData: IAccountValidationModel = window["dataModel"];

    const onValidationClicked = React.useCallback((event) => {
        event.preventDefault();

        ajaxPost({
            actionUrl: requestUrl.accountValidation.validateRegistration,
            data: accountValidationMetaData,
        });
    }, []);

    return (
        <div className="validationContainer">
            <div className="validationContent">
                <h2>Account validation</h2>
                Do you want to validate your account?
                
                <input type="button" className="btn btn-primary" value="Validate" onClick={onValidationClicked}/>

                <div className="validationBackToLoginBtnContainer">
                    <a className="btn btn-primary validationBackToLoginBtn" href="/">Back to login</a>
                </div>
            </div>
        </div>
    );
}

export default AccountValidationDialog;