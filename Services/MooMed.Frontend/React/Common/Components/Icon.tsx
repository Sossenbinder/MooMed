// Framework
import * as React from "react";

const ICON_BASE_PATH = "/Resources/Icons/";

type Props = {
	iconName: string;
	onClick?: () => void;
	className?: string;
	alt?: string;
	iconExtension?: string;
	size?: number;
}

export const Icon: React.FC<Props> = ({ iconName, onClick, className, alt, iconExtension = "png", size = 64 }) => {

	const iconPath = React.useMemo(() => `${ICON_BASE_PATH}/${iconName}.${iconExtension}`, [ iconName, iconExtension ])

	return (
		<img
			className={className}
			alt={alt}
			style={{height: size, width: size}}
			onClick={onClick}
			src={iconPath} />
	);
}

export default Icon;