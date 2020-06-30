// Framework
import * as React from "react";

// Components
import Flex from "common/Components/Flex";
import Icon from "common/Components/Icon";

// Functionality
import useServices from "hooks/useServices";

// Types
import { GridCellProps } from "common/Components/DataGrid/gridTypes";
import { ExchangeTradedItem } from "modules/Stocks/types";

import "./ActionCell.less";

type Props = GridCellProps<ExchangeTradedItem> & {

}

export const ActionCell: React.FC<Props> = ({ rowData }) => {

	const { PortfolioService } = useServices();

	return (
		<Flex
			className="ShowOnHover ActionCell">
			<Icon
				className="Plus"
				iconName="plusSign"
				size={20}
				onClick={async () => await PortfolioService.addToPortfolio(rowData.isin, 1)}/>
		</Flex>
	);
}

export default ActionCell;