// Framework
import * as React from "react";
import { connect } from "react-redux";
import { Link } from "react-router-dom";

// Components
import Flex from "common/components/Flex";
import MaterialIcon from "common/components/MaterialIcon"

// Functionality
import { ReduxStore } from "data/store";
import ajaxPost from "helper/ajaxHelper";
import requestUrls from "helper/requestUrls";
import { Account } from "modules/Account/types";
import { reducer as accountReducer } from "modules/Account/Reducer/AccountReducer";
import useServices from "hooks/useServices";

import "./Styles/ProfilePreview.less";

type Props = {
	account: Account;
	updateAccount: (account: Account) => void;
}

export const ProfilePreview: React.FC<Props> = ({ account, updateAccount }) => {

	const { LogonService } = useServices();

	const uploadPicture = React.useCallback((event: HTMLInputElement) => {
		const formData = new FormData();
		const files = event.files;
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
		<Flex
			className="rounded border ProfilePreview"
			direction="Row">
			<div className="ProfilePicture">
				<label htmlFor="PictureUploadHiddenInput" className="PictureLabel">
					<img className="Picture" src={account.profilePicturePath} alt="" />
				</label>
				<input
					id="PictureUploadHiddenInput"
					className="PictureUploadHiddenInput"
					onChange={uploadPicture}
					type="file"
					accept=".png,.img" />
			</div>
			<Flex
				className="UserName"
				crossAlign="Center"
				mainAlign="Start">
				<Link to={`/profileDetails/${account.id}`}>{account.userName}</Link>
			</Flex>
			<Flex
				className="LogOff">
				<MaterialIcon
					iconName="power_settings_new"
					onClick={LogonService.logOff} />
			</Flex>
		</Flex>
	);
}

const mapStateToProps = (state: ReduxStore) => {
	return {
		account: state.accountReducer.data
	};
}

const mapDispatchToProps = (dispatch: any) => {
	return {
		updateAccount: (account: Account) => dispatch(accountReducer.update(account))
	}
}

export default connect(mapStateToProps, mapDispatchToProps)(ProfilePreview);