// Framework
import * as React from "react";
import { $enum } from "ts-enum-util";

// Components
import Flex from "common/components/Flex";
import Dropdown, { DropdownItem } from "common/components/general/input/Dropdown";

// Types
import { ExchangeTradedType } from "enums/moomedEnums";
import { ExchangeTradedUiFilters } from "modules/Stocks/types";

import "./Styles/ExchangeTradedListFilters.less";

type Props = {
	filters: ExchangeTradedUiFilters;
	setFilters: React.Dispatch<ExchangeTradedUiFilters>;
}

export const ExchangeTradedListFilters: React.FC<Props> = ({ filters, setFilters }) => {
	
	const exchangeTradedDropdownEntries: Array<DropdownItem> = React.useMemo(() => {
		const exchangeTradedEnum = $enum(ExchangeTradedType);
		
		const valueFilters = exchangeTradedEnum
			.getKeys()
			.map((x): DropdownItem => {
				const enumVal = exchangeTradedEnum.getValueOrDefault(x);

				return {
					title: x,
					value: enumVal.toString(),
					selected: filters?.type === enumVal,
				}
			});

		valueFilters.unshift({
			selected: true,
			title: "All",
			value: "-1",
		})

		return valueFilters;
	}, [filters]);
	
	return (
		<Flex
			className="ExchangeTradedListFilters"
			direction="Column">
			<Dropdown 
				label="Type"
				labelPosition="Left"
				entries={exchangeTradedDropdownEntries}
				onSelect={(x) => setFilters({
					...filters,
					type: x
				})} />
		</Flex>
	);
}

export default ExchangeTradedListFilters;