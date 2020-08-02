import * as React from "react";

import PostRequest from "helper/requests/PostRequest";
import requestUrls from "helper/requestUrls";

export const LogOff: React.FC = () => {

    const handleLogOff = async () => {
		const request = new PostRequest<any, any>(requestUrls.logOn.logOff);
		const response = await request.post();

		if (response.success) {
			location.href = "/";
		}
    }
    
    return (
        <div onClick={handleLogOff} className="logOffIoBtnContainer">
            <div className="logOffIoBtn"></div>
        </div>
    );
}

export default LogOff;