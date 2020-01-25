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

export const LoginDialog: React.FC = () => {

	const [email, setEmail] = React.useState<IFormElement<string>>();
	const [password, setPassword] = React.useState<IFormElement<string>>();
	const [rememberMe, setRememberMe] = React.useState<IFormElement<boolean>>();

	const onChangeUpdate = <T extends {}>(newVal: T, currentVal: IFormElement<T>, setFunc: React.Dispatch<React.SetStateAction<IFormElement<T>>>, isValid?: boolean) => {

		if (currentVal.Value === newVal){
			return;
		}

		const newStateVal: IFormElement<T> = { ...currentVal };
		
		if (typeof isValid !== "undefined") {
			newStateVal.IsValid = isValid;
		}
		
		newStateVal.Value = newVal;

		setFunc(newStateVal);
	}

	const hasErrors = () => {
		return email.IsValid || password.IsValid;
	};

	const handleLogin = async () => {
		
		if (!hasErrors()) {

            const loginModel: ILoginModel = {
				Email: email.Value,
				Password: password.Value,
				RememberMe: rememberMe.Value,
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
	
	return(
		<div>
			<form className="signInDialog" id="loginForm" onSubmit={handleLogin}>
				<ErrorAttachedTextInput
					name="Email"
					payload=""
					errorMessage="Please provide a valid email address"
					onChangeFunc={(newVal, isValid) => onChangeUpdate<string>(newVal, email, setEmail, isValid)}
					errorFunc={(currentVal) =>  currentVal === "" || currentVal.search(/^\S+@\S+$/) === -1}/>
				<ErrorAttachedTextInput
					name="Password"
					inputType="password"
					payload=""
					onChangeFunc={(newVal, isValid) => onChangeUpdate<string>(newVal, password, setPassword, isValid)}
					errorMessage="Please provide a valid password"
					errorFunc={(currentVal) => currentVal === ""}/>
				<CheckBoxToggle
					name="Stay logged in?"
					initialToggle={false}
					onChange={(newVal) => onChangeUpdate(newVal, rememberMe, setRememberMe)}
				/>
				<div className="form-group">
					<Button
						title={Translation.Login}
						customStyles="col-md-offset-2 col-md-10"
						disabled={hasErrors()}
						handleClick={handleLogin}
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

export default LoginDialog;