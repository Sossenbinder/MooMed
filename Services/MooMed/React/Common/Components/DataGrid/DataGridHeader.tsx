// Framework
import * as React from "react";

import "./Styles/DataGridHeader.less";

type Props = {
	headers: Array<string>;
}

export const DataGridHeader: React.FC<Props> = ({ headers }) => {
	
	const tableHeaders = React.useMemo(() => {		
		return headers.map(h => (
			<th key={h}>
				{h}
			</th>
		));
	}, [headers]);

	return (
		<tr className="DataGridHeader">
			{ tableHeaders }
		</tr>
	)
}

export default DataGridHeader;