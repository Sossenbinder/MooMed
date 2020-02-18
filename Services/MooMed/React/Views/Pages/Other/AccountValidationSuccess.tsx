// Framework
import * as React from "react";

// Components
import Flex from "Views/Components/General/Flex";

export const AccountValidationSuccess: React.FC = () =>  {
    return (
        <Flex className="validationContainer">
            <Flex className="validationContent">
                <h2>Account validation</h2>
                <p>Great - your account was validated successfully. You can now login.</p>
                <Flex className="validationBackToLoginBtnContainer">
                    <a className="btn btn-primary validationBackToLoginBtn" href="/">Back to login</a>
                </Flex>
            </Flex>
        </Flex>
    );
}

export default AccountValidationSuccess;