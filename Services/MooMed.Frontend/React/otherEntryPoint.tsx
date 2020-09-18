//Functionality
import { services } from "helper/serviceRegistry";
import AccountValidationService from "modules/Account/Service/AccountValidationService";

// Components
import renderOtherPage from "views/Pages/Other/OtherPage";

window.onload = async () => {

	await initServices();

	renderOtherPage();
};

const initServices = async () => {
	const accountValidationService = new AccountValidationService();
	services.AccountValidationService = accountValidationService;
}