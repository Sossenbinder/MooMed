import * as React from "react";
import { setTimeout } from "timers";

import SearchBarPreview from "./SearchBarPreview";

import ajaxPost from "helper/ajaxHelper";
import requestUrls from "helper/requestUrls";

import { ClickEvent } from "views/Components/Helper/GlobalClickCapturer";
import { IsInRect } from "helper/Coordinate/CoordinateHelper";

interface ISearchBarProps {

}

interface ISearchBarState {
    searchQuery: string;
	searchResult: ISearchResult;

	isClosed: boolean;
}

class SearchBar extends React.Component<ISearchBarProps, ISearchBarState> {

	_typingTimer: NodeJS.Timer;

	_searchBarPreviewCoordinates: ClientRect | DOMRect;

    constructor(props: ISearchBarProps) {
        super(props);

        this.state = {
	        searchQuery: "",
	        searchResult: {
		        correspondingAccounts: [],
			},
	        isClosed: true,
		};
    }

	componentDidMount() {
		this._searchBarPreviewCoordinates = document.getElementById('searchBarPreview').getBoundingClientRect();
		ClickEvent.Register(this._handleGlobalClicks);
    }

    render() {
        return (
			<div>
                <div className="autoCompleteSearchBar">
                    <form onSubmit={this._onSearch}>
						<input
							className="form-control searchBarInput"
							id="searchBar"
							autoComplete="off"
							onChange={this._handleChange}
							onClick={() => this.setState({
								isClosed: false
							})}
							type="text"
							placeholder="Search..."
							value={this.state.searchQuery} />
						<input
							className="btn btn-primary"
							type="submit"
							id="searchBarSubmit"
							name="searchBarSubmit"
							value="Search" />
                    </form>
                </div>
				<SearchBarPreview
					visibility={!this.state.isClosed && this.state.searchResult.correspondingAccounts.length > 0}
					searchResult={this.state.searchResult}
					onClosePreview={this._closePreview}
                />
            </div>
        );
    }

    _handleChange = (event: any) => {
        this.setState({ searchQuery: event.target.value });

        clearTimeout(this._typingTimer);
        this._typingTimer = setTimeout(() => {
            ajaxPost({
                actionUrl: requestUrls.search.searchForQuery,
                data: {
                    query: this.state.searchQuery
                },
                onSuccess: (response: any) => {
                    this.setState({
                        searchResult: {
                            correspondingAccounts: response.data.searchResult.correspondingUsers
						},
						isClosed: false
                    });
                }
            });
        }, 250);
    }

    _onSearch = (event: any) => {
        ajaxPost({
            actionUrl: requestUrls.search.searchForQuery,
            data: {
                query: this.state.searchQuery
            },
            onSuccess: (response: any) => {
                this.setState({
                    searchResult: {
                        correspondingAccounts: response.data
					},
                    isClosed: false
                });
            }
        });

        event.preventDefault();
	}

	_handleGlobalClicks = async (event: MouseEvent) => {
		if (!this.state.isClosed && !IsInRect(this._searchBarPreviewCoordinates, event.x, event.y)) {
			this.setState({
				isClosed: true,
			});
		}
	}

	_closePreview = () => {
		this.setState({
			isClosed: true,
		});
	}
}

export default SearchBar;