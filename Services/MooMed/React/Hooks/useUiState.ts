// Framework
import * as React from "react";

export const useUiState = <T>(initialValue?: T): [T, (updatedVal: T) => Promise<void>] => {

	const [uiSettings, setUiSettings] = React.useState<T>(initialValue);

	const updateState = React.useCallback(async (updatedVal: T) => {
		setUiSettings(updatedVal);
	}, [setUiSettings]);
	
    return [uiSettings, updateState];
}

export default useUiState;