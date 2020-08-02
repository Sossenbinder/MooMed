// Framework
import * as React from "react";

// Types
import { FormData } from "definitions/Forms";

export const useFormState = <T>(defaultVal: T, isValid = false): [FormData<T>, React.Dispatch<FormData<T>>] => {
	const [formState, setFormState] = React.useState<FormData<T>>({
		Value: defaultVal,
		IsValid: isValid,		
	});

	return [formState, setFormState];
}

export default useFormState;