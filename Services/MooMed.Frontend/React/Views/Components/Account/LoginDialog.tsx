// Framework
import * as React from "react";
import { Link } from "react-router-dom";

// Components
import ErrorAttachedTextInput from "common/components/General/Input/ErrorAttached/ErrorAttachedTextInput";
import { CheckBoxToggle } from "common/components/General/Input/CheckBoxToggle";
import Button from "common/components/General/Input/Buttons/Button";

// Functionality
import requestUrls from "helper/requestUrls";
import PostRequest from "helper/requests/PostRequest";
import { PopUpMessageLevel } from "definitions/PopUpNotificationDefinitions";
import { createPopUpMessage } from "helper/popUpMessageHelper";
import { IFormElement } from "definitions/Forms";
import { LoginResponseCode } from "enums/moomedEnums";
import { useTranslations } from "hooks/useTranslations";

interface ILoginModel {
    Email: string;
    Password: string;
    RememberMe: boolean;
}

type ILoginResponsePayload = {
	loginResponseCode: LoginResponseCode;
	account: Account;
}

export const LoginDialog: React.FC = () => {

	const [email, setEmail] = React.useState<IFormElement<string>>({ Value: "", IsValid: false});
	const [password, setPassword] = React.useState<IFormElement<string>>({ Value: "", IsValid: false});
	const [rememberMe, setRememberMe] = React.useState<IFormElement<boolean>>({ Value: false, IsValid: true});

	const Translation = useTranslations();

	const onChangeUpdate = React.useCallback(<T extends {}>(newVal: T, currentVal: IFormElement<T>, setFunc: React.Dispatch<React.SetStateAction<IFormElement<T>>>, isValid?: boolean) => {

		if (currentVal.Value === newVal){
			return;
		}

		const newStateVal: IFormElement<T> = { ...currentVal };
		
		if (typeof isValid !== "undefined") {
			newStateVal.IsValid = isValid;
		}
		
		newStateVal.Value = newVal;

		setFunc(newStateVal);
	}, []);

	const hasErrors = () => {
		return !(email.IsValid && password.IsValid && rememberMe.IsValid);
	};

	const handleLogin = async () => {

		if (!hasErrors()) {

            const loginModel: ILoginModel = {
				Email: email.Value,
				Password: password.Value,
				RememberMe: rememberMe.Value,
			}

			const request = new PostRequest<ILoginModel, ILoginResponsePayload>(requestUrls.logOn.login);
			const response = await request.post(loginModel);

			if (response.success) {
				location.href = "/";
			} else {
				const loginResultErrorCode = response.payload.loginResponseCode;

				let errorMessage = "";
				switch (loginResultErrorCode){
					case LoginResponseCode.AccountNotFound:
						errorMessage = Translation.AccountNotFound;
						break;
					case LoginResponseCode.EmailNotValidated:
						errorMessage = Translation.AccountValidationEmailNotValidatedYet;
				}
				createPopUpMessage(errorMessage, PopUpMessageLevel.Error, undefined, 5000);
			}
        }
	};
	
	return(
		<div>
			<ErrorAttachedTextInput
				name="Email"
				payload=""
				errorMessage="Please provide a valid email address"
				onChangeFunc={(newVal, isValid) => onChangeUpdate<string>(newVal, email, setEmail, isValid)}
				errorFunc={(currentVal) => currentVal === "" || currentVal.search(/^\S+@\S+$/) === -1}/>
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
			<hr/>
			<div className="align-middle">
				<Link to="/forgotPassword">Forgot password?</Link>
			</div>
		</div>
	);
}

export default LoginDialog;