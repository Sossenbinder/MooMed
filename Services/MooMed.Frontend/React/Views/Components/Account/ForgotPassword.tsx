import * as React from "react";

import ErrorAttachedTextInput from "common/components/General/Input/ErrorAttached/ErrorAttachedTextInput";

import { IFormElement } from "definitions/Forms";


export const ForgotPassword: React.FC = () => {

	const [forgotPasswordEmail, setForgotPasswordEmail] = React.useState<IFormElement<string>>({ Value: "", IsValid: false});
	
	return (
		<div>
			<ErrorAttachedTextInput
				name="Email"
				payload=""
				errorMessage="Please provide a valid email address"
				onEnterPress={() => {}}
				onChangeFunc={(newVal, isValid) => {
					if (forgotPasswordEmail.Value !== newVal) {
						setForgotPasswordEmail({						
							Value: newVal,
							IsValid: isValid,						
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

export default ForgotPassword;