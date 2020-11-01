// Framework
import * as React from "react";

// Component
import Flex from "common/components/Flex";

import "./Styles/DataGridPageSelector.less";

type Props = {
	currentPage: number;
	pageCount: number;
	setPage: React.Dispatch<number>;
}

export const DataGridPageSelector: React.FC<Props> = ({ currentPage, pageCount, setPage }) => {

	return (
		<Flex
			className="DataGridPageSelector"
			direction="Row">
			<Flex
				className="PageScroller"
				onClick={() => setPage(0)}>
				{"≪"}
			</Flex>
			<Flex
				className="PageScroller"
				onClick={() => setPage(currentPage - 1)}>
				{"<"}
			</Flex>
			{currentPage + 1}
			<Flex
				className="PageScroller"
				onClick={() => setPage(currentPage + 1)}>
				{">"}
			</Flex>
			<Flex
				className="PageScroller"
				onClick={() => setPage(pageCount - 1)}>
				{"≫"}
			</Flex>
		</Flex>
	);
}

export default DataGridPageSelector;