// Framework
import * as React from "react";
import { connect } from "react-redux";
import { Link } from "react-router-dom";

// Components
import LogOff from "views/Components/Account/LogOff";

// Functionality
import { ReduxStore } from "data/store";
import ajaxPost from "helper/ajaxHelper";
import requestUrls from "helper/requestUrls";
import { Account } from "modules/Account/types";
import { reducer as accountReducer } from "data/reducers/accountReducer";

import "./Styles/SmallAccountManager.less";

type Props = {
	account: Account;

	updateAccount: (account: Account) => void;
}

export const SmallAccountManager: React.FC<Props> = ({ account, updateAccount}) => {
	

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

				account.profilePicturePath = response.data;
				updateAccount(account);
			},
			useVerificationToken: true,
		});
	}, [updateAccount]);

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

const mapStateToProps = (state: ReduxStore) => {
	return {
		account: state.accountReducer.data[0]
	};
}

const mapDispatchToProps = (dispatch: any) => {
	return {
		updateAccount: (account: Account) => dispatch(accountReducer.update(account))
	}
}

export default connect(mapStateToProps, mapDispatchToProps)(SmallAccountManager);