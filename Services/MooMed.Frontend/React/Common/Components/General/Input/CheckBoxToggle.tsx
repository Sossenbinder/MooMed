// Framework
import * as React from "react";

// Components
import Flex from "common/Components/Flex";

import "./Styles/CheckBoxToggle.less";

type Props = {
	text: string;
	initialToggle: boolean;
	onChange: React.Dispatch<boolean>;
}

export const CheckBoxToggle: React.FC<Props> = ({ text, initialToggle, onChange}) => {

	const [state, setState] = React.useState<boolean>(initialToggle);

	const onSwitchClick = React.useCallback(() => {
		const newValue = !state;

		setState(newValue);

		onChange(newValue)
	}, [state]);

	return (
		<Flex className="form-group">
			<Flex className="CheckBoxToggle">
				<label className="SwitchContainer">
					<input type="checkbox" className="Switch" onClick={onSwitchClick}/>
					<span className="SwitchSlider"></span>
				</label>
				<p className="Label">
					{text}
				</p>
			</Flex>
		</Flex>
	);
}

export default CheckBoxToggle;