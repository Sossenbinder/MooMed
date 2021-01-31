// Framework
import * as React from "react";
import { connect } from 'react-redux';
import { Link } from "react-router-dom";

// Components
import Flex from "common/components/Flex";
import SearchBar from "modules/Search/View/SearchBar"
import ProfilePreview from "modules/account/components/profile/ProfilePreview";

// Functionality
import { Account } from "modules/Account/types";
import { ReduxStore } from "data/store";

import "./Styles/NavBar.less";

interface Props {
	account: Account;
}

export const NavBar: React.FC<Props> = () => (
	<Flex
		className="MainNavBar"
		direction="Row"
		space="Between"
		mainAlign="Center">
		<Flex>
			<Link
				to="/"
				className="Heading">
				MooMed - Finance done right
			</Link>
			<Link
				to="/"
				className="SubItem">
				Feed
			</Link>
			<Link
				to="/saving"
				className="SubItem">
				Saving &amp; Budgeting
			</Link>
			<Link
				to="/investing"
				className="SubItem">
				Investing
			</Link>
		</Flex>
		<Flex
			className="FunctionalPart"
			space="Between">
			<div className="SearchBarContainer">
				<SearchBar />
			</div>
			<div className="ProfilePreviewContainer">
				<ProfilePreview />
			</div>
		</Flex>
	</Flex>
);


const mapStateToProps = (store: ReduxStore) => {
	return {
		account: store.accountReducer.data,
	};
}

export default connect(mapStateToProps)(NavBar);;