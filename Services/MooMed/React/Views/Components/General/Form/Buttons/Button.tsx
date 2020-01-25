//Framework
import * as React from "react";

type IProps = {
    title: string;
    handleClick: () => Promise<void>;
	customStyles?: string;
	disabled: boolean;
}

export const Button: React.FC<IProps> = (props) => {

    const [loading, setLoading] = React.useState(false);

    const onClick = React.useCallback(() => async (event: React.MouseEvent<HTMLButtonElement, MouseEvent>) => {

        event.preventDefault();
        
        setLoading(true);
        await props.handleClick();
        setLoading(false);
    }, []);

    return (
        <button
            className={"mooMedButton" + (props.customStyles !== undefined ? (" " + props.customStyles) : "")}
            onClick={onClick}
            disabled={props.disabled}>
            <Choose>
                <When condition={loading}>
                    <div className="loadingButtonBubbleContainer">
                        <div className="loadingButtonBubble"></div>
                        <div className="loadingButtonBubble"></div>
                        <div className="loadingButtonBubble"></div>
                    </div>
                </When>
                <When condition={!loading}>
                    <div>
                        <p className="buttonParagraph">{props.title}</p>
                    </div>
                </When>
            </Choose>
        </button>
    );
}

export default Button;