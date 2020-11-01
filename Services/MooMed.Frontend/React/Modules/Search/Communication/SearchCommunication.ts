// Functionality
import PostRequest from "helper/requests/PostRequest";
import { Network } from "modules/Search/types";

export async function search(searchRequest: Network.SearchRequest) {
    const request = new PostRequest<Network.SearchRequest, Network.SearchResult>("/Search/SearchForQuery");

    return await request.post(searchRequest);    
}