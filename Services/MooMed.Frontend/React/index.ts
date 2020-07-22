// Entry point in here
import * as dataLoader from "helper/dataLoader";

// Functionality
import { services } from "helper/serviceRegistry";
import * as asyncUtils from "helper/utils/asyncUtils";

// Types
import { IModuleService } from "definitions/Service";
import SearchService from "modules/Search/Service/SearchService";
import AccountService from "modules/Account/Service/AccountService";
import FriendsService from "modules/Friends/Service/FriendsService";
import StocksService from "modules/Stocks/Service/StocksService";
import NotificationService from "modules/common/Notifications/NotificationService";
import ChatService from "modules/Chat/Service/ChatService";
import PortfolioService from "modules/Portfolio/Service/PortfolioService";
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

	const modules: Array<IModuleService> = [];

	// Core services
	services.SearchService = new SearchService();
	modules.push(services.SearchService);

	services.AccountService = new AccountService();
	modules.push(services.AccountService);

	services.NotificationService = new NotificationService(signalRConnectionProvider);
	modules.push(services.NotificationService);

	services.FriendsService = new FriendsService();	
	modules.push(services.FriendsService);

	services.ChatService = new ChatService(signalRConnectionProvider);
	modules.push(services.ChatService);

	await asyncUtils.asyncForEach(modules, x => x.start());
}

const initAdditionalServices = async (signalRConnectionProvider: SignalRConnectionProvider) => {
	
	const modules: Array<IModuleService> = [];

	services.StocksService = new StocksService();
	modules.push(services.StocksService);
	
	services.PortfolioService = new PortfolioService();
	modules.push(services.PortfolioService);

	await Promise.all(modules.map(x => x.start()));
}