import * as React from "react";

import ErrorAttachedTextInput from "views/Components/General/Form/ErrorAttached/ErrorAttachedTextInput";

import PostRequest from "helper/requests/PostRequest";
import requestUrls from "helper/requestUrls";

import { PopUpMessageLevel } from "definitions/PopUpNotificationDefinitions";
import { createPopUpMessage } from "helper/popUpMessageHelper";
import { RefObject } from "react";
import Button from "views/components/general/form/Buttons/Button";

import { IFormElement } from "definitions/Forms";

interface IRegisterModel {
    Email: string;
    UserName: string;
    Password: string;
    ConfirmPassword: string;
}

interface IProps {

}

interface IState {
	formElements: {
		email: IFormElement<string>;
		userName: IFormElement<string>;
		password: IFormElement<string>;
		confirmPassword: IFormElement<string>;
	};
}

export default class RegisterDialog extends React.Component<IProps, IState> {

	constructor(props: IProps) {
		super(props);

		this.state = {
			formElements: {
				email: {
					IsValid: true,
					Value: "",
				},
				userName: {
					IsValid: true,
					Value: "",
				},
				password: {
					IsValid: true,
					Value: "",
				},
				confirmPassword: {
					IsValid: true,
					Value: "",
				}
			},
		};
	}

	render() {
		return (
			<div>
				<div id="RegisterForm">
					<ErrorAttachedTextInput
						name="Username"
						payload=""
						errorMessage="Please provide a valid display name."
						onChangeFunc={(newVal, isValid) => this._onChangeUpdate<string>("userName", newVal, isValid)}
						errorFunc={(currentVal) => currentVal === ""}/>
					<ErrorAttachedTextInput
						name="Email"
						payload=""
						errorMessage="Please provide a valid email"
						onChangeFunc={(newVal, isValid) => this._onChangeUpdate<string>("email", newVal, isValid)}
						errorFunc={(currentVal) => {
	                        const isEmpty = currentVal === "";
	                        const isInValidEmail = currentVal.search(/^\S+@\S+$/) === -1;
	                        return isEmpty || isInValidEmail;
                        }}/>
					<ErrorAttachedTextInput
						name="Password"
						payload=""
						inputType="password"
						errorMessage="Please provide a valid password"
						onChangeFunc={(newVal, isValid) => this._onChangeUpdate<string>("password", newVal, isValid)}
						errorFunc={(currentVal) => currentVal === ""}/>
					<ErrorAttachedTextInput
						name="Confirm password"
						payload=""
						inputType="password"
						errorMessage="Please make sure the passwords are the same"
						onChangeFunc={(newVal, isValid) => this._onChangeUpdate<string>("confirmPassword", newVal, isValid)}
						errorFunc={(currentVal) => {
	                        const isEmpty = currentVal === "";
	                        const areEqual = this.state.formElements.password.Value === currentVal;
	                        return isEmpty && areEqual;
                        }}/>
					<div className="form-group">
						<Button
							title={Translation.Register}
							disabled={this._hasErrors()}
							customStyles="col-md-offset-2 col-md-10"
							handleClick={this._handleRegisterClick}/>
					</div>
				</div>
			</div>
		)
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
	
	_hasErrors = () => !this.state.formElements.email.IsValid || !this.state.formElements.userName.IsValid ||
		!this.state.formElements.password.IsValid || !this.state.formElements.confirmPassword.IsValid;

	_handleRegisterClick = async () => {

		if (!this._hasErrors()) {
			const registerModel: IRegisterModel = {
				Email: this.state.formElements.email.Value,
				UserName: this.state.formElements.userName.Value,
				Password: this.state.formElements.password.Value,
				ConfirmPassword: this.state.formElements.confirmPassword.Value,
			}

			const request = new PostRequest<IRegisterModel, any>(requestUrls.logOn.login);
			const response = await request.send(registerModel);

			if (response.success) {
				location.reload();
			} else {
				createPopUpMessage(response.payload.responseJson, PopUpMessageLevel.Error, "Registration failed", 5000);
			}
		}
	}
}