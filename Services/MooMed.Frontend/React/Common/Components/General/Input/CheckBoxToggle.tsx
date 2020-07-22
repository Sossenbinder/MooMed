import * as React from "react";

interface IProps {
    name: string;
	initialToggle: boolean;
	onChange: (currentVal: boolean) => void;
	errorMessage?: string;
}

interface IState {
    payload: boolean;
    touched: boolean;
}

export class CheckBoxToggle extends React.Component<IProps, IState> {

    constructor(props: IProps) {
        super(props);

        this.state = {
            payload: this.props.initialToggle,
            touched: false
        }
    }

    render() {
        return (
            <div className="form-group">
                <div className="toggleCheckBoxContainer">
                    <label className="toggleCheckBoxSwitchContainer">
                        <input type="checkbox" className="toggleCheckBoxSwitch" onClick={this._onSwitchClick}/>
                        <span className="toggleCheckBoxSwitchSlider"></span>
                    </label>
                    <p className="toggleCheckBoxLabel">{this.props.name}</p>
                </div>
            </div>
        );
    }

    _onSwitchClick = () => {
        this.setState({
            payload: !this.state.payload
		});

        this.props.onChange(this.state.payload);
    }

    public _getPayload = () => this.state.payload;
}