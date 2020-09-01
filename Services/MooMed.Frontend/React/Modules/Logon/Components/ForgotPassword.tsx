// Framework
import * as React from "react";

// Components
import ErrorAttachedTextInput from "common/components/General/Input/ErrorAttached/ErrorAttachedTextInput";

// Functionality
import useFormState from "hooks/useFormState";

export const ForgotPassword: React.FC = () => {

	const [forgotPasswordEmail, setForgotPasswordEmail] = useFormState<string>("");

	return (
		<ErrorAttachedTextInput
			name="Email"
			formData={forgotPasswordEmail}
			setFormData={setForgotPasswordEmail}
			errorMessage="Please provide a valid email address"
			errorFunc={(currentVal) => {
				const isEmpty = currentVal === "";
				const isInValidEmail = currentVal.search(/^\S+@\S+$/) === -1;
				return isEmpty || isInValidEmail;
			}}
		/>
	);
}

export default ForgotPassword;