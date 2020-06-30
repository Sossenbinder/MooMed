// Framework
import * as React from "react";

// Functionality
import useMapState from "hooks/useMapState";

type Props = {
	accountId: number;
	profilePicturePath: string;
}

export const ProfileFullPicture: React.FC<Props> = ({ accountId, profilePicturePath }) => {
	
	const loadedImages = useMapState<number, JSX.Element>();

	const loadedForAccount = typeof loadedImages.get(accountId) !== "undefined";
	
	React.useEffect(() => {
		if (!loadedForAccount) {
			const self = (
				<img 
				className={"Picture"} 
				src={profilePicturePath} 
				alt={"Profile picture"}
				onLoad={() => 
				{
					debugger;
					loadedImages.set(accountId, self);
				}}
			/>);
		}
	}, [accountId]);

	return (
		<Choose>
			<When condition={loadedForAccount}>
				{loadedImages.get(accountId)}
			</When>
			<If condition={true}>
				<img 
					className={"Picture"} 
					src={profilePicturePath} 
					alt={"Profile picture"}
					
					onLoad={() =>  
					{
						debugger;
					}}
				/>
			</If>
		</Choose>
	);
}

export default ProfileFullPicture;