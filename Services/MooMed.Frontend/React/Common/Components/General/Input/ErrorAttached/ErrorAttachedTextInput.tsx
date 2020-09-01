// Framework
import * as React from "react";

// Components
import Flex from "Common/Components/Flex";

// Types
import { FormData } from "definitions/Forms";

type Props = {
	name: string;

	formData: FormData<string>;
	setFormData: React.Dispatch<FormData<string>>;

	inputType?: string;

	errorFunc?: (currentVal: string) => boolean;
	errorMessage?: string;

	onEnterPress?: () => void;
}

export const ErrorAttachedTextInput: React.FC<Props> = ({
	name,
	inputType,
	formData,
	setFormData,
	errorFunc,
	errorMessage,
	onEnterPress }) => {

	let touchTimeout: number;

	// Internal states
	const [touched, setTouched] = React.useState(false);

	const calculateValidity = React.useCallback((data: string) => {
		let validity = true;

		if (touched && errorFunc) {
			validity = !errorFunc(data);
		}

		return validity;
	}, [touched, errorFunc]);

	const handleTouchTimeout = React.useCallback(() => {
		// Give the user a bit of time before we (possibly) show an error
		if (!touched) {

			if (touchTimeout) {
				clearTimeout(touchTimeout)
			}

			touchTimeout = window.setTimeout(() => {
				setTouched(true);
				calculateValidity(formData.Value);
			}, 500);
		}
	}, [touched, touchTimeout]);

	const handleChange = React.useCallback((event: React.ChangeEvent<HTMLInputElement>) => {

		// Get and set new payload
		const inputFieldValue = event.target.value;

		// Update validity
		const validity = calculateValidity(inputFieldValue);

		if (formData.Value !== inputFieldValue) {
			const newStateVal: FormData<string> = {
				IsValid: validity,
				Value: inputFieldValue
			};

			setFormData(newStateVal);
		}

		// Handle timeout
		handleTouchTimeout();
	}, [handleTouchTimeout, formData, setFormData]);

	const handleKeyPress = React.useCallback((event: React.KeyboardEvent<HTMLInputElement>) => {
		if (event.charCode === 13 && typeof onEnterPress !== "undefined") {
			onEnterPress();
		}
	}, [onEnterPress]);

	return (
		<Flex
			direction="Column"
			className="form-group">
			<input
				className={(touched && !formData.IsValid) ? "form-control is-invalid" : "form-control"}
				type={inputType != null ? inputType : "text"}
				name={name}
				value={formData.Value}
				onChange={handleChange}
				placeholder={name}
				onKeyPress={handleKeyPress}
			/>
			<If condition={touched && !formData.IsValid && errorMessage !== ""}>
				<Flex className="invalid-feedback">
					{errorMessage}
				</Flex>
			</If>
		</Flex>
	)
};

export default ErrorAttachedTextInput;