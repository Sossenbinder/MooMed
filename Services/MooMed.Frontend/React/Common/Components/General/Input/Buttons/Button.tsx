//Framework
import * as React from "react";
import classNames from "classnames";

// Components
import LoadingBubbles from "common/components/LoadingBubbles";

//Functionality
import useBackendCallWrapper from "hooks/useBackendCallWrapper";

import "./Styles/Button.less";

type Props = {
	title: string;
	onClick: () => Promise<void>;
	classname?: string;
	disabled?: boolean;
}

export const Button: React.FC<Props> = ({ title, onClick, classname: className, disabled = false}) => {
	
	const [loading, callBackend] = useBackendCallWrapper(onClick);

	const classes = classNames({
		"MooMedButton": true,
		"Disabled": disabled,
	});

	return (
		<button
			className={`${classes} ${className !== undefined && className !== "" ? className : ""}`}
			onClick={_ => callBackend()}
			disabled={disabled}>
			<Choose>
				<When condition={loading}>
					<LoadingBubbles />
				</When>
				<Otherwise>
					<p className="Text">
						{title}
					</p>
				</Otherwise>
			</Choose>
		</button>
	);
}

export default Button;