// Framework
import * as React from "react";

// Components
import { Flex } from "common/components/Flex";
import { ExchangeTradedListFilters } from "./ExchangeTradedListFilters";
import { ActionCell } from "./Cells/ActionCell";
import DataGrid from "common/components/DataGrid/DataGrid";

// Functionality
import useUiState from "hooks/useUiState";

// Types
import { ExchangeTradedItem, ExchangeTradedUiFilters } from "modules/stocks/types";
import { GridConfiguration } from "common/components/DataGrid/gridTypes";
import { ExchangeTradedType } from "enums/moomedEnums";

import "./Styles/ExchangeTradedList.less";

type Props = {
	exchangeTradeds: Array<ExchangeTradedItem>;
}

export const ExchangeTradedList: React.FC<Props> = ({ exchangeTradeds }) => {

	const [filters, setFilters] = useUiState<ExchangeTradedUiFilters>({} as ExchangeTradedUiFilters);
	
	const gridConfig: GridConfiguration<ExchangeTradedItem> = {
		columns: [
			{
				headerText: "Type",
				cellconfig: {
					customValueGenerator: x => ExchangeTradedType[x.type],
				},
			},
			{
				headerText: "Isin",
				cellconfig: {
					key: "isin",
				},
			},
			{
				headerText: "Product Family",
				cellconfig: {
					key: "productFamily"
				},
			},
			{
				headerText: "Xetra Symbol",
				cellconfig: {
					key: "xetraSymbol",
				},
			},
			{
				headerText: "Fee Percentage",
				cellconfig: {
					key: "feePercentage",
				},
			},
			{
				headerText: "Ongoing Charges",
				cellconfig: {
					key: "ongoingCharges",
				},
			},
			{
				headerText: "Profit Use",
				cellconfig: {
					key: "profitUse",
				},
			},
			{
				headerText: "Replication Method",
				cellconfig: {
					key: "replicationMethod",
				},
			},
			{
				headerText: "Fund Currency",
				cellconfig: {
					key: "fundCurrency",
				},
			},
			{
				cellconfig: {
					customCell: x => (
						<ActionCell 
							rowData={x}/>
					),
				}
			}
		],
		idField: "isin",
		pagingInfo: {
			entriesPerPage: 20,
		}
	};

	const filteredEntries = React.useMemo((): Array<ExchangeTradedItem> => {
		const { type } = filters;
		let filtered = [ ...exchangeTradeds ];
		
		if (type) {
			filtered = filtered.filter(x => x.type === type);
		}

		return filtered;
	}, [filters, exchangeTradeds])

	const grid = React.useMemo(() => (
		<Flex
			className="ExchangeTradedList">
			<DataGrid<ExchangeTradedItem>
				entries={filteredEntries}
				gridConfig={gridConfig}
			/>
		</Flex>
	), [filteredEntries, gridConfig]);

	return (
		<Flex
			direction="Row">
			<ExchangeTradedListFilters
				filters={filters}
				setFilters={setFilters}/>
			{ grid }
		</Flex>
	);
}

export default ExchangeTradedList;