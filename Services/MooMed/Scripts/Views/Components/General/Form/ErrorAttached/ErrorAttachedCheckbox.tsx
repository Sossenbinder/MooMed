import * as React from "react";
import { setTimeout } from "timers";

interface IProps {
    name: string;
    inputType?: string;
    payload: boolean;
    errorFunc?: (currentVal: boolean) => boolean;
    errorMessage?: string;
}

interface IState{
    payload: boolean;
    touched: boolean;
}
export default class ErrorAttachedCheckbox extends React.Component<IProps, IState> {

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
                <div className="form-check">
                    <input
                        id={this.props.name}
                        className="form-check-input"
                        type="checkbox"
                        name={this.props.name}
                        checked={this.state.payload}
                        onChange={this._handleChange}/>
                    <label className="form-check-label" htmlFor={this.props.name}>{this.props.name}?</label>
                </div>
            </div>
        );
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
            payload: event.target.checked as boolean
        });

        clearTimeout(this._errorTimer);
        this._errorTimer = setTimeout(() => {
            this.setState({
                touched: true
            });
        }, 500);
    }
}