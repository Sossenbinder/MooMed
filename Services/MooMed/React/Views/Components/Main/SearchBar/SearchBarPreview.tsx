import * as React from "react";

import SearchBarPreviewUserEntry from "./SearchBarPreviewEntry";

interface ISearchBarPreviewProps {
    searchResult: ISearchResult;
	visibility: boolean;
	onClosePreview: () => void;
}

interface ISearchBarPreviewState {
    userEntries: Array<JSX.Element>;
}

class SearchBarPreview extends React.Component<ISearchBarPreviewProps, ISearchBarPreviewState> {

    constructor(props: ISearchBarPreviewProps) {
        super(props);

        this.state = {
            userEntries: []
        }
    }

	render() {
		
        let userEntries: Array<JSX.Element> = [];
        this.props.searchResult.correspondingAccounts.forEach(account => {
			userEntries.push(
				<SearchBarPreviewUserEntry
					account={account}
					key={account.id}
                />
            );
        });

		return (
			<div id="searchBarPreview" style={{ visibility: this.props.visibility ? 'visible' : 'hidden' }} className="searchBarPreviewContainer">
                <div className="searchBarPreviewContent">
                    <p><strong>Users:</strong></p>
                    <div className="userEntries">
                        {userEntries}
                    </div>
					<div className="searchBarPreviewCloseBtn" onClick={this.props.onClosePreview}></div>
                </div>
            </div>
        );
    }
}

export default SearchBarPreview;