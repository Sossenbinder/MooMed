// Framework
import * as React from "react";

// Components
import SearchBarPreviewUserEntry from "./SearchBarPreviewEntry";
import Flex from "Common/Components/Flex";

// Functionality
import { Account } from "modules/Account/types";
import { ClickEvent } from "views/Components/Helper/GlobalClickCapturer";
import { IsInRect } from "helper/Coordinate/CoordinateHelper";

import "modules/Search/Styles/SearchBarPreview.less";

type Props = {
    previewAccounts: Array<Account>;
	visibility: boolean;
    onOpenStateChange: (openState: boolean) => void;
}

export const SearchBarPreview: React.FC<Props> = ({ previewAccounts, visibility, onOpenStateChange}) => {

    const searchBarPreviewRef = React.useRef<HTMLDivElement>();
    
    const userEntries = React.useMemo(() => previewAccounts?.map(account => 
        <SearchBarPreviewUserEntry
            account={account}
            key={account.id}
            onClick={() => onOpenStateChange(false)}
        />
    ), [previewAccounts]);        

	const handleGlobalClicks = React.useCallback(async (event: MouseEvent) => {
		if (visibility && !IsInRect(searchBarPreviewRef.current.getBoundingClientRect(), event.x, event.y)) {
			onOpenStateChange(false);
        }
    }, [visibility]);
    
	React.useEffect(() => {
        const listenerId = ClickEvent.Register(handleGlobalClicks);

        return () => ClickEvent.Unregister(listenerId);
	}, [visibility]);

    return (
        <div ref={searchBarPreviewRef}>
            <Flex
                style={{ visibility: visibility ? 'visible' : 'hidden' }} 
                className={"Container"}>
                <Flex className={"PreviewContent"}>
                    <p>
                        <strong>
                            Users:
                        </strong>
                    </p>
                    <Flex className={"UserEntries"}>
                        {userEntries}
                    </Flex>
                    <Flex className={"CloseButton"} onClick={() => onOpenStateChange(false)}></Flex>
                </Flex>
            </Flex>
        </div>
    );
}

export default SearchBarPreview;