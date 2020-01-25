import * as React from "react";
import MooEvent from "helper/Events/MooEvent";

interface IProps {

}

interface IState {

}

export let ClickEvent = new MooEvent<MouseEvent>();

export default class GlobalClickCapturer extends React.Component<IProps, IState> {

	componentDidMount() {
		document.body.addEventListener('click', this._onClick);
	}

	componentWillUnmount() {
		document.body.removeEventListener('click', this._onClick);
	}

	async _onClick(event: MouseEvent) {
		await ClickEvent.Raise(event);
	}

	render() {
		return (
			<div>
				{this.props.children}
			</div>);
	}
}