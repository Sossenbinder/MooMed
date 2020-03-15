// Entry point in here
import * as dataLoader from "helper/dataLoader";

// Functionality
import { services } from "hooks/useServices";
import SearchService from "modules/Search/Service/SearchService";
import AccountService from "modules/Account/Service/AccountService";
import FriendsService from "modules/Friends/Service/FriendsService";

window.onload = async () => {
    
    initServices();

    await dataLoader.init();
}

const initServices = () => {
    services.SearchService = new SearchService();
    services.AccountService = new AccountService();
    services.FriendsService = new FriendsService();
}