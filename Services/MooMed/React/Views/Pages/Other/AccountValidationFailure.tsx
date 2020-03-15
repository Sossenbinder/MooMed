// Framework
import * as React from "react";

// Components
import Flex from "Views/Components/General/Flex";

// Functionality
import { AccountValidationResult } from "enums/moomedEnums";

type IAccountValidationResponse = {
    AccountValidationResult: AccountValidationResult;
}

export const AccountValidationFailure: React.FC = () => {

    let accountValidationResponse: IAccountValidationResponse;

    if (window["dataModel"]) {
        accountValidationResponse = window["dataModel"];
    }

    return (
        <Flex className="validationContainer">
            <Flex className="validationContent">
                <h2>Account validation</h2>
                <p>Sadly your account could not be validated due to the following error:</p>

                <p>Blablabla</p>

                <div className="validationBackToLoginBtnContainer">
                    <a className="btn btn-primary validationBackToLoginBtn" href="/">Back to login</a>
                </div>
            </Flex>
        </Flex>
    );
}

export default AccountValidationFailure;