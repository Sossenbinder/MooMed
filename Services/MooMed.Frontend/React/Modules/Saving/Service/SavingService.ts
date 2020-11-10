// Framework

// Functionality
import { ISavingService } from "Definitions/Service";
import ModuleService from "modules/common/Service/ModuleService";
import * as savingCommunication from "modules/saving/communication/SavingCommunication";
import { reducer as savingConfigurationReducer } from "modules/saving/reducer/SavingConfigurationReducer";

// Types
import { Currency } from "enums/moomedEnums";
import { SavingInfo } from "../types";

export default class SavingService extends ModuleService implements ISavingService {

	public constructor() {
		super();
	}

	public async start(): Promise<void> {
		const savingInfo: SavingInfo = {} as SavingInfo;

		this.dispatch(savingConfigurationReducer.replace(savingInfo));
	}

	public async setCurrency(currency: Currency): Promise<void> {
		const response = await savingCommunication.setCurrency(currency);

		if (response.success) {

			const savingConfiguration = { ...this.getStore().savingConfigurationReducer.data } ?? {} as SavingInfo;

			savingConfiguration.currency = currency;

			this.dispatch(savingConfigurationReducer.replace(savingConfiguration));
		}
	}
}