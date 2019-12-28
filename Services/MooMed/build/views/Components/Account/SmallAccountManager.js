"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const react_redux_1 = require("react-redux");
const react_router_dom_1 = require("react-router-dom");
const ajaxHelper_1 = require("helper/ajaxHelper");
const requestUrls_1 = require("helper/requestUrls");
const LogOff_1 = require("views/Components/Account/LogOff");
const accountReducer_1 = require("data/reducers/accountReducer");
class SmallAccountManagerImpl extends React.Component {
    constructor() {
        super(...arguments);
        this._uploadPicture = (event) => {
            const formData = new FormData();
            const files = event.target.files;
            if (files.length > 0) {
                formData.append("UploadedImage", files[0]);
            }
            ajaxHelper_1.default({
                actionUrl: requestUrls_1.default.profile.uploadProfilePicture,
                uploadFile: true,
                data: formData,
                onSuccess: (response) => {
                    event.target.value = null;
                    this.setState({
                        profilePicturePath: response.data
                    });
                },
                useVerificationToken: true,
            });
        };
    }
    render() {
        return (React.createElement("div", { className: "row rounded border smallAccountManager", id: "profileBlock" },
            React.createElement("div", { id: "profilePicture", className: "smallAccountManagerPicture" },
                React.createElement("label", { id: "profilePictureLabel", htmlFor: "smallAccountManagerPictureUploadInput", className: "smallAccountManagerPictureLabel" },
                    React.createElement("img", { src: `https://moomedprofilepictures.blob.core.windows.net/${this.props.account.id}`, alt: "Profile picture" })),
                React.createElement("input", { id: "smallAccountManagerPictureUploadInput", onChange: this._uploadPicture, type: "file", accept: ".png,.img" })),
            React.createElement("div", { className: "smallAccountManagerDescription" },
                React.createElement("div", { className: "smallAccountManagerDescriptionUserName" },
                    React.createElement(react_router_dom_1.Link, { to: "/editProfile" }, this.props.account.UserName))),
            React.createElement("div", { className: "smallAccountManagerLogOff" },
                React.createElement(LogOff_1.default, null))));
    }
}
const mapStateToProps = state => {
    return {
        account: state.accountReducer.account
    };
};
const mapDispatchToProps = dispatch => {
    return {
        updateAccountPicture: profilePicturePath => dispatch(accountReducer_1.updateAccountPicture(profilePicturePath))
    };
};
const SmallAccountManager = react_redux_1.connect(mapStateToProps, mapDispatchToProps)(SmallAccountManagerImpl);
exports.default = SmallAccountManager;
//# sourceMappingURL=SmallAccountManager.js.map