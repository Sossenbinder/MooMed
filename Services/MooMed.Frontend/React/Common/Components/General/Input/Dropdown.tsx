// Framework
import * as React from "react";

// Components
import Flex from "common/Components/Flex";

 // Functionality
 import { ensureArray } from "helper/arrayUtils";

 // Types
 import { CouldBeArray } from "data/commonTypes";

 import "./Styles/Dropdown.less";

export type DropdownItem = {
	title: string;
	value: string;
	selected: boolean;
}

enum LabelPosition {
	Above,
	Left,
	Right,
	Below
}

type Props = {
	label?: string;
	labelPosition?: keyof typeof LabelPosition;
	entries: CouldBeArray<DropdownItem>;
	onSelect(selected: number): void;
}

export const Dropdown: React.FC<Props> = ({ label, labelPosition = "Above", entries, onSelect }) => {

	const entriesArray = ensureArray(entries);

	const dropdownEntries = React.useMemo(() => {
		return entriesArray.map((entry, i) => (
			<option 
				value={entry.value}
				key={i}>
				{ entry.title }
			</option>
		));
	}, [entries])

	const dir = React.useMemo(() => {
		switch (labelPosition) {
			default:
			case "Above":
				return "Column";
			case "Below":
				return "ColumnReverse";
			case "Left":
				return "Row";
			case "Right":
				return "RowReverse";
		}
	}, [ labelPosition ]);

	return (
		<Flex
			mainAlign={"Start"}
			direction={dir}
			className={"Dropdown"}>
			<Flex>
				{ label }
			</Flex>
			<Flex 
				className={"Input"}>
				<select
					value={entriesArray.find(x => x.selected)?.value}
					onChange={event => onSelect(Number(event.currentTarget.value))}>
					{ dropdownEntries }
				</select>
			</Flex>
		</Flex>
	);
}

export default Dropdown;