// Framework
import * as React from "react";

// Component
import Flex from "./Flex";

import "./Styles/ImagePreLoad.less";

type Props = {
	imagePath: string;
	className?: string;
	altName?: string;
}

export const ImagePreLoad: React.FC<Props> = ({ imagePath, className, altName }) => {
	
	const [imageLoaded, setImageLoaded] = React.useState(false);

	return (
		<Flex className={`${className} ImagePreLoad`}>
			<If condition={!imageLoaded}>
				<Flex className="PlaceHolder">
					
				</Flex>
			</If>
			<img 
				className="Image"
				src={imagePath} 
				alt={altName ?? "image"}				
				onLoad={() => setImageLoaded(true)}
			/>
		</Flex>
	);
}

export default ImagePreLoad;