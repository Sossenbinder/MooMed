import * as React from "react";

interface IProps {

}

interface IState {

}

export default class AccountValidationSuccess extends React.Component<IProps, IState> {
    render() {
        return (
            <div className="validationContainer">
                <div className="validationContent">
                    <h2>Account validation</h2>
                    <p>Great - your account was validated successfully. You can now login.</p>
                    <div className="validationBackToLoginBtnContainer">
                        <a className="btn btn-primary validationBackToLoginBtn" href="/">Back to login</a>
                    </div>
                </div>
            </div>
        );
    }
}