// Framework
import * as React from "react";

// Components
import Flex from "common/components/Flex";
import Icon from "common/components/Icon";

// Functionality

// Types

import "./Styles/CurrencyItem.less";

type Props = {
	size: number;
	text: string;
	isSelected?: boolean;
	onClick(): void;
}

export const CurrencyItem: React.FC<Props> = ({ size, text, isSelected = false, onClick }) => {
	return (
		<Flex
			className="CurrencyItem"
			style={{ height: size, width: size}}
			mainAlign="Center"
			crossAlign="Center"
			onClick={onClick}>
			<span>
				{text}
			</span>
			<If condition={isSelected}>
				<Icon
					className="SelectedCheckmark"
					iconName="greenCheckmark"
					size={24}/>
			</If>
		</Flex>
	);
}

export default CurrencyItem;