// Framework
import * as React from "react"
import classnames from "classnames";

// Components
import Flex from "common/components/Flex"

import "./Styles/TextInput.less";

export type InputProps = {
	name: string;
	classNames?: string;
	data: string | number | undefined;
	setData(data: string): void;
	inputType?: string;
	onEnterPress?: () => void;
	children?: React.ReactNode;
	step?: number;
}

export const TextInput: React.FC<InputProps> = ({
	name,
	classNames,
	data,
	setData,
	inputType,
	onEnterPress,
	children,
	step,
}: InputProps) => {

	const classes = React.useMemo(() => {
		return classnames("form-control", {
			"classNames": !!classNames,
		});
	}, [classNames])

	const handleKeyPress = React.useCallback((event: React.KeyboardEvent<HTMLInputElement>) => {
		if (typeof onEnterPress !== "undefined" && event.charCode === 13) {
			onEnterPress();
		}
	}, [onEnterPress]);

	return (
		<Flex
			direction="Column"
			className="TextInput">
			<input
				className={classes}
				type={inputType ?? "text"}
				name={name}
				value={data}
				onChange={event => setData(event.target.value)}
				placeholder={name}
				onKeyPress={handleKeyPress}
				step={step}
			/>
			{children}
		</Flex>
	);
}

export default TextInput;