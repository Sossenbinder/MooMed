﻿import * as React from "react";

import ErrorAttachedTextInput from "views/Components/General/Form/ErrorAttached/ErrorAttachedTextInput";

import PostRequest from "helper/requests/PostRequest";
import requestUrls from "helper/requestUrls";

import { PopUpMessageLevel } from "definitions/PopUpNotificationDefinitions";
import { createPopUpMessage } from "helper/popUpMessageHelper";
import Button from "views/components/general/form/Buttons/Button";

import { IFormElement } from "definitions/Forms";

interface IRegisterModel {
    Email: string;
    UserName: string;
    Password: string;
    ConfirmPassword: string;
}

export const RegisterDialog: React.FC = () => {

	const [email, setEmail] = React.useState<IFormElement<string>>({ Value: "", IsValid: false});
	const [userName, setUserName] = React.useState<IFormElement<string>>({ Value: "", IsValid: false});
	const [password, setPassword] = React.useState<IFormElement<string>>({ Value: "", IsValid: false});
	const [confirmPassword, setConfirmPassword] = React.useState<IFormElement<string>>({ Value: "", IsValid: false});
	

	const onChangeUpdate = (newVal: string, currentVal: IFormElement<string>, setFunc: React.Dispatch<React.SetStateAction<IFormElement<string>>>, isValid?: boolean) => {

		if (currentVal.Value === newVal){
			return;
		}

		const newStateVal =  { ...currentVal };
		
		if (typeof isValid !== "undefined") {
			newStateVal.IsValid = isValid;
		}
		
		newStateVal.Value = newVal;

		setFunc(newStateVal);
	}
	
	const hasErrors = () => !(email.IsValid && userName.IsValid && password.IsValid && confirmPassword.IsValid);

	const handleRegisterClick = async () => {

		if (!hasErrors()) {
			const registerModel: IRegisterModel = {
				Email: email.Value,
				UserName: userName.Value,
				Password: password.Value,
				ConfirmPassword: confirmPassword.Value,
			}

			const request = new PostRequest<IRegisterModel, any>(requestUrls.logOn.register);
			const response = await request.send(registerModel);

			if (response.success) {
				location.reload();
			} else {
				createPopUpMessage(response.payload.responseJson, PopUpMessageLevel.Error, "Registration failed", 5000);
			}
		}
	}

	return (
		<div>
			<div id="RegisterForm">
				<ErrorAttachedTextInput
					name="Username"
					payload=""
					errorMessage="Please provide a valid display name."
					onChangeFunc={(newVal, isValid) => onChangeUpdate(newVal, userName, setUserName, isValid)}
					errorFunc={(currentVal) => currentVal === ""}/>
				<ErrorAttachedTextInput
					name="Email"
					payload=""
					errorMessage="Please provide a valid email"
					onChangeFunc={(newVal, isValid) => onChangeUpdate(newVal, email, setEmail, isValid)}
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
					onChangeFunc={(newVal, isValid) => onChangeUpdate(newVal, password, setPassword, isValid)}
					errorFunc={(currentVal) => currentVal === ""}/>
				<ErrorAttachedTextInput
					name="Confirm password"
					payload=""
					inputType="password"
					errorMessage="Please make sure the passwords are the same"
					onChangeFunc={(newVal, isValid) => onChangeUpdate(newVal, confirmPassword, setConfirmPassword, isValid)}
					errorFunc={(currentVal) => {
						const isEmpty = currentVal === "";
						const areEqual = password.Value === currentVal;
						return isEmpty && areEqual;
					}}/>
				<div className="form-group">
					<Button
						title={Translation.Register}
						disabled={hasErrors()}
						customStyles="col-md-offset-2 col-md-10"
						handleClick={handleRegisterClick}/>
				</div>
			</div>
		</div>
	)
}

export default RegisterDialog;