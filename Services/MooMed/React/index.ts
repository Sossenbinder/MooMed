// Entry point in here
import * as dataLoader from "helper/dataLoader";

// Functionality
import { services } from "hooks/useServices";
import SearchService from "modules/Search/Service/SearchService";
import AccountService from "modules/Account/Service/AccountService";
import FriendsService from "modules/Friends/Service/FriendsService";
import NotificationService from "modules/common/Notifications/NotificationService";

window.onload = async () => {
    
    await initServices();

    await dataLoader.init();
}

const initServices = async () => {

	const notificationService = new NotificationService();
	await notificationService.start();
	services.NotificationService = notificationService;

	services.SearchService = new SearchService();	
	services.AccountService = new AccountService();

	const friendsService = new FriendsService();
	await friendsService.start();
	services.FriendsService = friendsService;
}