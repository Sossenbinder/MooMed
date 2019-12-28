import * as React from "react";

interface IProps {
    title: string;
    handleClick: () => Promise<void>;
	customStyles?: string;
	disabled: boolean;
}

interface IState {
    isLoading: boolean;
}

export default class Button extends React.Component<IProps, IState> {

	public static defaultProps = {
		handleClick: () => { },
		disabled: false
	};

	constructor(props: IProps) {
        super(props);
        
        this.state = {
            isLoading: false,
        };
	}

	render() {
		return (
			<button
				className={"mooMedButton" + (this.props.customStyles !== undefined ? (" " + this.props.customStyles) : "")}
				onClick={this._onClick}
				disabled={this.props.disabled}>
                <Choose>
                    <When condition={this.state.isLoading}>
                        <div className="loadingButtonBubbleContainer">
                            <div className="loadingButtonBubble"></div>
                            <div className="loadingButtonBubble"></div>
                            <div className="loadingButtonBubble"></div>
                        </div>
                    </When>
                    <When condition={!this.state.isLoading}>
                        <div>
                            <p className="buttonParagraph">{this.props.title}</p>
                        </div>
                    </When>
                </Choose>
			</button>
		);
	}
    
	_onClick = async (event: React.MouseEvent<HTMLButtonElement, MouseEvent>) => {

		event.preventDefault();

        this.setState({
            isLoading: true,
		});

        await this.props.handleClick();

		this.setState({
			isLoading: false,
		});
    }
}