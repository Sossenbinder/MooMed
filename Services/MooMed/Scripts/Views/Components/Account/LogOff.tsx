import * as React from "react";

import PostRequest from "helper/requests/PostRequest";
import requestUrls from "helper/requestUrls";

class LogOff extends React.Component {

    _antiForgeryToken: string;

    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div onClick={this._handleLogOff} className="logOffIoBtnContainer">
                <div className="logOffIoBtn"></div>
            </div>
        );
    }

	_handleLogOff = async () => {
		const request = new PostRequest<any, any>(requestUrls.logOn.logOff);
		const response = await request.send();

		if (response.success) {
			location.href = "/";
		}
    }
}

export default LogOff;