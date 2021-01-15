// Framework
import * as React from "react";
import { Link } from "react-router-dom";

// Components
import ErrorAttachedTextInput from "common/components/general/input/ErrorAttached/ErrorAttachedTextInput";
import { CheckBoxToggle } from "common/components/general/input/CheckBoxToggle";
import Button from "common/components/general/input/Buttons/Button";
import Flex from "common/components/Flex";

// Functionality
import useTranslations from "hooks/useTranslations";
import useFormState from "hooks/useFormState";
import useServices from "hooks/useServices";

export const LoginDialog: React.FC = () => {

	const [email, setEmail] = useFormState<string>("");
	const [password, setPassword] = useFormState<string>("");
	const [rememberMe, setRememberMe] = React.useState<boolean>();

	const Translation = useTranslations();
	const { LogonService } = useServices();

	const hasErrors = () => {
		return !(email.IsValid && password.IsValid);
	};

	const handleLogin = async () => {
		await LogonService.login(email.Value, password.Value, rememberMe);
	}

	return (
		<div>
			<ErrorAttachedTextInput
				name="Email"
				formData={email}
				setFormData={setEmail}
				errorMessage="Please provide a valid email address"
				errorFunc={(currentVal) => currentVal === "" || currentVal.search(/^\S+@\S+$/) === -1} />
			<ErrorAttachedTextInput
				name="Password"
				inputType="password"
				formData={password}
				setFormData={setPassword}
				errorMessage="Please provide a valid password"
				errorFunc={(currentVal) => currentVal === ""} />
			<CheckBoxToggle
				text="Stay logged in?"
				initialToggle={false}
				onChange={setRememberMe}
			/>
			<Flex className="form-group">
				<Button
					title={Translation.Login}
					classname="col-md-offset-2 col-md-10"
					disabled={hasErrors()}
					onClick={handleLogin}
				/>
			</Flex>
			<hr />
			<Flex className="align-middle">
				<Link to="/forgotPassword">Forgot password?</Link>
			</Flex>
		</div>
	);
}

export default LoginDialog;