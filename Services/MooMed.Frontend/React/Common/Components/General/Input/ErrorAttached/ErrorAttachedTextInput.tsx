// Framework
import * as React from "react";

// Components
import Flex from "Common/Components/Flex";
import TextInput from "common/Components/General/Input/General/TextInput";

// Types
import { FormData } from "definitions/Forms";

export type InputProps = {
	name: string;

	formData: FormData<string>;
	setFormData(value: FormData<string>): void;

	inputType?: string;

	errorFunc?: (currentVal: string) => boolean;
	errorMessage?: string;

	onEnterPress?: () => void;

	timeout?: number;
}

export const ErrorAttachedTextInput: React.FC<InputProps> = ({
	name,
	inputType,
	formData,
	setFormData,
	errorFunc,
	errorMessage,
	onEnterPress,
	timeout = 500}) => {

	let touchTimeout: number;

	// Internal states
	const [touched, setTouched] = React.useState(false);

	// Required so the touchTimeout CB can work correctly
	const formDataRef = React.useRef(formData);
	formDataRef.current = formData;

	const calculateValidity = React.useCallback((data: string, wasTouched: boolean) => {
		return !wasTouched || !errorFunc ? true : !errorFunc(data);
	}, [errorFunc]);

	const handleTouchTimeout = React.useCallback(() => {
		// When touched, no need to do this anymore
		if (!touched) {
			
			// Whenever new input is coming in while the timeout is still active, clear the timeout
			if (touchTimeout !== undefined) {
				clearTimeout(touchTimeout)
			}

			// Set a new timeout
			touchTimeout = window.setTimeout(() => {
				setTouched(true);
				
				setFormData({ 
					Value: formDataRef.current.Value,
					IsValid: calculateValidity(formDataRef.current.Value, true)
				 });
			}, timeout);
		}
	}, [touched, formDataRef, touchTimeout]);

	const handleChange = React.useCallback((newData: string) => {

		const isValid = calculateValidity(newData, touched);

		// Update the data
		setFormData({
			IsValid: isValid,
			Value: newData
		});

		// Handle timeout
		handleTouchTimeout();
	}, [handleTouchTimeout, formData, setFormData, touched]);

	return (
		<TextInput
			classNames={(touched && !formData.IsValid) ? "form-control is-invalid" : "form-control"}
			inputType={inputType != null ? inputType : "text"}
			name={name}
			data={formData.Value}
			setData={handleChange}
			onEnterPress={onEnterPress}>
			<If condition={touched && !formData.IsValid && errorMessage !== ""}>
				<Flex className="invalid-feedback">
					{errorMessage}
				</Flex>
			</If>
		</TextInput>
	)
};

export default ErrorAttachedTextInput;