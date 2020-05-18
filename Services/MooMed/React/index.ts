// Entry point in here
import * as dataLoader from "helper/dataLoader";

// Functionality
import { services } from "helper/serviceRegistry";
import SearchService from "modules/Search/Service/SearchService";
import AccountService from "modules/Account/Service/AccountService";
import FriendsService from "modules/Friends/Service/FriendsService";
import NotificationService from "modules/common/Notifications/NotificationService";
import ChatService from "modules/Chat/Service/ChatService";
import SignalRConnectionProvider from "modules/common/Helper/SignalRConnectionProvider";

window.onload = async () => {
    
    await initServices();

    await dataLoader.init();
}

const initServices = async () => {

	const signalRConnectionProvider = new SignalRConnectionProvider();
	await signalRConnectionProvider.start();

	const notificationService = new NotificationService(signalRConnectionProvider);
	await notificationService.start();
	services.NotificationService = notificationService;

	services.SearchService = new SearchService();	
	services.AccountService = new AccountService();

	const friendsService = new FriendsService();
	await friendsService.start();
	services.FriendsService = friendsService;

	const chatService = new ChatService(signalRConnectionProvider);
	await chatService.start();
	services.ChatService = chatService;
}