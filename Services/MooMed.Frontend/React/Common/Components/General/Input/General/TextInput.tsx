// Framework
import * as React from "react"
import classnames from "classnames";

// Components
import Flex from "common/components/Flex"

import "./Styles/TextInput.less";

export type DataTypes = number | string | readonly string[];

export type InputProps<T extends DataTypes> = {
	name: string;
	classNames?: string;
	data: T;
	setData(data: T): void;
	inputType?: string;
	onEnterPress?: () => void;
	children?: React.ReactNode;
}

export const TextInput = <T extends DataTypes = string>({
	name,
	classNames,
	data,
	setData,
	inputType,
	onEnterPress,
	children
}: InputProps<T>) => {

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
			className="form-group TextInput">
			<input
				className={classes}
				type={inputType ?? "text"}
				name={name}
				value={data}
				onChange={event => setData(event.target.value as T)}
				placeholder={name}
				onKeyPress={handleKeyPress}
			/>
			{children}
		</Flex>
	);
}

export default TextInput;