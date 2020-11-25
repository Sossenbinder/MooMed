// Framework

// Functionality
import { ISavingService } from "Definitions/Service";
import ModuleService from "modules/common/Service/ModuleService";
import * as savingCommunication from "modules/saving/communication/SavingCommunication";
import { reducer as savingConfigurationReducer } from "modules/saving/reducer/SavingConfigurationReducer";

// Types
import { Currency } from "enums/moomedEnums";
import { BasicSavingInfo, SavingInfo } from "../types";

export default class SavingService extends ModuleService implements ISavingService {

	public constructor() {
		super();
	}

	public async start(): Promise<void> {

	}

	public async initSavingService(): Promise<void> {
		const response = await savingCommunication.getSavingInfo();

		const savingInfo = response.success ? response.payload : {} as SavingInfo;
		
		this.dispatch(savingConfigurationReducer.replace(savingInfo));
	}
	
	public async saveBasicSettings(income: number, rent: number, groceries: number): Promise<void> {

		const basicSavingInfo: BasicSavingInfo = {
			income,
			rent,
			groceries,
		};

		//const response = await savingCommunication.saveBasicSettings(basicSavingInfo);

		if (true) {
			const savingInfo = this.getStore().savingConfigurationReducer.data;

			savingInfo.basicSavingInfo = basicSavingInfo;

			this.dispatch(savingConfigurationReducer.update(savingInfo))
		}
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