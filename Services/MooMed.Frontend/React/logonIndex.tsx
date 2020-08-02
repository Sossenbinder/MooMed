// Functionality
import { services } from "helper/serviceRegistry";
import LogonService from "modules/Logon/Service/LogonService";

// Components
import renderLogonView from "views/Pages/LogOn/LogonPage";

window.onload = async () => {
	
	await initServices();

	renderLogonView();
};

const initServices = async () => {	
	
	const logonService = new LogonService();
	await logonService.start();
	services.LogonService = logonService;
}