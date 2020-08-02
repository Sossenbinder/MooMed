// Framework
import * as React from "react";

// Components
import Flex from "Common/Components/Flex";

type Props = {
	name: string;
	inputType?: string;
	payload: string;
	onChangeFunc: (currentVal: string, isValid: boolean) => void;
	errorFunc?: (currentVal: string) => boolean;
	errorMessage?: string;
	onEnterPress?: () => void;
}

export const ErrorAttachedTextInput: React.FC<Props> = ({ name, inputType, payload: propPayload, onChangeFunc, errorFunc, errorMessage, onEnterPress }) => {

	let touchTimeout: number;

	const [touched, setTouched] = React.useState(false);
	const [payload, setPayload] = React.useState(propPayload);
	const [isValid, setIsValid] = React.useState(true);

	const calculateValidity = React.useCallback((data: string) => {
		let validity = true;

		if (touched) {
			if (errorFunc) {
				validity = !errorFunc(data);
			}
		}
		
		setIsValid(validity);

		return validity;
	}, [touched, errorFunc]);

	const handleChange = React.useCallback((event: React.ChangeEvent<HTMLInputElement>) => {

		const val = event.target.value;

		setPayload(val);

		const validity = calculateValidity(val);

		onChangeFunc(val, validity);

		if (!touched) {

			if (touchTimeout) {
				clearTimeout(touchTimeout)
			}

			touchTimeout = window.setTimeout(() => {
				setTouched(true);
			}, 500);
		}
	}, [touched]);

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
				className={(touched && !isValid) ?  "form-control is-invalid" : "form-control"}
				type={inputType != null ? inputType : "text"}
				name={name}
				value={payload}
				onChange={handleChange}
				placeholder={name}
				onKeyPress={handleKeyPress}
			/>
			<If condition={touched && !isValid && errorMessage !== ""}>
				<Flex className="invalid-feedback">
					{errorMessage}
				</Flex>
			</If>
		</Flex>
	)
};

export default ErrorAttachedTextInput;