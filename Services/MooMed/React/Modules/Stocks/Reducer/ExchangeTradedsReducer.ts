// Functionality
import { createReducer } from "modules/common/reducer/CrudReducer"
import { ExchangeTradedItem } from "modules/stocks/types";

export const reducer = createReducer<ExchangeTradedItem>({
	actionIdentifier: "EXCHANGETRADEDS",
	key: "isin"
});

export default reducer;