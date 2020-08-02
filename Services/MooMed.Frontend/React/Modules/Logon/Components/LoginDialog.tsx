// Framework
import * as React from "react";
import { Link } from "react-router-dom";

// Components
import ErrorAttachedTextInput from "common/components/General/Input/ErrorAttached/ErrorAttachedTextInput";
import { CheckBoxToggle } from "common/components/General/Input/CheckBoxToggle";
import Button from "common/components/General/Input/Buttons/Button";
import Flex from "common/Components/Flex";

// Functionality
import { FormData } from "definitions/Forms";
import { LoginResponseCode } from "enums/moomedEnums";
import useTranslations from "hooks/useTranslations";
import useFormState from "hooks/useFormState";
import useServices from "hooks/useServices";

export const LoginDialog: React.FC = () => {

	const [email, setEmail] = useFormState<string>("");
	const [password, setPassword] = useFormState<string>("");
	const [rememberMe, setRememberMe] = useFormState<boolean>(false, true);

	const Translation = useTranslations();
	const { LogonService } = useServices();

	const onChangeUpdate = React.useCallback(<T extends {}>(
		newVal: T, 
		currentVal: FormData<T>, 
		setFunc: React.Dispatch<React.SetStateAction<FormData<T>>>, 
		isValid?: boolean) => {

		// Check if there was any change in the value
		if (currentVal.Value === newVal){
			return;
		}

		const newStateVal: FormData<T> = { ...currentVal };
		
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
		await LogonService.login(email.Value, password.Value, rememberMe.Value);
	}
	
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
				text="Stay logged in?"
				initialToggle={false}
				onChange={(newVal) => onChangeUpdate(newVal, rememberMe, setRememberMe)}
			/>
			<Flex className="form-group">
				<Button
					title={Translation.Login}
					classname="col-md-offset-2 col-md-10"
					disabled={hasErrors()}
					onClick={handleLogin}
				/>
			</Flex>
			<hr/>
			<Flex className="align-middle">
				<Link to="/forgotPassword">Forgot password?</Link>
			</Flex>
		</div>
	);
}

export default LoginDialog;