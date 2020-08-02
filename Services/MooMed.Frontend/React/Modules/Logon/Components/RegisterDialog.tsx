// Framework
import * as React from "react";

// Components
import ErrorAttachedTextInput from "common/components/General/Input/ErrorAttached/ErrorAttachedTextInput";
import Button from "common/components/general/Input/Buttons/Button";

// Functionality
import PostRequest from "helper/requests/PostRequest";
import requestUrls from "helper/requestUrls";
import { PopUpMessageLevel } from "definitions/PopUpNotificationDefinitions";
import { createPopUpMessage } from "helper/popUpMessageHelper";
import useTranslations from "hooks/useTranslations";
import useFormState from "hooks/useFormState";

// Types
import { FormData } from "definitions/Forms";

interface IRegisterModel {
	Email: string;
	UserName: string;
	Password: string;
	ConfirmPassword: string;
}

export const RegisterDialog: React.FC = () => {

	const [email, setEmail] = useFormState("");
	const [userName, setUserName] = useFormState("");
	const [password, setPassword] = useFormState("");
	const [confirmPassword, setConfirmPassword] = useFormState("");

	const Translation = useTranslations();	

	const onChangeUpdate = (newVal: string, currentVal: FormData<string>, setFunc: React.Dispatch<React.SetStateAction<FormData<string>>>, isValid?: boolean) => {

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
			const response = await request.post(registerModel);

			debugger;
			
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
						classname="col-md-offset-2 col-md-10"
						onClick={handleRegisterClick}/>
				</div>
			</div>
		</div>
	)
}

export default RegisterDialog;