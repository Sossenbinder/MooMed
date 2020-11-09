// Framework
import * as React from "react"

// Components
import Flex, { FlexDirections } from "common/components/Flex";
import TextInput from "./TextInput";

// Types
import { InputProps, DataTypes } from "./TextInput";

import "./Styles/LabelledTextInput.less";

enum LabelPosition {
	Top,
	Right,
	Bottom,
	Left,
};

const dirLookUp = new Map<string, keyof typeof FlexDirections>([
	[
		LabelPosition[LabelPosition.Top], "Column"
	],
	[
		LabelPosition[LabelPosition.Right], "RowReverse"
	],
	[
		LabelPosition[LabelPosition.Bottom], "ColumnReverse"
	],
	[
		LabelPosition[LabelPosition.Left], "Row"
	]
])

type Props<T extends DataTypes> = InputProps<T> & {
	labelPosition?: keyof typeof LabelPosition;
	labelText: string;
}

export const LabelledTextInput = <T extends DataTypes = string>(props: Props<T>) => {
	return (
		<Flex
			className="LabelledTextInput"
			direction={dirLookUp.get(props.labelPosition ?? "Left")}>
			<Flex
				mainAlign="Center"
				crossAlign="Center"
				className="Label">
				<span>{props.labelText}</span>
			</Flex>
			<TextInput 
				{...props} />
		</Flex>
	);
}

export default LabelledTextInput;