// Framework
import * as React from "react";

// Components
import SearchBarPreview from "./SearchBarPreview";
import Flex from "common/components/Flex";

// Functionality
import { Account } from "modules/Account/types";
import { SearchResult } from "modules/Search/types";
import useServices from "hooks/useServices";

import "modules/Search/Styles/SearchBar.less";

export const SearchBar: React.FC = () => {

	const [searchQuery, setSearchQuery] = React.useState("");
	const [isPreviewOpen, setIsPreviewOpen] = React.useState(false);
	const [search, setSearch] = React.useState({
		correspondingAccounts: []
    } as SearchResult);
    
	const { SearchService } = useServices();
	
	let typingTimer: number;

	const handleChange = (event: any) => {
		const newSearchQuery = event.target.value;
		setSearchQuery(newSearchQuery);

		clearTimeout(typingTimer);
		
		typingTimer = window.setTimeout(async () => {
		
			const searchResult = await SearchService.search(newSearchQuery);

            setSearch(searchResult);
			setIsPreviewOpen(true);
        }, 250);
    };

	const onSearch = React.useCallback(async () => {
	}, [searchQuery]);
    
    const hasContent = React.useMemo(() => search?.correspondingAccounts?.length > 0, [search]);

	return (
		<Flex direction={"Column"}>
            <Flex
                className={"SearchBar"}
                direction={"Row"}>
                <input
                    className={"Input"}
                    autoComplete={"off"}
                    onChange={handleChange}
                    onClick={() => {
                        if (hasContent) {
                            setIsPreviewOpen(true);
                        }
                    }}
                    type={"text"}
                    placeholder={"Search..."}
                    value={searchQuery} />
                <button
                    className={"Button"}
                    onClick={onSearch}>
                    {"Search"}
                </button>
            </Flex>
            <If condition={isPreviewOpen && hasContent}>
                <SearchBarPreview
                    visibility={isPreviewOpen && hasContent}
                    previewAccounts={search?.correspondingAccounts}
                    onOpenStateChange={newState => setIsPreviewOpen(newState)}
                />
            </If>
		</Flex>
	);
}


export default SearchBar;