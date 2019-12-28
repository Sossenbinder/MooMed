import * as React from "react";
import * as ReactDOM from "react-dom";
import { Provider } from "react-redux";
import { store } from "data/store";

//Move partial into this and listen to posts inside
export default class FriendList extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div>
                Friends
            </div>
        );
    }
}