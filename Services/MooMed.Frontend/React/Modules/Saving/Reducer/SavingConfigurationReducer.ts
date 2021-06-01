// Functionality
import { createSingleReducer } from "modules/common/reducer/CrudReducer";

// Types
import { SavingInfo, Assets } from "modules/saving/types";
import { ReducerState, ReducerAction } from "modules/common/Reducer/types";

export const UPDATE_ASSETS = "SAVING_ASSETS_UPDATE";

export const updateAssets = (assets: Assets): ReducerAction<SavingInfo> => ({
	type: UPDATE_ASSETS,
	payload: {
		assets,
	} as SavingInfo,
});

export const reducer = createSingleReducer<SavingInfo>({
	actionIdentifier: "SAVING",
	additionalActions: [
		{
			type: UPDATE_ASSETS,
			action: (state: ReducerState<SavingInfo>, action: ReducerAction<SavingInfo>) => {
				const data = { ...state.data };

				data.assets = action.payload.assets;

				return {
					data,
				};
			},
		}
	]
});

export default reducer;