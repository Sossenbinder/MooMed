// Framework
import * as React from "react";
import { connect } from 'react-redux';
import { Link } from "react-router-dom";

// Components
import Flex from "common/Components/Flex";
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
		className={"MainNavBar navbar navbar-expand-md navbar-dark navbar-fixed-top"}
		direction={"Row"}>
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
			to="/Saving"
			className="SubItem">
			Saving
		</Link>
		<Link 
			to="/Budgeting"
			className="SubItem">
			Budgeting
		</Link>
		<Link 
			to="/Investing"
			className="SubItem">
			Investing
		</Link>
		<div className="searchBar nav navbar-nav ml-auto">
			<SearchBar />
		</div>
		<SmallAccountManager />
	</Flex>
);


const mapStateToProps = (store: ReduxStore) => {
	return {
		account: store.accountReducer.data[0],
	};
}

export default connect(mapStateToProps)(NavBar);;