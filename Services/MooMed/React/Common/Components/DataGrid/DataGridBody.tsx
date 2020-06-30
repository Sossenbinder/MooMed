// Framework
import * as React from "react";

// Types
import { GridCellConfig } from "./gridTypes";

import "./Styles/DataGridBody.less";

type Props<T> = {
	entries: Array<T>;
	cellConfigs: Array<GridCellConfig<T>>;
	idField: keyof T;
}

export const DataGridBody = <T extends {}>({ entries, cellConfigs, idField, }: Props<T>) => {

	const tableEntries = React.useMemo(() => {
		return entries?.map(entry => {

			const trKey = String(entry[idField]);
			
			const tableCells = cellConfigs.map((cellConfig, i) => {

				const { key, cellValueIfNull, customValueGenerator, customCell } = cellConfig;

				if (customCell) {
					return (
						<td
							key={`customCell_${i}`}>
							{ customCell(entry) }
						</td>
					);
				}

				let value: string = undefined;

				if (key) {
					value = String(entry[key]);
				}

				if (!value) {
					value = cellValueIfNull;
				}

				if (customValueGenerator)
				{
					value = customValueGenerator(entry);
				}

				return (
					<td
						key={`${trKey}_${i}`}>
						{value}
					</td>
				);
			});
			
			return (
				<tr 
					className="TableRow"
					key={trKey}>
					{tableCells}
				</tr>
			);
		});
	}, [entries])

	return (
		<>
			{tableEntries}
		</>
	);
}

export default DataGridBody;