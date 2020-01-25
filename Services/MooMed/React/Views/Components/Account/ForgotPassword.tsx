import * as React from "react";

import ErrorAttachedTextInput from "views/Components/General/Form/ErrorAttached/ErrorAttachedTextInput";

import { IFormElement } from "definitions/Forms";

interface IProps {

}

interface IState {
	forgotPasswordEmail: IFormElement<string>;
}

export default class ForgotPassword extends React.Component<IProps, IState> {
	
	constructor(props: any) {
		super(props);

		this.state = {
			forgotPasswordEmail: {
				Value: "",
				IsValid: true,
			},
		}
	}

	// Use submit instead
	render() {
		return (
			<div>
				<ErrorAttachedTextInput
					name="Email"
					payload=""
					errorMessage="Please provide a valid email address"
					onEnterPress={this._handleRequestPasswordReset}
					onChangeFunc={(newVal, isValid) => {
						if (this.state.forgotPasswordEmail.Value !== newVal) {
							this.setState({
								forgotPasswordEmail: {
									Value: newVal,
									IsValid: isValid,
								}
							});
						}
					}}
					errorFunc={(currentVal) => {
						const isEmpty = currentVal === "";
						const isInValidEmail = currentVal.search(/^\S+@\S+$/) === -1;
						return isEmpty || isInValidEmail;
					}} />
			</div>
		);
	}

	_handleRequestPasswordReset = () => {
		console.log(this.state.forgotPasswordEmail.Value);
	}
}