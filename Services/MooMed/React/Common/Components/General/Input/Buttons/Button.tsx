//Framework
import * as React from "react";

// Components
import LoadingBubbles from "common/Components/LoadingBubbles";

//Functionality
import * as hookUtils from "helper/Utils/hookUtils";

import "./Styles/Controls.less";

type IProps = {
	title: string;
	handleClick: () => Promise<void>;
	customStyles?: string;
	disabled: boolean;
}

export const Button: React.FC<IProps> = (props) => {

	const [loading, setLoading] = React.useState(false);

	const onClick = async (event: React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
		event.preventDefault();
		
		await hookUtils.usingBoolAsync(setLoading, props.handleClick);
	}

	return (
		<button
			className={"MooMedButton" + (props.customStyles !== undefined ? (" " + props.customStyles) : "")}
			onClick={onClick}
			disabled={props.disabled}>
			<Choose>
				<When condition={loading}>
					<LoadingBubbles />
				</When>
				<When condition={!loading}>
					<div>
						<p className="ButtonParagraph">{props.title}</p>
					</div>
				</When>
			</Choose>
		</button>
	);
}

export default Button;