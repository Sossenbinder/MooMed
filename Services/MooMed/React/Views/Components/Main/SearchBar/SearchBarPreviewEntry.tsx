import * as React from "react";
import { Link } from "react-router-dom";

interface IProps {
	account: Account;
}

interface IState {

}

export default class SearchBarPreviewUserEntry extends React.Component<IProps, IState> {

	render() {
		return (
			<div className="searchBarPreviewEntryContainer">
				<img src={this.props.account.profilePicturePath} alt="Profile picture" className="searchBarPreviewEntryProfilePicture"/>
				<Link to={`/profile/${this.props.account.id}`} className="searchBarPreviewEntryProfileLink">{this.props.account.userName}</Link>
			</div>
        );
    }
}