// Framework
import * as React from "react";

//Functionality
import * as hookUtils from "helper/Utils/hookUtils";

type BackendCall<TIn, TOut> = (data?: TIn) => Promise<TOut>;

export const useBackendCallWrapper = <TIn, TOut>(backendCall: BackendCall<TIn, TOut>): [boolean, BackendCall<TIn, TOut>] => {
	const [loading, setLoading] = React.useState<boolean>();

	const runBackendCall = React.useCallback(async (data?: TIn) => {
		return await hookUtils.usingBoolAsync(setLoading, () => backendCall(data));
	}, [backendCall]);

	return [loading, runBackendCall];
}

export default useBackendCallWrapper;