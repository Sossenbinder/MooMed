"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const React = require("react");
const react_redux_1 = require("react-redux");
const react_router_dom_1 = require("react-router-dom");
const LogOff_1 = require("views/Components/Account/LogOff");
const ajaxHelper_1 = require("helper/ajaxHelper");
const requestUrls_1 = require("helper/requestUrls");
const accountReducer_1 = require("data/reducers/accountReducer");
require("./Styles/SmallAccountManager.less");
exports.SmallAccountManager = ({ account, updateAccount }) => {
    const uploadPicture = React.useCallback((event) => {
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
                account.profilePicturePath = response.data;
                updateAccount(account);
            },
            useVerificationToken: true,
        });
    }, [updateAccount]);
    return (React.createElement("div", { className: "row rounded border smallAccountManager", id: "profileBlock" },
        React.createElement("div", { id: "profilePicture", className: "smallAccountManagerPicture" },
            React.createElement("label", { id: "profilePictureLabel", htmlFor: "smallAccountManagerPictureUploadInput", className: "smallAccountManagerPictureLabel" },
                React.createElement("img", { src: account.profilePicturePath, alt: "Profile picture" })),
            React.createElement("input", { id: "smallAccountManagerPictureUploadInput", onChange: uploadPicture, type: "file", accept: ".png,.img" })),
        React.createElement("div", { className: "smallAccountManagerDescription" },
            React.createElement("div", { className: "smallAccountManagerDescriptionUserName" },
                React.createElement(react_router_dom_1.Link, { to: `/profileDetails/${account.id}` }, account.userName))),
        React.createElement("div", { className: "smallAccountManagerLogOff" },
            React.createElement(LogOff_1.default, null))));
};
const mapStateToProps = (state) => {
    return {
        account: state.accountReducer.data[0]
    };
};
const mapDispatchToProps = (dispatch) => {
    return {
        updateAccount: (account) => dispatch(accountReducer_1.reducer.update(account))
    };
};
exports.default = react_redux_1.connect(mapStateToProps, mapDispatchToProps)(exports.SmallAccountManager);
//# sourceMappingURL=SmallAccountManager.js.map