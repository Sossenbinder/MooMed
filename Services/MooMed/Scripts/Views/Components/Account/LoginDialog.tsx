import * as React from "react";
import { Link } from "react-router-dom";

import ErrorAttachedTextInput from "views/Components/General/Form/ErrorAttached/ErrorAttachedTextInput";
import { CheckBoxToggle } from "views/Components/General/Form/CheckBoxToggle";

import requestUrls from "helper/requestUrls";
import PostRequest from "helper/requests/PostRequest";

import { PopUpMessageLevel } from "definitions/PopUpNotificationDefinitions";
import { createPopUpMessage } from "helper/popUpMessageHelper";
import Button from "views/components/general/form/Buttons/Button";

import { IFormElement } from "definitions/Forms";

interface ILoginModel {
    Email: string;
    Password: string;
    RememberMe: boolean;
}

interface IProps {

}

interface IState {
	formElements: {
		email: IFormElement<string>;
		password: IFormElement<string>;
		rememberMe: IFormElement<boolean>;
	};
	isLoading: boolean;
}

export default class LoginDialog extends React.Component<IProps, IState> {

    constructor(props: any) {
        super(props);

		this.state = {
			formElements: {
				email: {
					IsValid: true,
					Value: "",
				},
				password: {
					IsValid: true,
					Value: "",
				},
				rememberMe: {
					IsValid: true,
					Value: false,
				},
			},
            isLoading: false,
        };
    }

    render() {
        return(
            <div>
				<form className="signInDialog" id="loginForm" onSubmit={this._handleLogin}>
                    <ErrorAttachedTextInput
                        name="Email"
                        payload=""
						errorMessage="Please provide a valid email address"
						onChangeFunc={(newVal, isValid) => this._onChangeUpdate<string>("email", newVal, isValid)}
                        errorFunc={(currentVal) =>  currentVal === "" || currentVal.search(/^\S+@\S+$/) === -1}/>
                    <ErrorAttachedTextInput
						name="Password"
						inputType="password"
						payload=""
						onChangeFunc={(newVal, isValid) => this._onChangeUpdate<string>("password", newVal, isValid)}
						errorMessage="Please provide a valid password"
						errorFunc={(currentVal) => currentVal === ""}/>
                    <CheckBoxToggle
                        name="Stay logged in?"
						initialToggle={false}
						onChange={(newVal) => this._onChangeUpdate("rememberMe", newVal)}
					/>
                    <div className="form-group">
						<Button
							title={Translation.Login}
							customStyles="col-md-offset-2 col-md-10"
							disabled={this._hasErrors()}
							handleClick={this._handleLogin}
						/>
                    </div>
                </form>
                <hr/>
                <div className="align-middle">
                    <Link to="/forgotPassword">Forgot password?</Link>
                </div>
            </div>
        );
	}

	_onChangeUpdate = <T extends {}>(identifier: string, newState: T, isValid?: boolean) => {

		const currentState = this.state.formElements[identifier];

		if (newState !== currentState) {
			const respectiveFormElement: IFormElement<T> = this.state.formElements[identifier];

			respectiveFormElement.Value = newState;

			if (typeof isValid !== "undefined") {
				respectiveFormElement.IsValid = isValid;
			}

			const formElementsNew = this.state.formElements;

			formElementsNew[identifier] = respectiveFormElement;

			this.setState({
				formElements: formElementsNew,
			});
		}
	}

	_hasErrors = () => {
		return this.state.formElements.email.IsValid || this.state.formElements.password.IsValid;
	};

    _handleChange = (event) => {
        let newStateProperty = {};
        newStateProperty[event.target.id] = event.target.value;

        this.setState(newStateProperty);
    }

	_handleLogin = async (): Promise<void> => {
		
		if (!this._hasErrors()) {

            const loginModel: ILoginModel = {
				Email: this.state.formElements.email.Value,
				Password: this.state.formElements.password.Value,
				RememberMe: this.state.formElements.rememberMe.Value,
			}

			const request = new PostRequest<ILoginModel, any>(requestUrls.logOn.login);
			const response = await request.send(loginModel);

			if (response.success) {
				location.href = "/";
			} else {
				createPopUpMessage(response.errorMessage, PopUpMessageLevel.Error, undefined, 5000);
			}
        }
    }
}