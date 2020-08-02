//Framework
import * as React from "react";

// Components
import LoadingBubbles from "common/Components/LoadingBubbles";

//Functionality
import useBackendCallWrapper from "hooks/useBackendCallWrapper";

import "./Styles/Controls.less";

type Props = {
	title: string;
	onClick: () => Promise<void>;
	classname?: string;
	disabled: boolean;
}

export const Button: React.FC<Props> = ({ title, onClick, classname, disabled}) => {
	
	const [loading, callBackend] = useBackendCallWrapper(onClick);

	return (
		<button
		className={"MooMedButton " + (classname ?? "")}
			onClick={async _ => await callBackend()}
			disabled={disabled}>
			<Choose>
				<When condition={loading}>
					<LoadingBubbles />
				</When>
				<Otherwise>
					<p className="ButtonParagraph">
						{title}
					</p>
				</Otherwise>
			</Choose>
		</button>
	);
}

export default Button;