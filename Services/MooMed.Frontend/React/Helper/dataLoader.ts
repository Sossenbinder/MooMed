// Functionality
import { reducer as accountReducer } from "modules/Account/Reducer/AccountReducer";
import { store } from "data/store";
import renderMainView from "views/Pages/Home/Main";
import useServices from "hooks/useServices";

export const init = async () => {

	const { AccountService } = useServices();

	const account = await AccountService.getOwnAccount();

	if (account) {
		store.dispatch(accountReducer.replace(account));
		renderMainView();
	}
};