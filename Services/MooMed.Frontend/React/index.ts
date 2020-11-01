﻿// Entry point in here
import * as dataLoader from "helper/dataLoader";

// Functionality
import { services } from "helper/serviceRegistry";
import * as asyncUtils from "helper/utils/asyncUtils";

// Types
import { IModuleService } from "definitions/Service";
import LogonService from "modules/Logon/Service/LogonService";
import SearchService from "modules/Search/Service/SearchService";
import AccountService from "modules/Account/Service/AccountService";
import FriendsService from "modules/Friends/Service/FriendsService";
import StocksService from "modules/Stocks/Service/StocksService";
import SavingService from "modules/Saving/Service/SavingService";
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

	initAdditionalServices();
}

const initCoreServices = async (signalRConnectionProvider: SignalRConnectionProvider) => {

	const modules: Array<IModuleService> = [];

	// Core services
	services.SearchService = new SearchService();
	modules.push(services.SearchService);

	services.AccountService = new AccountService();
	modules.push(services.AccountService);

	services.LogonService = new LogonService();
	modules.push(services.LogonService);

	services.NotificationService = new NotificationService(signalRConnectionProvider);
	modules.push(services.NotificationService);

	services.FriendsService = new FriendsService();	
	modules.push(services.FriendsService);

	services.ChatService = new ChatService(signalRConnectionProvider);
	modules.push(services.ChatService);

	await asyncUtils.asyncForEach(modules, x => x.start());
}

const initAdditionalServices = async () => {
	
	const modules: Array<IModuleService> = [];

	services.StocksService = new StocksService();
	modules.push(services.StocksService);
	
	services.PortfolioService = new PortfolioService();
	modules.push(services.PortfolioService);

	services.SavingService = new SavingService();
	modules.push(services.SavingService);

	await Promise.all(modules.map(x => x.start()));
}