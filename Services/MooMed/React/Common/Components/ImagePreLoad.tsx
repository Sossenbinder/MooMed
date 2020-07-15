// Framework
import * as React from "react";

// Component
import Flex from "./Flex";
import LoadingBubbles from "./LoadingBubbles";

import "./Styles/ImagePreLoad.less";

type Props = {
	imagePath: string;
	containerClassName?: string;
	imageClassName?: string;
	altName?: string;
}

export const ImagePreLoad: React.FC<Props> = ({ imagePath, containerClassName, imageClassName, altName }) => {
	
	const [imageLoaded, setImageLoaded] = React.useState(false);

	return (
		<Flex className={containerClassName}>
			<If condition={!imageLoaded}>
				<Flex className="PlaceHolder">
					<LoadingBubbles />
				</Flex>
			</If>
			<img 
				className={imageClassName}
				src={imagePath} 
				alt={altName ?? "image"}				
				onLoad={() => setImageLoaded(true)}
			/>
		</Flex>
	);
}

export default ImagePreLoad;