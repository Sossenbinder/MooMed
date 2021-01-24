// Framework
import * as React from "react";
import { connect } from 'react-redux';
import { Link } from "react-router-dom";

// Components
import Flex from "common/components/Flex";
import SearchBar from "modules/Search/View/SearchBar"
import SmallAccountManager from "views/Components/Account/SmallAccountManager";

// Functionality
import { Account } from "modules/Account/types";
import { ReduxStore } from "data/store";

import "./Styles/NavBar.less";

interface Props {
	account: Account;
}

export const NavBar: React.FC<Props> = () => (
	<Flex
		className="MainNavBar navbar navbar-expand-md"
		direction="Row"
		space="Between"
		mainAlign="Center">
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
		<div className="SearchBar nav navbar-nav ml-auto">
			<SearchBar />
		</div>
		<div className="SmallAccountManager">
			<SmallAccountManager />
		</div>
	</Flex>
);


const mapStateToProps = (store: ReduxStore) => {
	return {
		account: store.accountReducer.data[0],
	};
}

export default connect(mapStateToProps)(NavBar);;