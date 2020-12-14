// Framework
import * as React from "react";

// Components
import ErrorAttachedTextInput from "common/components/general/input/ErrorAttached/ErrorAttachedTextInput";
import Button from "common/components/general/input/Buttons/Button";

// Functionality
import useTranslations from "hooks/useTranslations";
import useFormState from "hooks/useFormState";
import useServices from "hooks/useServices";


export const RegisterDialog: React.FC = () => {

	const [email, setEmail] = useFormState("");
	const [userName, setUserName] = useFormState("");
	const [password, setPassword] = useFormState("");
	const [confirmPassword, setConfirmPassword] = useFormState("");

	const Translation = useTranslations();
	const { LogonService } = useServices();

	const hasErrors = () => !(email.IsValid && userName.IsValid && password.IsValid && confirmPassword.IsValid);

	const handleRegisterClick = async () => {

		if (!hasErrors()) {			
			await LogonService.register(email.Value, userName.Value, password.Value, confirmPassword.Value);
		}
	}

	return (
		<div>
			<div id="RegisterForm">
				<ErrorAttachedTextInput
					name="Username"
					formData={userName}
					setFormData={setUserName}
					errorMessage="Please provide a valid display name."
					errorFunc={(currentVal) => currentVal === ""} />
				<ErrorAttachedTextInput
					name="Email"
					formData={email}
					setFormData={setEmail}
					errorMessage="Please provide a valid email"
					errorFunc={(currentVal) => {
						const isEmpty = currentVal === "";
						const isInValidEmail = currentVal.search(/^\S+@\S+$/) === -1;
						return isEmpty || isInValidEmail;
					}} />
				<ErrorAttachedTextInput
					name="Password"
					formData={password}
					setFormData={setPassword}
					inputType="password"
					errorMessage="Please provide a valid password"
					errorFunc={(currentVal) => currentVal === ""} />
				<ErrorAttachedTextInput
					name="Confirm password"
					formData={confirmPassword}
					setFormData={setConfirmPassword}
					inputType="password"
					errorMessage="Please make sure the passwords are the same"
					errorFunc={(currentVal) => {
						const isEmpty = currentVal === "";
						const areEqual = password.Value === currentVal;
						return isEmpty && areEqual;
					}} />
				<div className="form-group">
					<Button
						title={Translation.Register}
						disabled={hasErrors()}
						classname="col-md-offset-2 col-md-10"
						onClick={handleRegisterClick} />
				</div>
			</div>
		</div>
	)
}

export default RegisterDialog;