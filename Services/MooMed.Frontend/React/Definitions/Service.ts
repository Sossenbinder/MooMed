// Types
import { Account, AccountValidationResult, PersonalData, PasswordData } from "modules/Account/types";
import { SearchResult } from "modules/Search/types";
import { Currency, NotificationType } from "enums/moomedEnums";
import { SignalRNotification } from "data/notifications";

export interface IModuleService {
	start(): Promise<void>;
}

export interface IAccountService extends IModuleService {
	getAccount(accountId: number): Promise<Account>;
	getOwnAccount(): Promise<Account>;

	updateProfilePicture(file: File): Promise<void>;

	updatePersonalData(personalData: PersonalData): Promise<boolean>;
	updatePassword(passwordData: PasswordData): Promise<boolean>;
}

export interface IAccountValidationService extends IModuleService {
	validateRegistration(accountId: number, token: string): Promise<AccountValidationResult>;
}

export interface ILogonService extends IModuleService {
	login(email: string, password: string, rememberMe: boolean): Promise<void>;
	register(email: string, userName: string, password: string, confirmPassword: string): Promise<void>;
	logOff(): Promise<void>;
}

export interface IFriendsService extends IModuleService {
	addFriend(friendId: number): Promise<void>;
}

export interface IStocksService extends IModuleService {

}

export interface IChatService extends IModuleService {
	openChat(partnerId: number): void;
	registerForActiveChatChange(handler: (partnerId: number) => void): void;
	initChatRoom(id: number): Promise<void>;

	sendMessage(message: string, receiverId: number): Promise<void>;
}

export interface ISearchService extends IModuleService {
	search(query: string): Promise<SearchResult>;
}

export interface INotificationService extends IModuleService {
	subscribe<T>(notificationType: NotificationType, onNotify: (notification: SignalRNotification<T>) => void): void;
	unsubscribe(notificationType: NotificationType): void;
}

export interface IPortfolioService extends IModuleService {
	addToPortfolio(isin: string, amount: number): Promise<void>;
}

export interface ISavingService extends IModuleService {
	initSavingService(): Promise<void>;
	saveBasicSettings(): Promise<void>;
	setCurrency(currency: Currency): Promise<void>;
}

// Contexts
export type ServiceContext = {
	AccountService: IAccountService;
	AccountValidationService: IAccountValidationService;
	LogonService: ILogonService;
	FriendsService: IFriendsService;
	SearchService: ISearchService;
	NotificationService: INotificationService;
	ChatService: IChatService;
	StocksService: IStocksService;
	PortfolioService: IPortfolioService;
	SavingService: ISavingService;
}