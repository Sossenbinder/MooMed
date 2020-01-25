import * as React from "react";
import { setTimeout } from "timers";

interface IProps {
    name: string;
    inputType?: string;
	payload: string;
	onChangeFunc: (currentVal: string, isValid: boolean) => void;
    errorFunc?: (currentVal: string) => boolean;
	errorMessage?: string;
	onEnterPress?: () => void;
}

interface IState {
    payload: string;
    touched: boolean;
}

export default class ErrorAttachedTextInput extends React.Component<IProps, IState> {

    _errorTimer: NodeJS.Timer;

    constructor(props: IProps) {
        super(props);

        this.state = {
            payload: props.payload,
            touched: false
        }
    }

    render() {
        return (
            <div className="form-group">
                <input
                    className={this._isError() ? "form-control is-invalid" : "form-control"}
                    type={this.props.inputType != null ? this.props.inputType : "text"}
                    name={this.props.name}
                    value={this.state.payload}
                    onChange={this._handleChange}
					placeholder={this.props.name}
					onKeyPress={this._handleKeyPress}
                />
                {
                    (this.props.errorMessage !== "") &&
                    <div className="invalid-feedback">
                        {this.props.errorMessage}
                    </div>
                }
            </div>
        )
    }

    public _getPayload = () => this.state.payload;

    public _isError = () => {

        if (this.state.touched) {
            if (this.props.errorFunc) {
                return this.props.errorFunc(this.state.payload);
            }
        }

        return false;
    };

    private _handleChange = (event) => {

        this.setState({
            payload: event.target.value as string
		});

        this.props.onChangeFunc(event.target.value, this._isError());

        clearTimeout(this._errorTimer);
        this._errorTimer = setTimeout(() => {
            this.setState({
                touched: true
            });
        }, 500);
	}

	private _handleKeyPress = (event: React.KeyboardEvent<HTMLInputElement>) => {

		if (event.charCode === 13 && this.props.onEnterPress !== undefined) {
			this.props.onEnterPress();
		}
	}
}