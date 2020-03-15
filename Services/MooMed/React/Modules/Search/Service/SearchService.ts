// Functionality
import { ISearchService } from "Definitions/Service";
import * as searchCommunication from "modules/Search/Communication/SearchCommunication";
import { SearchResult } from "modules/Search/types";

export default class SearchService implements ISearchService {

	public async search(query: string): Promise<SearchResult> {
		const searchResponse = await searchCommunication.search({
			query
		});

		return {
			correspondingAccounts: searchResponse.payload.correspondingUsers
		};
	}
}