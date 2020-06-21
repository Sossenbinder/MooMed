// Entry point in here
import * as dataLoader from "helper/dataLoader";

// Functionality
import { services } from "helper/serviceRegistry";
import SearchService from "modules/Search/Service/SearchService";
import AccountService from "modules/Account/Service/AccountService";
import FriendsService from "modules/Friends/Service/FriendsService";
import StocksService from "modules/Stocks/Service/StocksService";
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

	await initCoreServices(signalRConnectionProvider);

	initAdditionalServices(signalRConnectionProvider);
}

const initCoreServices = async (signalRConnectionProvider: SignalRConnectionProvider) => {

	// Core services
	services.SearchService = new SearchService();	
	services.AccountService = new AccountService();

	const notificationService = new NotificationService(signalRConnectionProvider);
	await notificationService.start();
	services.NotificationService = notificationService;

	const chatService = new ChatService(signalRConnectionProvider);
	await chatService.start();
	services.ChatService = chatService;
}

const initAdditionalServices = async (signalRConnectionProvider: SignalRConnectionProvider) => {

	const parallelServiceInits: Array<Promise<void>> = [];
	
	const friendsService = new FriendsService();
	parallelServiceInits.push(friendsService.start());
	services.FriendsService = friendsService;

	const stocksService = new StocksService();
	parallelServiceInits.push(stocksService.start());
	services.StocksService = stocksService;

	await Promise.all(parallelServiceInits);
}