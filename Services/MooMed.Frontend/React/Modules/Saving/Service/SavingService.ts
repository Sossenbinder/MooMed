// Framework

// Functionality
import { ISavingService } from "Definitions/Service";
import ModuleService from "modules/common/Service/ModuleService";
import * as savingCommunication from "modules/saving/communication/SavingCommunication";

// Types
import { Currency } from "enums/moomedEnums";

export default class SavingService extends ModuleService implements ISavingService {

	public constructor() {
		super();
	}

	public async start(): Promise<void> {

	}

	public async setCurrency(currency: Currency): Promise<void> {
		const response = await savingCommunication.setCurrency(currency);

		console.log("Setting currency response: " + response.success);

		if (response.success) {
			
		}
	}
}