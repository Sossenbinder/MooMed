// Framework
import * as React from "react";

// Components
import SearchBarPreviewUserEntry from "./SearchBarPreviewEntry";
import Flex from "common/components/Flex";

// Functionality
import { Account } from "modules/Account/types";
import { IsInRect } from "helper/Coordinate/CoordinateHelper";
import useGlobalClickHandler from "hooks/useGlobalClickHandler";

import "modules/Search/Styles/SearchBarPreview.less";

type Props = {
	previewAccounts: Array<Account>;
	visibility: boolean;
	onOpenStateChange: (openState: boolean) => void;
}

export const SearchBarPreview: React.FC<Props> = ({ previewAccounts, visibility, onOpenStateChange }) => {

	const searchBarPreviewRef = React.useRef<HTMLDivElement>();

	const userEntries = React.useMemo(() => previewAccounts?.map(account =>
		<SearchBarPreviewUserEntry
			account={account}
			key={account.id}
			onClick={() => onOpenStateChange(false)}
		/>
	), [previewAccounts]);

	const handleGlobalClick = React.useCallback(async (event: MouseEvent) => {
		if (visibility && !IsInRect(searchBarPreviewRef.current.getBoundingClientRect(), event.x, event.y)) {
			onOpenStateChange(false);
		}
	}, [visibility]);

	useGlobalClickHandler(handleGlobalClick);

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
					<Flex
						direction="Column"
						className={"UserEntries"}>
						{userEntries}
					</Flex>
					<Flex className={"CloseButton"} onClick={() => onOpenStateChange(false)}></Flex>
				</Flex>
			</Flex>
		</div>
	);
}

export default SearchBarPreview;