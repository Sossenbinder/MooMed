// Framework
import * as React from "react";

// Components
import Flex from "common/components/Flex";
import MaterialIcon from "common/components/MaterialIcon";

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
			style={{ height: size, width: size }}
			mainAlign="Center"
			crossAlign="Center"
			onClick={onClick}>
			<span>
				{text}
			</span>
			<If condition={isSelected}>

				<MaterialIcon
					style={{ fontSize: '24px' }}
					className="SelectedCheckmark"
					iconName="done" />
			</If>
		</Flex>
	);
}

export default CurrencyItem;