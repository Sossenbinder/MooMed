// Functionality
import { Account } from "modules/Account/types";
import GetRequest from "helper/requests/GetRequest";

export async function getOwnAccount() {
    const request = new GetRequest<Account>("/Account/GetOwnAccount");

    return await request.send();    
}