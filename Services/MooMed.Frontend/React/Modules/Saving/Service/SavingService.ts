// Framework

// Functionality
import { ISavingService } from "Definitions/Service"
import ModuleService from "modules/common/Service/ModuleService"

// Types

export default class SavingService extends ModuleService implements ISavingService {

	public constructor() {
		super()
	}

	public async start(): Promise<void> {

	}
}