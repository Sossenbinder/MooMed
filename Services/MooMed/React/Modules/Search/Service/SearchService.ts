// Functionality
import { ISearchService } from "Definitions/Service";
import ModuleService from "modules/common/Service/ModuleService";
import * as searchCommunication from "modules/Search/Communication/SearchCommunication";
import { SearchResult } from "modules/Search/types";

export default class SearchService extends ModuleService implements ISearchService {

	public constructor() {
		super();
	}

	public async start() {
		
	}

	public async search(query: string): Promise<SearchResult> {
		const searchResponse = await searchCommunication.search({
			query
		});

		return {
			correspondingAccounts: searchResponse.payload.correspondingUsers
		};
	}
}