// Framework
import * as React from "react"

// Components
import Flex, { FlexDirections } from "common/components/Flex";
import TextInput from "./TextInput";

// Types
import { InputProps } from "./TextInput";

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

type Props = InputProps & {
	labelPosition?: keyof typeof LabelPosition;
}

export const LabelledTextInput: React.FC<Props> = ({ labelPosition }, props) => {
	return (
		<Flex
			direction={dirLookUp.get(labelPosition)}>
			<Flex
				mainAlign="Center"
				crossAlign="Center">
				<span>{props.name}</span>
			</Flex>
			<TextInput 
				{...props} />
		</Flex>
	);
}

export default LabelledTextInput;