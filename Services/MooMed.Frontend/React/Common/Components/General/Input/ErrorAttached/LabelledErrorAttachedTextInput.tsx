// Framework
import * as React from "react"

// Components
import Flex from "common/components/Flex"

// Types
import ErrorAttachedTextInput, { InputProps } from "./ErrorAttachedTextInput";
import { FlexDirections } from "common/components/Flex"

enum LabelPosition {
	Top,
	Right,
	Bottom,
	Left,
};

type InternalProps = InputProps & {
	className?: string;
	labelPosition?: keyof typeof LabelPosition;
}

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

export const LabelledErrorAttachedTextInput: React.FC<InternalProps> = ({ className, labelPosition = LabelPosition[LabelPosition.Left], ...props}) => {

	return (
		<Flex
			className={className ?? ""}
			direction={dirLookUp.get(labelPosition)}>
			<Flex
				mainAlign="Center"
				crossAlign="Center">
				<span>{props.name}</span>
			</Flex>
			<ErrorAttachedTextInput 
				{...props} />
		</Flex>
	);
}

export default LabelledErrorAttachedTextInput;