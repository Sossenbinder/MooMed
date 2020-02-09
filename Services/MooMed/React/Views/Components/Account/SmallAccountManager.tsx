import * as React from "react";
import { connect } from "react-redux";
import { Link } from "react-router-dom";

import ajaxPost from "helper/ajaxHelper";
import requestUrls from "helper/requestUrls";

import LogOff from "views/Components/Account/LogOff";

import { updateAccountPicture } from "data/reducers/accountReducer";

import "./Styles/SmallAccountManager.less";

interface IProps {
	account: Account;

	updateAccountPicture: (profilePicturePath: string) => any;
}

interface IState {

}

class SmallAccountManager extends React.Component<IProps, IState> {
	
	render() {
        return (
            <div className="row rounded border smallAccountManager" id="profileBlock">
                <div id="profilePicture" className="smallAccountManagerPicture">
                    <label id="profilePictureLabel" htmlFor="smallAccountManagerPictureUploadInput" className="smallAccountManagerPictureLabel">
                        <img src={this.props.account.profilePicturePath} alt="Profile picture" />
                    </label>
                    <input id="smallAccountManagerPictureUploadInput" onChange={this._uploadPicture} type="file" accept=".png,.img" />
                </div>
                <div className="smallAccountManagerDescription">
                    <div className="smallAccountManagerDescriptionUserName">
                        <Link to="/editProfile">{this.props.account.userName}</Link>
                    </div>
                </div>
                <div className="smallAccountManagerLogOff">
                    <LogOff />
                </div>
            </div>
        );
    }

    _uploadPicture = (event: any) => {

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
				this.props.updateAccountPicture(response.data);
            },
            useVerificationToken: true,
        });
    }
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