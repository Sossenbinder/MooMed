"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
Object.defineProperty(exports, "__esModule", { value: true });
const dataLoader = require("helper/dataLoader");
const serviceRegistry_1 = require("helper/serviceRegistry");
const asyncUtils = require("helper/utils/asyncUtils");
const SearchService_1 = require("modules/Search/Service/SearchService");
const AccountService_1 = require("modules/Account/Service/AccountService");
const FriendsService_1 = require("modules/Friends/Service/FriendsService");
const StocksService_1 = require("modules/Stocks/Service/StocksService");
const NotificationService_1 = require("modules/common/Notifications/NotificationService");
const ChatService_1 = require("modules/Chat/Service/ChatService");
const PortfolioService_1 = require("modules/Portfolio/Service/PortfolioService");
const SignalRConnectionProvider_1 = require("modules/common/Helper/SignalRConnectionProvider");
window.onload = () => __awaiter(void 0, void 0, void 0, function* () {
    yield initServices();
    yield dataLoader.init();
});
const initServices = () => __awaiter(void 0, void 0, void 0, function* () {
    const signalRConnectionProvider = new SignalRConnectionProvider_1.default();
    yield signalRConnectionProvider.start();
    yield initCoreServices(signalRConnectionProvider);
    initAdditionalServices(signalRConnectionProvider);
});
const initCoreServices = (signalRConnectionProvider) => __awaiter(void 0, void 0, void 0, function* () {
    const modules = [];
    serviceRegistry_1.services.SearchService = new SearchService_1.default();
    modules.push(serviceRegistry_1.services.SearchService);
    serviceRegistry_1.services.AccountService = new AccountService_1.default();
    modules.push(serviceRegistry_1.services.AccountService);
    serviceRegistry_1.services.NotificationService = new NotificationService_1.default(signalRConnectionProvider);
    modules.push(serviceRegistry_1.services.NotificationService);
    serviceRegistry_1.services.FriendsService = new FriendsService_1.default();
    modules.push(serviceRegistry_1.services.FriendsService);
    serviceRegistry_1.services.ChatService = new ChatService_1.default(signalRConnectionProvider);
    modules.push(serviceRegistry_1.services.ChatService);
    yield asyncUtils.asyncForEach(modules, x => x.start());
});
const initAdditionalServices = (signalRConnectionProvider) => __awaiter(void 0, void 0, void 0, function* () {
    const modules = [];
    serviceRegistry_1.services.StocksService = new StocksService_1.default();
    modules.push(serviceRegistry_1.services.StocksService);
    serviceRegistry_1.services.PortfolioService = new PortfolioService_1.default();
    modules.push(serviceRegistry_1.services.PortfolioService);
    yield Promise.all(modules.map(x => x.start()));
});
//# sourceMappingURL=index.js.map