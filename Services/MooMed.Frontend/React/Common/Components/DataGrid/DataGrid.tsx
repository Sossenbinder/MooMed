// Framework
import * as React from "react";

// Component
import DataGridHeader from "./DataGridHeader";
import DataGridBody from "./DataGridBody";
import Flex from "common/components/Flex";
import DataGridPageSelector from "./DataGridPageSelector";

// Functionality
import { split } from "helper/arrayUtils";

// Types
import { GridConfiguration } from "./gridTypes";

type Props<T> = {
	gridConfig: GridConfiguration<T>;
	entries: Array<T>;
}

export const DataGrid = <T extends any>({ 
	gridConfig, 
	entries 
}: Props<T>) => {

	const [page, setPage] = React.useState(0);	

	const headerTexts = React.useMemo(() => gridConfig.columns.map(x => x.headerText), [gridConfig.columns]);

	const cellConfigs = React.useMemo(() => gridConfig.columns.map(x => x.cellconfig), [gridConfig.columns]);

	const preparedEntries = React.useMemo((): Array<Array<T>> => {

		if (!gridConfig.pagingInfo) {
			return [entries];
		}
		
		return split(entries, gridConfig.pagingInfo.entriesPerPage);
	}, [entries]);

	const pageCount = React.useMemo(() => preparedEntries.length, [preparedEntries]);

	React.useEffect(() => setPage(0), [entries]);

	return (
		<Flex>
			<table>
				<thead>
					<DataGridHeader 
						headers={headerTexts}/>
				</thead>
				<tbody>
					<DataGridBody<T>
						entries={preparedEntries[page]}
						cellConfigs={cellConfigs}
						idField={gridConfig.idField} />
				</tbody>
			</table>
				<If condition={typeof gridConfig.pagingInfo !== "undefined"}>
					<DataGridPageSelector
						currentPage={page}
						pageCount={pageCount}
						setPage={setPage} />
				</If>
		</Flex>
	);
}

export default DataGrid;