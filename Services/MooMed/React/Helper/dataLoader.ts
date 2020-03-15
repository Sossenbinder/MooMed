// Functionality
import { addAccount } from "data/reducers/accountReducer";
import { store } from "data/store";
import renderMainView from "views/Pages/Home/Main";
import useServices from "hooks/useServices";

export const init = async () => {
    
    const { AccountService } = useServices();

    const account = await AccountService.getOwnAccount();

    if (account) {
        store.dispatch(addAccount(account));
        renderMainView();
    }
};