// Framework
import * as React from "react";
import { connect } from "react-redux";
import { Link } from "react-router-dom";

// Components
import LogOff from "views/Components/Account/LogOff";

// Functionality
import { updateAccountPicture } from "data/reducers/accountReducer";
import ajaxPost from "helper/ajaxHelper";
import requestUrls from "helper/requestUrls";
import { Account } from "modules/Account/types";

import "./Styles/SmallAccountManager.less";

type Props = {
	account: Account;

	updateAccountPicture: (profilePicturePath: string) => any;
}

export const SmallAccountManager: React.FC<Props> = ({ account, updateAccountPicture}) => {
	

    const uploadPicture = React.useCallback((event: any) => {
        const formData = new FormData();
        const files = event.target.files;
        if (files.length > 0) {

            formData.append("UploadedImage", files[0]);
        }

        ajaxPost({
            actionUrl: requestUrls.profile.uploadProfilePicture,
            uploadFile: true,
            data: formData,
			onSuccess: (response) => {
				updateAccountPicture(response.data);
            },
            useVerificationToken: true,
        });
    }, [updateAccountPicture]);

    return (
        <div className="row rounded border smallAccountManager" id="profileBlock">
            <div id="profilePicture" className="smallAccountManagerPicture">
                <label id="profilePictureLabel" htmlFor="smallAccountManagerPictureUploadInput" className="smallAccountManagerPictureLabel">
                    <img src={account.profilePicturePath} alt="Profile picture" />
                </label>
                <input id="smallAccountManagerPictureUploadInput" onChange={uploadPicture} type="file" accept=".png,.img" />
            </div>
            <div className="smallAccountManagerDescription">
                <div className="smallAccountManagerDescriptionUserName">
                    <Link to={`/profileDetails/${account.id}`}>{account.userName}</Link>
                </div>
            </div>
            <div className="smallAccountManagerLogOff">
                <LogOff />
            </div>
        </div>
    );
}

const mapStateToProps = (state: any) => {
    return {
        account: state.accountReducer.account
    };
}

const mapDispatchToProps = (dispatch: any) => {
    return {
        updateAccountPicture: (profilePicturePath: string) => dispatch(updateAccountPicture(profilePicturePath))
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(SmallAccountManager);