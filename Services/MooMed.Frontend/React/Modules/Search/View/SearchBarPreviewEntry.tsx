// Framework
import * as React from "react";
import { Link } from "react-router-dom";

// Components
import Flex from "common/components/Flex";

// Functionality
import { Account } from "modules/Account/types";

import "modules/Search/Styles/SearchBarPreviewEntry.less";

type Props = {
	account: Account;
	onClick: () => void;
}

export const SearchBarPreviewUserEntry: React.FC<Props> = ({account, onClick}) => 
(
	<Flex 
		className="entryContainer">
		<img 
			src={account.profilePicturePath} 
			alt="Profile picture" 
			className="entryProfilePicture"/>
		<Link 
			to={`/profileDetails/${account.id}`}
			onClick={onClick}
			className="entryProfileLink">
				{account.userName}
		</Link>
	</Flex>
);

export default SearchBarPreviewUserEntry;